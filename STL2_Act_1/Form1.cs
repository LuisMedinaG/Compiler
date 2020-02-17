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

    private void buttonCopiarTexto_Click(object sender, EventArgs e)
    {
      CopiarTexto(txtBoxOrg.Text);
    }

    private void buttonAnalizadorLexico_Click(object sender, EventArgs e)
    {
      lexico = new Lexico();
      lexico.Analizar(txtBoxOrg.Text);

      dataGridViewWords.Rows.Clear();
      foreach (Token t in lexico.Tokens) {
        dataGridViewWords.Rows.Add(t.Dato, t.Estado);
      }
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
    }

    private void CopiarTexto(string texto)
    {
      string copia = ""; // split
      foreach (char c in texto) {
        if (c == ' ' || c == '\n' || c == '\t' || c == '\r') {
          txtBoxCopy.AppendText(copia + Environment.NewLine);
          copia = "";
        } else {
          copia += c;
        }
      }
      txtBoxCopy.AppendText(copia + Environment.NewLine);
    }
  }
}
