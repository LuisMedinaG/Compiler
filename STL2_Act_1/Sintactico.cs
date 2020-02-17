using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace STL2_Act_1
{
    class Sintactico
    {
        Lexico lexico;
        Stack<string> Pila;
        List<string[]> Tabla;

        static readonly string tableFile = @"C:\Users\Fabiola\Documents\GR2slrTable.txt";
        static readonly string rulesFile = @"C:\Users\Fabiola\Documents\GR2slrRulesId.txt";

        public Sintactico(Lexico lexico)
        {
            this.lexico = lexico;
            Pila = new Stack<string>();
            Tabla = new List<string[]>();
        }

        public bool Analizar(string text)
        {
            ConstruirTabla();
            Token currToken;
            string currSymbol;
            int celda;

            Pila.Push("0");
            while (Pila.Count != 0) {
                currToken = lexico.Tokens.Pop();
                currSymbol = Pila.Peek();

                celda = GOTO(currToken.Tipo, currSymbol);
                if (celda == 0) {
                    // ERROR NO EXISTE REGLA
                    break;
                } else if (celda < 0) {
                    /***** REDUCCION *****/
                } else if (celda == 0) {
                    /***** DEZPLAZAR *****/
                } else if (celda == -1) {
                }
            }
            return true;
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

        private int GOTO(int column, string rowStr)
        {
            int row; int.TryParse(rowStr, out row);
            int cell; int.TryParse(Tabla[column + 1][row], out cell);
            return cell;
        }
    }
}
