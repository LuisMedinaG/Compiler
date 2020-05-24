using System;
using System.Windows.Forms;

namespace Compiler
{
  public partial class MainForm : Form
  {
    private Interpreter interpreter;

    public MainForm()
    {
      InitializeComponent();
    }

    private void ButtonAnalyzeLexic_Click(object sender, EventArgs e)
    {
      interpreter = new Interpreter(input.Text);

      //table_Lexic.Rows.Clear();
      //foreach(Token t in lexic.tokens) {
      //  table_Lexic.Rows.Add(t.Value, t.Type);
      //}

      //parser =  {
      //  TableStack = table_Stack
      //};

      //if(parser.Parse()) {
      //  MessageBox.Show("¡Sintaxis correcta!", "Resultado");
      //} else {
      //  MessageBox.Show("Sintaxis Incorrecta.", "Resultado");
      //}
    }
  } 
}