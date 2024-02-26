﻿namespace Trading.GUI
{
    partial class StockInfo
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
            label1 = new Label();
            button1 = new Button();
            dateTimePicker1 = new DateTimePicker();
            textBox1 = new TextBox();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            label2 = new Label();
            label3 = new Label();
            progressBar1 = new ProgressBar();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(342, 23);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            label1.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(28, 61);
            button1.Name = "button1";
            button1.Size = new Size(255, 73);
            button1.TabIndex = 1;
            button1.Text = "Show Graph";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(631, 41);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(116, 27);
            dateTimePicker1.TabIndex = 2;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(658, 343);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(120, 27);
            textBox1.TabIndex = 3;
            textBox1.Text = "cooking...";
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = System.Data.UpdateRowSource.None;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 153);
            label2.Name = "label2";
            label2.Size = new Size(87, 20);
            label2.TabIndex = 4;
            label2.Text = "Latest Price:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 188);
            label3.Name = "label3";
            label3.Size = new Size(50, 20);
            label3.TabIndex = 5;
            label3.Text = "label3";
            label3.Click += label3_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(658, 385);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(119, 38);
            progressBar1.Step = 5;
            progressBar1.TabIndex = 6;
            progressBar1.Value = 40;
            // 
            // StockInfo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(progressBar1);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(dateTimePicker1);
            Controls.Add(button1);
            Controls.Add(label1);
            Name = "StockInfo";
            Text = "StockInfo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Button button1;
        private DateTimePicker dateTimePicker1;
        private TextBox textBox1;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Label label2;
        private Label label3;
        private ProgressBar progressBar1;
    }
}