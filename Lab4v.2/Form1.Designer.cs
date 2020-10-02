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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(17, 16);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1058, 718);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // AddPointButton
            // 
            this.AddPointButton.Location = new System.Drawing.Point(1096, 16);
            this.AddPointButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddPointButton.Name = "AddPointButton";
            this.AddPointButton.Size = new System.Drawing.Size(147, 28);
            this.AddPointButton.TabIndex = 1;
            this.AddPointButton.Text = "Добавить точку";
            this.AddPointButton.UseVisualStyleBackColor = true;
            this.AddPointButton.Click += new System.EventHandler(this.AddPoint_Click);
            // 
            // AddEdgeButton
            // 
            this.AddEdgeButton.Location = new System.Drawing.Point(1096, 66);
            this.AddEdgeButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddEdgeButton.Name = "AddEdgeButton";
            this.AddEdgeButton.Size = new System.Drawing.Size(147, 28);
            this.AddEdgeButton.TabIndex = 2;
            this.AddEdgeButton.Text = "Добавить ребро";
            this.AddEdgeButton.UseVisualStyleBackColor = true;
            this.AddEdgeButton.Click += new System.EventHandler(this.AddEdge_Click);
            // 
            // AddPolygonButton
            // 
            this.AddPolygonButton.Location = new System.Drawing.Point(1096, 119);
            this.AddPolygonButton.Margin = new System.Windows.Forms.Padding(4);
            this.AddPolygonButton.Name = "AddPolygonButton";
            this.AddPolygonButton.Size = new System.Drawing.Size(147, 28);
            this.AddPolygonButton.TabIndex = 3;
            this.AddPolygonButton.Text = "Добавить полигон";
            this.AddPolygonButton.UseVisualStyleBackColor = true;
            this.AddPolygonButton.Click += new System.EventHandler(this.AddPoly_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(1168, 706);
            this.ClearButton.Margin = new System.Windows.Forms.Padding(4);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(100, 28);
            this.ClearButton.TabIndex = 4;
            this.ClearButton.Text = "Очистить";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.Clear_Click);
            // 
            // RotateEdgeButton
            // 
            this.RotateEdgeButton.Location = new System.Drawing.Point(1096, 171);
            this.RotateEdgeButton.Margin = new System.Windows.Forms.Padding(4);
            this.RotateEdgeButton.Name = "RotateEdgeButton";
            this.RotateEdgeButton.Size = new System.Drawing.Size(147, 28);
            this.RotateEdgeButton.TabIndex = 5;
            this.RotateEdgeButton.Text = "Повернуть ребро";
            this.RotateEdgeButton.UseVisualStyleBackColor = true;
            this.RotateEdgeButton.Click += new System.EventHandler(this.RotateEdge_Click);
            // 
            // InfoTextBox
            // 
            this.InfoTextBox.Location = new System.Drawing.Point(1083, 581);
            this.InfoTextBox.Multiline = true;
            this.InfoTextBox.Name = "InfoTextBox";
            this.InfoTextBox.ReadOnly = true;
            this.InfoTextBox.Size = new System.Drawing.Size(245, 109);
            this.InfoTextBox.TabIndex = 8;
            this.InfoTextBox.Text = "InfoBox";
            this.InfoTextBox.TextChanged += new System.EventHandler(this.InfoTextBox_TextChanged);
            // 
            // PointPositionButton
            // 
            this.PointPositionButton.AutoSize = true;
            this.PointPositionButton.Location = new System.Drawing.Point(1096, 277);
            this.PointPositionButton.Name = "PointPositionButton";
            this.PointPositionButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PointPositionButton.Size = new System.Drawing.Size(147, 21);
            this.PointPositionButton.TabIndex = 7;
            this.PointPositionButton.Text = "Положение точки";
            this.PointPositionButton.UseVisualStyleBackColor = true;
            this.PointPositionButton.CheckedChanged += new System.EventHandler(this.PointPositionbutton_Click);
            // 
            // PointInsideButton
            // 
            this.PointInsideButton.AutoSize = true;
            this.PointInsideButton.Location = new System.Drawing.Point(1096, 250);
            this.PointInsideButton.Name = "PointInsideButton";
            this.PointInsideButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PointInsideButton.Size = new System.Drawing.Size(184, 21);
            this.PointInsideButton.TabIndex = 6;
            this.PointInsideButton.Text = "Точка внутри полигона";
            this.PointInsideButton.UseVisualStyleBackColor = true;
            this.PointInsideButton.CheckedChanged += new System.EventHandler(this.PointInsideButton_Click);
            // 
            // EdgeIntesectionButton
            // 
            this.EdgeIntesectionButton.AutoSize = true;
            this.EdgeIntesectionButton.Location = new System.Drawing.Point(1096, 221);
            this.EdgeIntesectionButton.Name = "EdgeIntesectionButton";
            this.EdgeIntesectionButton.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.EdgeIntesectionButton.Size = new System.Drawing.Size(205, 21);
            this.EdgeIntesectionButton.TabIndex = 5;
            this.EdgeIntesectionButton.Text = "Поиск пересечения ребер";
            this.EdgeIntesectionButton.UseVisualStyleBackColor = true;
            this.EdgeIntesectionButton.CheckedChanged += new System.EventHandler(this.EdgeIntesectionButton_Click);
            // 
            // Moving
            // 
            this.Moving.Location = new System.Drawing.Point(1083, 305);
            this.Moving.Margin = new System.Windows.Forms.Padding(4);
            this.Moving.Name = "Moving";
            this.Moving.Size = new System.Drawing.Size(160, 28);
            this.Moving.TabIndex = 5;
            this.Moving.Text = "Режим перемещение";
            this.Moving.UseVisualStyleBackColor = true;
            this.Moving.Click += new System.EventHandler(this.Moving_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 750);
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
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
    }
}

