using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MG_24_2020
{
    public partial class Form3 : Form
    {
        baza b = new baza();
        int r = 4;
        int c = 4;
        Form1 frm1 = null;
        public Form3()
        {
            InitializeComponent();
            frm1 = new Form1();
            popuni();
        }

        public List<User> najbolji(int x,int y)
        {
            string rang = x + " x " + y;
            return b.dajNajboljeg(rang);
        }

        public void dodaj(int x,int y)
        {
            List<User> najboljiRez = najbolji(x, y);
            if (najboljiRez == null)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = "Neuspesna komunikacija sa bazom.";
                dataGridView1.Rows.Add(row);
            }
            else if (najboljiRez.Count > 0)
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = najboljiRez[0].ime;
                row.Cells[1].Value = najboljiRez[0].score +" poteza - " + najboljiRez[0].vreme+" s";
                row.Cells[2].Value = x+" x "+y;
                dataGridView1.Rows.Add(row);
            }
            else
            {
                DataGridViewRow row = (DataGridViewRow)dataGridView1.Rows[0].Clone();
                row.Cells[0].Value = "Niko jos nije igrao.";
                row.Cells[2].Value = x + " x " + y;
                dataGridView1.Rows.Add(row);
            }
        }

        public void popuni()
        {
            if(DBConnection.conn == null)
            {
                return;
            }
            dodaj(2, 2);
            dodaj(2, 3);
            dodaj(3, 4);
            dodaj(4, 4);
            dodaj(4, 5);
            dodaj(5, 6);
            dodaj(6, 6);


        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm1.Show();
            this.Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            frm1.tema = "pics/";
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            frm1.tema = "pics2/";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            frm1.tema = "pics3/";
        }
    }
}
