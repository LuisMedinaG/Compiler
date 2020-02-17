using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STL2_Act_1
{
  public partial class MainForm : Form
  {
    Lexico lexico;
    Sintactico sintactico;

    public MainForm()
    {
      InitializeComponent();
    }

    private void buttonAnalizadorLexico_Click(object sender, EventArgs e)
    {
      lexico = new Lexico();
      lexico.Analizar(txtBoxOrg.Text);

      dataGridViewWords.Rows.Clear();
      foreach (Token t in lexico.Tokens) {
        dataGridViewWords.Rows.Add(t.Dato, t.Tipo);
      }
      bttnSintactico.Enabled = true;
    }

    private void buttonAnalizadorSintactico_Click(object sender, EventArgs e)
    {
      sintactico = new Sintactico(lexico);
      sintactico.tablePila = tablePila;
      tablePila.Rows.Clear();
      if (sintactico.Analizar(txtBoxOrg.Text)) {
        MessageBox.Show("¡Sintaxis correcta!");
      } else {
        MessageBox.Show("Sintaxis Incorrecta.");
      }
      bttnSintactico.Enabled = false;
    }
  }
}
