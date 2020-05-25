using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Compiler
{
  class Parser
  {
    public Lexic lexer;
    public Token token;
    public Stack<Node> stack;

    private List<string> errors;
    private List<TableSymbol> SymbolTable;


    readonly List<Rule> grammarRules;
    readonly List<string[]> actionTable;

    const string tableFile = "GR2slrTableBien.txt";
    const string rulesFile = "GR2slrRulesId.txt";

    internal DataGridView tableStack;
    string ruleStr;

    public Parser(Lexic _lexer, DataGridView _tableStack)
    {
      lexer = _lexer;
      token = _lexer.tokens.Dequeue();
      tableStack = _tableStack;
      stack = new Stack<Node>();
      actionTable = new List<string[]>();
      grammarRules = new List<Rule>();

      BuildActionTable();
      BuildGrammarRules();
    }

    public Node Parse()
    {
      tableStack.Rows.Clear();
      PrintStack();

      if(actionTable.Count == 0 || grammarRules.Count == 0) return null;

      stack.Push(new Node(0));
      while(stack.Count > 0) {
        int state = stack.Peek().state; // State on top of stack
        int t = ACTION_GOTO(state, token.type); // Check grammar table

        if(t > 0) {
          /* SHIFT */
          stack.Push(new Node(token));
          stack.Push(new Node(t));
          token = lexer.tokens.Dequeue();
        } else if(t < -1) {
          /* REDUCE */
          Rule rule = grammarRules[Math.Abs(t) - 1];
          Node node = ReduceStack(rule);

          state = ACTION_GOTO(stack.Peek().state, rule.Column);
          
          stack.Push(node);
          stack.Push(new Node(state));
          ruleStr = rule.Detail; // For printing
        } else if(t == -1) {
          /* ACCEPT */
          stack.Pop();
          return stack.Pop(); // Return the root of tree
        } else {
          /* ERROR */
          return null;
        }
        PrintStack();
      }
      return null;
    }

    private int ACTION_GOTO(int state, int token)
    {
      if(int.TryParse(actionTable[state][token + 1], out int action))
        return action;
      return 0;
    }

    private Node ReduceStack(Rule rule)
    {
      Node node = new Node(rule);
      Node aux;

      switch(rule.Id) {
        case 0:  //<programa> ::= <Definiciones>
          node = new Programa(stack);
          break;
        case 2://<Definiciones> ::= <Definicion> <Definiciones> 
        case 15://<DefLocales> ::= <DefLocal> <DefLocales> 
        case 19://<Sentencias> ::= <Sentencia> <Sentencias>
        case 29://<Argumentos> ::= <Expresion> <ListaArgumentos> 
          stack.Pop();
          aux = stack.Pop();
          stack.Pop();
          node = stack.Pop();
          node.next = aux;
          break;
        case 3://<Definicion> ::= <DefVar>
        case 4://<Definicion> ::= <DefFunc> 
        case 16://<DefLocal> ::= <DefVar> 
        case 17://<DefLocal> ::= <Sentencia>
        case 33://<Expresion> ::= <Id>
        case 34://<Expresion> ::= <Constante>
        case 32://<Expresion> ::= <LlamadaFunc> 
        case 36://<SentenciaBloque> ::= <Sentencia> 
        case 37://<SentenciaBloque> ::= <Bloque> 
          stack.Pop();
          node = stack.Pop();
          break;
        case 5:// <DefVar> ::= tipo id <ListaVar> ;
          node = new DefVar(stack);
          break;
        case 7://<ListaVar> ::= , id <ListaVar>
          stack.Pop(); //Quita estado
          Node lvar = stack.Pop(); //Quita ListaVar
          stack.Pop(); //Quita estado
          node = new Id(stack.Pop().symbol); //Quita Id
          node.next = lvar;
          stack.Pop(); //Quita estado;
          stack.Pop();//Quita ,
          break;
        case 8://<DefFunc> ::= tipo id ( <Parametros> ) <BloqFunc>
          node = new DefFunc(stack);
          //node.validatipos(SymbolTable, errors);
          break;
        case 10://<Parametros> ::= tipo id <ListaParam>
          node = new Parametros(stack);
          break;
        case 12://<ListaParam> ::= , tipo id <ListaParam>
          node = new Parametros(stack);
          stack.Pop();//quita estado;
          stack.Pop();//quita la coma
          break;
        case 13://<BloqFunc> ::= { <DefLocales> }
        case 27://<Bloque> ::= { <Sentencias> } 
        case 38://<Expresion> ::= ( <Expresion> ) 
          stack.Pop();//quita estado
          stack.Pop();//quita }
          stack.Pop();//quita estado
          node = stack.Pop();//quita <deflocales> o <sentencias>
          stack.Pop();
          stack.Pop();//quita la {
          break;
        case 20: //<Sentencia> ::= id = <Expresion> ;
          node = new Asignacion(stack);
          break;
        case 21://<Sentencia> ::= if ( <Expresion> ) <SentenciaBloque> <Otro>
          node = new If(stack);
          break;
        case 22://<Sentencia> ::= while ( <Expresion> ) <Bloque> 
          node = new While(stack);
          break;
        case 23://<Sentencia> ::= return <Expresion> ;
          node = new Return(stack);
          break;
        case 24://<Sentencia> ::= <LlamadaFunc> ; 
          stack.Pop();
          stack.Pop(); //quita ;
          stack.Pop();
          node = stack.Pop();//quita llamadafunc
          break;
        case 26://<Otro> ::= else <SentenciaBloque> 
          stack.Pop();
          node = stack.Pop(); //quita sentencia bloque
          stack.Pop();
          stack.Pop(); //quita el else
          break;
        case 31:// <ListaArgumentos> ::= , <Expresion> <ListaArgumentos> 
          stack.Pop();
          aux = stack.Pop();//quita la lsta de argumentos
          stack.Pop();
          node = stack.Pop();//quita expresion
          stack.Pop();
          stack.Pop();//quita la ,
          node.next = aux;
          break;
        case 35:
          node = new Llamadafunc(stack);
          break;
        case 39: // Expresion -> Expresion opSuma Expresion
        case 40: // Expresion -> Expresion opLogico Expresion
        case 41: // Expresion -> Expresion opMul Expresion
        case 42: // Expresion -> Expresion opRelac Expresion
          node = new Expresion(stack);
          break;
        // Aqui cae R1,R6,R9,R11,R14,R18,R25,R28,R30
        default: // Ningun pop
          break;
      }
      return node;
    }

    /**************************** Auxiliar functions *******************************/
    private void PrintStack()
    {
      string input = GetInput();
      string queue = "";

      foreach(Node n in stack) {
        if(n.type != null)
          queue = $"{n.type}, {queue}";
        else if(n.symbol != null)
          queue = $"{n.symbol.value}, {queue}";
        else
          queue = $"{n.state}, {queue}";
      }
      tableStack.Rows.Add(tableStack.Rows.Count + 1, queue, input, ruleStr);
      ruleStr = "";
    }

    private string GetInput()
    {
      string input = "";
      foreach(Token t in lexer.tokens) {
        input += t.value + ' ';
      }
      return input;
    }

    private void BuildActionTable()
    {
      if(File.Exists(tableFile)) {
        string[] lines = File.ReadAllLines(tableFile);
        foreach(string line in lines) {
          actionTable.Add(line.Split(null));
        }
      }
    }

    private void BuildGrammarRules()
    {
      if(File.Exists(rulesFile)) {
        string[] lines = File.ReadAllLines(rulesFile);

        int id = 0;
        foreach(string line in lines) {
          string[] rule = line.Split(null);
          string detail = "";
          for(int i = 2; i < rule.Length; i++) {
            detail += rule[i] + ' ';
          }
          _ = int.TryParse(rule[0], out int column);
          _ = int.TryParse(rule[1], out int popNum);
          grammarRules.Add(new Rule(id++, column, popNum, detail, rule[2]));
        }
      }
    }

  }
}
