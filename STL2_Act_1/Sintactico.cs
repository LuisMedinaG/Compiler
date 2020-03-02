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

    static readonly string tableFile = @"C:\Users\Fabiola\Documents\GR2slrTable.txt";
    static readonly string rulesFile = @"C:\Users\Fabiola\Documents\GR2slrRulesId.txt";
    
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
      DetalleRegla = "";
    }

    /*
     * makeNodo(operator, left, right){}
     * makeLeaf(id, entrada){}
     */

    public bool Analizar()
    {
      ConstruirTabla();
      ConstruirReglas();

      Token token;

      Pila.Push(0);
      while (Pila.Count > 0) {
        // Print the current state of the stack
        PRINT_PILA();
        // Get the next token in the list without removinig it from the queue        
        token = lexico.Tokens.Peek();
        int transicion = Pila.Peek();
        // Get the correspondent action from the table
        int accion = ACCION(transicion, token.Tipo);
        if (accion == -1) {
          /***** ACEPTAR *****/
          return true;
        } else if (accion == 0) {
          /***** RECHAZAR *****/
          return false;
        } else if (accion > 0) {
          /*** DEZPLAZAMIENTO ***/
          lexico.Tokens.Dequeue();
          Pila.Push(token.Tipo);
          Pila.Push(accion);
        } else if (accion < 0) {
          /***** REDUCCION *****/
          int idxRegla = Math.Abs(accion) - 1;
          var regla = Reglas[idxRegla];
          // ------ ARBOL SINTACTICO ------ //
          Nodo nodo = new Nodo();
          // ------------------------------ //
          int numPops = regla.Item2 * 2;
          // Definimos el texto de la regla
          DetalleRegla = DetallesReglas[idxRegla];
          
          // switch de pops
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

    private int ACCION(int transicion, int simbolo)
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
