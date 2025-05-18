using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class Takip
{
    public DateTime Tarih { get; set; }
    public string Aciklama { get; set; }

    public Takip(DateTime tarih, string aciklama)
    {
        Tarih = tarih;
        Aciklama = aciklama;
    }
}

