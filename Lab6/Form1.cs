﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Lab4.AffinStuff;

namespace Affin3D
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            bm = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bm;
            g = Graphics.FromImage(bm);
            //cur_mode = 
        }
       
        Mode cur_mode;
        Polyhedron cur_polyhedron;
        Bitmap bm;
        Graphics g;

        private void OrtButtonAvailability()
        {
            if (!Ortxy.Checked && !Ortxz.Checked && !Ortyz.Checked)
            {
                ort_button.Enabled = false;
            }
        }

        private void Ortxy_CheckedChanged(object sender, EventArgs e)
        {
            if (Ortxy.Checked)
            {
                Ortxz.Checked = false;
                Ortyz.Checked = false;
                cur_mode = Mode.XY;
                ort_button.Enabled = true;
            }
            else OrtButtonAvailability();

        }

        private void Ortxz_CheckedChanged(object sender, EventArgs e)
        {
            if (Ortxz.Checked)
            {
                Ortxy.Checked = false;
                Ortyz.Checked = false;
                cur_mode = Mode.XZ;
                ort_button.Enabled = true;
            } else OrtButtonAvailability();
        }

        private void Ortyz_CheckedChanged(object sender, EventArgs e)
        {
            if (Ortyz.Checked)
            {
                Ortxz.Checked = false;
                Ortxy.Checked = false;
                cur_mode = Mode.YZ;
                ort_button.Enabled = true;
            } else OrtButtonAvailability();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (cur_polyhedron is null)
                return;
            var edges = ToOrtographics(cur_polyhedron, cur_mode);
            foreach(var edge in edges)
            {
                DrawEdge(ref g, ref bm, edge);
            }
            pictureBox1.Image = bm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cur_polyhedron = CreateCube(new Point3D(200, 200, 200), 50);
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Image = bm;
            g = Graphics.FromImage(bm);
        }
    }
}
