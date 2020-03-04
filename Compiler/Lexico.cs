﻿using System.Collections.Generic;
// TODO : Change lexical analysis to hash table
namespace STL2_Act_1
{
  class Token
  {
    public string Dato { get; set; }
    public int Tipo { get; set; }

    public Token()
    {
      Dato = ""; Tipo = 0;
    }
    public Token(string Dato, int Tipo)
    {
      this.Dato = Dato; this.Tipo = Tipo;
    }
  }

  class Lexico
  {
    public Queue<Token> Tokens { get; set; }

    public Lexico()
    {
      Tokens = new Queue<Token>();
    }

    public void Analizar(string cadena)
    {
      Token t;
      int idx = 0;
      int estado;
      char carAct;
      string token;

      while (idx < cadena.Length) {
        t = new Token();
        token = "";
        estado = 0;
        while (estado != 20 && idx < cadena.Length) {
          carAct = cadena[idx];
          if (estado == 0) {
            if (esLetra(carAct)) {
              estado = 1;
            } else if (esEspacio(carAct)) {
              idx++;
            } else if (esCaracter(carAct)) {
              estado = AsignarEstado(carAct);
            } else if (esNumero(carAct)) {
              estado = 13;
            } else if (esOperSum(carAct)) {
              estado = 14;
            } else if (esOperLogi(carAct)) {
              estado = 15;
            } else if (esOperMult(carAct)) {
              estado = 16;
            } else if (esOperRela(carAct)) {
              estado = 17;
            }
          } else if (estado == 1) {
            if (esLetra(carAct) || esNumero(carAct)) {
              token += cadena[idx++];
            } else {
              t.Tipo = QuePalabraReservadaEs(token, estado);
              estado = 20;
            }
          } else if ((estado >= 2 && estado <= 7) || (estado >= 9 && estado <= 12) ||
                      estado == 14 || estado == 16 || estado == 18) {
            token += cadena[idx++];
            t.Tipo = estado;
            estado = 20;
          } else if (estado == 8) {
            if (idx + 1 < cadena.Length && carAct == cadena[idx + 1]) {
              token += cadena[idx++];
              t.Tipo = 17;
            } else {
              t.Tipo = 8;
            }
            token += cadena[idx++];
            estado = 20;
          } else if (estado == 13) {
            if (esNumero(carAct)) {
              token += cadena[idx++];
            } else {
              t.Tipo = estado;
              estado = 20;
            }
          } else if (estado == 15) {
            if (idx + 1 < cadena.Length && carAct == cadena[idx + 1]) {
              token += cadena[idx++];
              t.Tipo = estado;
            } else {
              t.Tipo = 20;
            }
            token += cadena[idx++];
            estado = 20;
          } else if (estado == 17) {
            if (cadena[idx + 1] == '=') {
              token += cadena[idx++];
            }
            token += cadena[idx++];
            t.Tipo = estado;
            estado = 20;
          }
        }
        t.Dato = token;
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
    private bool esEspacio(char c)
    {
      return c == '\n' || c == '\t' || c == '\r' || c == ' ';
    }
    private bool esLetra(char c)
    {
      return c >= 'a' && c <= 'z' || c >= 'A' && c <= 'Z' || c == '_';
    }
    private bool esNumero(char c)
    {
      return c >= '0' && c <= '9';
    }
    private bool esOperLogi(char c)
    {
      return c == '&' || c == '|';
    }
    private bool esOperRela(char c)
    {
      return c == '<' || c == '>' || c == '!';
    }
    private bool esOperMult(char c)
    {
      return c == '*' || c == '/';
    }
    private bool esOperSum(char c)
    {
      return c == '+' || c == '-';
    }
    private bool esCaracter(char c)
    {
      return c == ';' || c == ',' || c == '=' || c == '(' ||
              c == ')' || c == '{' || c == '}';
    }
  }
}