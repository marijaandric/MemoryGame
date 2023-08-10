using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MG_24_2020
{
    [Serializable()]
    public partial class Form1 : Form
    {
        List<int> numbers = new List<int>();
        string firstChoice = null;
        string secondChoice = null;
        public static int tries;
        public List<PictureBox> pictureBoxes = new List<PictureBox>();
        PictureBox picA = null;
        PictureBox picB = null;
        public static int totalTime = 0;
        bool gameOver = false;
        public static int rows = 4;
        public static int columns = 4;


        baza b = new baza();
        public string tema = "pics/";

        public Form1()
        {
            InitializeComponent();
            button1.Image = Image.FromFile("pics/newGame.png");
            loadPictures();
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void picture_Click(object sender, EventArgs e)
        {
            SoundPlayer sp = new SoundPlayer(@"mixkit-mouse-click-close-1113.wav");
            sp.Play();
            if (gameOver)
            {
                return;
            }
            if(firstChoice == null)
            {
                picA = sender as PictureBox;
                if(picA.Tag != null && picA.Name == "null")
                {
                    picA.Image = Image.FromFile(tema + (string)picA.Tag + ".png");
                    firstChoice = (string)picA.Tag;
                    picA.Name = (string)picA.Tag;
                }
            }
            else if(secondChoice == null)
            {
                picB = sender as PictureBox;
                if (picB.Tag != null && picB.Name == "null")
                {
                    picB.Image = Image.FromFile(tema + (string)picB.Tag + ".png");
                    secondChoice = (string)picB.Tag;
                    picB.Name = (string)picB.Tag;
                    if (firstChoice == secondChoice)
                    {
                        picA.Tag = null;
                        picB.Tag = null;
                        tries++;
                        label1.Text = "Broj poteza : " + tries;
                    }
                    else
                    {
                        tries++;
                        label1.Text = "Broj poteza : " + tries;
                    }


                    if (pictureBoxes.All(o => o.Tag == null))
                    {
                        End("Pobedili ste!!!");
                    }

                }
            }
            else
            {
                firstChoice = null;
                secondChoice = null;

                foreach (PictureBox x in pictureBoxes.ToList())
                {
                    if(x.Tag != null)
                    {
                        x.Image = Image.FromFile("pics/upitnik.png");
                        x.Name = "null";
                    }

                }

            }

            
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            int val = ++totalTime;
            label2.Text = "Vreme : " + val.ToString()+" s";
        }

        private void loadPictures()
        {
            int leftPos = 0;
            int topPos = 0;
            int r = 0;
            numbers = new List<int>();
            pictureBoxes = new List<PictureBox>();
            for (int i= 0;i<(rows*columns)/2;i++)
            {
                numbers.Add(i);
                numbers.Add(i);
            }
            
            for (int i=0;i<(rows*columns);i++)
            {
                PictureBox newPic = new PictureBox();
                newPic.Height = panel1.Height/columns;
                newPic.Width = panel1.Width/rows;
                newPic.BackColor = Color.White;
                newPic.SizeMode = PictureBoxSizeMode.StretchImage;
                newPic.Click += picture_Click;
                newPic.BorderStyle = BorderStyle.FixedSingle;
                if(r < rows)
                {
                    r++;
                    newPic.Left = leftPos;
                    newPic.Top = topPos;
                    leftPos = leftPos + panel1.Width / rows+1;
                }

                if(r == rows)
                {
                    leftPos = 0;
                    topPos += panel1.Height /columns+1;
                    r = 0;
                }
                
                pictureBoxes.Add(newPic);
            }

            restartGame();
        }

        private void restartGame()
        {
            var randomList = numbers.OrderBy(x => Guid.NewGuid()).ToList();
            numbers = randomList;
            panel1.Controls.Clear();

            for (int i = 0;i<numbers.Count;i++)
            {
                pictureBoxes[i].Image = Image.FromFile("pics/upitnik.png");
                pictureBoxes[i].Tag = numbers[i].ToString();
                pictureBoxes[i].Name = "null";
                panel1.Controls.Add(pictureBoxes[i]);
            }

            tries = 0;
            gameOver = false;
            GameTimer.Stop();
            totalTime = 0;
            GameTimer.Start();
            string rang = rows + " x " + columns;
            List<User> najboljiRez = b.dajNajboljeg(rang);
            label1.Text = "Broj poteza : " + tries;
            label2.Text = "Vreme : " + totalTime+" s";
            if(najboljiRez == null)
                label4.Text = "Komunikacija sa bazom \nnije uspostavljena";
            else if (najboljiRez.Count > 0)
                label4.Text = "Najbolji rezultat za tablu " + rang + "\nje napravio igrac pod imenom : \n" + najboljiRez[0].ime + "\nkoji je imao " + najboljiRez[0].score + " poteza\ni vreme " + najboljiRez[0].vreme+" sekundi.";
            else
                label4.Text = "Jos uvek ne postoji \nnajbolji rezultat.";
            label4.BorderStyle = BorderStyle.FixedSingle;
            panel1.BorderStyle = BorderStyle.FixedSingle;


        }

        private void End(string msg)
        {
            GameTimer.Stop();
            gameOver = true;
            if(b.konekcija == false)
            {
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show("Cestitamo na pobedi! Zbog konekcije sa bazom, niste u mogucnosti da upisete rezultat. ", "Igra je zavrsena!", buttons);
                if (result == DialogResult.Yes)
                {
                    return;
                }
                else
                {
                    System.Environment.Exit(0);
                }
            }

            Form2 frm2 = new Form2();
            frm2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadPictures();
        }

       

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            rows = 2;
            columns = 2;
            loadPictures();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            rows = 2;
            columns = 3;
            loadPictures();
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            rows = 3;
            columns = 4;
            loadPictures();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            rows = 4;
            columns = 4;
            loadPictures();
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            rows = 4;
            columns = 5;
            loadPictures();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            rows = 5;
            columns = 6;
            loadPictures();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            rows = 6;
            columns = 6;
            loadPictures();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

    }
}
