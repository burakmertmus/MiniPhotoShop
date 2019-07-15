using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace GoruntuIsleme
{

    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        List<Bitmap> resimList = new List<Bitmap>();
        private int ListSayici = 0;

        public int[] kirmizi = new int[256];
        public int[] yesil = new int[256];
        public int[] mavi = new int[256];
        public int[] gray = new int[256];

        private void Form1_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            groupBoxRenk.Enabled = false;
            groupBoxDondurme.Enabled = false;
            groupBoxRGB.Enabled = false;
            buttonKaydet.Enabled = false;
            buttonIleri.Enabled = false;
            buttonGeri.Enabled = false;
            buttonIlkResim.Enabled = false; 
            groupBoxOlcek.Enabled = false;
            groupBoxHistogram.Enabled = false;
            pictureBox1.Enabled = false;

            textBoxR.Enabled = false;
            textBoxG.Enabled = false;
            textBoxB.Enabled = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            resimList.Clear();
            ListSayici = 0;
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Resim dosyası  |*.BMP;*PNG;*JPEG;*.JPG;*.GIF|All files (*.*)|*.*";
            file.Title = "Aç";

            if (file.ShowDialog() != (System.Windows.Forms.DialogResult.OK))
            {
                return;
            }

            pictureBox1.ImageLocation = file.FileName;
            Bitmap rsm;
            rsm = new Bitmap(file.FileName);

            
            resimList.Add(rsm);


            textBoxR.Enabled = true; 
            textBoxG.Enabled = true; 
            textBoxB.Enabled = true;


            

            pictureBox1.Enabled = true;
            groupBoxRenk.Enabled = true;
            groupBoxDondurme.Enabled = true;
            groupBoxOlcek.Enabled = true;
            groupBoxRGB.Enabled=true;
            buttonKaydet.Enabled = true;
            groupBoxHistogram.Enabled = true;
            buttonIlkResim.Enabled = true;
            
            
        }
        //---------GriResim
        private Bitmap griImg(Bitmap rsm)
        {

            

            for (int i = 0; i < rsm.Width; i++)
            {
                for (int j = 0; j < rsm.Height; j++)
                {
                    int griDegeri = Convert.ToInt16(rsm.GetPixel(i,j).R * 0.299 + rsm.GetPixel(i,j).G * 0.587 + rsm.GetPixel(i,j).B * 0.114);
                    // int griDegeri = (bmp.GetPixel(i,j).R + bmp.GetPixel(i,j).G + bmp.GetPixel(i,j).B) / 3;


                    Color renk;
                    renk = Color.FromArgb(griDegeri, griDegeri, griDegeri);
                    rsm.SetPixel(i,j, renk);

                }
            }

            return rsm;

        }

        private void buttonGri_Click(object sender, EventArgs e)
        {
            Bitmap tmp;
            if (resimList.Count != ListSayici + 1)
            {
                resimList.RemoveRange(ListSayici+1, resimList.Count - ListSayici-1);
                buttonIleri.Enabled = false;
            }
            tmp = new Bitmap(pictureBox1.Image);
            buttonGeri.Enabled = true;
            tmp = griImg(tmp);
            resimList.Add(tmp);
            ListSayici++;
            pictureBox1.Image = tmp;
        }
        //---Negatif---------
        private Bitmap negatifYap(Bitmap bmp)
        {
            Bitmap dnm = new Bitmap(bmp);

            for (int x = 0; x < bmp.Width; x++)
            {
                for (int y = 0; y < bmp.Height; y++)
                {

                    Color renk;
                    renk = Color.FromArgb(255 - bmp.GetPixel(x, y).R, 255 - bmp.GetPixel(x, y).G, 255 - bmp.GetPixel(x, y).B);

                    dnm.SetPixel(x, y, renk);

                }
            }

            return dnm;
        }

        private void butonNeg_Click(object sender, EventArgs e)
        {
            Bitmap tmp;
            if (resimList.Count != ListSayici + 1)
            {
                resimList.RemoveRange(ListSayici+1, resimList.Count - ListSayici - 1);
                buttonIleri.Enabled = false;
            }
            tmp = new Bitmap(pictureBox1.Image);
            buttonGeri.Enabled = true;
            tmp = negatifYap(tmp);
            resimList.Add(tmp);
            ListSayici++;
            pictureBox1.Image = tmp;
        }
        //------------------------------------------------
        //---------Yatay Aynala----------
        //------------------------------------------------
        private Bitmap yatayAynala(Bitmap rsm)
        {
            Bitmap dnm = new Bitmap(rsm.Width, rsm.Height);

            for (int i = 0; i < rsm.Width; i++)
            {
                for (int j = 0; j < rsm.Height; j++)
                {
                    dnm.SetPixel(i, dnm.Height - j - 1, rsm.GetPixel(i, j));

                }
            }

            return dnm;
        }

        private void buttonYatayAyna_Click(object sender, EventArgs e)
        {
            Bitmap tmp;
            if (resimList.Count != ListSayici + 1)
            {
                resimList.RemoveRange(ListSayici+1, resimList.Count - ListSayici - 1);
                buttonIleri.Enabled = false; 
            }
            tmp = new Bitmap(pictureBox1.Image);
            buttonGeri.Enabled = true;
            tmp = yatayAynala(tmp);
            resimList.Add(tmp);
            ListSayici++;
            pictureBox1.Image = tmp;
        }
        //------------------------------------------------------------------------
        //---------Dikey Aynalama işlemi------------------------
        //-----------------------------------------------------------------------

        private Bitmap DikeyAynala(Bitmap rsm)
        {
            Bitmap dnm = new Bitmap(rsm.Width, rsm.Height);

            for (int i = 0; i < rsm.Width; i++)
            {
                for (int j = 0; j < rsm.Height; j++)
                {
                    dnm.SetPixel(dnm.Width - i - 1, j, rsm.GetPixel(i, j));

                }
            }

            return dnm;
        }

        private void buttonDikeyAyna_Click(object sender, EventArgs e)
        {
            Bitmap tmp;
            if (resimList.Count != ListSayici + 1)
            {
                resimList.RemoveRange(ListSayici+1, resimList.Count - ListSayici - 1);
                buttonIleri.Enabled = false; 
            } 
            tmp = new Bitmap(pictureBox1.Image);
            buttonGeri.Enabled = true;
            tmp = DikeyAynala(tmp);
            resimList.Add(tmp);
            ListSayici++;
            pictureBox1.Image = tmp;
        }


        //------------------------------------------------------------------------
        //---------Sağa Çevirme------------------------
        //-----------------------------------------------------------------------
        private Bitmap dondurSag(Bitmap rsm)
        {
            Bitmap dnm = new Bitmap(rsm.Height, rsm.Width);

            for (int i = 0; i < rsm.Width; i++)
            {
                for (int j = 0; j < rsm.Height; j++)
                {
                    dnm.SetPixel(dnm.Width - j - 1, i, rsm.GetPixel(i, j));

                }
            }
            return dnm;
        }

        private void buttonSag_Click(object sender, EventArgs e)
        {
            Bitmap tmp;
            if (resimList.Count != ListSayici + 1)
            {
                resimList.RemoveRange(ListSayici, resimList.Count - ListSayici - 1);
                buttonIleri.Enabled = false;
            }
            tmp = new Bitmap(pictureBox1.Image);
            buttonGeri.Enabled = true;
            tmp = dondurSag(tmp);
            resimList.Add(tmp);
            ListSayici++;
            pictureBox1.Image = tmp;
        }
        //-----------------------------------------------------------------------
        //-------Sola Dondurme-------------- 
        //-----------------------------------------------------------------------

        private Bitmap dondurSol(Bitmap rsm)
        {

            Bitmap dnm = new Bitmap(rsm.Height, rsm.Width);

            for (int i = 0; i < rsm.Width; i++)
            {
                for (int j = 0; j < rsm.Height; j++)
                {
                    dnm.SetPixel(j, dnm.Height - 1 - i, rsm.GetPixel(i, j));

                }
            }
            return dnm;
        }

        private void buttonSol_Click(object sender, EventArgs e)
        {
            Bitmap tmp;
            if (resimList.Count != ListSayici + 1)
            {
                resimList.RemoveRange(ListSayici, resimList.Count - ListSayici - 1);
                buttonIleri.Enabled = false;
            }
            tmp = new Bitmap(pictureBox1.Image);
            buttonGeri.Enabled = true;
            tmp = dondurSol(tmp);
            resimList.Add(tmp);
            ListSayici++;
            pictureBox1.Image = tmp;
            
          
        }

          //resimList.RemoveRange(ListSayici, resimList.Count - ListSayici);


        //--------------------------------------------------------------------
        //-----------Red Kanalı-----------------------------------------------
        private void buttonR_Click(object sender, EventArgs e)
        {
            Bitmap tmp;
            if (resimList.Count != ListSayici + 1)
            {
                resimList.RemoveRange(ListSayici+1, resimList.Count - ListSayici - 1);
                buttonIleri.Enabled = false;

            }
            tmp = new Bitmap(pictureBox1.Image);
            buttonGeri.Enabled = true;
            tmp = rKanalı(tmp);
            resimList.Add(tmp);
            ListSayici++;
            pictureBox1.Image = tmp;
        }

        private Bitmap rKanalı(Bitmap bmp)
        {

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {

                     Color color = bmp.GetPixel(i, j);
                     
                     Color tmp =Color.FromArgb(color.R,color.R,color.R);

                    bmp.SetPixel(i,j,tmp);
                    
                    
                }
            }

            return bmp;
        }
        //--------------------------------------------------------------------
        //-----------Yeşil Kanalı-----------------------------------------------
        private void buttonG_Click(object sender, EventArgs e)
        {
            Bitmap tmp;
            if (resimList.Count != ListSayici + 1)
            {
                resimList.RemoveRange(ListSayici+1, resimList.Count - ListSayici - 1);
                buttonIleri.Enabled = false;


            } 
            tmp = new Bitmap(pictureBox1.Image);
            buttonGeri.Enabled = true;
            tmp = gKanalı(tmp);
            resimList.Add(tmp);
            ListSayici++;
            pictureBox1.Image = tmp;
        }

        private Bitmap gKanalı(Bitmap bmp)
        {

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {


                    Color color = bmp.GetPixel(i, j);

                    Color tmp = Color.FromArgb(color.G, color.G, color.G);

                    bmp.SetPixel(i, j, tmp);


                }
            }
           
            return bmp;
        }

        //--------------------------------------------------------------------
        //-----------Mavi Kanalı-----------------------------------------------
        private void buttonB_Click(object sender, EventArgs e)
        {
            Bitmap tmp;
            if (resimList.Count != ListSayici + 1)
            {
                resimList.RemoveRange(ListSayici+1, resimList.Count - ListSayici - 1);
                buttonIleri.Enabled = false;
            }
            tmp = new Bitmap(pictureBox1.Image);
            buttonGeri.Enabled = true;
            tmp = bKanalı(tmp);
            resimList.Add(tmp);
            ListSayici++;
            pictureBox1.Image = tmp;
        }

        private Bitmap bKanalı(Bitmap bmp)
        {

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {


                    Color color = bmp.GetPixel(i, j);

                    Color tmp = Color.FromArgb(color.B,color.B,color.B);

                    bmp.SetPixel(i, j, tmp);


                }
            }
            return bmp;
        }

        //-----------------------------------------------------------------------
        //----------Geri alma Butonu İslemi
        //-----------------------------------------------------------------------
        private void buttonGeri_Click(object sender, EventArgs e)
        {
            if (ListSayici > 0)
            {
                ListSayici--;
                pictureBox1.Image = resimList[ListSayici];
                
            }
            if (ListSayici == 0)
            {
                buttonGeri.Enabled = false;
            }
            buttonIleri.Enabled = true;

        }
        //-----------------------------------------------------------------------
        //----------İleri ALma Butonu
        //-----------------------------------------------------------------------
        private void buttonIleri_Click(object sender, EventArgs e)
        {

            if (resimList.Count > ListSayici)
            {
                ListSayici++;
                pictureBox1.Image = resimList[ListSayici];

            }
            if (resimList.Count == ListSayici + 1)
            {
                buttonIleri.Enabled = false;

            }
            buttonGeri.Enabled = true;
        }



        //-----------------------------------------------------------------------
        //----------İlk resim------------
        //-----------------------------------------------------------------------//-----------------------------------------------------------------------

        private void buttonIlkResim_Click(object sender, EventArgs e)
        {

            resimList.Add(resimList[0]);
            ListSayici++;
            pictureBox1.Image = resimList[ListSayici];
        }

        //-----------------------------------------------------------------------
        //------------------HİSTOGRAM-----
        //-----------------------------------------------------------------------
        public void histogram(Bitmap rsm)
        { 
            diziSifirla();

            for (int i = 0; i < rsm.Size.Height; i++)
                for (int j = 0; j < rsm.Size.Width; j++)
                {
                    Color renk = rsm.GetPixel(j, i);

                    kirmizi[renk.R]++;

                    yesil[renk.G]++;
                    mavi[renk.B]++;
                }

            histoGrafik(rsm);
            
            diziSifirla();

            
            
        }
        

        private void histoGrafik(Bitmap rsm)
        {

            // System.Drawing.Graphics grafiknesne;
            // grafiknesne = this.CreateGraphics();
            Graphics grafikCizgi;

            grafikCizgi = pictureBoxHistogram.CreateGraphics();
            Pen black = new Pen(Color.Black, 2);
            Pen redPen = new Pen(System.Drawing.Color.Red, 1);
            Pen bluePen = new Pen(System.Drawing.Color.Blue, 1);
            Pen greenPen = new Pen(System.Drawing.Color.Green, 1);

            pictureBoxHistogram.BackColor = SystemColors.ControlDark;
            int yatayKaydir = 30,dikeyKaydir = -1;
            for (int i = 0; i < 256; i++)
            {

                if (i != 255)
                {

                    int oran = Convert.ToInt16(textBoxHisto.Text) ;
                    grafikCizgi.DrawLine(redPen, i + yatayKaydir, (250 - (kirmizi[i] / oran)) + dikeyKaydir, i + yatayKaydir + 1, (250 - (kirmizi[i + 1] / oran)) + dikeyKaydir);
                    grafikCizgi.DrawLine(bluePen, i + yatayKaydir, (250 - (mavi[i] / oran) + dikeyKaydir), i + yatayKaydir + 1, (250 - (mavi[i + 1] / oran)) + dikeyKaydir);
                    grafikCizgi.DrawLine(greenPen, i + yatayKaydir, (250 - (yesil[i] / oran) + dikeyKaydir), i + yatayKaydir + 1, (250 - (yesil[i + 1] / oran)) + dikeyKaydir);
                    if ((i % 15) == 0)
                    {
                        grafikCizgi.DrawLine(black, i + yatayKaydir+15, 250-4, i + yatayKaydir+15, 256-4);
                    }

                }
            }
            grafikCizgi.DrawLine(black, yatayKaydir, 0, yatayKaydir, 325);//Dikey siyah Cizgi
            grafikCizgi.DrawLine(black, 0, 250+dikeyKaydir, 370, 250+dikeyKaydir);//Yatay siyah çizgi

            grafikCizgi.DrawLine(black, 365, 250 + dikeyKaydir, 360, 250 + dikeyKaydir-5);
            grafikCizgi.DrawLine(black, 365, 250 + dikeyKaydir, 360, 250 + dikeyKaydir+5);

            grafikCizgi.DrawLine(black, yatayKaydir, 0, yatayKaydir - 5, 5);//dikeydeki doğru okları
            grafikCizgi.DrawLine(black, yatayKaydir, 0, yatayKaydir + 5, 5);//dikeydeki doğru okları


        }


        private void diziSifirla()
        {
            for (int i = 0; i < 256; i++)
            {
                mavi[i] = 0;
                yesil[i] = 0;
                kirmizi[i] = 0;

            }
        }

        private void buttonHistogram_Click(object sender, EventArgs e)
        {
            pictureBoxHistogram.Refresh();
            if (textBoxHisto.Text == "")
            {
                MessageBox.Show("Histogram Deger Azaltma Katsayısını giriniz");
            }
            else
            {

                
                Bitmap rsm = new Bitmap(pictureBox1.Image);
                histogram(rsm);
            }

        }
        //**---------------------------------------------------
        //**---------------------------------------------------
        //-RGB bul
        //**---------------------------------------------------

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            double genislik = 448 * resimList[ListSayici].Width / resimList[ListSayici].Height;
            //418,448
            double bosluk = (448 - genislik) / 2.00;
            //sola kaydırma
            if (resimList[ListSayici].Height >= resimList[ListSayici].Width && e.X >= bosluk && e.X < 448 - bosluk)
            {
                double sayi = resimList[ListSayici].Height;
                double oran = sayi / 448.00;

                Color color = resimList[ListSayici].GetPixel(Convert.ToInt16((e.X - bosluk) * oran), Convert.ToInt16(e.Y * oran));

                textBoxR.Text = color.R.ToString();
                textBoxG.Text = color.G.ToString();
                textBoxB.Text = color.B.ToString();

                panelRenk.BackColor = color;
            }
            //üste kaydırma
            else if (resimList[ListSayici].Height < resimList[ListSayici].Width)
            {
                genislik = 418 * resimList[ListSayici].Height / resimList[ListSayici].Width;
                bosluk = (418 - genislik) / 2;

                if (e.Y >= bosluk && e.Y < 418 - bosluk)
                {
                    double sayi = resimList[ListSayici].Width;
                    double oran = sayi / 418.00;

                    Color color = resimList[ListSayici].GetPixel(Convert.ToInt16(e.X * oran), Convert.ToInt16((e.Y - bosluk) * oran));

                    textBoxR.Text = color.R.ToString();
                    textBoxG.Text = color.G.ToString();
                    textBoxB.Text = color.B.ToString();

                    panelRenk.BackColor = color;
                }
            }
        }
        //-----------------------------------------------------------------------
        //---------Ölçeklendime-----------------------------------------
        //-----------------------------------------------------------------------

        private Bitmap ResizeNow()
        {
            int target_height = Convert.ToInt16(textBoxHeight.Text);
            int target_width = Convert.ToInt16(textBoxWidth.Text);

            Bitmap rsm = new Bitmap(pictureBox1.Image);
            Image target_image;
            target_image = (Image)rsm;
            Rectangle dest_rect = new Rectangle(0, 0, target_width, target_height);
            Bitmap destImage = new Bitmap(target_width, target_height);
            destImage.SetResolution(target_image.HorizontalResolution, target_image.VerticalResolution);
            using (var g = Graphics.FromImage(destImage))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                using (var wrapmode = new ImageAttributes())
                {
                    wrapmode.SetWrapMode(WrapMode.TileFlipXY);
                    g.DrawImage(target_image, dest_rect, 0, 0, target_image.Width, target_image.Height, GraphicsUnit.Pixel, wrapmode);
                }
            }
            return destImage;
        }

        private void buttonOlcekle_Click(object sender, EventArgs e)
        {
            

            if (textBoxWidth.Text == "" || textBoxHeight.Text == "")
            {

                MessageBox.Show("Lütfen Genişlik Ve Derinlik Giriniz");

            }
            else
            {
                Bitmap olcekle = ResizeNow();
                resimList.Add(olcekle);
                ListSayici++;
                pictureBox1.Image = olcekle;
            }

        }

        private void textBoxHisto_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

       

        private void textBoxHisto_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Resim dosyası  |*.BMP;*PNG;*JPEG;*.JPG;*.GIF|All files (*.*)|*.*";
            save.Title = "Kayıt";
            save.FileName = "shoped";
            DialogResult sonuc = save.ShowDialog();
            if(sonuc == DialogResult.OK){
                pictureBox1.Image.Save(save.FileName);
            }


        }

        private void buttonGriHisto_Click(object sender, EventArgs e)
        {

            pictureBoxHistogram.Refresh();

            
            if (textBoxHisto.Text == "")
            {
                MessageBox.Show("Histogram Deger Azaltma Katsayısını giriniz");
            }
            else
            {
                pictureBoxHistogram.BackColor = SystemColors.ControlDark;
                Bitmap rsm = new Bitmap(pictureBox1.Image);
                for (int i = 0; i < rsm.Size.Width; i++)
                    for (int j = 0; j < rsm.Size.Height; j++)
                    { 
                        int griDegeri = Convert.ToInt32((rsm.GetPixel(i,j).R + rsm.GetPixel(i,j).G   + rsm.GetPixel(i,j).B )/3);

                        gray[griDegeri]++;
                    }

                Graphics grafikCizgi;

            grafikCizgi = pictureBoxHistogram.CreateGraphics();
             //grafikCizgi = panelHisto.CreateGraphics();
            Pen black = new Pen(Color.Black, 2);
            Pen grayPen = new Pen(System.Drawing.Color.Gray,1);

            int yatayKaydir = 30, dikeyKaydir = 0;
            for (int i = 0; i < 256; i++)
            {
                if (i != 255)
                {

                    int oran = Convert.ToInt16(textBoxHisto.Text) ;
                    grafikCizgi.DrawLine(grayPen, i + yatayKaydir, 250 - dikeyKaydir, i + yatayKaydir, (250 - (gray[i + 1] / oran)) + dikeyKaydir);
                    if ((i % 15) == 0)
                    {
                        grafikCizgi.DrawLine(black, i + yatayKaydir + 15, 250, i + yatayKaydir + 15, 253);
                    }

                }
            }

                 
            grafikCizgi.DrawLine(black, yatayKaydir, 0, yatayKaydir, 325);//Dikey siyah Cizgi
            grafikCizgi.DrawLine(black, 0, 250 + dikeyKaydir, 370, 250 + dikeyKaydir);//Yatay siyah çizgi

            grafikCizgi.DrawLine(black, 365, 250 + dikeyKaydir, 360, 250 + dikeyKaydir - 5);//yataydaki doğru okları
            grafikCizgi.DrawLine(black, 365, 250 + dikeyKaydir, 360, 250 + dikeyKaydir + 5);//yataydaki doğru okları
             
            grafikCizgi.DrawLine(black, yatayKaydir, 0, yatayKaydir-5, 5);//dikeydeki doğru okları
            grafikCizgi.DrawLine(black, yatayKaydir, 0, yatayKaydir+5, 5) ;//dikeydeki doğru okları



               
          }

            
                for (int i = 0; i < 256; i++)
                {
                    
                    gray[i]=0;
                }
           
        }

    


    }
}
