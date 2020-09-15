using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RGB_to_gray
{
    public partial class Form3 : Form
    {
        Dictionary<int, int> samples;
        int group_size;
        Bitmap bm;
        public Form3(Bitmap bitmap, int gr_size)
        {
            bm = bitmap;
            InitializeComponent();
            group_size = gr_size;
            PrepareDictinary(group_size);
            MakeChart();
        }
        private void PrepareDictinary(int group_size)
        {
            samples = new Dictionary<int, int>();
            for(int i = 0; i < 256; i+= group_size)
            {
                samples[i] = 0;
            }
        }

        private int GetGroupIndex(int number)
        {
           return samples.Keys.Where((x) => number >= x).Max(); 
        }

        private void PrepareSeries()
        {
            for(int i = 0; i < bm.Width; i++)
                for(int j = 0; j < bm.Height; j++)
                {
                    Color pixel = bm.GetPixel(i, j);
                    samples[GetGroupIndex(pixel.G)] += 1;
                }
        }
        private string IntervalToString(int start)
        {
            return start.ToString() + "-" + (Math.Min(start + group_size, 255)).ToString();
        }

        private void MakeChart()
        {
            PrepareSeries();
            foreach(var kv in samples)
            {
                chart1.Series["Intensity"].Points.AddXY(kv.Key.ToString(), kv.Value);
            }
        }

        

    }

    
}
