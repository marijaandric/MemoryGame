using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MG_24_2020
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            ucitaj();
        }

        public void ucitaj()
        {
            label1.Text = "Vaš rezultat je :\n"+Form1.tries+ " poteza\n"+Form1.totalTime+" sekundi ";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                MessageBox.Show("Ime mora sadržati barem jedno slovo!");
            }
            else
            {
                baza b = new baza();
                b.upisiRezultat(textBox1.Text, Form1.tries, Form1.totalTime, (Form1.rows + " x " + Form1.columns));
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Želite li ponovo da igrate?", "Čestitamo na pobedi!", buttons);
                if (result == DialogResult.Yes)
                {
                    Form1 frm1 = new Form1();
                    this.Close();
                }
                else
                {
                    System.Environment.Exit(0);
                }
            }

        }
    }
}
