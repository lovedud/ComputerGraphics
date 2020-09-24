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
            this.EdgeIntesectionButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            // 
            // AddPointButton
            // 
            this.AddPointButton.Location = new System.Drawing.Point(850, 27);
            this.AddPointButton.Name = "AddPointButton";
            this.AddPointButton.Size = new System.Drawing.Size(110, 23);
            this.AddPointButton.TabIndex = 1;
            this.AddPointButton.Text = "Добавить точку";
            this.AddPointButton.UseVisualStyleBackColor = true;
            this.AddPointButton.Click += new System.EventHandler(this.AddPoint_Click);
            // 
            // AddEdgeButton
            // 
            this.AddEdgeButton.Location = new System.Drawing.Point(850, 68);
            this.AddEdgeButton.Name = "AddEdgeButton";
            this.AddEdgeButton.Size = new System.Drawing.Size(110, 23);
            this.AddEdgeButton.TabIndex = 2;
            this.AddEdgeButton.Text = "Добавить ребро";
            this.AddEdgeButton.UseVisualStyleBackColor = true;
            this.AddEdgeButton.Click += new System.EventHandler(this.AddEdge_Click);
            // 
            // AddPolygonButton
            // 
            this.AddPolygonButton.Location = new System.Drawing.Point(850, 108);
            this.AddPolygonButton.Name = "AddPolygonButton";
            this.AddPolygonButton.Size = new System.Drawing.Size(110, 23);
            this.AddPolygonButton.TabIndex = 3;
            this.AddPolygonButton.Text = "Добавить полигон";
            this.AddPolygonButton.UseVisualStyleBackColor = true;
            this.AddPolygonButton.Click += new System.EventHandler(this.AddPoly_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Location = new System.Drawing.Point(870, 477);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 4;
            this.ClearButton.Text = "Очистить";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.Clear_Click);
            // 
            // RotateEdgeButton
            // 
            this.RotateEdgeButton.Location = new System.Drawing.Point(850, 207);
            this.RotateEdgeButton.Name = "RotateEdgeButton";
            this.RotateEdgeButton.Size = new System.Drawing.Size(110, 23);
            this.RotateEdgeButton.TabIndex = 5;
            this.RotateEdgeButton.Text = "Повернуть ребро";
            this.RotateEdgeButton.UseVisualStyleBackColor = true;
            this.RotateEdgeButton.Click += new System.EventHandler(this.RotateEdge_Click);
            // 
            // EdgeIntesectionButton
            // 
            this.EdgeIntesectionButton.Location = new System.Drawing.Point(844, 238);
            this.EdgeIntesectionButton.Name = "EdgeIntesectionButton";
            this.EdgeIntesectionButton.Size = new System.Drawing.Size(122, 36);
            this.EdgeIntesectionButton.TabIndex = 6;
            this.EdgeIntesectionButton.Text = "Поиск пересечения ребер";
            this.EdgeIntesectionButton.UseVisualStyleBackColor = true;
            this.EdgeIntesectionButton.Click += new System.EventHandler(this.EdgeIntesectionButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 609);
            this.Controls.Add(this.EdgeIntesectionButton);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button AddPointButton;
        private System.Windows.Forms.Button AddEdgeButton;
        private System.Windows.Forms.Button AddPolygonButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button RotateEdgeButton;
        private System.Windows.Forms.Button EdgeIntesectionButton;
    }
}

