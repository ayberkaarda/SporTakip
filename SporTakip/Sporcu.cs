using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Sporcu
{
    public string Ad { get; set; }
    public string SporDali { get; set; }
    public List<Antrenman> Antrenmanlar { get; set; } = new List<Antrenman>();
    public List<Takip> Ilerlemeler { get; set; } = new List<Takip>();

    public void ProgramOlustur(Antrenman antrenman)
    {
        Antrenmanlar.Add(antrenman);
    }

    public void IlerlemeKaydet(Takip takip)
    {
        Ilerlemeler.Add(takip);
    }

    public void RaporAl()
    {
        Console.WriteLine($"\n=== {Ad} - {SporDali} Raporu =");
        foreach (var takip in Ilerlemeler)
        {
            Console.WriteLine($"{takip.Tarih.ToShortDateString()} - {takip.Aciklama}");
        }
    }
}