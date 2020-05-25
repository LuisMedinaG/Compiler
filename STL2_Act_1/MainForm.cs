using System;
using System.Windows.Forms;

namespace Compiler
{
  public partial class MainForm : Form
  {
    Lexic lexer;
    Parser parser;
    Semantic semantic;
    Node tree;

    public MainForm()
    {
      InitializeComponent();
    }

    private void ButtonAnalyzeLexic_Click(object sender, EventArgs e)
    {
      lexer = new Lexic(input.Text);
      printTokens();
      parser = new Parser(lexer, table_Stack);
      tree = parser.Parse();
      semantic = new Semantic(tree);
    }

    private void printTokens()
    {
      table_Lexic.Rows.Clear();
      foreach(Token t in lexer.tokens) {
        table_Lexic.Rows.Add(t.value, t.type);
      }
    }
  }
}