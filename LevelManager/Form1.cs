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
            //MessageBox.Show("x "+ Convert.ToInt32(e.X / kx) + " y " + Convert.ToInt32(e.Y / ky) + " color "+ ((Bitmap)pictureBox3.Image).GetPixel(Convert.ToInt32(e.X / kx), Convert.ToInt32(e.Y / ky)));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int yel = 0;
            int red = 0;
            int gra = 0;
            int blu = 0;
            int bla = 0;
            int whi = 0;
            for (int x = 0; x < pictureBox3.Image.Width; x++)
            {
                for (int y = 0; y < pictureBox3.Image.Height; y++)
                {
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
                }
            }
            MessageBox.Show("Эта карта содержит:\nЗолота: " + yel + "\nУскорителей: " + blu + "\nАгрессии: " + red + "\nКамней: " + gra + "\nБелоты: " + whi + "\nЧерноты: " + bla);
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
                            checkedListBox1.Items.Add(GD.LevelNames[i] + " " + i + 1);
                        }
                        else
                        {
                            checkedListBox1.Items.Add("Уровень " + i + 1);
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
        {//Добавляем уровень
            if (textBox1.Text != "")
            {
                GameData GD = GameData.getInstance();
                int AddedLevel = Convert.ToInt32(checkedListBox1.CheckedItems[1].ToString().Last());//Номер уровня, который хотим добавить
                for (int i = GD.LevelQuantity - 1; i < AddedLevel - 1; i--)
                {
                    GD.LevelNames[i + 1] = GD.LevelNames[i];
                    for (int j = 0; j < 3; j++)
                    {
                        GD.LevelLimitations[i + 1, j] = GD.LevelLimitations[i, j];
                    }
                    //public int[,,] LevelMaps = new int[201, 101, 101];
                    for (int j = 0; j < 201; j++)
                    {
                        for (int a = 0; a < 101; a++)
                        {
                            for (int b = 0; b < 101; b++)
                            {
                                GD.LevelMaps[j + 1, a, b] = GD.LevelMaps[j, a, b];
                            }
                        }
                    }
                    //public int[,,] LevelScore = new int[3, 201, 5];
                    for (int j = 0; j < 3; j++)
                    {
                        for (int a = 0; a < 201; a++)
                        {
                            for (int b = 0; b < 5; b++)
                            {
                                GD.LevelScore[j, a + 1, b] = GD.LevelScore[j, a, b];
                            }
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
                        if (((Bitmap)pictureBox3.Image).GetPixel(x, y).R == Color.Yellow.R && ((Bitmap)pictureBox3.Image).GetPixel(x, y).G == Color.Yellow.G && ((Bitmap)pictureBox3.Image).GetPixel(x, y).B == Color.Yellow.B)
                        {
                            GD.LevelMaps[AddedLevel, x, y] = 1;//************************************************РЫБА*******************************************************
                        }
                       
                        
                    }
                }
                    
                textBox1.Text = "";
            }
            else
            {
                MessageBox.Show("Имя уровня не введено");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {//Удаляем уровень

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
            label7.Text = "Ограничение по времени " + trackBar3.Value * 30 + " с.";            
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
        public int[,] LevelLimitations = new int[201, 2];
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
            GD.LevelMaps = GDS.LevelMaps;
            GD.MemCards = GDS.MemCards;
            //		for (int i=0;i<20;i++){
            //			if ((GDS.MemCardsMove [i] != 0) || (GDS.MemCardsMove [i] != 1) || (GDS.MemCardsMove [i] != 2)) {
            //				GD.MemCardsMove [i] = 1;
            //			
            //			} else {
            //				GD.MemCardsMove[i] = GDS.MemCardsMove[i];
            //			}
            //			//GD.MemCardsMove [i] = 1;
            //			//			for(int j=0;j<101;j++){
            //			//				for(int k=0;k<101;k++){
            //			//					if (GD.LevelMaps[i,j,k]!=0){
            //			Debug.Log (GD.MemCardsMove[i]);//,j,k]);
            //			//					}
            //			//				}
            //			//			}
            //		}
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
            public int[,] LevelLimitations = new int[201, 2];
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
