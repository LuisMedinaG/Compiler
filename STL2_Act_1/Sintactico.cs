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

    static readonly string tableFile = "GR2slrTable.txt";
    static readonly string rulesFile = "GR2slrRulesId.txt";

    internal DataGridView tablePila;
    List<string> DetallesReglas;
    string DetalleRegla;

    public Sintactico(Lexico lexico)
    {
      this.lexico = lexico;
      Pila = new Stack<int>();
      Tabla = new List<string[]>();
      Reglas = new List<Tuple<int, int>>();
      DetallesReglas = new List<string>();
      
      Pila.Push(0);

      ConstruirTabla();
      ConstruirReglas();
    }

    /*
     * makeNodo(operator, left, right){}
     * makeLeaf(id, entrada){}
     */

    public bool Analizar()
    {
      Token token;

      token = lexico.NextToken();
      while (Pila.Count > 0) {
        PRINT_PILA();
        
        int transition = Pila.Peek();
        int action = ACTION(transition, token.Tipo);
        
        if (action == -1) {
          /***** ACCEPT *****/
          return true;
        } else if (action == 0) {
          /***** ERROR *****/
          return false;
        } else if (action > 0) {
          /***** SHIFT *****/
          Pila.Push(token.Tipo);
          Pila.Push(action);
          token = lexico.NextToken();
        } else if (action < 0) {
          /***** REDUCTION *****/
          int idxRegla = Math.Abs(action) - 1;
          var regla = Reglas[idxRegla];
          int pops = regla.Item2 * 2;
          // Definimos el texto de la regla
          DetalleRegla = DetallesReglas[idxRegla];

          // switch de pops
          // TODO : defirnir el numero de pops dependiendo de la reduccion
          for (int i = 0; i < pops; i++) {
            if (Pila.Count == 0) {
              return false;
            }
            Pila.Pop();
          }

          int row = Pila.Peek();
          int col = regla.Item1;
          Pila.Push(col);
          Pila.Push(ACTION(row, col));
        }
      }
      return false;
    }

    private int ACTION(int transicion, int simbolo)
    {
      int.TryParse(Tabla[transicion][simbolo + 1], out int accion);
      return accion;
    }

    /**************************** Auxiliar functions *******************************/
    private void PRINT_PILA()
    {
      string cadenaActual = GET_CADENA();
      string pila = "";
      foreach (int p in Pila) {
        pila = p.ToString() + ", " + pila;
      }
      tablePila.Rows.Add(tablePila.Rows.Count + 1, pila, cadenaActual, DetalleRegla);
      DetalleRegla = "";
    }

    private string GET_CADENA()
    {
      string cadActual = "";
      foreach (Token t in lexico.Tokens) {
        cadActual += t.Dato + ' ';
      }
      return cadActual;
    }

    private void ConstruirReglas()
    {
      if (File.Exists(rulesFile)) {
        string[] lines = File.ReadAllLines(rulesFile);

        foreach (string line in lines) {
          string[] regla = line.Split(null);
          string reglaStr = "";
          for (int i = 2; i < regla.Length; i++) {
            reglaStr += regla[i] + ' ';
          }
          DetallesReglas.Add(reglaStr);
          Reglas.Add(new Tuple<int, int>(int.Parse(regla[0]), int.Parse(regla[1])));
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
