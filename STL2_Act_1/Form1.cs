﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace STL2_Act_1
{
  public partial class MainForm : Form
  {
    Lexico lexico;
    Sintactico sintactico;

    public MainForm()
    {
      InitializeComponent();
    }


    private void buttonCopiarTexto_Click(object sender, EventArgs e)
    {
      CopiarTexto(txtBoxOrg.Text);
    }

    private void buttonAnalizadorLexico_Click(object sender, EventArgs e)
    {
      lexico = new Lexico();
      lexico.Analizar(txtBoxOrg.Text);

      /* Agregar tokens a tabla */
      dataGridViewWords.Rows.Clear();
      foreach (Token t in lexico.Tokens) {
        dataGridViewWords.Rows.Add(t.Dato, t.Tipo);
      }
    }

    private void buttonAnalizadorSintactico_Click(object sender, EventArgs e)
    {
      sintactico = new Sintactico(lexico);
      sintactico.Analizar(txtBoxOrg.Text);
    }

    private void CopiarTexto(string texto)
    {
      string copia = ""; // split
      foreach (char c in texto) {
        if (c == ' ' || c == '\n' || c == '\t') {
          txtBoxCopy.AppendText(copia + Environment.NewLine);
          copia = "";
        } else {
          copia += c;
        }
      }
      txtBoxCopy.AppendText(copia + Environment.NewLine);
    }
  }
}
