namespace HuffmanAlgoClient
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
            this.buttonInput = new System.Windows.Forms.Button();
            this.textBoxInput = new System.Windows.Forms.TextBox();
            this.groupBoxClassicHuffmanAlgo = new System.Windows.Forms.GroupBox();
            this.groupBoxAdaptiveHuffmanAlgo = new System.Windows.Forms.GroupBox();
            this.buttonRun = new System.Windows.Forms.Button();
            this.labelBlock = new System.Windows.Forms.Label();
            this.checkBoxBlock = new System.Windows.Forms.CheckBox();
            this.textBoxBlock = new System.Windows.Forms.TextBox();
            this.labelOutС = new System.Windows.Forms.Label();
            this.labelSpeedEncodeС = new System.Windows.Forms.Label();
            this.labelSpeedDecodeС = new System.Windows.Forms.Label();
            this.labelSizeSourceС = new System.Windows.Forms.Label();
            this.labelSizeCompressС = new System.Windows.Forms.Label();
            this.labelCoefficientCompressС = new System.Windows.Forms.Label();
            this.labelSizeDictionaryС = new System.Windows.Forms.Label();
            this.labelCoefficientCompressA = new System.Windows.Forms.Label();
            this.labelSizeCompressA = new System.Windows.Forms.Label();
            this.labelSizeSourceA = new System.Windows.Forms.Label();
            this.labelSpeedDecodeA = new System.Windows.Forms.Label();
            this.labelSpeedEncodeA = new System.Windows.Forms.Label();
            this.labelOutA = new System.Windows.Forms.Label();
            this.textBoxOutC = new System.Windows.Forms.TextBox();
            this.textBoxSpeedEncodeС = new System.Windows.Forms.TextBox();
            this.textBoxSpeedDecodeС = new System.Windows.Forms.TextBox();
            this.textBoxSizeSourceС = new System.Windows.Forms.TextBox();
            this.textBoxSizeCompressС = new System.Windows.Forms.TextBox();
            this.textBoxCoefficientCompressС = new System.Windows.Forms.TextBox();
            this.textBoxSizeDictionaryС = new System.Windows.Forms.TextBox();
            this.textBoxCoefficientCompressA = new System.Windows.Forms.TextBox();
            this.textBoxSizeCompressA = new System.Windows.Forms.TextBox();
            this.textBoxSizeSourceA = new System.Windows.Forms.TextBox();
            this.textBoxSpeedDecodeA = new System.Windows.Forms.TextBox();
            this.textBoxSpeedEncodeA = new System.Windows.Forms.TextBox();
            this.textBoxOutA = new System.Windows.Forms.TextBox();
            this.openFileDialogInput = new System.Windows.Forms.OpenFileDialog();
            this.textBoxTimeBuildTree = new System.Windows.Forms.TextBox();
            this.labelTimeBuildTree = new System.Windows.Forms.Label();
            this.labelDecodeFileC = new System.Windows.Forms.Label();
            this.labelDecodeFileA = new System.Windows.Forms.Label();
            this.textBoxDecodeFileC = new System.Windows.Forms.TextBox();
            this.textBoxDecodeFileA = new System.Windows.Forms.TextBox();
            this.groupBoxClassicHuffmanAlgo.SuspendLayout();
            this.groupBoxAdaptiveHuffmanAlgo.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonInput
            // 
            this.buttonInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonInput.Location = new System.Drawing.Point(40, 32);
            this.buttonInput.Name = "buttonInput";
            this.buttonInput.Size = new System.Drawing.Size(202, 41);
            this.buttonInput.TabIndex = 0;
            this.buttonInput.Text = "Input";
            this.buttonInput.UseVisualStyleBackColor = true;
            this.buttonInput.Click += new System.EventHandler(this.buttonInput_Click);
            // 
            // textBoxInput
            // 
            this.textBoxInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxInput.Location = new System.Drawing.Point(282, 32);
            this.textBoxInput.Name = "textBoxInput";
            this.textBoxInput.Size = new System.Drawing.Size(1042, 41);
            this.textBoxInput.TabIndex = 1;
            // 
            // groupBoxClassicHuffmanAlgo
            // 
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.textBoxDecodeFileC);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.labelDecodeFileC);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.textBoxTimeBuildTree);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.labelTimeBuildTree);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.textBoxSizeDictionaryС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.textBoxCoefficientCompressС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.textBoxSizeCompressС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.textBoxSizeSourceС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.textBoxSpeedDecodeС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.textBoxSpeedEncodeС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.textBoxOutC);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.labelSizeDictionaryС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.labelCoefficientCompressС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.labelSizeCompressС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.labelSizeSourceС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.labelSpeedDecodeС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.labelSpeedEncodeС);
            this.groupBoxClassicHuffmanAlgo.Controls.Add(this.labelOutС);
            this.groupBoxClassicHuffmanAlgo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxClassicHuffmanAlgo.Location = new System.Drawing.Point(12, 106);
            this.groupBoxClassicHuffmanAlgo.Name = "groupBoxClassicHuffmanAlgo";
            this.groupBoxClassicHuffmanAlgo.Size = new System.Drawing.Size(649, 552);
            this.groupBoxClassicHuffmanAlgo.TabIndex = 2;
            this.groupBoxClassicHuffmanAlgo.TabStop = false;
            this.groupBoxClassicHuffmanAlgo.Text = "Классический алгоритм Хаффмана";
            // 
            // groupBoxAdaptiveHuffmanAlgo
            // 
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.textBoxDecodeFileA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.labelDecodeFileA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.textBoxCoefficientCompressA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.textBoxSizeCompressA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.textBoxSizeSourceA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.textBoxSpeedDecodeA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.textBoxSpeedEncodeA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.textBoxOutA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.labelCoefficientCompressA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.labelSizeCompressA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.labelSizeSourceA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.labelSpeedDecodeA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.labelSpeedEncodeA);
            this.groupBoxAdaptiveHuffmanAlgo.Controls.Add(this.labelOutA);
            this.groupBoxAdaptiveHuffmanAlgo.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxAdaptiveHuffmanAlgo.Location = new System.Drawing.Point(676, 106);
            this.groupBoxAdaptiveHuffmanAlgo.Name = "groupBoxAdaptiveHuffmanAlgo";
            this.groupBoxAdaptiveHuffmanAlgo.Size = new System.Drawing.Size(679, 552);
            this.groupBoxAdaptiveHuffmanAlgo.TabIndex = 3;
            this.groupBoxAdaptiveHuffmanAlgo.TabStop = false;
            this.groupBoxAdaptiveHuffmanAlgo.Text = "Адаптивный (динамический) алгоритм Хаффмана";
            this.groupBoxAdaptiveHuffmanAlgo.Enter += new System.EventHandler(this.groupBoxAdaptiveHuffmanAlgo_Enter);
            // 
            // buttonRun
            // 
            this.buttonRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRun.Location = new System.Drawing.Point(558, 781);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(229, 39);
            this.buttonRun.TabIndex = 4;
            this.buttonRun.Text = "Выполнить";
            this.buttonRun.UseVisualStyleBackColor = true;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // labelBlock
            // 
            this.labelBlock.AutoSize = true;
            this.labelBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBlock.Location = new System.Drawing.Point(12, 722);
            this.labelBlock.Name = "labelBlock";
            this.labelBlock.Size = new System.Drawing.Size(201, 32);
            this.labelBlock.TabIndex = 5;
            this.labelBlock.Text = "Размер блока";
            // 
            // checkBoxBlock
            // 
            this.checkBoxBlock.AutoSize = true;
            this.checkBoxBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxBlock.Location = new System.Drawing.Point(311, 680);
            this.checkBoxBlock.Name = "checkBoxBlock";
            this.checkBoxBlock.Size = new System.Drawing.Size(774, 36);
            this.checkBoxBlock.TabIndex = 6;
            this.checkBoxBlock.Text = "Применить блочную одаптивность к обоим алгоритмам";
            this.checkBoxBlock.UseVisualStyleBackColor = true;
            // 
            // textBoxBlock
            // 
            this.textBoxBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxBlock.Location = new System.Drawing.Point(330, 722);
            this.textBoxBlock.Name = "textBoxBlock";
            this.textBoxBlock.Size = new System.Drawing.Size(267, 38);
            this.textBoxBlock.TabIndex = 7;
            this.textBoxBlock.Text = "1000";
            // 
            // labelOutС
            // 
            this.labelOutС.AutoSize = true;
            this.labelOutС.Location = new System.Drawing.Point(6, 48);
            this.labelOutС.Name = "labelOutС";
            this.labelOutС.Size = new System.Drawing.Size(90, 29);
            this.labelOutС.TabIndex = 0;
            this.labelOutС.Text = "Output:";
            // 
            // labelSpeedEncodeС
            // 
            this.labelSpeedEncodeС.AutoSize = true;
            this.labelSpeedEncodeС.Location = new System.Drawing.Point(6, 92);
            this.labelSpeedEncodeС.Name = "labelSpeedEncodeС";
            this.labelSpeedEncodeС.Size = new System.Drawing.Size(254, 29);
            this.labelSpeedEncodeС.TabIndex = 1;
            this.labelSpeedEncodeС.Text = "Время кодирования:";
            // 
            // labelSpeedDecodeС
            // 
            this.labelSpeedDecodeС.AutoSize = true;
            this.labelSpeedDecodeС.Location = new System.Drawing.Point(6, 136);
            this.labelSpeedDecodeС.Name = "labelSpeedDecodeС";
            this.labelSpeedDecodeС.Size = new System.Drawing.Size(282, 29);
            this.labelSpeedDecodeС.TabIndex = 2;
            this.labelSpeedDecodeС.Text = "Время декодирования:";
            // 
            // labelSizeSourceС
            // 
            this.labelSizeSourceС.AutoSize = true;
            this.labelSizeSourceС.Location = new System.Drawing.Point(6, 184);
            this.labelSizeSourceС.Name = "labelSizeSourceС";
            this.labelSizeSourceС.Size = new System.Drawing.Size(313, 29);
            this.labelSizeSourceС.TabIndex = 3;
            this.labelSizeSourceС.Text = "Размер исходного файла:";
            // 
            // labelSizeCompressС
            // 
            this.labelSizeCompressС.AutoSize = true;
            this.labelSizeCompressС.Location = new System.Drawing.Point(6, 232);
            this.labelSizeCompressС.Name = "labelSizeCompressС";
            this.labelSizeCompressС.Size = new System.Drawing.Size(287, 29);
            this.labelSizeCompressС.TabIndex = 4;
            this.labelSizeCompressС.Text = "Размер сжатого файла:";
            this.labelSizeCompressС.Click += new System.EventHandler(this.labelSizeCompressС_Click);
            // 
            // labelCoefficientCompressС
            // 
            this.labelCoefficientCompressС.AutoSize = true;
            this.labelCoefficientCompressС.Location = new System.Drawing.Point(6, 281);
            this.labelCoefficientCompressС.Name = "labelCoefficientCompressС";
            this.labelCoefficientCompressС.Size = new System.Drawing.Size(259, 29);
            this.labelCoefficientCompressС.TabIndex = 5;
            this.labelCoefficientCompressС.Text = "Коэффицент сжатия:";
            this.labelCoefficientCompressС.Click += new System.EventHandler(this.labelCoefficientCompressС_Click);
            // 
            // labelSizeDictionaryС
            // 
            this.labelSizeDictionaryС.AutoSize = true;
            this.labelSizeDictionaryС.Location = new System.Drawing.Point(6, 332);
            this.labelSizeDictionaryС.Name = "labelSizeDictionaryС";
            this.labelSizeDictionaryС.Size = new System.Drawing.Size(208, 29);
            this.labelSizeDictionaryС.TabIndex = 6;
            this.labelSizeDictionaryС.Text = "Размер словаря:";
            // 
            // labelCoefficientCompressA
            // 
            this.labelCoefficientCompressA.AutoSize = true;
            this.labelCoefficientCompressA.Location = new System.Drawing.Point(21, 281);
            this.labelCoefficientCompressA.Name = "labelCoefficientCompressA";
            this.labelCoefficientCompressA.Size = new System.Drawing.Size(259, 29);
            this.labelCoefficientCompressA.TabIndex = 12;
            this.labelCoefficientCompressA.Text = "Коэффицент сжатия:";
            // 
            // labelSizeCompressA
            // 
            this.labelSizeCompressA.AutoSize = true;
            this.labelSizeCompressA.Location = new System.Drawing.Point(21, 232);
            this.labelSizeCompressA.Name = "labelSizeCompressA";
            this.labelSizeCompressA.Size = new System.Drawing.Size(287, 29);
            this.labelSizeCompressA.TabIndex = 11;
            this.labelSizeCompressA.Text = "Размер сжатого файла:";
            // 
            // labelSizeSourceA
            // 
            this.labelSizeSourceA.AutoSize = true;
            this.labelSizeSourceA.Location = new System.Drawing.Point(21, 184);
            this.labelSizeSourceA.Name = "labelSizeSourceA";
            this.labelSizeSourceA.Size = new System.Drawing.Size(313, 29);
            this.labelSizeSourceA.TabIndex = 10;
            this.labelSizeSourceA.Text = "Размер исходного файла:";
            // 
            // labelSpeedDecodeA
            // 
            this.labelSpeedDecodeA.AutoSize = true;
            this.labelSpeedDecodeA.Location = new System.Drawing.Point(21, 136);
            this.labelSpeedDecodeA.Name = "labelSpeedDecodeA";
            this.labelSpeedDecodeA.Size = new System.Drawing.Size(282, 29);
            this.labelSpeedDecodeA.TabIndex = 9;
            this.labelSpeedDecodeA.Text = "Время декодирования:";
            // 
            // labelSpeedEncodeA
            // 
            this.labelSpeedEncodeA.AutoSize = true;
            this.labelSpeedEncodeA.Location = new System.Drawing.Point(21, 92);
            this.labelSpeedEncodeA.Name = "labelSpeedEncodeA";
            this.labelSpeedEncodeA.Size = new System.Drawing.Size(254, 29);
            this.labelSpeedEncodeA.TabIndex = 8;
            this.labelSpeedEncodeA.Text = "Время кодирования:";
            // 
            // labelOutA
            // 
            this.labelOutA.AutoSize = true;
            this.labelOutA.Location = new System.Drawing.Point(21, 48);
            this.labelOutA.Name = "labelOutA";
            this.labelOutA.Size = new System.Drawing.Size(90, 29);
            this.labelOutA.TabIndex = 7;
            this.labelOutA.Text = "Output:";
            // 
            // textBoxOutC
            // 
            this.textBoxOutC.Location = new System.Drawing.Point(102, 43);
            this.textBoxOutC.Name = "textBoxOutC";
            this.textBoxOutC.Size = new System.Drawing.Size(541, 34);
            this.textBoxOutC.TabIndex = 7;
            // 
            // textBoxSpeedEncodeС
            // 
            this.textBoxSpeedEncodeС.Location = new System.Drawing.Point(378, 87);
            this.textBoxSpeedEncodeС.Name = "textBoxSpeedEncodeС";
            this.textBoxSpeedEncodeС.Size = new System.Drawing.Size(265, 34);
            this.textBoxSpeedEncodeС.TabIndex = 8;
            this.textBoxSpeedEncodeС.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSpeedDecodeС
            // 
            this.textBoxSpeedDecodeС.Location = new System.Drawing.Point(378, 131);
            this.textBoxSpeedDecodeС.Name = "textBoxSpeedDecodeС";
            this.textBoxSpeedDecodeС.Size = new System.Drawing.Size(265, 34);
            this.textBoxSpeedDecodeС.TabIndex = 9;
            this.textBoxSpeedDecodeС.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSizeSourceС
            // 
            this.textBoxSizeSourceС.Location = new System.Drawing.Point(378, 179);
            this.textBoxSizeSourceС.Name = "textBoxSizeSourceС";
            this.textBoxSizeSourceС.Size = new System.Drawing.Size(265, 34);
            this.textBoxSizeSourceС.TabIndex = 10;
            this.textBoxSizeSourceС.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSizeCompressС
            // 
            this.textBoxSizeCompressС.Location = new System.Drawing.Point(378, 227);
            this.textBoxSizeCompressС.Name = "textBoxSizeCompressС";
            this.textBoxSizeCompressС.Size = new System.Drawing.Size(265, 34);
            this.textBoxSizeCompressС.TabIndex = 11;
            this.textBoxSizeCompressС.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxCoefficientCompressС
            // 
            this.textBoxCoefficientCompressС.Location = new System.Drawing.Point(378, 276);
            this.textBoxCoefficientCompressС.Name = "textBoxCoefficientCompressС";
            this.textBoxCoefficientCompressС.Size = new System.Drawing.Size(265, 34);
            this.textBoxCoefficientCompressС.TabIndex = 12;
            this.textBoxCoefficientCompressС.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSizeDictionaryС
            // 
            this.textBoxSizeDictionaryС.Location = new System.Drawing.Point(378, 327);
            this.textBoxSizeDictionaryС.Name = "textBoxSizeDictionaryС";
            this.textBoxSizeDictionaryС.Size = new System.Drawing.Size(265, 34);
            this.textBoxSizeDictionaryС.TabIndex = 13;
            this.textBoxSizeDictionaryС.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxCoefficientCompressA
            // 
            this.textBoxCoefficientCompressA.Location = new System.Drawing.Point(408, 276);
            this.textBoxCoefficientCompressA.Name = "textBoxCoefficientCompressA";
            this.textBoxCoefficientCompressA.Size = new System.Drawing.Size(265, 34);
            this.textBoxCoefficientCompressA.TabIndex = 19;
            this.textBoxCoefficientCompressA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSizeCompressA
            // 
            this.textBoxSizeCompressA.Location = new System.Drawing.Point(408, 227);
            this.textBoxSizeCompressA.Name = "textBoxSizeCompressA";
            this.textBoxSizeCompressA.Size = new System.Drawing.Size(265, 34);
            this.textBoxSizeCompressA.TabIndex = 18;
            this.textBoxSizeCompressA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSizeSourceA
            // 
            this.textBoxSizeSourceA.Location = new System.Drawing.Point(408, 179);
            this.textBoxSizeSourceA.Name = "textBoxSizeSourceA";
            this.textBoxSizeSourceA.Size = new System.Drawing.Size(265, 34);
            this.textBoxSizeSourceA.TabIndex = 17;
            this.textBoxSizeSourceA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSpeedDecodeA
            // 
            this.textBoxSpeedDecodeA.Location = new System.Drawing.Point(408, 131);
            this.textBoxSpeedDecodeA.Name = "textBoxSpeedDecodeA";
            this.textBoxSpeedDecodeA.Size = new System.Drawing.Size(265, 34);
            this.textBoxSpeedDecodeA.TabIndex = 16;
            this.textBoxSpeedDecodeA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxSpeedEncodeA
            // 
            this.textBoxSpeedEncodeA.Location = new System.Drawing.Point(408, 87);
            this.textBoxSpeedEncodeA.Name = "textBoxSpeedEncodeA";
            this.textBoxSpeedEncodeA.Size = new System.Drawing.Size(265, 34);
            this.textBoxSpeedEncodeA.TabIndex = 15;
            this.textBoxSpeedEncodeA.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBoxOutA
            // 
            this.textBoxOutA.Location = new System.Drawing.Point(132, 43);
            this.textBoxOutA.Name = "textBoxOutA";
            this.textBoxOutA.Size = new System.Drawing.Size(541, 34);
            this.textBoxOutA.TabIndex = 14;
            // 
            // openFileDialogInput
            // 
            this.openFileDialogInput.FileName = "openFileDialog1";
            // 
            // textBoxTimeBuildTree
            // 
            this.textBoxTimeBuildTree.Location = new System.Drawing.Point(378, 382);
            this.textBoxTimeBuildTree.Name = "textBoxTimeBuildTree";
            this.textBoxTimeBuildTree.Size = new System.Drawing.Size(265, 34);
            this.textBoxTimeBuildTree.TabIndex = 15;
            this.textBoxTimeBuildTree.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelTimeBuildTree
            // 
            this.labelTimeBuildTree.AutoSize = true;
            this.labelTimeBuildTree.Location = new System.Drawing.Point(6, 387);
            this.labelTimeBuildTree.Name = "labelTimeBuildTree";
            this.labelTimeBuildTree.Size = new System.Drawing.Size(328, 29);
            this.labelTimeBuildTree.TabIndex = 14;
            this.labelTimeBuildTree.Text = "Время построения дерева:";
            // 
            // labelDecodeFileC
            // 
            this.labelDecodeFileC.AutoSize = true;
            this.labelDecodeFileC.Location = new System.Drawing.Point(6, 462);
            this.labelDecodeFileC.Name = "labelDecodeFileC";
            this.labelDecodeFileC.Size = new System.Drawing.Size(287, 29);
            this.labelDecodeFileC.TabIndex = 16;
            this.labelDecodeFileC.Text = "Декодированный файл:";
            // 
            // labelDecodeFileA
            // 
            this.labelDecodeFileA.AutoSize = true;
            this.labelDecodeFileA.Location = new System.Drawing.Point(21, 462);
            this.labelDecodeFileA.Name = "labelDecodeFileA";
            this.labelDecodeFileA.Size = new System.Drawing.Size(287, 29);
            this.labelDecodeFileA.TabIndex = 20;
            this.labelDecodeFileA.Text = "Декодированный файл:";
            // 
            // textBoxDecodeFileC
            // 
            this.textBoxDecodeFileC.Location = new System.Drawing.Point(11, 512);
            this.textBoxDecodeFileC.Name = "textBoxDecodeFileC";
            this.textBoxDecodeFileC.Size = new System.Drawing.Size(632, 34);
            this.textBoxDecodeFileC.TabIndex = 17;
            // 
            // textBoxDecodeFileA
            // 
            this.textBoxDecodeFileA.Location = new System.Drawing.Point(26, 512);
            this.textBoxDecodeFileA.Name = "textBoxDecodeFileA";
            this.textBoxDecodeFileA.Size = new System.Drawing.Size(647, 34);
            this.textBoxDecodeFileA.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1367, 832);
            this.Controls.Add(this.textBoxBlock);
            this.Controls.Add(this.checkBoxBlock);
            this.Controls.Add(this.labelBlock);
            this.Controls.Add(this.buttonRun);
            this.Controls.Add(this.groupBoxAdaptiveHuffmanAlgo);
            this.Controls.Add(this.groupBoxClassicHuffmanAlgo);
            this.Controls.Add(this.textBoxInput);
            this.Controls.Add(this.buttonInput);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBoxClassicHuffmanAlgo.ResumeLayout(false);
            this.groupBoxClassicHuffmanAlgo.PerformLayout();
            this.groupBoxAdaptiveHuffmanAlgo.ResumeLayout(false);
            this.groupBoxAdaptiveHuffmanAlgo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonInput;
        private System.Windows.Forms.TextBox textBoxInput;
        private System.Windows.Forms.GroupBox groupBoxClassicHuffmanAlgo;
        private System.Windows.Forms.GroupBox groupBoxAdaptiveHuffmanAlgo;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Label labelBlock;
        private System.Windows.Forms.CheckBox checkBoxBlock;
        private System.Windows.Forms.TextBox textBoxBlock;
        private System.Windows.Forms.Label labelSizeDictionaryС;
        private System.Windows.Forms.Label labelCoefficientCompressС;
        private System.Windows.Forms.Label labelSizeCompressС;
        private System.Windows.Forms.Label labelSizeSourceС;
        private System.Windows.Forms.Label labelSpeedDecodeС;
        private System.Windows.Forms.Label labelSpeedEncodeС;
        private System.Windows.Forms.Label labelOutС;
        private System.Windows.Forms.Label labelCoefficientCompressA;
        private System.Windows.Forms.Label labelSizeCompressA;
        private System.Windows.Forms.Label labelSizeSourceA;
        private System.Windows.Forms.Label labelSpeedDecodeA;
        private System.Windows.Forms.Label labelSpeedEncodeA;
        private System.Windows.Forms.Label labelOutA;
        private System.Windows.Forms.TextBox textBoxSizeDictionaryС;
        private System.Windows.Forms.TextBox textBoxCoefficientCompressС;
        private System.Windows.Forms.TextBox textBoxSizeCompressС;
        private System.Windows.Forms.TextBox textBoxSizeSourceС;
        private System.Windows.Forms.TextBox textBoxSpeedDecodeС;
        private System.Windows.Forms.TextBox textBoxSpeedEncodeС;
        private System.Windows.Forms.TextBox textBoxOutC;
        private System.Windows.Forms.TextBox textBoxCoefficientCompressA;
        private System.Windows.Forms.TextBox textBoxSizeCompressA;
        private System.Windows.Forms.TextBox textBoxSizeSourceA;
        private System.Windows.Forms.TextBox textBoxSpeedDecodeA;
        private System.Windows.Forms.TextBox textBoxSpeedEncodeA;
        private System.Windows.Forms.TextBox textBoxOutA;
        private System.Windows.Forms.OpenFileDialog openFileDialogInput;
        private System.Windows.Forms.TextBox textBoxTimeBuildTree;
        private System.Windows.Forms.Label labelTimeBuildTree;
        private System.Windows.Forms.TextBox textBoxDecodeFileC;
        private System.Windows.Forms.Label labelDecodeFileC;
        private System.Windows.Forms.TextBox textBoxDecodeFileA;
        private System.Windows.Forms.Label labelDecodeFileA;
    }
}

