using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace STL2_Act_1
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

      // First element in stack
      stack.Push(new Node("0", 0));

      BuildAnalysisTable();
      BuildGrammarRules();
    }

    public bool Analyze()
    {
      Token token = tokens.Dequeue();
      while (stack.Count > 0) {
        PRINT_STACK();

        var transition = stack.Peek().Symbol; 
        int action = ACTION(transition, token.Type);

        if (action == -1) {
          /***** ACCEPT *****/
          return true;
        } else if (action == 0) {
          /***** ERROR *****/
          return false;
        } else if (action > 0) {
          /***** SHIFT *****/
          Node node = new Node(token.Type.ToString(), action);

          //stack.Push(token.Type);
          //stack.Push(node);
          //stack.Push(action);
          token = tokens.Dequeue();
        } else if (action < 0) {
          /***** REDUCTION *****/
          int ruleIdx = Math.Abs(action) - 1;
          var rule = rules[ruleIdx];
          
          /*************** TODO *****************/
          int pops = rule.Item2 * 2;
          // Definimos el texto de la regla
          ruleDetail = ruleDetails[ruleIdx];

          // TODO : defirnir el numero de pops dependiendo de la reduccion
          for (int i = 0; i < pops; i++) {
            if (stack.Count == 0) {
              return false;
            }
            stack.Pop();
          }
          /**************************************/

          //int row = stack.Peek();
          int col = rule.Item1;
          //stack.Push(col);
          //stack.Push(ACTION(row, col));
        }
      }
      return false;
    }

    private int ACTION(string transition, int symbol)
    {
      int.TryParse(transition, out int row);
      int.TryParse(table[row][symbol + 1], out int action);
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
      foreach (Token t in tokens) {
        currString += t.Data + ' ';
      }
      return currString;
    }

    private void BuildGrammarRules()
    {
      if (File.Exists(rulesFile)) {
        string[] lines = File.ReadAllLines(rulesFile);

        foreach (string line in lines) {
          string[] regla = line.Split(null);
          string reglaStr = "";
          for (int i = 2; i < regla.Length; i++) {
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
      if (File.Exists(tableFile)) {
        string[] lines = File.ReadAllLines(tableFile);
        foreach (string line in lines) {
          table.Add(line.Split(null));
        }
      }
    }
  }
}
