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
            this.testbox = new System.Windows.Forms.TextBox();
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
            this.button1.Location = new System.Drawing.Point(830, 11);
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
            this.ort_button.Location = new System.Drawing.Point(822, 129);
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
            this.Ortxy.Location = new System.Drawing.Point(830, 176);
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
            this.Ortxz.Location = new System.Drawing.Point(830, 198);
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
            this.Ortyz.Location = new System.Drawing.Point(830, 220);
            this.Ortyz.Margin = new System.Windows.Forms.Padding(2);
            this.Ortyz.Name = "Ortyz";
            this.Ortyz.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortyz.Size = new System.Drawing.Size(36, 17);
            this.Ortyz.TabIndex = 2;
            this.Ortyz.Text = "yz";
            this.Ortyz.UseVisualStyleBackColor = true;
            this.Ortyz.CheckedChanged += new System.EventHandler(this.Ortyz_CheckedChanged);
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
            this.rotateAroundLine.Location = new System.Drawing.Point(846, 262);
            this.rotateAroundLine.Name = "rotateAroundLine";
            this.rotateAroundLine.Size = new System.Drawing.Size(115, 35);
            this.rotateAroundLine.TabIndex = 3;
            this.rotateAroundLine.Text = "Вращение вокруг прямой";
            this.rotateAroundLine.UseVisualStyleBackColor = true;
            this.rotateAroundLine.Click += new System.EventHandler(this.button3_Click);
            // 
            // OX
            // 
            this.OX.Location = new System.Drawing.Point(850, 303);
            this.OX.Name = "OX";
            this.OX.Size = new System.Drawing.Size(31, 23);
            this.OX.TabIndex = 4;
            this.OX.Text = "OX";
            this.OX.UseVisualStyleBackColor = true;
            this.OX.Click += new System.EventHandler(this.OX_Click);
            // 
            // OY
            // 
            this.OY.Location = new System.Drawing.Point(887, 303);
            this.OY.Name = "OY";
            this.OY.Size = new System.Drawing.Size(31, 23);
            this.OY.TabIndex = 5;
            this.OY.Text = "OY";
            this.OY.UseVisualStyleBackColor = true;
            this.OY.Click += new System.EventHandler(this.OY_Click);
            // 
            // OZ
            // 
            this.OZ.Location = new System.Drawing.Point(924, 303);
            this.OZ.Name = "OZ";
            this.OZ.Size = new System.Drawing.Size(31, 23);
            this.OZ.TabIndex = 6;
            this.OZ.Text = "OZ";
            this.OZ.UseVisualStyleBackColor = true;
            this.OZ.Click += new System.EventHandler(this.OZ_Click);
            // 
            // Custom
            // 
            this.Custom.Location = new System.Drawing.Point(846, 332);
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
            this.lab1.Location = new System.Drawing.Point(869, 372);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(58, 17);
            this.lab1.TabIndex = 8;
            this.lab1.Text = "1 точка";
            // 
            // lab9
            // 
            this.lab9.AutoSize = true;
            this.lab9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lab9.Location = new System.Drawing.Point(869, 426);
            this.lab9.Name = "lab9";
            this.lab9.Size = new System.Drawing.Size(58, 17);
            this.lab9.TabIndex = 9;
            this.lab9.Text = "2 точка";
            // 
            // lab2
            // 
            this.lab2.AutoSize = true;
            this.lab2.Location = new System.Drawing.Point(806, 398);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(14, 13);
            this.lab2.TabIndex = 12;
            this.lab2.Text = "X";
            // 
            // s_x
            // 
            this.s_x.Location = new System.Drawing.Point(822, 395);
            this.s_x.Name = "s_x";
            this.s_x.Size = new System.Drawing.Size(34, 20);
            this.s_x.TabIndex = 13;
            // 
            // s_y
            // 
            this.s_y.Location = new System.Drawing.Point(880, 395);
            this.s_y.Name = "s_y";
            this.s_y.Size = new System.Drawing.Size(37, 20);
            this.s_y.TabIndex = 15;
            // 
            // lab5
            // 
            this.lab5.AutoSize = true;
            this.lab5.Location = new System.Drawing.Point(862, 398);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(14, 13);
            this.lab5.TabIndex = 14;
            this.lab5.Text = "Y";
            // 
            // s_z
            // 
            this.s_z.Location = new System.Drawing.Point(940, 395);
            this.s_z.Name = "s_z";
            this.s_z.Size = new System.Drawing.Size(35, 20);
            this.s_z.TabIndex = 17;
            // 
            // lab6
            // 
            this.lab6.AutoSize = true;
            this.lab6.Location = new System.Drawing.Point(922, 398);
            this.lab6.Name = "lab6";
            this.lab6.Size = new System.Drawing.Size(14, 13);
            this.lab6.TabIndex = 16;
            this.lab6.Text = "Z";
            // 
            // e_z
            // 
            this.e_z.Location = new System.Drawing.Point(940, 446);
            this.e_z.Name = "e_z";
            this.e_z.Size = new System.Drawing.Size(35, 20);
            this.e_z.TabIndex = 23;
            // 
            // lab3
            // 
            this.lab3.AutoSize = true;
            this.lab3.Location = new System.Drawing.Point(922, 449);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(14, 13);
            this.lab3.TabIndex = 22;
            this.lab3.Text = "Z";
            // 
            // e_y
            // 
            this.e_y.Location = new System.Drawing.Point(880, 446);
            this.e_y.Name = "e_y";
            this.e_y.Size = new System.Drawing.Size(36, 20);
            this.e_y.TabIndex = 21;
            // 
            // lab7
            // 
            this.lab7.AutoSize = true;
            this.lab7.Location = new System.Drawing.Point(862, 449);
            this.lab7.Name = "lab7";
            this.lab7.Size = new System.Drawing.Size(14, 13);
            this.lab7.TabIndex = 20;
            this.lab7.Text = "Y";
            // 
            // e_x
            // 
            this.e_x.Location = new System.Drawing.Point(822, 446);
            this.e_x.Name = "e_x";
            this.e_x.Size = new System.Drawing.Size(34, 20);
            this.e_x.TabIndex = 19;
            // 
            // lab8
            // 
            this.lab8.AutoSize = true;
            this.lab8.Location = new System.Drawing.Point(806, 449);
            this.lab8.Name = "lab8";
            this.lab8.Size = new System.Drawing.Size(14, 13);
            this.lab8.TabIndex = 18;
            this.lab8.Text = "X";
            // 
            // testbox
            // 
            this.testbox.Location = new System.Drawing.Point(830, 514);
            this.testbox.Name = "testbox";
            this.testbox.Size = new System.Drawing.Size(100, 20);
            this.testbox.TabIndex = 24;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 590);
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
            this.Controls.Add(this.Ortyz);
            this.Controls.Add(this.Ortxz);
            this.Controls.Add(this.Ortxy);
            this.Controls.Add(this.ort_button);
            this.Controls.Add(this.button2);
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
        private System.Windows.Forms.TextBox testbox;
    }
}

