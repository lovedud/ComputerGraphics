namespace AffinTransform3D
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.octahedron = new System.Windows.Forms.RadioButton();
            this.hexahedron = new System.Windows.Forms.RadioButton();
            this.tetrahedron = new System.Windows.Forms.RadioButton();
            this.rotate_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.z_rotate = new System.Windows.Forms.NumericUpDown();
            this.y_rotate = new System.Windows.Forms.NumericUpDown();
            this.x_rotate = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.files_figures_label = new System.Windows.Forms.Label();
            this.load_texture = new System.Windows.Forms.Button();
            this.openFileDialog_texture = new System.Windows.Forms.OpenFileDialog();
            this.delete_texture = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.z_rotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_rotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_rotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // octahedron
            // 
            this.octahedron.AutoSize = true;
            this.octahedron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.octahedron.Location = new System.Drawing.Point(1183, 173);
            this.octahedron.Margin = new System.Windows.Forms.Padding(4);
            this.octahedron.Name = "octahedron";
            this.octahedron.Size = new System.Drawing.Size(85, 21);
            this.octahedron.TabIndex = 2;
            this.octahedron.Text = "Октаэдр";
            this.octahedron.UseVisualStyleBackColor = true;
            this.octahedron.CheckedChanged += new System.EventHandler(this.shape_CheckedChanged);
            // 
            // hexahedron
            // 
            this.hexahedron.AutoSize = true;
            this.hexahedron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hexahedron.Location = new System.Drawing.Point(1183, 132);
            this.hexahedron.Margin = new System.Windows.Forms.Padding(4);
            this.hexahedron.Name = "hexahedron";
            this.hexahedron.Size = new System.Drawing.Size(90, 21);
            this.hexahedron.TabIndex = 1;
            this.hexahedron.Text = "Гексаэдр";
            this.hexahedron.UseVisualStyleBackColor = true;
            this.hexahedron.CheckedChanged += new System.EventHandler(this.shape_CheckedChanged);
            // 
            // tetrahedron
            // 
            this.tetrahedron.AutoSize = true;
            this.tetrahedron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tetrahedron.Location = new System.Drawing.Point(1183, 86);
            this.tetrahedron.Margin = new System.Windows.Forms.Padding(4);
            this.tetrahedron.Name = "tetrahedron";
            this.tetrahedron.Size = new System.Drawing.Size(92, 21);
            this.tetrahedron.TabIndex = 0;
            this.tetrahedron.Text = "Тетраэдр";
            this.tetrahedron.UseVisualStyleBackColor = true;
            this.tetrahedron.CheckedChanged += new System.EventHandler(this.shape_CheckedChanged);
            // 
            // rotate_button
            // 
            this.rotate_button.Location = new System.Drawing.Point(1190, 411);
            this.rotate_button.Margin = new System.Windows.Forms.Padding(4);
            this.rotate_button.Name = "rotate_button";
            this.rotate_button.Size = new System.Drawing.Size(120, 33);
            this.rotate_button.TabIndex = 10;
            this.rotate_button.Text = "ОК";
            this.rotate_button.UseVisualStyleBackColor = true;
            this.rotate_button.Click += new System.EventHandler(this.rotate_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(1180, 373);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "0Z:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(1180, 339);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "0Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(1180, 308);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "0X:";
            // 
            // z_rotate
            // 
            this.z_rotate.Location = new System.Drawing.Point(1217, 368);
            this.z_rotate.Margin = new System.Windows.Forms.Padding(4);
            this.z_rotate.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.z_rotate.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.z_rotate.Name = "z_rotate";
            this.z_rotate.Size = new System.Drawing.Size(85, 22);
            this.z_rotate.TabIndex = 6;
            // 
            // y_rotate
            // 
            this.y_rotate.Location = new System.Drawing.Point(1217, 338);
            this.y_rotate.Margin = new System.Windows.Forms.Padding(4);
            this.y_rotate.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.y_rotate.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.y_rotate.Name = "y_rotate";
            this.y_rotate.Size = new System.Drawing.Size(85, 22);
            this.y_rotate.TabIndex = 5;
            this.y_rotate.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // x_rotate
            // 
            this.x_rotate.Location = new System.Drawing.Point(1217, 308);
            this.x_rotate.Margin = new System.Windows.Forms.Padding(4);
            this.x_rotate.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.x_rotate.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.x_rotate.Name = "x_rotate";
            this.x_rotate.Size = new System.Drawing.Size(85, 22);
            this.x_rotate.TabIndex = 4;
            this.x_rotate.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1180, 275);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 17);
            this.label13.TabIndex = 11;
            this.label13.Text = "Поворот:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(172, 73);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(967, 692);
            this.pictureBox.TabIndex = 16;
            this.pictureBox.TabStop = false;
            // 
            // files_figures_label
            // 
            this.files_figures_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.files_figures_label.Location = new System.Drawing.Point(1, 14);
            this.files_figures_label.Name = "files_figures_label";
            this.files_figures_label.Size = new System.Drawing.Size(174, 62);
            this.files_figures_label.TabIndex = 88;
            this.files_figures_label.Text = "Текстурирование";
            this.files_figures_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // load_texture
            // 
            this.load_texture.Location = new System.Drawing.Point(29, 86);
            this.load_texture.Name = "load_texture";
            this.load_texture.Size = new System.Drawing.Size(111, 43);
            this.load_texture.TabIndex = 90;
            this.load_texture.Text = "Загрузить текстуру";
            this.load_texture.UseVisualStyleBackColor = true;
            this.load_texture.Click += new System.EventHandler(this.load_texture_Click);
            // 
            // delete_texture
            // 
            this.delete_texture.Location = new System.Drawing.Point(29, 150);
            this.delete_texture.Name = "delete_texture";
            this.delete_texture.Size = new System.Drawing.Size(111, 44);
            this.delete_texture.TabIndex = 91;
            this.delete_texture.Text = "Удалить текстуру";
            this.delete_texture.UseVisualStyleBackColor = true;
            this.delete_texture.Click += new System.EventHandler(this.delete_texture_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 819);
            this.Controls.Add(this.delete_texture);
            this.Controls.Add(this.load_texture);
            this.Controls.Add(this.files_figures_label);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.rotate_button);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.z_rotate);
            this.Controls.Add(this.y_rotate);
            this.Controls.Add(this.octahedron);
            this.Controls.Add(this.x_rotate);
            this.Controls.Add(this.hexahedron);
            this.Controls.Add(this.tetrahedron);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "3D векторная графика";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.z_rotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_rotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_rotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton tetrahedron;
        private System.Windows.Forms.RadioButton hexahedron;
        private System.Windows.Forms.RadioButton octahedron;
        private System.Windows.Forms.Button rotate_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown z_rotate;
        private System.Windows.Forms.NumericUpDown y_rotate;
        private System.Windows.Forms.NumericUpDown x_rotate;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label files_figures_label;
        private System.Windows.Forms.Button load_texture;
        private System.Windows.Forms.OpenFileDialog openFileDialog_texture;
        private System.Windows.Forms.Button delete_texture;
    }
}

