using System.Collections.Generic;

/* 
  tipo de dato 0
  id 1
  ; 2
  , 3
  ( 4
  ) 5
  { 6
  } 7
  = 8
  if 9
  while 10
  return 11
  else 12
  constante 13
  opSuma 14
  opLogico 15
  opMultiplicacion 16
  opRelacional 17
  $ 18
*/
namespace Compiler
{
  class Lexic
  {
    private int pos;
    private string input;

    internal Queue<Token> Tokens { get; set; }

    private int[][] Table;

    public Lexic(string input)
    {
      this.input = input;
      Tokens = new Queue<Token>();
    }

    internal bool Initialize_lexical_analysis()
    {

      while(pos < input.Length) {
        int state = 0;
        Token token = new Token();

        while(state != 20) {
          state = Table[state][next_character()];

          token.Value += input[pos];
        }
      }
      return false;
    }

    internal int next_character()
    {
      char c = input[pos++];
      if(isSpace(c)) return -1;
      if(isLetter(c)) return 1;
      switch(c) {
        case ';': return 2;
        case ',': return 3;
        case '(': return 4;
        case ')': return 5;
        case '{': return 6;
        case '}': return 7;
        case '=': return 8;
      }
      if(isNum(c)) return 13;
      if(esOpeSum(c)) return 14;
      if(isOpeLog(c)) return 15;
      if(isOpeMul(c)) return 16;
      if(isOpeRel(c)) return 17;
      return 20;
    }

    public void Analyse()
    {
      while(pos < input.Length) {
        int state = 0;
        Token t = new Token();

        while(state != 20 && pos < input.Length) {
          char c = input[pos];

          if(state == 0) {
            if(isLetter(c)) {
              state = 1;
            } else if(isSpace(c)) {
              pos++;
            } else if(isChar(c)) {
              state = whatChar(c);
            } else if(isNum(c)) {
              state = 13;
            } else if(esOpeSum(c)) {
              state = 14;
            } else if(isOpeLog(c)) {
              state = 15;
            } else if(isOpeMul(c)) {
              state = 16;
            } else if(isOpeRel(c)) {
              state = 17;
            }
          } else if(state == 1) {
            if(isLetter(c) || isNum(c)) {
              t.Value += input[pos++];
            } else {
              t.Type = QuePalabraReservadaEs(t.Value, state);
              state = 20;
            }
          } else if((state >= 2 && state <= 7) || (state >= 9 && state <= 12) ||
                      state == 14 || state == 16 || state == 18) {
            t.Value += input[pos++];
            t.Type = state;
            state = 20;
          } else if(state == 8) {
            if(pos + 1 < input.Length && c == input[pos + 1]) {
              t.Value += input[pos++];
              t.Type = 17;
            } else {
              t.Type = 8;
            }
            t.Value += input[pos++];
            state = 20;
          } else if(state == 13) {
            if(isNum(c)) {
              t.Value += input[pos++];
            } else {
              t.Type = state;
              state = 20;
            }
          } else if(state == 15) {
            if(pos + 1 < input.Length && c == input[pos + 1]) {
              t.Value += input[pos++];
              t.Type = state;
            } else {
              t.Type = 20;
            }
            t.Value += input[pos++];
            state = 20;
          } else if(state == 17) {
            if(input[pos + 1] == '=') {
              t.Value += input[pos++];
            }
            t.Value += input[pos++];
            t.Type = state;
            state = 20;
          }
        }
        Tokens.Enqueue(t);
      }
      Tokens.Enqueue(new Token("$", 18));
    }

    public Token NextToken()
    {
      return Tokens.Dequeue();
    }

    ///////////////////////////////////////////////////////////////////
    private int QuePalabraReservadaEs(string token, int edo)
    {
      switch(token.ToLower()) {
        case "int":
        case "char":
        case "bool":
        case "string":
        case "float": return 0;
        case "if": return 9;
        case "while": return 10;
        case "return": return 11;
        case "else": return 12;
        default:
          break;
      }
      return edo;
    }

    private static int whatChar(char c)
    {
      switch(c) {
        case ';': return 2;
        case ',': return 3;
        case '(': return 4;
        case ')': return 5;
        case '{': return 6;
        case '}': return 7;
        case '=': return 8;
      }
      return 20;
    }
    private static bool isSpace(char c) => c == '\n' || c == '\t' || c == '\r' || c == ' ';
    private static bool isLetter(char c) => c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '_';
    private static bool isNum(char c) => c >= '0' && c <= '9';
    private static bool isOpeLog(char c) => c == '&' || c == '|';
    private static bool isOpeRel(char c) => c == '<' || c == '>' || c == '!';
    private static bool isOpeMul(char c) => c == '*' || c == '/';
    private static bool esOpeSum(char c) => c == '+' || c == '-';
    private static bool isChar(char c) => c == ';' || c == ',' || c == '=' || c == '(' ||
              c == ')' || c == '{' || c == '}';
  }
}