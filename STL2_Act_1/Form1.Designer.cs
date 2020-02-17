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
      this.tablePila = new System.Windows.Forms.DataGridView();
      this.Paso = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Pila = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cadenaentrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWords)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tablePila)).BeginInit();
      this.SuspendLayout();
      // 
      // buttonCopiar
      // 
      this.buttonCopiar.Location = new System.Drawing.Point(12, 259);
      this.buttonCopiar.Name = "buttonCopiar";
      this.buttonCopiar.Size = new System.Drawing.Size(147, 49);
      this.buttonCopiar.TabIndex = 13;
      this.buttonCopiar.Text = "Copiar";
      this.buttonCopiar.UseVisualStyleBackColor = true;
      this.buttonCopiar.Click += new System.EventHandler(this.buttonCopiarTexto_Click);
      // 
      // txtBoxOrg
      // 
      this.txtBoxOrg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtBoxOrg.Location = new System.Drawing.Point(12, 12);
      this.txtBoxOrg.Multiline = true;
      this.txtBoxOrg.Name = "txtBoxOrg";
      this.txtBoxOrg.Size = new System.Drawing.Size(221, 241);
      this.txtBoxOrg.TabIndex = 7;
      // 
      // txtBoxCopy
      // 
      this.txtBoxCopy.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtBoxCopy.Location = new System.Drawing.Point(244, 12);
      this.txtBoxCopy.Multiline = true;
      this.txtBoxCopy.Name = "txtBoxCopy";
      this.txtBoxCopy.Size = new System.Drawing.Size(221, 241);
      this.txtBoxCopy.TabIndex = 8;
      // 
      // buttonAnalizar
      // 
      this.buttonAnalizar.Location = new System.Drawing.Point(165, 259);
      this.buttonAnalizar.Name = "buttonAnalizar";
      this.buttonAnalizar.Size = new System.Drawing.Size(147, 49);
      this.buttonAnalizar.TabIndex = 14;
      this.buttonAnalizar.Text = "Lexico";
      this.buttonAnalizar.UseVisualStyleBackColor = true;
      this.buttonAnalizar.Click += new System.EventHandler(this.buttonAnalizadorLexico_Click);
      // 
      // numWord
      // 
      this.numWord.HeaderText = "Tipo";
      this.numWord.Name = "numWord";
      this.numWord.ReadOnly = true;
      // 
      // word
      // 
      this.word.HeaderText = "Palabra";
      this.word.Name = "word";
      this.word.ReadOnly = true;
      // 
      // dataGridViewWords
      // 
      this.dataGridViewWords.AllowUserToAddRows = false;
      this.dataGridViewWords.AllowUserToDeleteRows = false;
      this.dataGridViewWords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridViewWords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewWords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.word,
            this.numWord});
      this.dataGridViewWords.Location = new System.Drawing.Point(12, 314);
      this.dataGridViewWords.Name = "dataGridViewWords";
      this.dataGridViewWords.ReadOnly = true;
      this.dataGridViewWords.Size = new System.Drawing.Size(453, 204);
      this.dataGridViewWords.TabIndex = 6;
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(318, 259);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(147, 49);
      this.button1.TabIndex = 15;
      this.button1.Text = "Sintactico";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.buttonAnalizadorSintactico_Click);
      // 
      // tablePila
      // 
      this.tablePila.AllowUserToAddRows = false;
      this.tablePila.AllowUserToDeleteRows = false;
      this.tablePila.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.tablePila.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Paso,
            this.Pila,
            this.cadenaentrada});
      this.tablePila.Location = new System.Drawing.Point(471, 12);
      this.tablePila.Name = "tablePila";
      this.tablePila.ReadOnly = true;
      this.tablePila.ShowCellErrors = false;
      this.tablePila.ShowCellToolTips = false;
      this.tablePila.ShowEditingIcon = false;
      this.tablePila.ShowRowErrors = false;
      this.tablePila.Size = new System.Drawing.Size(355, 506);
      this.tablePila.TabIndex = 16;
      // 
      // Paso
      // 
      this.Paso.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Paso.HeaderText = "Paso";
      this.Paso.MaxInputLength = 5;
      this.Paso.Name = "Paso";
      this.Paso.ReadOnly = true;
      this.Paso.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.Paso.Width = 56;
      // 
      // Pila
      // 
      this.Pila.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Pila.HeaderText = "Pila";
      this.Pila.Name = "Pila";
      this.Pila.ReadOnly = true;
      this.Pila.Width = 49;
      // 
      // cadenaentrada
      // 
      this.cadenaentrada.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.cadenaentrada.HeaderText = "cadenaEntrada";
      this.cadenaentrada.Name = "cadenaentrada";
      this.cadenaentrada.ReadOnly = true;
      this.cadenaentrada.Width = 105;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(838, 530);
      this.Controls.Add(this.tablePila);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.buttonAnalizar);
      this.Controls.Add(this.buttonCopiar);
      this.Controls.Add(this.txtBoxCopy);
      this.Controls.Add(this.txtBoxOrg);
      this.Controls.Add(this.dataGridViewWords);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Analizador";
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.dataGridViewWords)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tablePila)).EndInit();
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
        private System.Windows.Forms.DataGridView tablePila;
        private System.Windows.Forms.DataGridViewTextBoxColumn Paso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pila;
        private System.Windows.Forms.DataGridViewTextBoxColumn cadenaentrada;
    }
}

