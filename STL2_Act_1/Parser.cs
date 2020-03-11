using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Compiler
{
  class Parser
  {
    readonly Queue<Token> tokens;
    readonly Stack<Node> stack;

    readonly List<string[]> table;
    readonly List<Tuple<int, int>> rules;

    const string tableFile = "GR2slrTable.txt";
    const string rulesFile = "GR2slrRulesId.txt";

    internal DataGridView table_Stack;
    List<string> ruleDetails;
    string ruleDetail;

    public Parser(Queue<Token> tokens)
    {
      this.tokens = tokens;

      stack = new Stack<Node>();
      table = new List<string[]>();
      rules = new List<Tuple<int, int>>();
      ruleDetails = new List<string>();

      stack.Push(new Node(0));

      BuildAnalysisTable();
      BuildGrammarRules();
    }

    public bool Analyze()
    {
      Token token = tokens.Dequeue();
      while(stack.Count > 0) {
        PRINT_STACK();

        int transition = stack.Peek().State;
        int action = ACTION(transition, token.Type);

        if(action == -1) {
          /***** ACCEPT *****/
          return true;
        } else if(action == 0) {
          /***** ERROR *****/
          return false;
        } else if(action > 0) {
          /***** SHIFT *****/
          stack.Push(new Node(token.Type));
          stack.Push(new Node(token));
          token = tokens.Dequeue();
        } else if(action < 0) {
          /***** REDUCTION *****/
          int rule = Math.Abs(action);
          Node node = RuleConstructor(rule);

          int row = stack.Peek().State;
          int col = rules[rule].Item1;
          transition = ACTION(row, col);

          stack.Push(node);
          stack.Push(new Node(transition));
        }
      }
      return false;
    }

    private Node RuleConstructor(int rule)
    {
      Node node = new Node();
      switch(rule) {
        case 1:  //<programa> ::= <Definiciones>
          node = new Programa(stack);
          break;
        case 3://<Definiciones> ::= <Definicion> <Definiciones> 
        case 16://<DefLocales> ::= <DefLocal> <DefLocales> 
        case 20://<Sentencias> ::= <Sentencia> <Sentencias>
        case 30://<Argumentos> ::= <Expresion> <ListaArgumentos> 
          stack.Pop();//quita estado
          Node aux = stack.Pop();//quita <definiciones>
          stack.Pop();//quita estado
          node = (stack.Pop());//quita <definicion>
          node.Next = aux;
          break;
        case 4://<Definicion> ::= <DefVar>
        case 5://<Definicion> ::= <DefFunc> 
        case 17://<DefLocal> ::= <DefVar> 
        case 18://<DefLocal> ::= <Sentencia> 
        // case 35://<Expresion> ::= <LlamadaFunc> 
        case 37://<SentenciaBloque> ::= <Sentencia> 
        case 38://<SentenciaBloque> ::= <Bloque> 
          stack.Pop();//quita estado
          node = stack.Pop();//quita defvar
          break;
        case 6:// <DefVar> ::= tipo id <ListaVar> ;
          node = new DefVar(stack);
          break;
        case 8://<ListaVar> ::= , id <ListaVar>
          stack.Pop();//quita estado
          Node leftVar = stack.Pop();
          stack.Pop();//quita estado
          node = new Id(stack.Pop().token);//quita id
          node.Next = leftVar;
          stack.Pop();//quita estado
          stack.Pop();//quita la coma
          break;
        case 9://<DefFunc> ::= tipo id ( <Parametros> ) <BloqFunc>
          node = new DefFunc(stack);
          break;
        case 11://<Parametros> ::= tipo id <ListaParam>
          node = new Parametros(stack);
          break;
        case 13://<ListaParam> ::= , tipo id <ListaParam>
          node = new Parametros(stack);
          stack.Pop();//quita estado;
          stack.Pop();//quita la coma
          break;
        case 14://<BloqFunc> ::= { <DefLocales> }
        case 28://<Bloque> ::= { <Sentencias> } 
        case 39://<Expresion> ::= ( <Expresion> ) 
          stack.Pop();//quita estado
          stack.Pop();//quita }
          stack.Pop();//quita estado
          node = (stack.Pop());//quita <deflocales> o <sentencias>
          stack.Pop();
          stack.Pop();//quita la {
          break;
        case 21: //<Sentencia> ::= id = <Expresion> ;
          node = new Asignacion(stack);
          break;
        case 22://<Sentencia> ::= if ( <Expresion> ) <SentenciaBloque> <Otro>
          //node = new If(stack) // TODO : Agregar la clase para If con su constructor
          break;
        case 23://<Sentencia> ::= while ( <Expresion> ) <Bloque> 
          node = new While(stack);
          break;
        case 24://<Sentencia> ::= return <Expresion> ;
          // node = new Return(stack);
          break;
        case 25://<Sentencia> ::= <LlamadaFunc> ; 
          stack.Pop();
          stack.Pop();//quita ;
          stack.Pop();
          node = stack.Pop();//quita llamadafunc
          break;
        case 27://<Otro> ::= else <SentenciaBloque> 
          stack.Pop();
          node = (stack.Pop());//quita sentencia bloque
          stack.Pop();
          stack.Pop();//quita el else
          break;
        case 32:// <ListaArgumentos> ::= , <Expresion> <ListaArgumentos> 
          stack.Pop();
          aux = (stack.Pop());//quita la lsta de argumentos
          stack.Pop();
          node = (stack.Pop());//quita expresion
          stack.Pop();
          stack.Pop();//quita la ,
          node.Next = aux;
          break;
        //case 33:
        //case 34:
        //case 36:
        //case 40:
        //case 41:
        //case 42:
        //case 43:
        //aqui cae R2,R7,R10,R12,R15,R19,R26,R29,R31
        default:
          for(int i = 0; i < rules[rule].Item2 * 2; i++) {
            if(stack.Count == 0) {/*return false;*/}
            stack.Pop();
          }
          break;
      }
      ruleDetail = ruleDetails[rule];
      return node;
    }

    private int ACTION(int state, int token)
    {
      int.TryParse(table[state][token + 1], out int action);
      return action;
    }

    /**************************** Auxiliar functions *******************************/
    private void PRINT_STACK()
    {
      string currString = GET_CADENA();
      string queue = "";
      /*
      foreach (int p in stack) {
        queue = $"{p}, {queue}";
      }
      */
      table_Stack.Rows.Add(table_Stack.Rows.Count + 1, queue, currString, ruleDetail);
      ruleDetail = "";
    }

    private string GET_CADENA()
    {
      string currString = "";
      foreach(Token t in tokens) {
        currString += t.Data + ' ';
      }
      return currString;
    }

    private void BuildGrammarRules()
    {
      if(File.Exists(rulesFile)) {
        string[] lines = File.ReadAllLines(rulesFile);

        foreach(string line in lines) {
          string[] regla = line.Split(null);
          string reglaStr = "";
          for(int i = 2; i < regla.Length; i++) {
            reglaStr += regla[i] + ' ';
          }
          ruleDetails.Add(reglaStr);
          _ = int.TryParse(regla[0], out int rule1);
          int.TryParse(regla[1], out int rule2);
          rules.Add(new Tuple<int, int>(rule1, rule2));
        }
      }
    }

    private void BuildAnalysisTable()
    {
      if(File.Exists(tableFile)) {
        string[] lines = File.ReadAllLines(tableFile);
        foreach(string line in lines) {
          table.Add(line.Split(null));
        }
      }
    }
  }
}
