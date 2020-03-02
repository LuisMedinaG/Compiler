﻿namespace STL2_Act_1
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
      this.txtBoxCadena = new System.Windows.Forms.TextBox();
      this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
      this.bttnLexico = new System.Windows.Forms.Button();
      this.numWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.word = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.tableLexico = new System.Windows.Forms.DataGridView();
      this.bttnSintactico = new System.Windows.Forms.Button();
      this.tablePila = new System.Windows.Forms.DataGridView();
      this.Paso = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Pila = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cadenaentrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Regla = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tableLexico)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.tablePila)).BeginInit();
      this.SuspendLayout();
      // 
      // txtBoxOrg
      // 
      this.txtBoxCadena.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txtBoxCadena.Location = new System.Drawing.Point(12, 12);
      this.txtBoxCadena.Multiline = true;
      this.txtBoxCadena.Name = "txtBoxOrg";
      this.txtBoxCadena.Size = new System.Drawing.Size(274, 241);
      this.txtBoxCadena.TabIndex = 7;
      // 
      // bttnLexico
      // 
      this.bttnLexico.Location = new System.Drawing.Point(12, 259);
      this.bttnLexico.Name = "bttnLexico";
      this.bttnLexico.Size = new System.Drawing.Size(134, 49);
      this.bttnLexico.TabIndex = 14;
      this.bttnLexico.Text = "Lexico";
      this.bttnLexico.UseVisualStyleBackColor = true;
      this.bttnLexico.Click += new System.EventHandler(this.ButtonAnalizadorLexico_Click);
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
      this.tableLexico.AllowUserToAddRows = false;
      this.tableLexico.AllowUserToDeleteRows = false;
      this.tableLexico.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.tableLexico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.tableLexico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.word,
            this.numWord});
      this.tableLexico.Location = new System.Drawing.Point(12, 314);
      this.tableLexico.Name = "dataGridViewWords";
      this.tableLexico.ReadOnly = true;
      this.tableLexico.Size = new System.Drawing.Size(274, 204);
      this.tableLexico.TabIndex = 6;
      // 
      // bttnSintactico
      // 
      this.bttnSintactico.Enabled = false;
      this.bttnSintactico.Location = new System.Drawing.Point(152, 259);
      this.bttnSintactico.Name = "bttnSintactico";
      this.bttnSintactico.Size = new System.Drawing.Size(134, 49);
      this.bttnSintactico.TabIndex = 15;
      this.bttnSintactico.Text = "Sintactico";
      this.bttnSintactico.UseVisualStyleBackColor = true;
      this.bttnSintactico.Click += new System.EventHandler(this.ButtonAnalizadorSintactico_Click);
      // 
      // tablePila
      // 
      this.tablePila.AllowUserToAddRows = false;
      this.tablePila.AllowUserToDeleteRows = false;
      this.tablePila.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.tablePila.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Paso,
            this.Pila,
            this.cadenaentrada,
            this.Regla});
      this.tablePila.Location = new System.Drawing.Point(292, 12);
      this.tablePila.Name = "tablePila";
      this.tablePila.ReadOnly = true;
      this.tablePila.ShowCellErrors = false;
      this.tablePila.ShowCellToolTips = false;
      this.tablePila.ShowEditingIcon = false;
      this.tablePila.ShowRowErrors = false;
      this.tablePila.Size = new System.Drawing.Size(565, 506);
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
      this.Pila.HeaderText = "Pila";
      this.Pila.Name = "Pila";
      this.Pila.ReadOnly = true;
      // 
      // cadenaentrada
      // 
      this.cadenaentrada.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.cadenaentrada.HeaderText = "cadenaEntrada";
      this.cadenaentrada.Name = "cadenaentrada";
      this.cadenaentrada.ReadOnly = true;
      this.cadenaentrada.Width = 105;
      // 
      // Regla
      // 
      this.Regla.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
      this.Regla.HeaderText = "Regla";
      this.Regla.Name = "Regla";
      this.Regla.ReadOnly = true;
      this.Regla.Width = 60;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(869, 530);
      this.Controls.Add(this.tablePila);
      this.Controls.Add(this.bttnSintactico);
      this.Controls.Add(this.bttnLexico);
      this.Controls.Add(this.txtBoxCadena);
      this.Controls.Add(this.tableLexico);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Analizador";
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tableLexico)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.tablePila)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtBoxCadena;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button bttnLexico;
        private System.Windows.Forms.DataGridViewTextBoxColumn numWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn word;
        private System.Windows.Forms.DataGridView tableLexico;
        private System.Windows.Forms.Button bttnSintactico;
        private System.Windows.Forms.DataGridView tablePila;
        private System.Windows.Forms.DataGridViewTextBoxColumn Paso;
        private System.Windows.Forms.DataGridViewTextBoxColumn Pila;
        private System.Windows.Forms.DataGridViewTextBoxColumn cadenaentrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn Regla;
    }
}

