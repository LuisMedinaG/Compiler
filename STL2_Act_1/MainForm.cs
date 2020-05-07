using System;
using System.Windows.Forms;

namespace Compiler
{
  public partial class MainForm : Form
  {
    private Lexic lexic;
    private Parser syntactic;

    public MainForm()
    {
      InitializeComponent();
    }

    private void ButtonAnalyzeLexic_Click(object sender, EventArgs e)
    {
      lexic = new Lexic(textBox_Input.Text);
      lexic.Analyse();

      table_Lexic.Rows.Clear();
      foreach(Token t in lexic.Tokens) {
        table_Lexic.Rows.Add(t.Value, t.Type);
      }
      bttn_AnalyzeSintax.Enabled = true;
    }

    private void ButtonAnalyzeSintax_Click(object sender, EventArgs e)
    {
      bttn_AnalyzeSintax.Enabled = false;

      syntactic = new Parser(lexic.Tokens);
      syntactic.table_Stack = table_Stack;

      if(syntactic.Parse()) {
        MessageBox.Show("¡Sintaxis correcta!", "Resultado");
      } else {
        MessageBox.Show("Sintaxis Incorrecta.", "Resultado");
      }
    }
  }
}