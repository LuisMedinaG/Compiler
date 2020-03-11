using System.Collections.Generic;
// TODO : Change lexical analysis to hash table
namespace Compiler
{
  class Lexic
  {
    public Queue<Token> Tokens { get; set; }

    public Lexic()
    {
      Tokens = new Queue<Token>();
    }

    public void Analyse(string input)
    {
      int idx = 0;
      
      while (idx < input.Length) {
        int state = 0;
        Token t = new Token();

        while (state != 20 && idx < input.Length) {
          char c = input[idx];

          if (state == 0) {
            if (esLetra(c)) {
              state = 1;
            } else if (esEspacio(c)) {
              idx++;
            } else if (esCaracter(c)) {
              state = AsignarEstado(c);
            } else if (esNumero(c)) {
              state = 13;
            } else if (esOperSum(c)) {
              state = 14;
            } else if (esOperLogi(c)) {
              state = 15;
            } else if (esOperMult(c)) {
              state = 16;
            } else if (esOperRela(c)) {
              state = 17;
            }
          } else if (state == 1) {
            if (esLetra(c) || esNumero(c)) {
              t.Data += input[idx++];
            } else {
              t.Type = QuePalabraReservadaEs(t.Data, state);
              state = 20;
            }
          } else if ((state >= 2 && state <= 7) || (state >= 9 && state <= 12) ||
                      state == 14 || state == 16 || state == 18) {
            t.Data += input[idx++];
            t.Type = state;
            state = 20;
          } else if (state == 8) {
            if (idx + 1 < input.Length && c == input[idx + 1]) {
              t.Data += input[idx++];
              t.Type = 17;
            } else {
              t.Type = 8;
            }
            t.Data += input[idx++];
            state = 20;
          } else if (state == 13) {
            if (esNumero(c)) {
              t.Data += input[idx++];
            } else {
              t.Type = state;
              state = 20;
            }
          } else if (state == 15) {
            if (idx + 1 < input.Length && c == input[idx + 1]) {
              t.Data += input[idx++];
              t.Type = state;
            } else {
              t.Type = 20;
            }
            t.Data += input[idx++];
            state = 20;
          } else if (state == 17) {
            if (input[idx + 1] == '=') {
              t.Data += input[idx++];
            }
            t.Data += input[idx++];
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

    /* * * * PRIVATE FUNCTIONS * * * */
    private int QuePalabraReservadaEs(string token, int edo)
    {
      switch (token.ToLower()) {
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

    private int AsignarEstado(char carAct)
    {
      switch (carAct) {
        case ';': return 2;
        case ',': return 3;
        case '(': return 4;
        case ')': return 5;
        case '{': return 6;
        case '}': return 7;
        case '=': return 8;
        default: break;
      }
      return 20;
    }

    /* ------ Funciones auxiliares ------ */
    private static bool esEspacio(char c) => c == '\n' || c == '\t' || c == '\r' || c == ' ';
    private static bool esLetra(char c) => c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '_';
    private static bool esNumero(char c) => c >= '0' && c <= '9';
    private static bool esOperLogi(char c) => c == '&' || c == '|';
    private static bool esOperRela(char c) => c == '<' || c == '>' || c == '!';
    private static bool esOperMult(char c) => c == '*' || c == '/';
    private static bool esOperSum(char c) => c == '+' || c == '-';
    private static bool esCaracter(char c) => c == ';' || c == ',' || c == '=' || c == '(' ||
              c == ')' || c == '{' || c == '}';
  }
}