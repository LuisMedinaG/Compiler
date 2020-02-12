namespace STL2_Act_1
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.buttonCopiar = new System.Windows.Forms.Button();
            this.txtBoxOrg = new System.Windows.Forms.TextBox();
            this.txtBoxCopy = new System.Windows.Forms.TextBox();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.buttonAnalizar = new System.Windows.Forms.Button();
            this.numWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.word = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewWords = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWords)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonCopiar
            // 
            this.buttonCopiar.Location = new System.Drawing.Point(216, 107);
            this.buttonCopiar.Name = "buttonCopiar";
            this.buttonCopiar.Size = new System.Drawing.Size(75, 23);
            this.buttonCopiar.TabIndex = 13;
            this.buttonCopiar.Text = "Copiar ->";
            this.buttonCopiar.UseVisualStyleBackColor = true;
            this.buttonCopiar.Click += new System.EventHandler(this.buttonCopiarTexto_Click);
            // 
            // txtBoxOrg
            // 
            this.txtBoxOrg.Location = new System.Drawing.Point(12, 12);
            this.txtBoxOrg.Multiline = true;
            this.txtBoxOrg.Name = "txtBoxOrg";
            this.txtBoxOrg.Size = new System.Drawing.Size(198, 241);
            this.txtBoxOrg.TabIndex = 7;
            // 
            // txtBoxCopy
            // 
            this.txtBoxCopy.Location = new System.Drawing.Point(297, 12);
            this.txtBoxCopy.Multiline = true;
            this.txtBoxCopy.Name = "txtBoxCopy";
            this.txtBoxCopy.Size = new System.Drawing.Size(196, 241);
            this.txtBoxCopy.TabIndex = 8;
            // 
            // buttonAnalizar
            // 
            this.buttonAnalizar.Location = new System.Drawing.Point(216, 136);
            this.buttonAnalizar.Name = "buttonAnalizar";
            this.buttonAnalizar.Size = new System.Drawing.Size(75, 23);
            this.buttonAnalizar.TabIndex = 14;
            this.buttonAnalizar.Text = "Analizar";
            this.buttonAnalizar.UseVisualStyleBackColor = true;
            this.buttonAnalizar.Click += new System.EventHandler(this.buttonAnalizadorLexico_Click);
            // 
            // numWord
            // 
            this.numWord.HeaderText = "Tipo";
            this.numWord.Name = "numWord";
            // 
            // word
            // 
            this.word.HeaderText = "Palabra";
            this.word.Name = "word";
            // 
            // dataGridViewWords
            // 
            this.dataGridViewWords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewWords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.word,
            this.numWord});
            this.dataGridViewWords.Location = new System.Drawing.Point(12, 262);
            this.dataGridViewWords.Name = "dataGridViewWords";
            this.dataGridViewWords.Size = new System.Drawing.Size(481, 199);
            this.dataGridViewWords.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(216, 165);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Sintactico";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonAnalizadorSintactico_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 473);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonAnalizar);
            this.Controls.Add(this.buttonCopiar);
            this.Controls.Add(this.txtBoxCopy);
            this.Controls.Add(this.txtBoxOrg);
            this.Controls.Add(this.dataGridViewWords);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonCopiar;
        private System.Windows.Forms.TextBox txtBoxOrg;
        private System.Windows.Forms.TextBox txtBoxCopy;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button buttonAnalizar;
        private System.Windows.Forms.DataGridViewTextBoxColumn numWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn word;
        private System.Windows.Forms.DataGridView dataGridViewWords;
        private System.Windows.Forms.Button button1;
    }
}

