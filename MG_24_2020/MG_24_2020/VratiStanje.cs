using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MG_24_2020
{
    public class VratiStanje
    {
        List<List<PictureBox>> stanja = new List<List<PictureBox>>();
        public int korak = 0;

        public VratiStanje(List<PictureBox> pic)
        {
            this.stanja.Add(pic);
        }

        public void zapamtiStanje(List<PictureBox> s)
        {
            stanja.Add(s);
            korak++;
        }

        public List<PictureBox> vratiPrethodno()
        {
            if (korak >= 0)
            {
                Console.WriteLine(stanja[korak - 1][0].Name);
                Console.WriteLine(stanja[korak - 1][2].Name);
                Console.WriteLine(stanja[korak - 1][3].Name);
                Console.WriteLine(stanja[korak - 1][4].Name);
                Console.WriteLine(stanja[korak - 1][5].Name);
                Console.WriteLine(stanja[korak - 1][6].Name); 
                Console.WriteLine(stanja[korak - 1][7].Name);
                return stanja[korak--];
            }
            return stanja[0];
        }
    }
}
