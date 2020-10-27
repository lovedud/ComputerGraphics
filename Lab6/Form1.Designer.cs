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
            this.cube_button = new System.Windows.Forms.Button();
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
            this.perspective_button = new System.Windows.Forms.Button();
            this.scaleButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.grx2 = new System.Windows.Forms.TextBox();
            this.lx2 = new System.Windows.Forms.Label();
            this.grx1 = new System.Windows.Forms.TextBox();
            this.lx1 = new System.Windows.Forms.Label();
            this.gry2 = new System.Windows.Forms.TextBox();
            this.ly2 = new System.Windows.Forms.Label();
            this.gry1 = new System.Windows.Forms.TextBox();
            this.ly1 = new System.Windows.Forms.Label();
            this.Y_step = new System.Windows.Forms.TextBox();
            this.lystep = new System.Windows.Forms.Label();
            this.X_step = new System.Windows.Forms.TextBox();
            this.lxstep = new System.Windows.Forms.Label();
            this.Graph = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.Ortxyz = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(8, 5);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(813, 641);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // cube_button
            // 
            this.cube_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cube_button.Location = new System.Drawing.Point(841, 9);
            this.cube_button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.cube_button.Name = "cube_button";
            this.cube_button.Size = new System.Drawing.Size(83, 31);
            this.cube_button.TabIndex = 1;
            this.cube_button.Text = "Гексаэдр";
            this.cube_button.UseVisualStyleBackColor = true;
            this.cube_button.Click += new System.EventHandler(this.Cub_Button_Click);
            // 
            // ort_button
            // 
            this.ort_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ort_button.Location = new System.Drawing.Point(862, 251);
            this.ort_button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ort_button.Name = "ort_button";
            this.ort_button.Size = new System.Drawing.Size(137, 27);
            this.ort_button.TabIndex = 1;
            this.ort_button.Text = "Ортографическая";
            this.ort_button.UseVisualStyleBackColor = true;
            this.ort_button.Click += new System.EventHandler(this.Ort_Button_Click);
            // 
            // Ortxy
            // 
            this.Ortxy.AutoSize = true;
            this.Ortxy.Checked = true;
            this.Ortxy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Ortxy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Ortxy.Location = new System.Drawing.Point(1004, 211);
            this.Ortxy.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Ortxy.Name = "Ortxy";
            this.Ortxy.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortxy.Size = new System.Drawing.Size(40, 21);
            this.Ortxy.TabIndex = 2;
            this.Ortxy.Text = "xy";
            this.Ortxy.UseVisualStyleBackColor = true;
            this.Ortxy.CheckedChanged += new System.EventHandler(this.Ortxy_CheckedChanged);
            // 
            // Ortxz
            // 
            this.Ortxz.AutoSize = true;
            this.Ortxz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Ortxz.Location = new System.Drawing.Point(1004, 233);
            this.Ortxz.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Ortxz.Name = "Ortxz";
            this.Ortxz.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortxz.Size = new System.Drawing.Size(40, 21);
            this.Ortxz.TabIndex = 2;
            this.Ortxz.Text = "xz";
            this.Ortxz.UseVisualStyleBackColor = true;
            this.Ortxz.CheckedChanged += new System.EventHandler(this.Ortxz_CheckedChanged);
            // 
            // Ortyz
            // 
            this.Ortyz.AutoSize = true;
            this.Ortyz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Ortyz.Location = new System.Drawing.Point(1004, 255);
            this.Ortyz.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Ortyz.Name = "Ortyz";
            this.Ortyz.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortyz.Size = new System.Drawing.Size(41, 21);
            this.Ortyz.TabIndex = 2;
            this.Ortyz.Text = "yz";
            this.Ortyz.UseVisualStyleBackColor = true;
            this.Ortyz.CheckedChanged += new System.EventHandler(this.Ortyz_CheckedChanged);
            // 
            // clear_button
            // 
            this.clear_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.clear_button.Location = new System.Drawing.Point(908, 622);
            this.clear_button.Name = "clear_button";
            this.clear_button.Size = new System.Drawing.Size(91, 23);
            this.clear_button.TabIndex = 1;
            this.clear_button.Text = "Очистить";
            this.clear_button.UseVisualStyleBackColor = true;
            this.clear_button.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // iso_button
            // 
            this.iso_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.iso_button.Location = new System.Drawing.Point(862, 220);
            this.iso_button.Name = "iso_button";
            this.iso_button.Size = new System.Drawing.Size(137, 26);
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
            this.rotateAroundLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rotateAroundLine.Location = new System.Drawing.Point(874, 386);
            this.rotateAroundLine.Name = "rotateAroundLine";
            this.rotateAroundLine.Size = new System.Drawing.Size(170, 45);
            this.rotateAroundLine.TabIndex = 3;
            this.rotateAroundLine.Text = "Вращение вокруг прямой";
            this.rotateAroundLine.UseVisualStyleBackColor = true;
            this.rotateAroundLine.Click += new System.EventHandler(this.Rota_Click);
            // 
            // OX
            // 
            this.OX.Enabled = false;
            this.OX.Location = new System.Drawing.Point(903, 438);
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
            this.OY.Location = new System.Drawing.Point(943, 438);
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
            this.OZ.Location = new System.Drawing.Point(981, 438);
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
            this.Custom.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Custom.Location = new System.Drawing.Point(873, 468);
            this.Custom.Name = "Custom";
            this.Custom.Size = new System.Drawing.Size(171, 37);
            this.Custom.TabIndex = 7;
            this.Custom.Text = "Произвольная прямая";
            this.Custom.UseVisualStyleBackColor = true;
            this.Custom.Click += new System.EventHandler(this.Custom_Click);
            // 
            // lab1
            // 
            this.lab1.AutoSize = true;
            this.lab1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lab1.Location = new System.Drawing.Point(911, 512);
            this.lab1.Name = "lab1";
            this.lab1.Size = new System.Drawing.Size(91, 17);
            this.lab1.TabIndex = 8;
            this.lab1.Text = "Старт линии";
            // 
            // lab9
            // 
            this.lab9.AutoSize = true;
            this.lab9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.lab9.Location = new System.Drawing.Point(888, 565);
            this.lab9.Name = "lab9";
            this.lab9.Size = new System.Drawing.Size(146, 17);
            this.lab9.TabIndex = 9;
            this.lab9.Text = "Вектор направления";
            // 
            // lab2
            // 
            this.lab2.AutoSize = true;
            this.lab2.Location = new System.Drawing.Point(865, 537);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(14, 13);
            this.lab2.TabIndex = 12;
            this.lab2.Text = "X";
            // 
            // s_x
            // 
            this.s_x.Location = new System.Drawing.Point(881, 534);
            this.s_x.Name = "s_x";
            this.s_x.Size = new System.Drawing.Size(34, 20);
            this.s_x.TabIndex = 13;
            this.s_x.TextChanged += new System.EventHandler(this.CheckForCustomLine);
            // 
            // s_y
            // 
            this.s_y.Location = new System.Drawing.Point(939, 534);
            this.s_y.Name = "s_y";
            this.s_y.Size = new System.Drawing.Size(37, 20);
            this.s_y.TabIndex = 15;
            this.s_y.TextChanged += new System.EventHandler(this.CheckForCustomLine);
            // 
            // lab5
            // 
            this.lab5.AutoSize = true;
            this.lab5.Location = new System.Drawing.Point(921, 537);
            this.lab5.Name = "lab5";
            this.lab5.Size = new System.Drawing.Size(14, 13);
            this.lab5.TabIndex = 14;
            this.lab5.Text = "Y";
            // 
            // s_z
            // 
            this.s_z.Location = new System.Drawing.Point(999, 534);
            this.s_z.Name = "s_z";
            this.s_z.Size = new System.Drawing.Size(35, 20);
            this.s_z.TabIndex = 17;
            this.s_z.TextChanged += new System.EventHandler(this.CheckForCustomLine);
            // 
            // lab6
            // 
            this.lab6.AutoSize = true;
            this.lab6.Location = new System.Drawing.Point(981, 537);
            this.lab6.Name = "lab6";
            this.lab6.Size = new System.Drawing.Size(14, 13);
            this.lab6.TabIndex = 16;
            this.lab6.Text = "Z";
            // 
            // e_z
            // 
            this.e_z.Location = new System.Drawing.Point(999, 585);
            this.e_z.Name = "e_z";
            this.e_z.Size = new System.Drawing.Size(35, 20);
            this.e_z.TabIndex = 23;
            this.e_z.TextChanged += new System.EventHandler(this.CheckForCustomLine);
            // 
            // lab3
            // 
            this.lab3.AutoSize = true;
            this.lab3.Location = new System.Drawing.Point(981, 588);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(14, 13);
            this.lab3.TabIndex = 22;
            this.lab3.Text = "Z";
            // 
            // e_y
            // 
            this.e_y.Location = new System.Drawing.Point(939, 585);
            this.e_y.Name = "e_y";
            this.e_y.Size = new System.Drawing.Size(36, 20);
            this.e_y.TabIndex = 21;
            this.e_y.TextChanged += new System.EventHandler(this.CheckForCustomLine);
            // 
            // lab7
            // 
            this.lab7.AutoSize = true;
            this.lab7.Location = new System.Drawing.Point(921, 588);
            this.lab7.Name = "lab7";
            this.lab7.Size = new System.Drawing.Size(14, 13);
            this.lab7.TabIndex = 20;
            this.lab7.Text = "Y";
            // 
            // e_x
            // 
            this.e_x.Location = new System.Drawing.Point(881, 585);
            this.e_x.Name = "e_x";
            this.e_x.Size = new System.Drawing.Size(34, 20);
            this.e_x.TabIndex = 19;
            this.e_x.TextChanged += new System.EventHandler(this.CheckForCustomLine);
            // 
            // lab8
            // 
            this.lab8.AutoSize = true;
            this.lab8.Location = new System.Drawing.Point(865, 588);
            this.lab8.Name = "lab8";
            this.lab8.Size = new System.Drawing.Size(14, 13);
            this.lab8.TabIndex = 18;
            this.lab8.Text = "X";
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button3.Location = new System.Drawing.Point(874, 316);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(170, 29);
            this.button3.TabIndex = 3;
            this.button3.Text = "Смещение";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // perspective_button
            // 
            this.perspective_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.perspective_button.Location = new System.Drawing.Point(862, 282);
            this.perspective_button.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.perspective_button.Name = "perspective_button";
            this.perspective_button.Size = new System.Drawing.Size(137, 25);
            this.perspective_button.TabIndex = 1;
            this.perspective_button.Text = "Перспективная";
            this.perspective_button.UseVisualStyleBackColor = true;
            this.perspective_button.Click += new System.EventHandler(this.perspective_button_Click);
            // 
            // scaleButton
            // 
            this.scaleButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.scaleButton.Location = new System.Drawing.Point(874, 350);
            this.scaleButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.scaleButton.Name = "scaleButton";
            this.scaleButton.Size = new System.Drawing.Size(170, 31);
            this.scaleButton.TabIndex = 29;
            this.scaleButton.Text = "Масштабирование";
            this.scaleButton.UseVisualStyleBackColor = true;
            this.scaleButton.Click += new System.EventHandler(this.scaleButton_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.button1.Location = new System.Drawing.Point(939, 9);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 31);
            this.button1.TabIndex = 1;
            this.button1.Text = "Загрузить obj файл";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.load_obj_click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Sin(x) + Sin(y)",
            "Sin(x)*Cos(y)"});
            this.comboBox1.Location = new System.Drawing.Point(881, 72);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(150, 21);
            this.comboBox1.TabIndex = 30;
            // 
            // grx2
            // 
            this.grx2.Location = new System.Drawing.Point(994, 98);
            this.grx2.Name = "grx2";
            this.grx2.Size = new System.Drawing.Size(37, 20);
            this.grx2.TabIndex = 34;
            this.grx2.Text = "5";
            // 
            // lx2
            // 
            this.lx2.AutoSize = true;
            this.lx2.Location = new System.Drawing.Point(954, 102);
            this.lx2.Name = "lx2";
            this.lx2.Size = new System.Drawing.Size(20, 13);
            this.lx2.TabIndex = 33;
            this.lx2.Text = "X2";
            // 
            // grx1
            // 
            this.grx1.Location = new System.Drawing.Point(900, 99);
            this.grx1.Name = "grx1";
            this.grx1.Size = new System.Drawing.Size(34, 20);
            this.grx1.TabIndex = 32;
            this.grx1.Text = "-5";
            // 
            // lx1
            // 
            this.lx1.AutoSize = true;
            this.lx1.Location = new System.Drawing.Point(859, 102);
            this.lx1.Name = "lx1";
            this.lx1.Size = new System.Drawing.Size(20, 13);
            this.lx1.TabIndex = 31;
            this.lx1.Text = "X1";
            // 
            // gry2
            // 
            this.gry2.Location = new System.Drawing.Point(994, 124);
            this.gry2.Name = "gry2";
            this.gry2.Size = new System.Drawing.Size(37, 20);
            this.gry2.TabIndex = 38;
            this.gry2.Text = "5";
            // 
            // ly2
            // 
            this.ly2.AutoSize = true;
            this.ly2.Location = new System.Drawing.Point(954, 128);
            this.ly2.Name = "ly2";
            this.ly2.Size = new System.Drawing.Size(20, 13);
            this.ly2.TabIndex = 37;
            this.ly2.Text = "Y2";
            // 
            // gry1
            // 
            this.gry1.Location = new System.Drawing.Point(900, 125);
            this.gry1.Name = "gry1";
            this.gry1.Size = new System.Drawing.Size(34, 20);
            this.gry1.TabIndex = 36;
            this.gry1.Text = "-5";
            this.gry1.TextChanged += new System.EventHandler(this.gry1_TextChanged);
            // 
            // ly1
            // 
            this.ly1.AutoSize = true;
            this.ly1.Location = new System.Drawing.Point(859, 128);
            this.ly1.Name = "ly1";
            this.ly1.Size = new System.Drawing.Size(20, 13);
            this.ly1.TabIndex = 35;
            this.ly1.Text = "Y1";
            // 
            // Y_step
            // 
            this.Y_step.Location = new System.Drawing.Point(994, 151);
            this.Y_step.Name = "Y_step";
            this.Y_step.Size = new System.Drawing.Size(37, 20);
            this.Y_step.TabIndex = 42;
            this.Y_step.Text = "30";
            // 
            // lystep
            // 
            this.lystep.AutoSize = true;
            this.lystep.Location = new System.Drawing.Point(937, 154);
            this.lystep.Name = "lystep";
            this.lystep.Size = new System.Drawing.Size(52, 13);
            this.lystep.TabIndex = 41;
            this.lystep.Text = "Y amount";
            // 
            // X_step
            // 
            this.X_step.Location = new System.Drawing.Point(900, 151);
            this.X_step.Name = "X_step";
            this.X_step.Size = new System.Drawing.Size(34, 20);
            this.X_step.TabIndex = 40;
            this.X_step.Text = "30";
            // 
            // lxstep
            // 
            this.lxstep.AutoSize = true;
            this.lxstep.Location = new System.Drawing.Point(842, 154);
            this.lxstep.Name = "lxstep";
            this.lxstep.Size = new System.Drawing.Size(52, 13);
            this.lxstep.TabIndex = 39;
            this.lxstep.Text = "X amount";
            // 
            // Graph
            // 
            this.Graph.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Graph.Location = new System.Drawing.Point(914, 177);
            this.Graph.Name = "Graph";
            this.Graph.Size = new System.Drawing.Size(81, 27);
            this.Graph.TabIndex = 43;
            this.Graph.Text = "Graph";
            this.Graph.UseVisualStyleBackColor = true;
            this.Graph.Click += new System.EventHandler(this.Graph_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1144, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 44;
            this.label1.Text = "Фигура вращения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1095, 75);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 45;
            this.label2.Text = "Образующая";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1095, 154);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Разбиения";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1095, 184);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 47;
            this.label4.Text = "Ось";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1171, 73);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(108, 70);
            this.textBox1.TabIndex = 48;
            this.textBox1.Text = "0,0,0\r\n0,100,0\r\n100,100,0\r\n100,0,0";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(1171, 152);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            359,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown1.TabIndex = 49;
            this.numericUpDown1.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "OX",
            "OY",
            "OZ"});
            this.comboBox2.Location = new System.Drawing.Point(1171, 184);
            this.comboBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(92, 21);
            this.comboBox2.TabIndex = 50;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(1146, 228);
            this.button4.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(93, 29);
            this.button4.TabIndex = 51;
            this.button4.Text = "Построить";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Ortxyz
            // 
            this.Ortxyz.AutoSize = true;
            this.Ortxyz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Ortxyz.Location = new System.Drawing.Point(1004, 280);
            this.Ortxyz.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Ortxyz.Name = "Ortxyz";
            this.Ortxyz.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Ortxyz.Size = new System.Drawing.Size(47, 21);
            this.Ortxyz.TabIndex = 52;
            this.Ortxyz.Text = "xyz";
            this.Ortxyz.UseVisualStyleBackColor = true;
            this.Ortxyz.CheckedChanged += new System.EventHandler(this.Ortxyz_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1295, 657);
            this.Controls.Add(this.Ortxyz);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Graph);
            this.Controls.Add(this.Y_step);
            this.Controls.Add(this.lystep);
            this.Controls.Add(this.X_step);
            this.Controls.Add(this.lxstep);
            this.Controls.Add(this.gry2);
            this.Controls.Add(this.ly2);
            this.Controls.Add(this.gry1);
            this.Controls.Add(this.ly1);
            this.Controls.Add(this.grx2);
            this.Controls.Add(this.lx2);
            this.Controls.Add(this.grx1);
            this.Controls.Add(this.lx1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.scaleButton);
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
            this.Controls.Add(this.perspective_button);
            this.Controls.Add(this.ort_button);
            this.Controls.Add(this.clear_button);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cube_button);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button cube_button;
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
        private System.Windows.Forms.Button scaleButton;
        private System.Windows.Forms.Button perspective_button;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TextBox grx2;
        private System.Windows.Forms.Label lx2;
        private System.Windows.Forms.TextBox grx1;
        private System.Windows.Forms.Label lx1;
        private System.Windows.Forms.TextBox gry2;
        private System.Windows.Forms.Label ly2;
        private System.Windows.Forms.TextBox gry1;
        private System.Windows.Forms.Label ly1;
        private System.Windows.Forms.TextBox Y_step;
        private System.Windows.Forms.Label lystep;
        private System.Windows.Forms.TextBox X_step;
        private System.Windows.Forms.Label lxstep;
        private System.Windows.Forms.Button Graph;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox Ortxyz;
    }
}

