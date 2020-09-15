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
    public partial class Form2 : Form
    {
        public Form2(Bitmap bm)
        {
            InitializeComponent();
            pictureBox1.Image = bm;
        }
    }
}
