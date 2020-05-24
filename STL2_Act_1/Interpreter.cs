using System;

namespace Compiler
{
  class Interpreter
  {
    public Lexic lexer;
    public Parser parser;
    public Semantic semantic;

    public Interpreter(string text)
    {
      lexer = new Lexic(text);
      parser = new Parser(lexer);
      semantic = new Semantic(parser);
    }
  }
}
