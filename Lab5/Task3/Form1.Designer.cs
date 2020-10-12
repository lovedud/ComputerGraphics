namespace Lab6
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
            this.pbox = new System.Windows.Forms.PictureBox();
            this.Add = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.Move = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.AddOnLine = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbox)).BeginInit();
            this.SuspendLayout();
            // 
            // pbox
            // 
            this.pbox.Location = new System.Drawing.Point(0, 0);
            this.pbox.Name = "pbox";
            this.pbox.Size = new System.Drawing.Size(1056, 607);
            this.pbox.TabIndex = 0;
            this.pbox.TabStop = false;
            this.pbox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbox_MouseDown);
            this.pbox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbox_MouseMove);
            this.pbox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbox_MouseUp);
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(1062, 146);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(110, 50);
            this.Add.TabIndex = 1;
            this.Add.Text = "Добавить новую точку";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Remove
            // 
            this.Remove.Location = new System.Drawing.Point(1062, 285);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(110, 50);
            this.Remove.TabIndex = 2;
            this.Remove.Text = "Удалить точку";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // Move
            // 
            this.Move.Location = new System.Drawing.Point(1062, 357);
            this.Move.Name = "Move";
            this.Move.Size = new System.Drawing.Size(110, 50);
            this.Move.TabIndex = 3;
            this.Move.Text = "Переместить точку";
            this.Move.UseVisualStyleBackColor = true;
            this.Move.Click += new System.EventHandler(this.Move_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(1062, 545);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(110, 50);
            this.Clear.TabIndex = 4;
            this.Clear.Text = "Очистить";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // AddOnLine
            // 
            this.AddOnLine.Location = new System.Drawing.Point(1062, 216);
            this.AddOnLine.Name = "AddOnLine";
            this.AddOnLine.Size = new System.Drawing.Size(110, 50);
            this.AddOnLine.TabIndex = 5;
            this.AddOnLine.Text = "Добавить точку на линию";
            this.AddOnLine.UseVisualStyleBackColor = true;
            this.AddOnLine.Click += new System.EventHandler(this.AddOnLine_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1198, 607);
            this.Controls.Add(this.AddOnLine);
            this.Controls.Add(this.Clear);
            this.Controls.Add(this.Move);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.pbox);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbox;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button Move;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button AddOnLine;
    }
}

