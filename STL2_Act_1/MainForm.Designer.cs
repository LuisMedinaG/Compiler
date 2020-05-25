namespace Compiler
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
      this.input = new System.Windows.Forms.TextBox();
      this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
      this.bttnLexico = new System.Windows.Forms.Button();
      this.numWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.word = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.table_Lexic = new System.Windows.Forms.DataGridView();
      this.table_Stack = new System.Windows.Forms.DataGridView();
      this.Paso = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Pila = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.cadenaentrada = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Regla = new System.Windows.Forms.DataGridViewTextBoxColumn();
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.table_Lexic)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.table_Stack)).BeginInit();
      this.SuspendLayout();
      // 
      // input
      // 
      this.input.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.input.Location = new System.Drawing.Point(12, 67);
      this.input.Multiline = true;
      this.input.Name = "input";
      this.input.Size = new System.Drawing.Size(225, 441);
      this.input.TabIndex = 7;
      // 
      // bttnLexico
      // 
      this.bttnLexico.Location = new System.Drawing.Point(12, 12);
      this.bttnLexico.Name = "bttnLexico";
      this.bttnLexico.Size = new System.Drawing.Size(225, 49);
      this.bttnLexico.TabIndex = 14;
      this.bttnLexico.Text = "Compilar";
      this.bttnLexico.UseVisualStyleBackColor = true;
      this.bttnLexico.Click += new System.EventHandler(this.ButtonAnalyzeLexic_Click);
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
      // table_Lexic
      // 
      this.table_Lexic.AllowUserToAddRows = false;
      this.table_Lexic.AllowUserToDeleteRows = false;
      this.table_Lexic.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
      this.table_Lexic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.table_Lexic.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.word,
            this.numWord});
      this.table_Lexic.Location = new System.Drawing.Point(243, 12);
      this.table_Lexic.Name = "table_Lexic";
      this.table_Lexic.ReadOnly = true;
      this.table_Lexic.RowHeadersVisible = false;
      this.table_Lexic.Size = new System.Drawing.Size(207, 496);
      this.table_Lexic.TabIndex = 6;
      // 
      // table_Stack
      // 
      this.table_Stack.AllowUserToAddRows = false;
      this.table_Stack.AllowUserToDeleteRows = false;
      this.table_Stack.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.table_Stack.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Paso,
            this.Pila,
            this.cadenaentrada,
            this.Regla});
      this.table_Stack.Location = new System.Drawing.Point(456, 12);
      this.table_Stack.Name = "table_Stack";
      this.table_Stack.ReadOnly = true;
      this.table_Stack.RowHeadersVisible = false;
      this.table_Stack.ShowCellErrors = false;
      this.table_Stack.ShowCellToolTips = false;
      this.table_Stack.ShowEditingIcon = false;
      this.table_Stack.ShowRowErrors = false;
      this.table_Stack.Size = new System.Drawing.Size(499, 496);
      this.table_Stack.TabIndex = 16;
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
      this.Pila.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Pila.HeaderText = "Pila";
      this.Pila.Name = "Pila";
      this.Pila.ReadOnly = true;
      // 
      // cadenaentrada
      // 
      this.cadenaentrada.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.cadenaentrada.HeaderText = "cadenaEntrada";
      this.cadenaentrada.Name = "cadenaentrada";
      this.cadenaentrada.ReadOnly = true;
      // 
      // Regla
      // 
      this.Regla.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
      this.Regla.HeaderText = "Regla";
      this.Regla.Name = "Regla";
      this.Regla.ReadOnly = true;
      // 
      // MainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(967, 520);
      this.Controls.Add(this.table_Stack);
      this.Controls.Add(this.bttnLexico);
      this.Controls.Add(this.input);
      this.Controls.Add(this.table_Lexic);
      this.Name = "MainForm";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Analizador";
      ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.table_Lexic)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.table_Stack)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button bttnLexico;
        private System.Windows.Forms.DataGridViewTextBoxColumn numWord;
        private System.Windows.Forms.DataGridViewTextBoxColumn word;
        private System.Windows.Forms.DataGridView table_Lexic;
        public System.Windows.Forms.DataGridView table_Stack;
    private System.Windows.Forms.DataGridViewTextBoxColumn Paso;
    private System.Windows.Forms.DataGridViewTextBoxColumn Pila;
    private System.Windows.Forms.DataGridViewTextBoxColumn cadenaentrada;
    private System.Windows.Forms.DataGridViewTextBoxColumn Regla;
  }
}

