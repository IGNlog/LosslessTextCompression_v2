namespace HuffmanAlgoClient
{
    partial class Options
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
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.buttonInput = new System.Windows.Forms.Button();
            this.labelSizeText = new System.Windows.Forms.Label();
            this.buttonTextSize = new System.Windows.Forms.Button();
            this.comboBoxTextSize = new System.Windows.Forms.ComboBox();
            this.textBoxOutC = new System.Windows.Forms.TextBox();
            this.labelOutС = new System.Windows.Forms.Label();
            this.openFileDialogTextSize = new System.Windows.Forms.OpenFileDialog();
            this.labelCountWordsInInput = new System.Windows.Forms.Label();
            this.textBoxCountWordsInInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxInput
            // 
            this.textBoxInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxInput.Location = new System.Drawing.Point(293, 24);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(1042, 41);
            this.textBoxInput.TabIndex = 3;
            // 
            // buttonInput
            // 
            this.buttonInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInput.Location = new System.Drawing.Point(51, 24);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(202, 41);
            this.buttonInput.TabIndex = 2;
            this.buttonInput.Text = "Input";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // labelSizeText
            // 
            this.labelSizeText.AutoSize = true;
            this.labelSizeText.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSizeText.Location = new System.Drawing.Point(61, 171);
            this.labelSizeText.Name = "labelSizeText";
            this.labelSizeText.Size = new System.Drawing.Size(492, 32);
            this.labelSizeText.TabIndex = 4;
            this.labelSizeText.Text = "Выберете желаемый размер текста";
            // 
            // buttonTextSize
            // 
            this.buttonTextSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTextSize.Location = new System.Drawing.Point(1124, 161);
            this.buttonTextSize.Name = "buttonTextSize";
            this.buttonTextSize.Size = new System.Drawing.Size(159, 42);
            this.buttonTextSize.TabIndex = 5;
            this.buttonTextSize.Text = "Получить";
            this.buttonTextSize.UseVisualStyleBackColor = true;
            this.buttonTextSize.Click += new System.EventHandler(this.buttonTextSize_Click);
            // 
            // comboBoxTextSize
            // 
            this.comboBoxTextSize.AutoCompleteCustomSource.AddRange(new string[] {
            "100",
            "500",
            "1000",
            "5000",
            "10000",
            "15000",
            "20000",
            "30000",
            "50000",
            "100000",
            "150000",
            "200000",
            "300000",
            "400000",
            "500000"});
            this.comboBoxTextSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.comboBoxTextSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxTextSize.FormattingEnabled = true;
            this.comboBoxTextSize.Items.AddRange(new object[] {
            "100",
            "1000",
            "10000",
            "100000",
            "15000",
            "150000",
            "20000",
            "200000",
            "30000",
            "300000",
            "400000",
            "500",
            "5000",
            "50000",
            "500000"});
            this.comboBoxTextSize.Location = new System.Drawing.Point(688, 164);
            this.comboBoxTextSize.Name = "comboBoxTextSize";
            this.comboBoxTextSize.Size = new System.Drawing.Size(229, 39);
            this.comboBoxTextSize.Sorted = true;
            this.comboBoxTextSize.TabIndex = 6;
            this.comboBoxTextSize.SelectedValueChanged += new System.EventHandler(this.comboBoxTextSize_SelectedValueChanged);
            // 
            // textBoxOutC
            // 
            this.textBoxOutC.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxOutC.Location = new System.Drawing.Point(293, 282);
            this.textBoxOutC.Name = "textBoxOutC";
            this.textBoxOutC.Size = new System.Drawing.Size(1042, 38);
            this.textBoxOutC.TabIndex = 9;
            // 
            // labelOutС
            // 
            this.labelOutС.AutoSize = true;
            this.labelOutС.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelOutС.Location = new System.Drawing.Point(71, 285);
            this.labelOutС.Name = "labelOutС";
            this.labelOutС.Size = new System.Drawing.Size(109, 32);
            this.labelOutС.TabIndex = 8;
            this.labelOutС.Text = "Output:";
            // 
            // labelCountWordsInInput
            // 
            this.labelCountWordsInInput.AutoSize = true;
            this.labelCountWordsInInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCountWordsInInput.Location = new System.Drawing.Point(296, 92);
            this.labelCountWordsInInput.Name = "labelCountWordsInInput";
            this.labelCountWordsInInput.Size = new System.Drawing.Size(483, 32);
            this.labelCountWordsInInput.TabIndex = 10;
            this.labelCountWordsInInput.Text = "Количество слов в входном файле:";
            // 
            // textBoxCountWordsInInput
            // 
            this.textBoxCountWordsInInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCountWordsInInput.Location = new System.Drawing.Point(817, 89);
            this.textBoxCountWordsInInput.Name = "textBoxCountWordsInInput";
            this.textBoxCountWordsInInput.Size = new System.Drawing.Size(192, 38);
            this.textBoxCountWordsInInput.TabIndex = 11;
            this.textBoxCountWordsInInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // Options
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1407, 492);
            this.Controls.Add(this.textBoxCountWordsInInput);
            this.Controls.Add(this.labelCountWordsInInput);
            this.Controls.Add(this.textBoxOutC);
            this.Controls.Add(this.labelOutС);
            this.Controls.Add(this.comboBoxTextSize);
            this.Controls.Add(this.buttonTextSize);
            this.Controls.Add(this.labelSizeText);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.buttonInput);
            this.Name = "Options";
            this.Text = "Options";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.Label labelSizeText;
        private System.Windows.Forms.Button buttonTextSize;
        private System.Windows.Forms.ComboBox comboBoxTextSize;
        private System.Windows.Forms.TextBox textBoxOutC;
        private System.Windows.Forms.Label labelOutС;
        private System.Windows.Forms.OpenFileDialog openFileDialogTextSize;
        private System.Windows.Forms.Label labelCountWordsInInput;
        private System.Windows.Forms.TextBox textBoxCountWordsInInput;
    }
}