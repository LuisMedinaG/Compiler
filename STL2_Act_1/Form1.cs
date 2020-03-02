using System;
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

    private void ButtonAnalizadorLexico_Click(object sender, EventArgs e)
    {
      lexico = new Lexico();
      lexico.Analizar(txtBoxCadena.Text);

      tableLexico.Rows.Clear();
      foreach (Token t in lexico.Tokens) {
        tableLexico.Rows.Add(t.Dato, t.Tipo);
      }
      bttnSintactico.Enabled = true;
    }

    private void ButtonAnalizadorSintactico_Click(object sender, EventArgs e)
    {
      sintactico = new Sintactico(lexico) {
        tablePila = tablePila
      };
      tablePila.Rows.Clear();
      if (sintactico.Analizar()) {
        MessageBox.Show("¡Sintaxis correcta!");
      } else {
        MessageBox.Show("Sintaxis Incorrecta.");
      }
      bttnSintactico.Enabled = false;
    }
  }
}
