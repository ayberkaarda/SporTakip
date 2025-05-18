using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Antrenman
{
    public string AntrenmanAdi { get; set; }
    public string Detaylar { get; set; }

    public Antrenman(string ad, string detay)
    {
        AntrenmanAdi = ad;
        Detaylar = detay;
    }
}
