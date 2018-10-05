using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LevelManager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int terminator = trackBar1.Value;
            pictureBox2.Image = new Bitmap(pictureBox1.Image.Width,pictureBox1.Image.Height);
            for (int x = 0; x < pictureBox1.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Image.Height; y++)
                {
                    if (((Bitmap)pictureBox1.Image).GetPixel(x, y).ToArgb() > -terminator*65536)
                    {
                        ((Bitmap)pictureBox2.Image).SetPixel(x, y, Color.Black);
                    }
                    else 
                    {
                        if (((Bitmap)pictureBox1.Image).GetPixel(x, y).ToArgb() <= -terminator * 65536)
                        {
                            ((Bitmap)pictureBox2.Image).SetPixel(x, y, Color.White);
                        }                        
                    }
                }
            }
       
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);

        }
    }
}
