using System;
using System.Windows.Forms;
using SporTakip;

static class Program
{
    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        // Ana formunu burada başlat (örnek: MainForm)
        Application.Run(new Form1());
    }
}
