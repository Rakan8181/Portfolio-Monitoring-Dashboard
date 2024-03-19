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
            FirstNameTextBox = new TextBox();
            SecondNameTextBox = new TextBox();
            AddClientButton = new Button();
            label1 = new Label();
            label2 = new Label();
            label4 = new Label();
            DeleteClientButton = new Button();
            ClientsComboBox = new ComboBox();
            label3 = new Label();
            label5 = new Label();
            StocksComboBox = new ComboBox();
            label6 = new Label();
            SuspendLayout();
            // 
            // FirstNameTextBox
            // 
            FirstNameTextBox.Location = new Point(568, 77);
            FirstNameTextBox.Name = "FirstNameTextBox";
            FirstNameTextBox.Size = new Size(191, 27);
            FirstNameTextBox.TabIndex = 0;
            // 
            // SecondNameTextBox
            // 
            SecondNameTextBox.Location = new Point(568, 121);
            SecondNameTextBox.Name = "SecondNameTextBox";
            SecondNameTextBox.Size = new Size(191, 27);
            SecondNameTextBox.TabIndex = 1;
            // 
            // AddClientButton
            // 
            AddClientButton.Location = new Point(568, 166);
            AddClientButton.Name = "AddClientButton";
            AddClientButton.Size = new Size(89, 53);
            AddClientButton.TabIndex = 2;
            AddClientButton.Text = "Add Client";
            AddClientButton.UseVisualStyleBackColor = true;
            AddClientButton.Click += AddClientButton_Click;
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
            // DeleteClientButton
            // 
            DeleteClientButton.Location = new Point(663, 166);
            DeleteClientButton.Name = "DeleteClientButton";
            DeleteClientButton.Size = new Size(89, 53);
            DeleteClientButton.TabIndex = 12;
            DeleteClientButton.Text = "Delete Client";
            DeleteClientButton.UseVisualStyleBackColor = true;
            DeleteClientButton.Click += DeleteClientButton_Click;
            // 
            // ClientsComboBox
            // 
            ClientsComboBox.FormattingEnabled = true;
            ClientsComboBox.Location = new Point(25, 121);
            ClientsComboBox.Name = "ClientsComboBox";
            ClientsComboBox.Size = new Size(273, 28);
            ClientsComboBox.TabIndex = 15;
            ClientsComboBox.SelectedIndexChanged += ClientsComboBox_SelectedIndexChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(25, 77);
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
            // StocksComboBox
            // 
            StocksComboBox.FormattingEnabled = true;
            StocksComboBox.Location = new Point(25, 311);
            StocksComboBox.Name = "StocksComboBox";
            StocksComboBox.Size = new Size(267, 28);
            StocksComboBox.TabIndex = 19;
            StocksComboBox.SelectedIndexChanged += StockInformationComboBox_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(338, 21);
            label6.Name = "label6";
            label6.Size = new Size(107, 46);
            label6.TabIndex = 20;
            label6.Text = "Menu";
            // 
            // Menu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 451);
            Controls.Add(label6);
            Controls.Add(StocksComboBox);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(ClientsComboBox);
            Controls.Add(DeleteClientButton);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(AddClientButton);
            Controls.Add(SecondNameTextBox);
            Controls.Add(FirstNameTextBox);
            Name = "Menu";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox FirstNameTextBox;
        private TextBox SecondNameTextBox;
        private Button AddClientButton;
        private Label label1;
        private Label label2;
        private Label label4;
        private Button DeleteClientButton;
        private ComboBox ClientsComboBox;
        private Label label3;
        private Label label5;
        private ComboBox StocksComboBox;
        private Label label6;
    }
}