namespace Trading.GUI
{
    partial class Dashboard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            progressBar1 = new ProgressBar();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            textBox1 = new TextBox();
            button1 = new Button();
            label1 = new Label();
            label7 = new Label();
            textBox2 = new TextBox();
            label8 = new Label();
            label9 = new Label();
            button2 = new Button();
            comboBox1 = new ComboBox();
            textBox3 = new TextBox();
            label10 = new Label();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(444, 389);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(337, 55);
            progressBar1.TabIndex = 0;
            progressBar1.Value = 50;
            progressBar1.Click += progressBar1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(36, 93);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 2;
            label2.Text = "Stocks";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(36, 157);
            label3.Name = "label3";
            label3.Size = new Size(65, 20);
            label3.TabIndex = 3;
            label3.Text = "Symbols";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(132, 93);
            label4.Name = "label4";
            label4.Size = new Size(50, 20);
            label4.TabIndex = 4;
            label4.Text = "label4";
            label4.Click += label4_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(132, 157);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 5;
            label5.Text = "label5";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(343, 18);
            label6.Name = "label6";
            label6.Size = new Size(50, 20);
            label6.TabIndex = 6;
            label6.Text = "label6";
            label6.Click += label6_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(26, 225);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(96, 27);
            textBox1.TabIndex = 7;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Location = new Point(26, 268);
            button1.Name = "button1";
            button1.Size = new Size(210, 63);
            button1.TabIndex = 8;
            button1.Text = "Add Stock";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(26, 202);
            label1.Name = "label1";
            label1.Size = new Size(89, 20);
            label1.TabIndex = 9;
            label1.Text = "Stock Name";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(132, 202);
            label7.Name = "label7";
            label7.Size = new Size(127, 20);
            label7.TabIndex = 10;
            label7.Text = "Number of Stocks";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(132, 225);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(104, 27);
            textBox2.TabIndex = 11;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(37, 126);
            label8.Name = "label8";
            label8.Size = new Size(52, 20);
            label8.TabIndex = 12;
            label8.Text = "Shares";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(132, 126);
            label9.Name = "label9";
            label9.Size = new Size(50, 20);
            label9.TabIndex = 13;
            label9.Text = "label9";
            // 
            // button2
            // 
            button2.Location = new Point(520, 71);
            button2.Name = "button2";
            button2.Size = new Size(228, 55);
            button2.TabIndex = 14;
            button2.Text = "Update Number of Shares";
            button2.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(524, 148);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(230, 28);
            comboBox1.TabIndex = 15;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(684, 195);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(70, 27);
            textBox3.TabIndex = 16;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(524, 198);
            label10.Name = "label10";
            label10.Size = new Size(125, 20);
            label10.TabIndex = 17;
            label10.Text = "Number of stocks";
            // 
            // button3
            // 
            button3.Location = new Point(520, 228);
            button3.Name = "button3";
            button3.Size = new Size(117, 47);
            button3.TabIndex = 18;
            button3.Text = "Add";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(648, 228);
            button4.Name = "button4";
            button4.Size = new Size(106, 47);
            button4.TabIndex = 19;
            button4.Text = "Remove";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Dashboard
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(796, 450);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(label10);
            Controls.Add(textBox3);
            Controls.Add(comboBox1);
            Controls.Add(button2);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(textBox2);
            Controls.Add(label7);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(progressBar1);
            Name = "Dashboard";
            Text = "Dashboard";
            Load += Dashboard_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ProgressBar progressBar1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private TextBox textBox1;
        private Button button1;
        private Label label1;
        private Label label7;
        private TextBox textBox2;
        private Label label8;
        private Label label9;
        private Button button2;
        private ComboBox comboBox1;
        private TextBox textBox3;
        private Label label10;
        private Button button3;
        private Button button4;
    }
}