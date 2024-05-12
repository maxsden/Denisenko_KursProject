namespace Denisenko_KursProject
{
    partial class Form1
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
            this.loadCodeButton = new System.Windows.Forms.Button();
            this.saveCodeButton = new System.Windows.Forms.Button();
            this.saveLexemButton = new System.Windows.Forms.Button();
            this.savePostfixButton = new System.Windows.Forms.Button();
            this.saveMnemocodeButton = new System.Windows.Forms.Button();
            this.clearTextBoxButton = new System.Windows.Forms.Button();
            this.errorView = new System.Windows.Forms.ListView();
            this.TypeError = new System.Windows.Forms.ColumnHeader();
            this.RowError = new System.Windows.Forms.ColumnHeader();
            this.TextError = new System.Windows.Forms.ColumnHeader();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.errorsClearButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadCodeButton
            // 
            this.loadCodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadCodeButton.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.loadCodeButton.Location = new System.Drawing.Point(866, 23);
            this.loadCodeButton.Name = "loadCodeButton";
            this.loadCodeButton.Size = new System.Drawing.Size(232, 87);
            this.loadCodeButton.TabIndex = 1;
            this.loadCodeButton.Text = "Загрузить код \r\nиз файла";
            this.loadCodeButton.UseVisualStyleBackColor = true;
            this.loadCodeButton.Click += new System.EventHandler(this.loadCodeButton_Click);
            // 
            // saveCodeButton
            // 
            this.saveCodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveCodeButton.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saveCodeButton.Location = new System.Drawing.Point(866, 126);
            this.saveCodeButton.Name = "saveCodeButton";
            this.saveCodeButton.Size = new System.Drawing.Size(232, 87);
            this.saveCodeButton.TabIndex = 1;
            this.saveCodeButton.Text = "Сохранить код\r\nв файл";
            this.saveCodeButton.UseVisualStyleBackColor = true;
            this.saveCodeButton.Click += new System.EventHandler(this.saveCodeButton_Click);
            // 
            // saveLexemButton
            // 
            this.saveLexemButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveLexemButton.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saveLexemButton.Location = new System.Drawing.Point(866, 249);
            this.saveLexemButton.Name = "saveLexemButton";
            this.saveLexemButton.Size = new System.Drawing.Size(232, 87);
            this.saveLexemButton.TabIndex = 1;
            this.saveLexemButton.Text = "Лексический анализ";
            this.saveLexemButton.UseVisualStyleBackColor = true;
            this.saveLexemButton.Click += new System.EventHandler(this.saveLexemButton_Click);
            // 
            // savePostfixButton
            // 
            this.savePostfixButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.savePostfixButton.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.savePostfixButton.Location = new System.Drawing.Point(866, 351);
            this.savePostfixButton.Name = "savePostfixButton";
            this.savePostfixButton.Size = new System.Drawing.Size(232, 87);
            this.savePostfixButton.TabIndex = 1;
            this.savePostfixButton.Text = "Генератор постфикса";
            this.savePostfixButton.UseVisualStyleBackColor = true;
            this.savePostfixButton.Click += new System.EventHandler(this.savePostfixButton_Click);
            // 
            // saveMnemocodeButton
            // 
            this.saveMnemocodeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveMnemocodeButton.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.saveMnemocodeButton.Location = new System.Drawing.Point(866, 455);
            this.saveMnemocodeButton.Name = "saveMnemocodeButton";
            this.saveMnemocodeButton.Size = new System.Drawing.Size(232, 87);
            this.saveMnemocodeButton.TabIndex = 1;
            this.saveMnemocodeButton.Text = "Генератор кода";
            this.saveMnemocodeButton.UseVisualStyleBackColor = true;
            this.saveMnemocodeButton.Click += new System.EventHandler(this.saveMnemocodeButton_Click);
            // 
            // clearTextBoxButton
            // 
            this.clearTextBoxButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearTextBoxButton.BackColor = System.Drawing.Color.RosyBrown;
            this.clearTextBoxButton.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.clearTextBoxButton.Location = new System.Drawing.Point(866, 583);
            this.clearTextBoxButton.Name = "clearTextBoxButton";
            this.clearTextBoxButton.Size = new System.Drawing.Size(232, 42);
            this.clearTextBoxButton.TabIndex = 1;
            this.clearTextBoxButton.Text = "Очистить всё";
            this.clearTextBoxButton.UseVisualStyleBackColor = false;
            this.clearTextBoxButton.Click += new System.EventHandler(this.clearTextBoxButton_Click);
            // 
            // errorView
            // 
            this.errorView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TypeError,
            this.RowError,
            this.TextError});
            this.errorView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.errorView.Location = new System.Drawing.Point(3, 23);
            this.errorView.Name = "errorView";
            this.errorView.Size = new System.Drawing.Size(1105, 200);
            this.errorView.TabIndex = 2;
            this.errorView.UseCompatibleStateImageBehavior = false;
            this.errorView.View = System.Windows.Forms.View.Details;
            // 
            // TypeError
            // 
            this.TypeError.Text = "Тип ошибки";
            this.TypeError.Width = 200;
            // 
            // RowError
            // 
            this.RowError.Text = "Строка кода";
            this.RowError.Width = 100;
            // 
            // TextError
            // 
            this.TextError.Text = "Описание";
            this.TextError.Width = 599;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.errorView);
            this.groupBox1.Location = new System.Drawing.Point(21, 715);
            this.groupBox1.MaximumSize = new System.Drawing.Size(1800, 400);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1111, 226);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ошибки";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.HideSelection = false;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.MaximumSize = new System.Drawing.Size(1600, 1200);
            this.richTextBox1.MinimumSize = new System.Drawing.Size(661, 649);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(830, 671);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Location = new System.Drawing.Point(12, 23);
            this.panel1.MaximumSize = new System.Drawing.Size(1600, 900);
            this.panel1.MinimumSize = new System.Drawing.Size(700, 650);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(830, 671);
            this.panel1.TabIndex = 4;
            // 
            // errorsClearButton
            // 
            this.errorsClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.errorsClearButton.BackColor = System.Drawing.Color.RosyBrown;
            this.errorsClearButton.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.errorsClearButton.Location = new System.Drawing.Point(866, 631);
            this.errorsClearButton.Name = "errorsClearButton";
            this.errorsClearButton.Size = new System.Drawing.Size(232, 42);
            this.errorsClearButton.TabIndex = 1;
            this.errorsClearButton.Text = "Очистить ошибки";
            this.errorsClearButton.UseVisualStyleBackColor = false;
            this.errorsClearButton.Click += new System.EventHandler(this.errorsClearButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 953);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.errorsClearButton);
            this.Controls.Add(this.clearTextBoxButton);
            this.Controls.Add(this.saveMnemocodeButton);
            this.Controls.Add(this.savePostfixButton);
            this.Controls.Add(this.saveLexemButton);
            this.Controls.Add(this.saveCodeButton);
            this.Controls.Add(this.loadCodeButton);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(1100, 1000);
            this.Name = "Form1";
            this.Text = "Компилятор";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button loadCodeButton;
        private Button saveCodeButton;
        private Button saveLexemButton;
        private Button savePostfixButton;
        private Button saveMnemocodeButton;
        private Button clearTextBoxButton;
        private ListView errorView;
        private ColumnHeader TypeError;
        private ColumnHeader TextError;
        private ColumnHeader RowError;
        private GroupBox groupBox1;
        private RichTextBox richTextBox1;
        private Panel panel1;
        private Button errorsClearButton;
    }
}