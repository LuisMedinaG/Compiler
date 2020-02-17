using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace STL2_Act_1
{
  class Sintactico
  {
    Lexico lexico;
    Stack<int> Pila;
    List<string[]> Tabla;
    List<Tuple<int, int>> Reducciones;
    internal DataGridView tablePila;
    static readonly string tableFile = @"C:\Users\Fabiola\Documents\GR2slrTable.txt";
    static readonly string rulesFile = @"C:\Users\Fabiola\Documents\GR2slrRulesId.txt";


    public Sintactico(Lexico lexico)
    {
      this.lexico = lexico;
      Pila = new Stack<int>();
      Tabla = new List<string[]>();
      Reducciones = new List<Tuple<int, int>>();
    }

    public bool Analizar(string text)
    {
      ConstruirTabla();
      ConstruirReglas();
      Token currToken;
      int currSymbol;
      int celda;

      Pila.Push(0);
      while (Pila.Count > 0) {
        PRINT_PILA();
        currToken = lexico.Tokens.Peek();
        currSymbol = Pila.Peek();
        celda = ACCION(currToken.Estado, currSymbol);
        if (celda == -1) {
          return true;
        } else if (celda == 0) {
          return false;
        } else if (celda > 0) {
          /***** DEZPLAZAMIENTO *****/
          lexico.Tokens.Dequeue();
          Pila.Push(currToken.Estado);
          Pila.Push(celda);
        } else if (celda < 0) {
          /***** REDUCCION *****/
          int idxRegla = Math.Abs(celda) + 1;
          var rule = Reducciones[idxRegla];
          int numPops = rule.Item2;
          for (int i = 0; i < numPops; i++) {
            if (Pila.Count == 0) {
              return false;
            }
            Pila.Pop();
          }
          int GOTO_COL = rule.Item1;
          int GOTO_ROW = Pila.Peek();
          int GOTO_CELL = ACCION(idxRegla, GOTO_ROW);
          Pila.Push(GOTO_COL);
          Pila.Push(GOTO_CELL);
        }
      }
      return false;
    }

    private void PRINT_PILA()
    {
      string cadenaActual = GET_CADENA();
      string pila = "";
      foreach (int p in Pila) {
        pila = p.ToString() + ", " + pila;
      }
      tablePila.Rows.Add(tablePila.Rows.Count + 1, pila, cadenaActual);
    }

    private string GET_CADENA()
    {
      string cadActual = "";
      foreach (Token t in lexico.Tokens) {
        cadActual += t.Dato;
      }
      return cadActual;
    }

    private void ConstruirReglas()
    {
      if (File.Exists(rulesFile)) {
        string[] lines = File.ReadAllLines(rulesFile);

        foreach (string line in lines) {
          string[] pair = line.Split(null);
          Reducciones.Add(new Tuple<int, int>(int.Parse(pair[0]), int.Parse(pair[1])));
        }
      }
    }

    private void ConstruirTabla()
    {
      if (File.Exists(tableFile)) {
        string[] lines = File.ReadAllLines(tableFile);
        foreach (string line in lines) {
          Tabla.Add(line.Split(null));
        }
      }
    }

    private int ACCION(int estado, int symbol)
    {
      int cell; int.TryParse(Tabla[estado][symbol + 1], out cell);
      return cell;
    }
  }
}
