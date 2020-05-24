using System.Collections.Generic;
using System.Linq;

namespace Compiler
{
  internal class Semantic
  {
    private Parser parser;
    private List<string> errores;
    private List<SymbolTable> symbols;
    //private ArrayList<VarDecl> declerations;
    //private ArrayList<Identifier> identifiers;
    //private ArrayList<Assign> assigns;
    //private ArrayList<Exp> conditions;

    public Semantic(Parser parser)
    {
      this.parser = parser;
      errores = new List<string>();
      CheckIdentifiers();
    }

    private void CheckIdentifiers()
    {
      var root = parser.stack.First();
      root.validatipos(symbols, errores);
    }
  }
}