using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
            pictureBox2.Image = new Bitmap(pictureBox1.Image.Width, pictureBox1.Image.Height);
            for (int x = 0; x < pictureBox1.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox1.Image.Height; y++)
                {
                    if (((Bitmap)pictureBox1.Image).GetPixel(x, y).ToArgb() <= -terminator * 65536)
                    {
                        ((Bitmap)pictureBox2.Image).SetPixel(x, y, Color.Black);
                    }
                    else
                    {
                        if (((Bitmap)pictureBox1.Image).GetPixel(x, y).ToArgb() > -terminator * 65536)
                        {
                            ((Bitmap)pictureBox2.Image).SetPixel(x, y, Color.White);
                        }
                    }

                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {//Марафетим
            pictureBox3.Image = new Bitmap(pictureBox2.Image.Width, pictureBox2.Image.Height);
            pictureBox3.Image = (Bitmap)pictureBox2.Image;

        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            //pictureBox1.Width = (Int32)(this.Width / 4);
            //pictureBox2.Width = (Int32)(this.Width / 4);
            //pictureBox3.Width = (Int32)(this.Width / 4);
            //pictureBox1.Height = (Int32)(this.Height / 2);
            //pictureBox2.Height = (Int32)(this.Height / 2);
            //pictureBox3.Height = (Int32)(this.Height / 2);
            //pictureBox1.Left = 12;
            //pictureBox1.Top = 25;
            //pictureBox2.Left = 12+ pictureBox1.Width+12;
            //pictureBox2.Top = 25;
            //pictureBox3.Left = 12 + pictureBox1.Width + 12 + pictureBox1.Width+12;
            //pictureBox3.Top = 25;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            Color pnt = new Color();
            Graphics drpo;
            Rectangle rect = new Rectangle();
            drpo = Graphics.FromImage(pictureBox3.Image);
            float ky = (float)pictureBox3.Height / (float)pictureBox3.Image.Height;
            float kx = (float)pictureBox3.Width / (float)pictureBox3.Image.Width;
            pnt = ((Bitmap)pictureBox3.Image).GetPixel(Convert.ToInt32(e.X / kx), Convert.ToInt32(e.Y / ky));
            if (radioButton1.Checked)
            {//золото
                pnt = Color.Yellow;
            }
            if (radioButton2.Checked)
            {//ускорение
                pnt = Color.Blue;
            }
            if (radioButton3.Checked)
            {//Агрессия
                pnt = Color.Red;
            }
            if (radioButton4.Checked)
            {//Камень
                pnt = Color.Gray;
            }
            if (radioButton5.Checked)
            {//Вход
                pnt = Color.FromArgb(255, 128, 255, 128);
            }
            if (radioButton6.Checked)
            {//Выход
                pnt = Color.Lime;
            }
            if (radioButton7.Checked)
            {//Вода
                pnt = Color.Aqua;
            }
            if (radioButton8.Checked)
            {//Граница карты
                pnt = Color.FromArgb(255, 0, 64, 0);
            }
            if (radioButton9.Checked)
            {//Вход для неписей
                pnt = Color.Green;
            }
            if (radioButton10.Checked)
            {//Стационарный ускоритель (вход)
                pnt = Color.FromArgb(255, 255, 128, 0);
            }
            if (radioButton11.Checked)
            {//Стационарный ускоритель (выход)
                pnt = Color.FromArgb(255, 192, 64, 0);
            }
            if (trackBar2.Value == 0)
            {
                ((Bitmap)pictureBox3.Image).SetPixel(Convert.ToInt32(e.X / kx), Convert.ToInt32(e.Y / ky), pnt);
            }
            if (trackBar2.Value == 1)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        ((Bitmap)pictureBox3.Image).SetPixel(Convert.ToInt32(e.X / kx) - 1 + i, Convert.ToInt32(e.Y / ky) - 1 + j, pnt);
                    }
                }
            }
            if (trackBar2.Value == 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        ((Bitmap)pictureBox3.Image).SetPixel(Convert.ToInt32(e.X / kx) - 2 + i, Convert.ToInt32(e.Y / ky) - 2 + j, pnt);
                    }
                }
            }
            if (trackBar2.Value == 3)
            {
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        ((Bitmap)pictureBox3.Image).SetPixel(Convert.ToInt32(e.X / kx) - 3 + i, Convert.ToInt32(e.Y / ky) - 3 + j, pnt);
                    }
                }
            }

            pictureBox3.Invalidate();




            // Статистика
            int yel = 0;
            int red = 0;
            int gra = 0;
            int blu = 0;
            int bla = 0;
            int whi = 0;
            int start = 0;
            int finish = 0;
            int aqua = 0;
            int border = 0;
            int startnpc = 0;
            int acc_input = 0;
            int acc_output = 0;

            for (int x = 0; x < pictureBox3.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox3.Image.Height; y++)
                {
                    //+    0  - трава
                    //+    1  - бордюр
                    //+    2  - камень
                    //+    3  - дерево
                    //+    4  - золото
                    //+    5  - бонус скорость
                    //+    6  - бонус агрессия
                    //+    7  - вода
                    //-    8  - стационарный ускоритель вход
                    //-    9  - стационарный ускоритель выход
                    //+    89 - старт для неписей
                    //+    90 - старт
                    //+    91 - финиш
                    //    97 - хвост
                    //    98 - тело
                    //    99 - башка
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Yellow.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Yellow.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Yellow.B)
                    {
                        yel++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Gray.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Gray.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Gray.B)
                    {
                        gra++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Red.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Red.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Red.B)
                    {
                        red++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Blue.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Blue.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Blue.B)
                    {
                        blu++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Black.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Black.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Black.B)
                    {
                        bla++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.White.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.White.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.White.B)
                    {
                        whi++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 128 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 255 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 128)
                    {
                        start++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Lime.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Lime.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Lime.B)
                    {
                        finish++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Aqua.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Aqua.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Aqua.B)
                    {
                        aqua++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 0 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 64 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                    {
                        border++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Green.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Green.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Green.B)
                    {
                        startnpc++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 255 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 128 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                    {
                        acc_input++;
                    }
                    if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 192 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 64 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                    {
                        acc_output++;
                    }
                }
            }

            toolStripStatusLabel1.Text = "Эта карта содержит:   Золота: " + yel + "   Ускорителей: " + blu + "   Агрессии: " + red + "   Камней: " + gra + "   Травы: " + whi + "   Деревьев: " + bla +
                "   Воды: " + aqua + "   Ст.уск.вх: " + acc_input + "   Ст.уск.вых: " + acc_output + "   Вх.NPC: " + startnpc + "   Бордюра: " + border + "   Т.старта: " + start + "   Т.финиша: " + finish;

        }



        private void button8_Click(object sender, EventArgs e)
        {//Загружаем файл карт
            GameData GD = GameData.getInstance();
            checkedListBox1.Items.Clear();
            if (File.Exists("GameData"))
            {
                try
                {
                    GD.LoadGame();
                    for (int i = 0; i < GD.LevelQuantity; i++)
                    {
                        if (GD.LevelNames[i] != "")
                        {
                            checkedListBox1.Items.Add(GD.LevelNames[i] + " " + (i + 1));
                        }
                        else
                        {
                            checkedListBox1.Items.Add("Уровень " + (i + 1));
                        }

                    }
                }
                catch
                {
                    MessageBox.Show("Произошла ошибка добавления карт в список");
                }
            }
            else
            {
                if (MessageBox.Show("Создать новый файл?", "Файл GameData не найден", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GD.Bonuses.Initialize();
                    GD.CrusherLength = 100;
                    GD.CrusherStructure.Initialize();
                    GD.LevelLimitations.Initialize();
                    GD.LevelMaps.Initialize();
                    GD.LevelQuantity = 0;
                    GD.LevelScore.Initialize();
                    GD.LevelSetting.Initialize();
                    GD.MemCards.Initialize();
                    GD.MemCardsMove.Initialize();
                    GD.Money.Initialize();
                    GD.QActiveProfiles = 0;
                    GD.QMemCards = 0;
                    GD.LevelNames.Initialize();
                }
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();//Чистим список уровней
        }

        private void button6_Click(object sender, EventArgs e)
        {//Сохраняем наработки в файл
            GameData GD = GameData.getInstance();
            GD.SaveGame();
        }

        private void button9_Click(object sender, EventArgs e)
        {//Добавляем уровень ВЫШЕ
            if (textBox1.Text != "")
            {
                GameData GD = GameData.getInstance();
                int AddedLevel = checkedListBox1.SelectedIndex; ;//Номер уровня, который хотим добавить
                for (int i = GD.LevelQuantity - 1; i > AddedLevel - 1; i--)
                {
                    GD.LevelNames[i + 1] = GD.LevelNames[i];
                    for (int j = 0; j < 3; j++)
                    {
                        GD.LevelLimitations[i + 1, j] = GD.LevelLimitations[i, j];
                    }
                    //public int[,,] LevelMaps = new int[201, 101, 101];
                    for (int a = 0; a < 101; a++)
                    {
                        for (int b = 0; b < 101; b++)
                        {
                            GD.LevelMaps[i + 1, a, b] = GD.LevelMaps[i, a, b];
                        }
                    }

                    //public int[,,] LevelScore = new int[3, 201, 5];
                    for (int j = 0; j < 3; j++)
                    {
                        for (int b = 0; b < 5; b++)
                        {
                            GD.LevelScore[j, i + 1, b] = GD.LevelScore[j, i, b];
                        }
                    }
                    GD.LevelSetting[i + 1] = GD.LevelSetting[i];
                }

                //Создаем новый уровень
                GD.LevelNames[AddedLevel] = textBox1.Text; //название уровня
                GD.LevelLimitations[AddedLevel, 0] = trackBar3.Value * 30; //если есть - ограничение по времени прохождения уровня
                GD.LevelLimitations[AddedLevel, 1] = Convert.ToInt32(textBox2.Text); // если есть - ограничение по максимальной начальной длине змеи
                GD.LevelLimitations[AddedLevel, 2] = trackBar4.Value; //Процент плодовых деревьев среди всей массы деревьев уровня
                for (int x = 0; x < pictureBox3.Image.Width; x++)
                {
                    for (int y = 0; y < pictureBox3.Image.Height; y++)
                    {
                        //+    0  - трава
                        //+    1  - бордюр
                        //+    2  - камень
                        //+    3  - дерево
                        //+    4  - золото
                        //+    5  - бонус скорость
                        //+    6  - бонус агрессия
                        //+    7  - вода
                        //+    8  - стационарный ускоритель вход
                        //+    9  - стационарный ускоритель выход
                        //+    89 - старт для неписей
                        //+    90 - старт
                        //+    91 - финиш
                        //    97 - хвост
                        //    98 - тело
                        //    99 - башка
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Yellow.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Yellow.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Yellow.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 4;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Gray.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Gray.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Gray.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 2;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Red.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Red.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Red.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 6;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Blue.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Blue.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Blue.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 5;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Black.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Black.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Black.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 3;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.White.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.White.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.White.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 0;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 128 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 255 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 128)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 90;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Lime.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Lime.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Lime.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 91;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Aqua.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Aqua.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Aqua.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 7;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 0 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 64 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 1;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Green.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Green.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Green.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 89;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 255 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 128 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 8;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 192 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 64 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 9;
                        }
                    }
                }
                for (int scor = 0; scor < 3; scor++)
                {
                    GD.LevelScore[scor, AddedLevel, 0] = 0;
                    GD.LevelScore[scor, AddedLevel, 1] = Convert.ToInt32(textBox6.Text);
                    GD.LevelScore[scor, AddedLevel, 2] = Convert.ToInt32(textBox3.Text);
                    GD.LevelScore[scor, AddedLevel, 3] = Convert.ToInt32(textBox4.Text);
                    GD.LevelScore[scor, AddedLevel, 4] = Convert.ToInt32(textBox5.Text);
                }

                GD.LevelSetting[AddedLevel] =comboBox1.SelectedIndex;
                GD.LevelQuantity++;
                GD.SaveGame();
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Имя уровня не введено");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {//Удаляем уровень
            GameData GD = GameData.getInstance();
            int RemovedLevel = checkedListBox1.SelectedIndex;
            if (RemovedLevel != GD.LevelQuantity - 1)
            {
                for (int i = RemovedLevel; i < GD.LevelQuantity - 1; i++)
                {
                    GD.LevelNames[i] = GD.LevelNames[i + 1];
                    for (int j = 0; j < 3; j++)
                    {
                        GD.LevelLimitations[i, j] = GD.LevelLimitations[i + 1, j];
                    }
                    //public int[,,] LevelMaps = new int[201, 101, 101];

                    for (int a = 0; a < 101; a++)
                    {
                        for (int b = 0; b < 101; b++)
                        {
                            GD.LevelMaps[i, a, b] = GD.LevelMaps[i + 1, a, b];
                        }
                    }

                    //public int[,,] LevelScore = new int[3, 201, 5];
                    for (int j = 0; j < 3; j++)
                    {

                        for (int b = 0; b < 5; b++)
                        {
                            GD.LevelScore[j, i, b] = GD.LevelScore[j, i + 1, b];
                        }

                    }
                    GD.LevelSetting[i] = GD.LevelSetting[i + 1];
                }
            }
            GD.LevelQuantity--;
            GD.SaveGame();
            button8_Click(this, new EventArgs());
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Если отмечено больше 2 элементов, то снимаем выделение со всех и отмечаем текущий.
            if (checkedListBox1.CheckedItems.Count > 1)
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    checkedListBox1.SetItemChecked(i, false);
                checkedListBox1.SetItemChecked(checkedListBox1.SelectedIndex, true);
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label8.Text = "Ограничение по времени " + trackBar3.Value * 30 + " с.";
        }

        private void button11_Click(object sender, EventArgs e)
        {//Создать уровень в конце
            if (textBox1.Text != "")
            {
                GameData GD = GameData.getInstance();
                int AddedLevel = GD.LevelQuantity;//Номер уровня, который хотим добавить

                //Создаем новый уровень
                GD.LevelNames[AddedLevel] = textBox1.Text; //название уровня
                GD.LevelLimitations[AddedLevel, 0] = trackBar3.Value * 30; //если есть - ограничение по времени прохождения уровня
                GD.LevelLimitations[AddedLevel, 1] = Convert.ToInt32(textBox2.Text); // если есть - ограничение по максимальной начальной длине змеи
                GD.LevelLimitations[AddedLevel, 2] = trackBar4.Value; //Процент плодовых деревьев среди всей массы деревьев уровня
                for (int x = 0; x < pictureBox3.Image.Width; x++)
                {
                    for (int y = 0; y < pictureBox3.Image.Height; y++)
                    {
                        //+    0  - трава
                        //+    1  - бордюр
                        //+    2  - камень
                        //+    3  - дерево
                        //+    4  - золото
                        //+    5  - бонус скорость
                        //+    6  - бонус агрессия
                        //+    7  - вода
                        //+    8  - стационарный ускоритель вход
                        //+    9  - стационарный ускоритель выход
                        //+    89 - старт для неписей
                        //+    90 - старт
                        //+    91 - финиш
                        //    97 - хвост
                        //    98 - тело
                        //    99 - башка
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Yellow.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Yellow.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Yellow.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 4;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Gray.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Gray.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Gray.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 2;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Red.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Red.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Red.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 6;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Blue.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Blue.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Blue.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 5;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Black.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Black.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Black.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 3;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.White.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.White.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.White.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 0;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 128 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 255 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 128)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 90;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Lime.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Lime.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Lime.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 91;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Aqua.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Aqua.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Aqua.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 7;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 0 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 64 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 1;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Green.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Green.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Green.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 89;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 255 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 128 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 8;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 192 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 64 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 9;
                        }
                    }
                }
                for (int scor = 0; scor < 3; scor++)
                {
                    GD.LevelScore[scor, AddedLevel, 0] = 0;
                    GD.LevelScore[scor, AddedLevel, 1] = Convert.ToInt32(textBox6.Text);
                    GD.LevelScore[scor, AddedLevel, 2] = Convert.ToInt32(textBox3.Text);
                    GD.LevelScore[scor, AddedLevel, 3] = Convert.ToInt32(textBox4.Text);
                    GD.LevelScore[scor, AddedLevel, 4] = Convert.ToInt32(textBox5.Text);
                }

                GD.LevelSetting[AddedLevel] = comboBox1.SelectedIndex;
                GD.LevelQuantity++;
                GD.SaveGame();
                textBox1.Text = "";

                button8_Click(this, new EventArgs());
            }
            else
            {
                MessageBox.Show("Имя уровня не введено");
            }



        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label10.Text = "Процент плодовых деревьев " + trackBar4.Value + "%";
        }

        private void button5_Click(object sender, EventArgs e)
        {//Показать превью карты
            GameData GD = GameData.getInstance();
            int PreviewNumber = checkedListBox1.SelectedIndex;
            textBox1.Text = GD.LevelNames[PreviewNumber];//название уровня
            textBox2.Text = GD.LevelLimitations[PreviewNumber, 1].ToString();// если есть - ограничение по максимальной начальной длине змеи
            textBox3.Text = GD.LevelScore[0, PreviewNumber, 2].ToString();
            textBox4.Text = GD.LevelScore[0, PreviewNumber, 3].ToString();
            textBox5.Text = GD.LevelScore[0, PreviewNumber, 4].ToString();
            textBox6.Text = GD.LevelScore[0, PreviewNumber, 1].ToString();
            comboBox1.SelectedIndex = GD.LevelSetting[PreviewNumber];            
            trackBar3.Value = Convert.ToInt32(GD.LevelLimitations[PreviewNumber, 0]/30);//если есть - ограничение по времени прохождения уровня
            trackBar4.Value = GD.LevelLimitations[PreviewNumber, 2]; //Процент плодовых деревьев среди всей массы деревьев уровня
            int lenx = GD.LevelMaps.GetLength(1);
            int leny = GD.LevelMaps.GetLength(2);
            pictureBox4.Image = new Bitmap(lenx, leny);
            for (int x = 0; x < lenx; x++)
            {
                for (int y = 0; y < leny; y++)
                {
                    Color col = new Color();
                    switch (GD.LevelMaps[PreviewNumber, x, y])
                    {
                        //+    0  - трава
                        case 0:
                            col = Color.FromArgb(Color.White.R, Color.White.G, Color.White.B);
                            break;
                        //+    1  - бордюр
                        case 1:
                            col = Color.FromArgb(0, 64, 0);
                            break;
                        //+    2  - камень
                        case 2:
                            col = Color.FromArgb(Color.Gray.R, Color.Gray.G, Color.Gray.B);
                            break;
                        //+    3  - дерево
                        case 3:
                            col = Color.FromArgb(Color.Black.R, Color.Black.G, Color.Black.B);
                            break;
                        //+    4  - золото
                        case 4:
                            col = Color.FromArgb(Color.Yellow.R, Color.Yellow.G, Color.Yellow.B);
                            break;
                        //+    5  - бонус скорость
                        case 5:
                            col = Color.FromArgb(Color.Blue.R, Color.Blue.G, Color.Blue.B);
                            break;
                        //+    6  - бонус агрессия
                        case 6:
                            col = Color.FromArgb(Color.Red.R, Color.Red.G, Color.Red.B);
                            break;
                        //+    7  - вода
                        case 7:
                            col = Color.FromArgb(Color.Aqua.R, Color.Aqua.G, Color.Aqua.B);
                            break;
                        //+    8  - стационарный ускоритель вход
                        case 8:
                            col = Color.FromArgb(255, 128, 0);
                            break;
                        //+    9  - стационарный ускоритель выход
                        case 9:
                            col = Color.FromArgb(192, 64, 0);
                            break;
                        //+    89 - старт для неписей
                        case 89:
                            col = Color.FromArgb(Color.Green.R, Color.Green.G, Color.Green.B);
                            break;
                        //+    90 - старт
                        case 90:
                            col = Color.FromArgb(128, 255, 128);
                            break;
                        //+    91 - финиш
                        case 91:
                            col = Color.FromArgb(Color.Lime.R, Color.Lime.G, Color.Lime.B);
                            break;
                        default:
                            col = Color.FromArgb(Color.Black.R, Color.Black.G, Color.Black.B);
                            break;
                    }
                    ((Bitmap)pictureBox4.Image).SetPixel(x, y, col);


                }
            }


        }

        private void button7_Click(object sender, EventArgs e)
        {

            try
            {
                pictureBox3.Image = new Bitmap(pictureBox4.Image.Width, pictureBox4.Image.Height);
                pictureBox3.Image = (Bitmap)pictureBox4.Image;
                // Статистика
                int yel = 0;
                int red = 0;
                int gra = 0;
                int blu = 0;
                int bla = 0;
                int whi = 0;
                int start = 0;
                int finish = 0;
                int aqua = 0;
                int border = 0;
                int startnpc = 0;
                int acc_input = 0;
                int acc_output = 0;

                for (int x = 0; x < pictureBox3.Image.Width; x++)
                {
                    for (int y = 0; y < pictureBox3.Image.Height; y++)
                    {
                        //+    0  - трава
                        //+    1  - бордюр
                        //+    2  - камень
                        //+    3  - дерево
                        //+    4  - золото
                        //+    5  - бонус скорость
                        //+    6  - бонус агрессия
                        //+    7  - вода
                        //-    8  - стационарный ускоритель вход
                        //-    9  - стационарный ускоритель выход
                        //+    89 - старт для неписей
                        //+    90 - старт
                        //+    91 - финиш
                        //    97 - хвост
                        //    98 - тело
                        //    99 - башка
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Yellow.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Yellow.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Yellow.B)
                        {
                            yel++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Gray.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Gray.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Gray.B)
                        {
                            gra++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Red.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Red.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Red.B)
                        {
                            red++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Blue.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Blue.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Blue.B)
                        {
                            blu++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Black.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Black.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Black.B)
                        {
                            bla++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.White.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.White.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.White.B)
                        {
                            whi++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 128 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 255 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 128)
                        {
                            start++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Lime.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Lime.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Lime.B)
                        {
                            finish++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Aqua.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Aqua.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Aqua.B)
                        {
                            aqua++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 0 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 64 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                        {
                            border++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Green.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Green.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Green.B)
                        {
                            startnpc++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 255 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 128 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                        {
                            acc_input++;
                        }
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == 192 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == 64 && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == 0)
                        {
                            acc_output++;
                        }
                    }
                }

                toolStripStatusLabel1.Text = "Эта карта содержит:   Золота: " + yel + "   Ускорителей: " + blu + "   Агрессии: " + red + "   Камней: " + gra + "   Травы: " + whi + "   Деревьев: " + bla +
                    "   Воды: " + aqua + "   Ст.уск.вх: " + acc_input + "   Ст.уск.вых: " + acc_output + "   Вх.NPC: " + startnpc + "   Бордюра: " + border + "   Т.старта: " + start + "   Т.финиша: " + finish;

            }
            catch
            {
                MessageBox.Show("В превью пусто!");
            }

        }
    }
















    //Класс для хранения игровых данных
    public class GameData
    {
        private static GameData instance;
        //Объявляем как синглтон
        private GameData()
        {
        }

        public static GameData getInstance()
        {
            if (instance == null)
                instance = new GameData();
            return instance;
        }
        public string[] LevelNames = new string[201];
        //Названия карт
        public int QActiveProfiles;
        //Количество активных профилей сохранения
        public int[] Money = new int[3];
        //Деньге каждого профиля
        public int LevelQuantity;
        //Количество уровней
        public int[,,] LevelScore = new int[3, 201, 5];
        //0 - мой скор, 1- макс. скор, 2 - бронза, 3 - серебро, 4 - золото
        public int[,] LevelLimitations = new int[201, 3];
        //0 - время, если есть, то количество секунд, 1 - ограничение по начальной длине змеи, если есть, количество блоков
        public int[] LevelSetting = new int[201];
        //число - номер набора тайлов (сеттинг - поле, лес, горы, лава и пр)
        public int[,,] LevelMaps = new int[201, 101, 101];
        //Карты уровней, считаем что уровней 200, размером 100х100
        public int[,,,,] MemCards = new int[3, 20, 4, 10, 10];
        //профиль, максимальное количество карточек, 4 варианта повернутости карточки, максимальные размеры
        public int[] MemCardsMove = new int[20];
        public int QMemCards;
        public int[,] Bonuses = new int[3, 10];
        //каждый номер - остаток определенного бонуса в инвентарекаждого профиля, который можно использовать
        public int CrusherLength;
        //длина змеи
        public int[] CrusherStructure = new int[101];
        //состав змеи при том что максимальная длина змеи - 100 кусков
        public void SaveGame()
        {
            GameData GD = GameData.getInstance();
            GameDataSer GDS = new GameDataSer();
            GDS.LevelNames = GD.LevelNames;
            GDS.Money = GD.Money;
            GDS.QActiveProfiles = GD.QActiveProfiles;
            GDS.LevelQuantity = GD.LevelQuantity;
            GDS.LevelScore = GD.LevelScore;
            GDS.LevelLimitations = GD.LevelLimitations;
            GDS.LevelSetting = GD.LevelSetting;
            GDS.LevelMaps = GD.LevelMaps;
            GDS.MemCards = GD.MemCards;
            GDS.MemCardsMove = GD.MemCardsMove;
            GDS.QMemCards = GD.QMemCards;
            GDS.Bonuses = GD.Bonuses;
            GDS.CrusherLength = GD.CrusherLength;
            GDS.CrusherStructure = GD.CrusherStructure;
            BinaryFormatter BF = new BinaryFormatter();
            using (Stream fs = new FileStream("GameData", FileMode.Create,
                                   FileAccess.Write, FileShare.None))
            {
                BF.Serialize(fs, GDS);
            }
        }

        public void LoadGame()
        {
            GameData GD = GameData.getInstance();
            GameDataSer GDS = new GameDataSer();
            BinaryFormatter BF = new BinaryFormatter();
            using (Stream fs = new FileStream("GameData", FileMode.OpenOrCreate))
            {
                GDS = (GameDataSer)BF.Deserialize(fs);
            }

            GD.Money = GDS.Money;
            GD.QActiveProfiles = GDS.QActiveProfiles;
            GD.LevelQuantity = GDS.LevelQuantity;
            GD.LevelScore = GDS.LevelScore;
            GD.LevelLimitations = GDS.LevelLimitations;
            GD.LevelSetting = GDS.LevelSetting;
            GD.LevelNames = GDS.LevelNames;
            GD.LevelMaps = GDS.LevelMaps;
            GD.MemCards = GDS.MemCards;
            GD.MemCardsMove = GDS.MemCardsMove;
            GD.QMemCards = GDS.QMemCards;
            GD.Bonuses = GDS.Bonuses;
            GD.CrusherLength = GDS.CrusherLength;
            GD.CrusherStructure = GDS.CrusherStructure;
        }


        [Serializable]
        public class GameDataSer
        {
            public string[] LevelNames = new string[201];
            //Названия карт
            public int QActiveProfiles;
            //Количество активных профилей сохранения
            public int[] Money = new int[3];
            //Деньге
            public int LevelQuantity;
            //Количество уровней
            public int[,,] LevelScore = new int[3, 201, 5];
            //0 - мой скор, 1- макс. скор, 2 - бронза, 3 - серебро, 4 - золото
            public int[,] LevelLimitations = new int[201, 3];
            //0 - время, если есть, то количество секунд, 1 - ограничение по начальной длине змеи, если есть, количество блоков
            public int[] LevelSetting = new int[201];
            //число - номер набора тайлов (сеттинг - поле, лес, горы, лава и пр)
            public int[,,] LevelMaps = new int[201, 101, 101];
            //Карты уровней, считаем что уровней 200, размером 100х100
            public int[,,,,] MemCards = new int[3, 20, 4, 10, 10];
            public int[] MemCardsMove = new int[20];
            public int QMemCards;
            //максимальное количество карточек, 4 варианта повернутости карточки, максимальные размеры
            public int[,] Bonuses = new int[3, 10];
            //каждый номер - остаток определенного бонуса в инвентаре, который можно использовать
            public int CrusherLength;
            //длина змеи
            public int[] CrusherStructure = new int[101];
            //состав змеи при том что максимальная длина змеи - 100 кусков
        }
    }



























}
