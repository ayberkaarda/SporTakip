using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;

namespace SporTakip.Screenshots;

/// <summary>
/// Renders a form to a bitmap without ever showing it: the window handles are created hidden,
/// the Load event is raised by hand and Control.DrawToBitmap (WM_PRINT) paints the client area.
/// </summary>
internal static class OffscreenRenderer
{
    // Control.CreateControl(bool ignoreVisible): creates handles for a form that is not visible.
    private static readonly MethodInfo CreateControl = typeof(Control).GetMethod(
        "CreateControl", BindingFlags.NonPublic | BindingFlags.Instance, [typeof(bool)])
        ?? throw new InvalidOperationException("Control.CreateControl(bool) not found.");

    private static readonly MethodInfo OnLoad = typeof(Form).GetMethod(
        "OnLoad", BindingFlags.NonPublic | BindingFlags.Instance, [typeof(EventArgs)])
        ?? throw new InvalidOperationException("Form.OnLoad not found.");

    // Control.SetState(States, bool): sets WinForms' own visibility flag without calling ShowWindow.
    private static readonly MethodInfo SetState = typeof(Control)
        .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
        .Single(m => m.Name == "SetState" && m.GetParameters() is { Length: 2 } p && p[0].ParameterType.IsEnum && p[1].ParameterType == typeof(bool));

    [DllImport("user32.dll")]
    private static extern bool IsWindowVisible(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    private const int EM_GETMARGINS = 0xD4;

    public static Bitmap Ciz(Form form, Action<Form>? sonra)
    {
        form.ShowInTaskbar = false;
        form.StartPosition = FormStartPosition.Manual;
        form.Location = new Point(-32000, -32000);

        // Handles first, while the form is still invisible, so its window never gets WS_VISIBLE.
        CreateControl.Invoke(form, [true]);
        // Then let WinForms treat the form as visible: layout code that reads Control.Visible
        // (for example the unassigned strip of the weekly program) behaves as on screen.
        Type states = SetState.GetParameters()[0].ParameterType;
        SetState.Invoke(form, [Enum.Parse(states, "Visible"), true]);

        OnLoad.Invoke(form, [EventArgs.Empty]);
        MesajlariIsle();
        TutamaklariOlustur(form);

        if (sonra != null)
        {
            sonra(form);
            MesajlariIsle();
            TutamaklariOlustur(form);
        }

        foreach (ComboBox combo in Tumu<ComboBox>(form).ToList())
        {
            // A hidden drop-down list skips painting its selected text under WM_PRINT; the editable
            // style prints it through its edit box and looks the same in this theme.
            if (combo.DropDownStyle == ComboBoxStyle.DropDownList)
            {
                int secili = combo.SelectedIndex;
                combo.DropDownStyle = ComboBoxStyle.DropDown;
                combo.SelectedIndex = secili;
            }
            // No text should look selected in a still image.
            combo.Select(0, 0);
        }
        form.PerformLayout();
        MesajlariIsle();
        TutamaklariOlustur(form);

        if (IsWindowVisible(form.Handle))
            throw new InvalidOperationException($"{form.Name} became visible; screenshots must stay offscreen.");

        // 24 bpp: WM_PRINT leaves the alpha channel undefined, which would make pixels transparent.
        var bitmap = new Bitmap(form.Width, form.Height, PixelFormat.Format24bppRgb);
        form.DrawToBitmap(bitmap, new Rectangle(Point.Empty, form.Size));
        IpuclariniCiz(form, bitmap);
        return bitmap;
    }

    /// <summary>
    /// TextBox.PlaceholderText is painted by WinForms only on WM_PAINT, never on WM_PRINT. Draw it the same
    /// way (TextRenderer, SystemColors.GrayText, client rectangle offset by one pixel) for empty boxes.
    /// </summary>
    private static void IpuclariniCiz(Form form, Bitmap bitmap)
    {
        using Graphics g = Graphics.FromImage(bitmap);
        Point formKoku = form.PointToScreen(Point.Empty);
        foreach (TextBox kutu in Tumu<TextBox>(form))
        {
            if (!kutu.Visible || kutu.TextLength > 0 || string.IsNullOrEmpty(kutu.PlaceholderText)) continue;
            Point koku = kutu.PointToScreen(Point.Empty);
            int solPay = (int)SendMessage(kutu.Handle, EM_GETMARGINS, IntPtr.Zero, IntPtr.Zero) & 0xFFFF;
            var alan = new Rectangle(koku.X - formKoku.X + solPay, koku.Y - formKoku.Y, kutu.ClientSize.Width - solPay, kutu.ClientSize.Height);
            alan.Offset(1, 1);
            // The back colour gives GDI an opaque background; without it ClearType text on a bitmap smears.
            TextRenderer.DrawText(g, kutu.PlaceholderText, kutu.Font, alan, SystemColors.GrayText, kutu.BackColor,
                TextFormatFlags.NoPadding | TextFormatFlags.Top | TextFormatFlags.Left | TextFormatFlags.EndEllipsis);
        }
    }

    /// <summary>Controls added after the first pass (lists, cards) also need hidden handles to be printed.</summary>
    private static void TutamaklariOlustur(Control control)
    {
        if (!control.IsHandleCreated)
        {
            CreateControl.Invoke(control, [true]);
            return;
        }
        foreach (Control child in control.Controls) TutamaklariOlustur(child);
    }

    private static void MesajlariIsle()
    {
        for (int i = 0; i < 10; i++)
        {
            System.Windows.Forms.Application.DoEvents();
            Thread.Sleep(15);
        }
    }

    private static IEnumerable<T> Tumu<T>(Control parent) where T : Control
    {
        foreach (Control c in parent.Controls)
        {
            if (c is T t) yield return t;
            foreach (T child in Tumu<T>(c)) yield return child;
        }
    }
}
