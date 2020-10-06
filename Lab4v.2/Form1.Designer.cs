namespace Lab4
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
            this.AddPointButton = new System.Windows.Forms.Button();
            this.AddEdgeButton = new System.Windows.Forms.Button();
            this.AddPolygonButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.RotateEdgeButton = new System.Windows.Forms.Button();
            this.InfoTextBox = new System.Windows.Forms.TextBox();
            this.PointPositionButton = new System.Windows.Forms.CheckBox();
            this.PointInsideButton = new System.Windows.Forms.CheckBox();
            this.EdgeIntesectionButton = new System.Windows.Forms.CheckBox();
            this.Moving = new System.Windows.Forms.Button();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(794, 584);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // AddPointButton
            // 
            this.AddPointButton.Location = new System.Drawing.Point(822, 13);
            this.AddPointButton.Name = "AddPointButton";
            this.AddPointButton.Size = new System.Drawing.Size(110, 23);
            this.AddPointButton.TabIndex = 1;
            this.AddPointButton.Text = "Добавить точку";
            this.AddPointButton.UseVisualStyleBackColor = true;
            this.AddPointButton.Click += new System.EventHandler(this.AddPoint_Click);
            // 
            // AddEdgeButton
            // 
            this.AddEdgeButton.Location = new System.Drawing.Point(822, 54);
            this.AddEdgeButton.Name = "AddEdgeButton";
            this.AddEdgeButton.Size = new System.Drawing.Size(110, 23);
            this.AddEdgeButton.TabIndex = 2;
            this.AddEdgeButton.Text = "Добавить ребро";
            this.AddEdgeButton.UseVisualStyleBackColor = true;
            this.AddEdgeButton.Click += new System.EventHandler(this.AddEdge_Click);
            // 
            // AddPolygonButton
            // 
            this.AddPolygonButton.Location = new System.Drawing.Point(822, 97);
            this.AddPolygonButton.Name = "AddPolygonButton";
            this.AddPolygonButton.Size = new System.Drawing.Size(110, 23);
            this.AddPolygonButton.TabIndex = 3;
            this.AddPolygonButton.Text = "Добавить полигон";
            this.AddPolygonButton.UseVisualStyleBackColor = true;
            this.AddPolygonButton.Click += new System.EventHandler(this.AddPoly_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(876, 574);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 4;
            this.ClearButton.Text = "Очистить";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.Clear_Click);
            // 
            // RotateEdgeButton
            // 
            this.RotateEdgeButton.Location = new System.Drawing.Point(822, 139);
            this.RotateEdgeButton.Name = "RotateEdgeButton";
            this.RotateEdgeButton.Size = new System.Drawing.Size(110, 23);
            this.RotateEdgeButton.TabIndex = 5;
            this.RotateEdgeButton.Text = "Повернуть ребро";
            this.RotateEdgeButton.UseVisualStyleBackColor = true;
            this.RotateEdgeButton.Click += new System.EventHandler(this.RotateEdge_Click);
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.Location = new System.Drawing.Point(812, 472);
            this.InfoTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.InfoTextBox.Multiline = true;
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.ReadOnly = true;
            this.InfoTextBox.Size = new System.Drawing.Size(185, 89);
            this.InfoTextBox.TabIndex = 8;
            this.InfoTextBox.Text = "InfoBox";
            this.InfoTextBox.TextChanged += new System.EventHandler(this.InfoTextBox_TextChanged);
            // 
            // PointPositionButton
            // 
            this.PointPositionButton.AutoSize = true;
            this.PointPositionButton.Location = new System.Drawing.Point(822, 225);
            this.PointPositionButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PointPositionButton.Name = "PointPositionButton";
            this.PointPositionButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PointPositionButton.Size = new System.Drawing.Size(115, 17);
            this.PointPositionButton.TabIndex = 7;
            this.PointPositionButton.Text = "Положение точки";
            this.PointPositionButton.UseVisualStyleBackColor = true;
            this.PointPositionButton.CheckedChanged += new System.EventHandler(this.PointPositionbutton_Click);
            // 
            // PointInsideButton
            // 
            this.PointInsideButton.AutoSize = true;
            this.PointInsideButton.Location = new System.Drawing.Point(822, 203);
            this.PointInsideButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PointInsideButton.Name = "PointInsideButton";
            this.PointInsideButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PointInsideButton.Size = new System.Drawing.Size(143, 17);
            this.PointInsideButton.TabIndex = 6;
            this.PointInsideButton.Text = "Точка внутри полигона";
            this.PointInsideButton.UseVisualStyleBackColor = true;
            this.PointInsideButton.CheckedChanged += new System.EventHandler(this.PointInsideButton_Click);
            // 
            // EdgeIntesectionButton
            // 
            this.EdgeIntesectionButton.AutoSize = true;
            this.EdgeIntesectionButton.Location = new System.Drawing.Point(822, 180);
            this.EdgeIntesectionButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.EdgeIntesectionButton.Name = "EdgeIntesectionButton";
            this.EdgeIntesectionButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EdgeIntesectionButton.Size = new System.Drawing.Size(159, 17);
            this.EdgeIntesectionButton.TabIndex = 5;
            this.EdgeIntesectionButton.Text = "Поиск пересечения ребер";
            this.EdgeIntesectionButton.UseVisualStyleBackColor = true;
            this.EdgeIntesectionButton.CheckedChanged += new System.EventHandler(this.EdgeIntesectionButton_Click);
            // 
            // Moving
            // 
            this.Moving.Location = new System.Drawing.Point(839, 444);
            this.Moving.Name = "Moving";
            this.Moving.Size = new System.Drawing.Size(139, 23);
            this.Moving.TabIndex = 5;
            this.Moving.Text = "Преобразовать полигон";
            this.Moving.UseVisualStyleBackColor = true;
            this.Moving.Click += new System.EventHandler(this.Moving_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(846, 284);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(56, 20);
            this.numericUpDown1.TabIndex = 9;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(930, 285);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            2000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            2000,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(48, 20);
            this.numericUpDown2.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(864, 262);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Перемещение";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(826, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(909, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Y";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(882, 334);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Поворот";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(872, 350);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(72, 20);
            this.numericUpDown3.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(864, 402);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Масштабирование";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(839, 376);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(139, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "Преобразовать полигон";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(842, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(139, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "Преобразовать полигон";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(912, 423);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(828, 422);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "X";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(849, 419);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(53, 20);
            this.textBox1.TabIndex = 24;
            this.textBox1.Text = "1";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(932, 420);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(53, 20);
            this.textBox2.TabIndex = 25;
            this.textBox2.Text = "1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 609);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.numericUpDown3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.PointInsideButton);
            this.Controls.Add(this.EdgeIntesectionButton);
            this.Controls.Add(this.PointPositionButton);
            this.Controls.Add(this.InfoTextBox);
            this.Controls.Add(this.Moving);
            this.Controls.Add(this.RotateEdgeButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.AddPolygonButton);
            this.Controls.Add(this.AddEdgeButton);
            this.Controls.Add(this.AddPointButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button AddPointButton;
        private System.Windows.Forms.Button AddEdgeButton;
        private System.Windows.Forms.Button AddPolygonButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button RotateEdgeButton;
        private System.Windows.Forms.TextBox InfoTextBox;
        private System.Windows.Forms.CheckBox PointPositionButton;
        private System.Windows.Forms.CheckBox PointInsideButton;
        private System.Windows.Forms.CheckBox EdgeIntesectionButton;
        private System.Windows.Forms.Button Moving;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
    }
}

