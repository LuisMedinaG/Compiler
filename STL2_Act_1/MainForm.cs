using System;
using System.Windows.Forms;

namespace Compiler
{
  public partial class MainForm : Form
  {
    Lexic lexic;
    Parser syntactic;

    public MainForm()
    {
      InitializeComponent();
    }

    private void ButtonAnalyzeLexic_Click(object sender, EventArgs e)
    {
      lexic = new Lexic();
      lexic.Analyse(textBox_Input.Text);

      table_Lexic.Rows.Clear();
      foreach (Token t in lexic.Tokens) {
        table_Lexic.Rows.Add(t.Data, t.Type);
      }
      bttn_AnalyzeSintax.Enabled = true;
    }

    private void ButtonAnalyzeSintax_Click(object sender, EventArgs e)
    {
      syntactic = new Parser(lexic.Tokens) {
        table_Stack = table_Stack
      };
      table_Stack.Rows.Clear();
      if (syntactic.Analyze()) {
        MessageBox.Show("¡Sintaxis correcta!", "Resultado");
      } else {
        MessageBox.Show("Sintaxis Incorrecta.", "Resultado");
      }
      bttn_AnalyzeSintax.Enabled = false;
    }
  }
}
