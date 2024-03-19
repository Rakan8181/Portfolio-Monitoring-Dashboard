namespace Trading.GUI
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
            dateTimePicker1 = new DateTimePicker();
            sqliteCommand1 = new Microsoft.Data.Sqlite.SqliteCommand();
            label2 = new Label();
            LatestPriceLabel = new Label();
            label4 = new Label();
            label5 = new Label();
            comboBox1 = new ComboBox();
            Graph = new OxyPlot.WindowsForms.PlotView();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(524, 23);
            label1.Name = "label1";
            label1.Size = new Size(50, 20);
            label1.TabIndex = 0;
            label1.Text = "label1";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(1015, 62);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(116, 27);
            dateTimePicker1.TabIndex = 2;
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
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
            label2.Location = new Point(12, 23);
            label2.Name = "label2";
            label2.Size = new Size(87, 20);
            label2.TabIndex = 4;
            label2.Text = "Latest Price:";
            // 
            // LatestPriceLabel
            // 
            LatestPriceLabel.AutoSize = true;
            LatestPriceLabel.Location = new Point(105, 23);
            LatestPriceLabel.Name = "LatestPriceLabel";
            LatestPriceLabel.Size = new Size(50, 20);
            LatestPriceLabel.TabIndex = 5;
            LatestPriceLabel.Text = "label3";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1087, 23);
            label4.Name = "label4";
            label4.Size = new Size(85, 20);
            label4.TabIndex = 7;
            label4.Text = "Show Price ";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1015, 121);
            label5.Name = "label5";
            label5.Size = new Size(50, 20);
            label5.TabIndex = 8;
            label5.Text = "label5";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(1158, 62);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(66, 28);
            comboBox1.TabIndex = 9;
            comboBox1.SelectedIndexChanged += StockAttributesComboBox_SelectedIndexChanged;
            // 
            // Graph
            // 
            Graph.BackColor = SystemColors.Control;
            Graph.Location = new Point(12, 62);
            Graph.Name = "Graph";
            Graph.PanCursor = Cursors.Hand;
            Graph.Size = new Size(961, 475);
            Graph.TabIndex = 10;
            Graph.Text = "Graph";
            Graph.ZoomHorizontalCursor = Cursors.SizeWE;
            Graph.ZoomRectangleCursor = Cursors.SizeNWSE;
            Graph.ZoomVerticalCursor = Cursors.SizeNS;
            // 
            // StockInfo
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1309, 659);
            Controls.Add(Graph);
            Controls.Add(comboBox1);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(LatestPriceLabel);
            Controls.Add(label2);
            Controls.Add(dateTimePicker1);
            Controls.Add(label1);
            Name = "StockInfo";
            Text = "StockInfo";
            Load += StockInfo_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private DateTimePicker dateTimePicker1;
        private Microsoft.Data.Sqlite.SqliteCommand sqliteCommand1;
        private Label label2;
        private Label LatestPriceLabel;
        private Label label4;
        private Label label5;
        private ComboBox comboBox1;
        private OxyPlot.WindowsForms.PlotView Graph;
    }
}