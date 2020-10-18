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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(893, 550);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(909, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(62, 28);
            this.button1.TabIndex = 1;
            this.button1.Text = "Куб";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ort_button
            // 
            this.ort_button.Location = new System.Drawing.Point(899, 157);
            this.ort_button.Name = "ort_button";
            this.ort_button.Size = new System.Drawing.Size(145, 40);
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
            this.Ortxy.Location = new System.Drawing.Point(909, 215);
            this.Ortxy.Name = "Ortxy";
            this.Ortxy.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortxy.Size = new System.Drawing.Size(43, 21);
            this.Ortxy.TabIndex = 2;
            this.Ortxy.Text = "xy";
            this.Ortxy.UseVisualStyleBackColor = true;
            this.Ortxy.CheckedChanged += new System.EventHandler(this.Ortxy_CheckedChanged);
            // 
            // Ortxz
            // 
            this.Ortxz.AutoSize = true;
            this.Ortxz.Location = new System.Drawing.Point(909, 242);
            this.Ortxz.Name = "Ortxz";
            this.Ortxz.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortxz.Size = new System.Drawing.Size(43, 21);
            this.Ortxz.TabIndex = 2;
            this.Ortxz.Text = "xz";
            this.Ortxz.UseVisualStyleBackColor = true;
            this.Ortxz.CheckedChanged += new System.EventHandler(this.Ortxz_CheckedChanged);
            // 
            // Ortyz
            // 
            this.Ortyz.AutoSize = true;
            this.Ortyz.Location = new System.Drawing.Point(909, 269);
            this.Ortyz.Name = "Ortyz";
            this.Ortyz.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortyz.Size = new System.Drawing.Size(44, 21);
            this.Ortyz.TabIndex = 2;
            this.Ortyz.Text = "yz";
            this.Ortyz.UseVisualStyleBackColor = true;
            this.Ortyz.CheckedChanged += new System.EventHandler(this.Ortyz_CheckedChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(930, 515);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(91, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Очистить";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 550);
            this.Controls.Add(this.Ortyz);
            this.Controls.Add(this.Ortxz);
            this.Controls.Add(this.Ortxy);
            this.Controls.Add(this.ort_button);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
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
    }
}

