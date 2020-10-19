namespace Affin3D
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ort_button = new System.Windows.Forms.Button();
            this.Ortxy = new System.Windows.Forms.CheckBox();
            this.Ortxz = new System.Windows.Forms.CheckBox();
            this.Ortyz = new System.Windows.Forms.CheckBox();
            this.clear_button = new System.Windows.Forms.Button();
            this.iso_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.rotateAroundLine = new System.Windows.Forms.Button();
            this.OX = new System.Windows.Forms.Button();
            this.OY = new System.Windows.Forms.Button();
            this.OZ = new System.Windows.Forms.Button();
            this.Custom = new System.Windows.Forms.Button();
            this.lab1 = new System.Windows.Forms.Label();
            this.lab9 = new System.Windows.Forms.Label();
            this.lab2 = new System.Windows.Forms.Label();
            this.s_x = new System.Windows.Forms.TextBox();
            this.s_y = new System.Windows.Forms.TextBox();
            this.lab5 = new System.Windows.Forms.Label();
            this.s_z = new System.Windows.Forms.TextBox();
            this.lab6 = new System.Windows.Forms.Label();
            this.e_z = new System.Windows.Forms.TextBox();
            this.lab3 = new System.Windows.Forms.Label();
            this.e_y = new System.Windows.Forms.TextBox();
            this.lab7 = new System.Windows.Forms.Label();
            this.e_x = new System.Windows.Forms.TextBox();
            this.lab8 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.testbox = new System.Windows.Forms.TextBox();
            this.Tetrahedron = new System.Windows.Forms.Button();
            this.Octahedron = new System.Windows.Forms.Button();
            this.Icosahedron = new System.Windows.Forms.Button();
            this.dodecahedron = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(804, 579);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(826, 39);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(46, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Куб";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ort_button
            // 
            this.ort_button.Location = new System.Drawing.Point(822, 162);
            this.ort_button.Margin = new System.Windows.Forms.Padding(2);
            this.ort_button.Name = "ort_button";
            this.ort_button.Size = new System.Drawing.Size(109, 32);
            this.ort_button.TabIndex = 1;
            this.ort_button.Text = "Ортографическая";
            this.ort_button.UseVisualStyleBackColor = true;
            this.ort_button.Click += new System.EventHandler(this.button2_Click);
            // 
            // Ortxy
            // 
            this.Ortxy.AutoSize = true;
            this.Ortxy.Checked = true;
            this.Ortxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ortxy.Location = new System.Drawing.Point(830, 209);
            this.Ortxy.Margin = new System.Windows.Forms.Padding(2);
            this.Ortxy.Name = "Ortxy";
            this.Ortxy.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortxy.Size = new System.Drawing.Size(36, 17);
            this.Ortxy.TabIndex = 2;
            this.Ortxy.Text = "xy";
            this.Ortxy.UseVisualStyleBackColor = true;
            this.Ortxy.CheckedChanged += new System.EventHandler(this.Ortxy_CheckedChanged);
            // 
            // Ortxz
            // 
            this.Ortxz.AutoSize = true;
            this.Ortxz.Location = new System.Drawing.Point(830, 231);
            this.Ortxz.Margin = new System.Windows.Forms.Padding(2);
            this.Ortxz.Name = "Ortxz";
            this.Ortxz.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortxz.Size = new System.Drawing.Size(36, 17);
            this.Ortxz.TabIndex = 2;
            this.Ortxz.Text = "xz";
            this.Ortxz.UseVisualStyleBackColor = true;
            this.Ortxz.CheckedChanged += new System.EventHandler(this.Ortxz_CheckedChanged);
            // 
            // Ortyz
            // 
            this.Ortyz.AutoSize = true;
            this.Ortyz.Location = new System.Drawing.Point(830, 253);
            this.Ortyz.Margin = new System.Windows.Forms.Padding(2);
            this.Ortyz.Name = "Ortyz";
            this.Ortyz.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortyz.Size = new System.Drawing.Size(36, 17);
            this.Ortyz.TabIndex = 2;
            this.Ortyz.Text = "yz";
            this.Ortyz.UseVisualStyleBackColor = true;
            this.Ortyz.CheckedChanged += new System.EventHandler(this.Ortyz_CheckedChanged);
            // 
            // clear_button
            // 
            this.clear_button.Location = new System.Drawing.Point(850, 556);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(91, 23);
            this.clear_button.TabIndex = 1;
            this.clear_button.Text = "Очистить";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // iso_button
            // 
            this.iso_button.Location = new System.Drawing.Point(820, 117);
            this.iso_button.Name = "iso_button";
            this.iso_button.Size = new System.Drawing.Size(145, 40);
            this.iso_button.TabIndex = 1;
            this.iso_button.Text = "Изометрическая";
            this.iso_button.UseVisualStyleBackColor = true;
            this.iso_button.Click += new System.EventHandler(this.iso_button_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(865, 560);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(68, 19);
            this.button2.TabIndex = 1;
            this.button2.Text = "Очистить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // rotateAroundLine
            // 
            this.rotateAroundLine.Location = new System.Drawing.Point(850, 339);
            this.rotateAroundLine.Name = "rotateAroundLine";
            this.rotateAroundLine.Size = new System.Drawing.Size(115, 35);
            this.rotateAroundLine.TabIndex = 3;
            this.rotateAroundLine.Text = "Вращение вокруг прямой";
            this.rotateAroundLine.UseVisualStyleBackColor = true;
            this.rotateAroundLine.Click += new System.EventHandler(this.Rota_Click);
            // 
            // OX
            // 
            this.OX.Enabled = false;
            this.OX.Location = new System.Drawing.Point(854, 380);
            this.OX.Name = "OX";
            this.OX.Size = new System.Drawing.Size(31, 23);
            this.OX.TabIndex = 4;
            this.OX.Text = "OX";
            this.OX.UseVisualStyleBackColor = true;
            this.OX.Click += new System.EventHandler(this.OX_Click);
            // 
            // OY
            // 
            this.OY.Enabled = false;
            this.OY.Location = new System.Drawing.Point(891, 380);
            this.OY.Name = "OY";
            this.OY.Size = new System.Drawing.Size(31, 23);
            this.OY.TabIndex = 5;
            this.OY.Text = "OY";
            this.OY.UseVisualStyleBackColor = true;
            this.OY.Click += new System.EventHandler(this.OY_Click);
            // 
            // OZ
            // 
            this.OZ.Enabled = false;
            this.OZ.Location = new System.Drawing.Point(928, 380);
            this.OZ.Name = "OZ";
            this.OZ.Size = new System.Drawing.Size(31, 23);
            this.OZ.TabIndex = 6;
            this.OZ.Text = "OZ";
            this.OZ.UseVisualStyleBackColor = true;
            this.OZ.Click += new System.EventHandler(this.OZ_Click);
            // 
            // Custom
            // 
            this.Custom.Enabled = false;
            this.Custom.Location = new System.Drawing.Point(850, 409);
            this.Custom.Name = "Custom";
            this.Custom.Size = new System.Drawing.Size(105, 37);
            this.Custom.TabIndex = 7;
            this.Custom.Text = "Произвольная прямая";
            this.Custom.UseVisualStyleBackColor = true;
            this.Custom.Click += new System.EventHandler(this.Custom_Click);
            // 
            // lab1
            // 
            this.lab1.AutoSize = true;
            this.lab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lab1.Location = new System.Drawing.Point(873, 449);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(58, 17);
            this.lab1.TabIndex = 8;
            this.lab1.Text = "1 точка";
            // 
            // lab9
            // 
            this.lab9.AutoSize = true;
            this.lab9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lab9.Location = new System.Drawing.Point(873, 503);
            this.lab9.Name = "lab9";
            this.lab9.Size = new System.Drawing.Size(58, 17);
            this.lab9.TabIndex = 9;
            this.lab9.Text = "2 точка";
            // 
            // lab2
            // 
            this.lab2.AutoSize = true;
            this.lab2.Location = new System.Drawing.Point(810, 475);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(14, 13);
            this.lab2.TabIndex = 12;
            this.lab2.Text = "X";
            // 
            // s_x
            // 
            this.s_x.Location = new System.Drawing.Point(826, 472);
            this.s_x.Name = "s_x";
            this.s_x.Size = new System.Drawing.Size(34, 20);
            this.s_x.TabIndex = 13;
            // 
            // s_y
            // 
            this.s_y.Location = new System.Drawing.Point(884, 472);
            this.s_y.Name = "s_y";
            this.s_y.Size = new System.Drawing.Size(37, 20);
            this.s_y.TabIndex = 15;
            // 
            // lab5
            // 
            this.lab5.AutoSize = true;
            this.lab5.Location = new System.Drawing.Point(866, 475);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(14, 13);
            this.lab5.TabIndex = 14;
            this.lab5.Text = "Y";
            // 
            // s_z
            // 
            this.s_z.Location = new System.Drawing.Point(944, 472);
            this.s_z.Name = "s_z";
            this.s_z.Size = new System.Drawing.Size(35, 20);
            this.s_z.TabIndex = 17;
            // 
            // lab6
            // 
            this.lab6.AutoSize = true;
            this.lab6.Location = new System.Drawing.Point(926, 475);
            this.lab6.Name = "lab6";
            this.lab6.Size = new System.Drawing.Size(14, 13);
            this.lab6.TabIndex = 16;
            this.lab6.Text = "Z";
            // 
            // e_z
            // 
            this.e_z.Location = new System.Drawing.Point(944, 523);
            this.e_z.Name = "e_z";
            this.e_z.Size = new System.Drawing.Size(35, 20);
            this.e_z.TabIndex = 23;
            // 
            // lab3
            // 
            this.lab3.AutoSize = true;
            this.lab3.Location = new System.Drawing.Point(926, 526);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(14, 13);
            this.lab3.TabIndex = 22;
            this.lab3.Text = "Z";
            // 
            // e_y
            // 
            this.e_y.Location = new System.Drawing.Point(884, 523);
            this.e_y.Name = "e_y";
            this.e_y.Size = new System.Drawing.Size(36, 20);
            this.e_y.TabIndex = 21;
            // 
            // lab7
            // 
            this.lab7.AutoSize = true;
            this.lab7.Location = new System.Drawing.Point(866, 526);
            this.lab7.Name = "lab7";
            this.lab7.Size = new System.Drawing.Size(14, 13);
            this.lab7.TabIndex = 20;
            this.lab7.Text = "Y";
            // 
            // e_x
            // 
            this.e_x.Location = new System.Drawing.Point(826, 523);
            this.e_x.Name = "e_x";
            this.e_x.Size = new System.Drawing.Size(34, 20);
            this.e_x.TabIndex = 19;
            // 
            // lab8
            // 
            this.lab8.AutoSize = true;
            this.lab8.Location = new System.Drawing.Point(810, 526);
            this.lab8.Name = "lab8";
            this.lab8.Size = new System.Drawing.Size(14, 13);
            this.lab8.TabIndex = 18;
            this.lab8.Text = "X";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(853, 310);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = "Смещение";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // testbox
            // 
            this.testbox.Location = new System.Drawing.Point(1034, 449);
            this.testbox.Name = "testbox";
            this.testbox.Size = new System.Drawing.Size(100, 20);
            this.testbox.TabIndex = 24;
            // 
            // Tetrahedron
            // 
            this.Tetrahedron.Location = new System.Drawing.Point(826, 13);
            this.Tetrahedron.Name = "Tetrahedron";
            this.Tetrahedron.Size = new System.Drawing.Size(75, 23);
            this.Tetrahedron.TabIndex = 25;
            this.Tetrahedron.Text = "Тетраэдр";
            this.Tetrahedron.UseVisualStyleBackColor = true;
            this.Tetrahedron.Click += new System.EventHandler(this.Tetrahedron_Click);
            // 
            // Octahedron
            // 
            this.Octahedron.Location = new System.Drawing.Point(826, 67);
            this.Octahedron.Name = "Octahedron";
            this.Octahedron.Size = new System.Drawing.Size(75, 23);
            this.Octahedron.TabIndex = 26;
            this.Octahedron.Text = "Октаэдр";
            this.Octahedron.UseVisualStyleBackColor = true;
            this.Octahedron.Click += new System.EventHandler(this.Octahedron_Click);
            // 
            // Icosahedron
            // 
            this.Icosahedron.Location = new System.Drawing.Point(929, 12);
            this.Icosahedron.Name = "Icosahedron";
            this.Icosahedron.Size = new System.Drawing.Size(75, 23);
            this.Icosahedron.TabIndex = 27;
            this.Icosahedron.Text = "Икосаэдр";
            this.Icosahedron.UseVisualStyleBackColor = true;
            this.Icosahedron.Click += new System.EventHandler(this.Icosahedron_Click);
            // 
            // dodecahedron
            // 
            this.dodecahedron.Location = new System.Drawing.Point(929, 38);
            this.dodecahedron.Name = "dodecahedron";
            this.dodecahedron.Size = new System.Drawing.Size(75, 23);
            this.dodecahedron.TabIndex = 28;
            this.dodecahedron.Text = "Додекаэдр";
            this.dodecahedron.UseVisualStyleBackColor = true;
            this.dodecahedron.Click += new System.EventHandler(this.dodecahedron_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 640);
            this.Controls.Add(this.dodecahedron);
            this.Controls.Add(this.Icosahedron);
            this.Controls.Add(this.Octahedron);
            this.Controls.Add(this.Tetrahedron);
            this.Controls.Add(this.testbox);
            this.Controls.Add(this.e_z);
            this.Controls.Add(this.lab3);
            this.Controls.Add(this.e_y);
            this.Controls.Add(this.lab7);
            this.Controls.Add(this.e_x);
            this.Controls.Add(this.lab8);
            this.Controls.Add(this.s_z);
            this.Controls.Add(this.lab6);
            this.Controls.Add(this.s_y);
            this.Controls.Add(this.lab5);
            this.Controls.Add(this.s_x);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.lab9);
            this.Controls.Add(this.lab1);
            this.Controls.Add(this.Custom);
            this.Controls.Add(this.OZ);
            this.Controls.Add(this.OY);
            this.Controls.Add(this.OX);
            this.Controls.Add(this.rotateAroundLine);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Ortyz);
            this.Controls.Add(this.Ortxz);
            this.Controls.Add(this.Ortxy);
            this.Controls.Add(this.iso_button);
            this.Controls.Add(this.ort_button);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button ort_button;
        private System.Windows.Forms.CheckBox Ortxy;
        private System.Windows.Forms.CheckBox Ortxz;
        private System.Windows.Forms.CheckBox Ortyz;
        private System.Windows.Forms.Button clear_button;
        private System.Windows.Forms.Button iso_button;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button rotateAroundLine;
        private System.Windows.Forms.Button OX;
        private System.Windows.Forms.Button OY;
        private System.Windows.Forms.Button OZ;
        private System.Windows.Forms.Button Custom;
        private System.Windows.Forms.Label lab1;
        private System.Windows.Forms.Label lab9;
        private System.Windows.Forms.Label lab2;
        private System.Windows.Forms.TextBox s_x;
        private System.Windows.Forms.TextBox s_y;
        private System.Windows.Forms.Label lab5;
        private System.Windows.Forms.TextBox s_z;
        private System.Windows.Forms.Label lab6;
        private System.Windows.Forms.TextBox e_z;
        private System.Windows.Forms.Label lab3;
        private System.Windows.Forms.TextBox e_y;
        private System.Windows.Forms.Label lab7;
        private System.Windows.Forms.TextBox e_x;
        private System.Windows.Forms.Label lab8;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox testbox;
        private System.Windows.Forms.Button Tetrahedron;
        private System.Windows.Forms.Button Octahedron;
        private System.Windows.Forms.Button Icosahedron;
        private System.Windows.Forms.Button dodecahedron;
    }
}

