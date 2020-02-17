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
    List<Tuple<int, int>> Reglas;
    internal DataGridView tablePila;

    static readonly string tableFile = @"C:\Users\Fabiola\Documents\GR2slrTable.txt";
    static readonly string rulesFile = @"C:\Users\Fabiola\Documents\GR2slrRulesId.txt";


    public Sintactico(Lexico lexico)
    {
      this.lexico = lexico;
      Pila = new Stack<int>();
      Tabla = new List<string[]>();
      Reglas = new List<Tuple<int, int>>();
    }

    public bool Analizar(string text)
    {
      ConstruirTabla();
      ConstruirReglas();

      Token TokenActual;
      int TopePila;
      int accion;

      Pila.Push(0);
      while (Pila.Count > 0) {
        PRINT_PILA();
        TokenActual = lexico.Tokens.Peek();
        TopePila = Pila.Peek();
        accion = ACCION(TopePila, TokenActual.Tipo);
        if (accion == -1) {
          return true;
        } else if (accion == 0) {
          return false;
        } else if (accion > 0) {
          /*** DEZPLAZAMIENTO ***/
          lexico.Tokens.Dequeue();
          Pila.Push(TokenActual.Tipo);
          Pila.Push(accion);
        } else if (accion < 0) {
          /***** REDUCCION *****/
          int idxRegla = Math.Abs(accion) - 1;
          var regla = Reglas[idxRegla];
          int numPops = regla.Item2*2;
          for (int i = 0; i < numPops; i++) {
            if (Pila.Count == 0) {
              return false;
            }
            Pila.Pop();
          }
          int GOTO_ROW = Pila.Peek();
          int GOTO_COL = regla.Item1;
          int GOTO_CELL = ACCION(GOTO_ROW, GOTO_COL);
          Pila.Push(GOTO_COL);
          Pila.Push(GOTO_CELL);
        }
      }
      return false;
    }

    private int ACCION(int estado, int simbolo)
    {
      int cell; int.TryParse(Tabla[estado][simbolo + 1], out cell);
      return cell;
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
        cadActual += t.Dato + " ";
      }
      return cadActual;
    }

    private void ConstruirReglas()
    {
      if (File.Exists(rulesFile)) {
        string[] lines = File.ReadAllLines(rulesFile);

        foreach (string line in lines) {
          string[] pair = line.Split(null);
          Reglas.Add(new Tuple<int, int>(int.Parse(pair[0]), int.Parse(pair[1])));
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
  }
}
