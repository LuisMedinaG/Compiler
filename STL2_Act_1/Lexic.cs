using System.Collections.Generic;


namespace Compiler
{
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
  
  class Lexic
  {
    private int pos;
    private string text;
    //private int[][] Table;

    internal Queue<Token> tokens { get; set; }

    public Lexic(string _text)
    {
      tokens = new Queue<Token>();
      text = _text;
      Analyse();
    }

    //internal bool Initialize_lexical_analysis()
    //{
    //  while(pos < input.Length) {
    //    int state = 0;
    //    Token token = new Token();

    //    while(state != 20) {
    //      state = Table[state][next_character()];

    //      token.Value += input[pos];
    //    }
    //  }
    //  return true;
    //}

    internal int next_character()
    {
      char c = text[pos++];
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
      while(pos < text.Length) {
        int state = 0;
        Token t = new Token();

        while(state != 20 && pos < text.Length) {
          char c = text[pos];

          if(state == 0) {
            if(isLetter(c)) {
              state = 1;
            } else if(isSpace(c)) {
              pos++;
            } else if(isChar(c)) {
              state = WhatChar(c);
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
              t.value += text[pos++];
            } else {
              t.type = QuePalabraReservadaEs(t.value, state);
              state = 20;
            }
          } else if((state >= 2 && state <= 7) || (state >= 9 && state <= 12) ||
                      state == 14 || state == 16 || state == 18) {
            t.value += text[pos++];
            t.type = state;
            state = 20;
          } else if(state == 8) {
            if(pos + 1 < text.Length && c == text[pos + 1]) {
              t.value += text[pos++];
              t.type = 17;
            } else {
              t.type = 8;
            }
            t.value += text[pos++];
            state = 20;
          } else if(state == 13) {
            if(isNum(c)) {
              t.value += text[pos++];
            } else {
              t.type = state;
              state = 20;
            }
          } else if(state == 15) {
            if(pos + 1 < text.Length && c == text[pos + 1]) {
              t.value += text[pos++];
              t.type = state;
            } else {
              t.type = 20;
            }
            t.value += text[pos++];
            state = 20;
          } else if(state == 17) {
            if(text[pos + 1] == '=') {
              t.value += text[pos++];
            }
            t.value += text[pos++];
            t.type = state;
            state = 20;
          }
        }
        tokens.Enqueue(t);
      }
      tokens.Enqueue(new Token("$", 18));
    }

    public Token NextToken()
    {
      return tokens.Dequeue();
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

    private static int WhatChar(char c)
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