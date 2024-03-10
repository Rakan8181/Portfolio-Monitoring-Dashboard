namespace Trading.GUI
{
    partial class Menu
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            button1 = new Button();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            button3 = new Button();
            comboBox1 = new ComboBox();
            label3 = new Label();
            label5 = new Label();
            comboBox2 = new ComboBox();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(568, 77);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(191, 27);
            textBox1.TabIndex = 0;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(568, 121);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(191, 27);
            textBox2.TabIndex = 1;
            // 
            // button1
            // 
            button1.Location = new Point(568, 166);
            button1.Name = "button1";
            button1.Size = new Size(89, 53);
            button1.TabIndex = 2;
            button1.Text = "Add Client";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ImageAlign = ContentAlignment.MiddleRight;
            label1.Location = new Point(486, 84);
            label1.Name = "label1";
            label1.Size = new Size(80, 20);
            label1.TabIndex = 4;
            label1.Text = "First Name";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(464, 121);
            label2.Name = "label2";
            label2.Size = new Size(102, 20);
            label2.TabIndex = 6;
            label2.Text = "Second Name";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(568, 151);
            label4.Name = "label4";
            label4.Size = new Size(0, 20);
            label4.TabIndex = 8;
            // 
            // button3
            // 
            button3.Location = new Point(663, 166);
            button3.Name = "button3";
            button3.Size = new Size(89, 53);
            button3.TabIndex = 12;
            button3.Text = "Delete Client";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(25, 84);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(273, 28);
            comboBox1.TabIndex = 15;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 40);
            label3.Name = "label3";
            label3.Size = new Size(267, 20);
            label3.TabIndex = 16;
            label3.Text = "Select a client to display their portfolio";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(25, 272);
            label5.Name = "label5";
            label5.Size = new Size(221, 20);
            label5.TabIndex = 17;
            label5.Text = "Select stock to view information";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new Point(25, 311);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(267, 28);
            comboBox2.TabIndex = 19;
            comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(comboBox2);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(comboBox1);
            Controls.Add(button3);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Label label1;
        private Label label2;
        private Label label4;
        private Button button3;
        private ComboBox comboBox1;
        private Label label3;
        private Label label5;
        private ComboBox comboBox2;
    }
}