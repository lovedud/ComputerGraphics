using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2
{
    public partial class Form1 : Form
    {

        Bitmap bitmaporigin;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = "";
            OpenFileDialog myfile = new OpenFileDialog();
            myfile.Title = "First step Site";
            myfile.Filter = "png files (*.png)|*.png|All files (*.*)|*.*";
            if (myfile.ShowDialog() == DialogResult.OK)
            {
                path = myfile.FileName;
                pictureBox1.Image = Image.FromFile(path);
            }
            bitmaporigin = new Bitmap(pictureBox1.Image);
            button1.Enabled = true;
            button3.Enabled = true;
            trackBar1.Enabled = true;
            trackBar2.Enabled = true;
            trackBar3.Enabled = true;
        }

        private int Max3(int a, int b, int c)
        {
            if (a > b)
            {
                if (a > c)
                    return a;
                else
                    return c;
            }
            else
            {
                if (b > c)
                    return b;
                else
                    return c;
            }
        }
        private int Min3(int a, int b, int c)
        {
            if (a < b)
            {
                if (a < c)
                    return a;
                else
                    return c;
            }
            else
            {
                if (b < c)
                    return b;
                else
                    return c;
            }
        }

        private double GetHue(ref int r, ref int g, ref int b, ref int ormax, ref int ormin)
        {
            double red = r / 255.0;
            double green = g / 255.0;
            double blue = b / 255.0;
            double max = ormax / 255.0;
            double min = ormin / 255.0;
            if (ormax == ormin)
            {
                return 0;
            }
            else if (ormax == r)
            {
                if (green >= blue)
                    return (60 * (green - blue)/(max - min));
                else
                    return (60 * (green - blue) / (max - min) + 360);
            }
            else if (ormax == g)
            {
                return (60 * (blue - red) / (max - min) + 120);
            }
            else
            {
                return (60 * (red - green) / (max - min) + 240);
            }
        }

        private double GetSaturation(ref int ormax, ref int ormin)
        {
            double max = ormax / 255.0;
            double min = ormin / 255.0;
            if (ormax == 0)
                return 0;
            else
                return ((1 - min / max));
        }

        private Tuple<double, double, double> RGB_To_HSV(Color color)
        {
            int red = color.R;
            int green = color.G;
            int blue = color.B;
            int max = Max3(red, green, blue);
            int min = Min3(red, green, blue);

            return new Tuple<double, double, double>(GetHue(ref red, ref green, ref blue, ref max, ref min), 
                                                     GetSaturation(ref max, ref min), 
                                                     max / 255.0);
        }

        private Color HSV_To_RGB(Tuple<double, double, double> hsv)
        {
            int H = (int)Math.Floor(hsv.Item1 / 60.0) % 6;
            double f = (hsv.Item1 / 60.0) - (int)Math.Floor(hsv.Item1 / 60.0);
            double p = hsv.Item3 * (1 - hsv.Item2);
            double q = hsv.Item3 * (1 - f*hsv.Item2);
            double t = hsv.Item3 * (1 - (1 - f)*hsv.Item2);
            switch (H)
            {
                case 0:
                    return Color.FromArgb((int)(hsv.Item3 * 255), (int)(t * 255), (int)(p * 255));
                case 1:
                    return Color.FromArgb((int)(q * 255), (int)(hsv.Item3 * 255), (int)(p * 255));
                case 2:
                    return Color.FromArgb((int)(p * 255), (int)(hsv.Item3 * 255), (int)(t * 255));
                case 3:
                    return Color.FromArgb((int)(p * 255), (int)(q * 255), (int)(hsv.Item3 * 255));
                case 4:
                    return Color.FromArgb((int)(t * 255), (int)(p * 255), (int)(hsv.Item3 * 255));
                case 5:
                    return Color.FromArgb((int)(hsv.Item3 * 255), (int)(p * 255), (int)(q * 255));
                default:
                    return Color.Black;
            }
        }

        private double CheckBordersSatValue(double x)
        {
            if (x > 1)
                return 1;
            else if (x < 0)
                return 0;
            else
                return x;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(bitmaporigin);
            for (int x = 0; x < bitmap.Width; ++x)
            {
                //int a = 0;
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    Color pixelColor = bitmap.GetPixel(x, y);
                    var pix = RGB_To_HSV(pixelColor);
                    var newpix = new Tuple<double, double, double>(pix.Item1 + trackBar1.Value, 
                                                                   CheckBordersSatValue(pix.Item2 + (trackBar2.Value / 100.0)), 
                                                                   CheckBordersSatValue(pix.Item3 + (trackBar3.Value / 100.0)));
                    Color newcolor = HSV_To_RGB(newpix);
                    bitmap.SetPixel(x, y, newcolor);
                }
            }
            pictureBox1.Image = bitmap;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "JPeg Image|*.jpg|PNG Image|*.png";
            saveFileDialog1.Title = "Save an Image File";
            saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName != "")
            {
                System.IO.FileStream fs =
                    (System.IO.FileStream)saveFileDialog1.OpenFile();
                switch (saveFileDialog1.FilterIndex)
                {
                    case 1:
                        pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;

                    case 2:
                        pictureBox1.Image.Save(fs,
                          System.Drawing.Imaging.ImageFormat.Png);
                        break;
                }
                fs.Close();
            }
        }
    }
}
