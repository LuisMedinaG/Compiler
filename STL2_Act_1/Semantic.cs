using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Compiler
{
  internal class Semantic
  {
    private List<string> errors;
    private List<TableSymbol> symbolTable; // TODO: Change structure to a STACK
    // TODO: Implement a scope counter

    public Semantic(Node tree)
    {
      errors = new List<string>();
      symbolTable = new List<TableSymbol>();

      if(tree == null) 
        errors.Add("Error de sintaxis. Arbol nulo.");
      else 
        tree.validatipos(symbolTable, errors);

      if(errors.Count > 0) 
        printErrors();
      else 
        MessageBox.Show("Compilacion exitosa.", "Resultado");
    }

    private void printErrors()
    {
      string output = "";
      for(int i = 0; i < errors.Count; i++) {
        output += (i + 1) + ". " + errors[i] + Environment.NewLine;
      }
      MessageBox.Show(output, "Errores");
    }
  }
}