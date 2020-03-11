﻿using System.Collections.Generic;

namespace Compiler
{
  internal class Node
  {
    internal Token token { get; set; }
    internal Node Next { get; set; }
    internal int State { get; set; }

    internal Node()
    {
      token = null;
      Next = null;
      State = -1;
    }

    internal Node(Token Token)
    {
      this.token = Token;
      Next = null;
      State = -1;
    }

    internal Node(int State)
    {
      token = null;
      Next = null;
      this.State = State;
    }
  }
  /*************************************/
  internal class Programa : Node
  {
    internal Programa(Stack<Node> pila)
    {
      pila.Pop(); //quita estado
      Next = pila.Pop();
    }
  }

  internal class Id : Node
  {
    internal Id(Token token)
    {
      this.token = token;
    }
  }

  internal class Tipo : Node
  {
    internal Tipo(Token token)
    {
      this.token = token;
    }
  }

  internal class DefVar : Node
  {
    Node tipo;
    Node id;
    Node lvar;

    internal DefVar(Stack<Node> pila)//<DefVar> ::= tipo id <ListaVar> ; 
    {
      pila.Pop(); //quita estado
      pila.Pop(); //quita  ;
      pila.Pop(); //quita estado estado
      lvar = (pila.Pop()); //quita ListaVar
      pila.Pop(); //quita estado
      id = new Id(pila.Pop().token); //quita Id
      pila.Pop(); //quita estado
      tipo = new Tipo(pila.Pop().token); //quita tipo
    }
  }

  internal class DefFunc : Node
  {
    Node tipo;
    Node id;
    Node parametros;
    Node bloqFunc;

    internal static string varlocal;

    internal DefFunc(Stack<Node> pila)//<DefFunc> ::= tipo id ( <Parametros> ) <BloqFunc> 
    {
      pila.Pop();//quita estado
      bloqFunc = (pila.Pop());//quita <bloqfunc>
      pila.Pop();//quita estado
      pila.Pop();//quita )
      pila.Pop();//quita estado
      parametros = (pila.Pop());//quita <parametros>
      pila.Pop();//quita estado
      pila.Pop();//quita (
      pila.Pop();//quita estado
      id = new Id((pila.Pop()).token);//quita id
      pila.Pop();//quita estado
      tipo = new Tipo((pila.Pop()).token);//quita el tipo
    }
  }

  internal class Parametros : Node //<Parametros> ::= tipo id <ListaParam> 
  {
    Node tipo;
    Node id;
    internal Node lparametros;
    internal Parametros(Stack<Node> pila)
    {
      pila.Pop();//quita estado
      lparametros = (pila.Pop());//quita la lista de aprametros
      pila.Pop();//quita estado
      id = new Id((pila.Pop()).token);//quita el id
      pila.Pop(); //quita estado
      tipo = new Tipo((pila.Pop()).token);//quita el tipo
    }
  }

  internal class Asignacion : Node//<Sentencia> ::= id = <Expresion> ; 
  {
    Node id;
    Node expresion;

    internal Asignacion(Stack<Node> pila)//<Sentencia> ::= id = <Expresion> ;
    {
      pila.Pop();
      pila.Pop();//quita la ;
      pila.Pop();
      expresion = (pila.Pop());//quita expresion
      pila.Pop();
      pila.Pop();//quita =
      pila.Pop();
      id = new Id((pila.Pop()).token);//quita id
    }
  }

  internal class While : Node //<Sentencia> ::= while ( <Expresion> ) <Bloque> 
  {
    Node expresion;
    Node bloque;

    internal While(Stack<Node> pila)
    {
      pila.Pop();
      bloque = (pila.Pop());//quita bloque
      pila.Pop();
      pila.Pop(); //quita )
      pila.Pop();
      expresion = (pila.Pop());//quita expresion
      pila.Pop();
      pila.Pop(); //quita (
      pila.Pop();
      pila.Pop(); //quita while
    }
  }

  // <Sentencia> ::= do <Bloque> while ( <Expresion> ) ;
  internal class Dowhile : Node
  {
    Node bloque;
    Node expresion;

    internal Dowhile(Stack<Node> pila)
    {
      pila.Pop();
      pila.Pop();//quita ;
      pila.Pop();
      pila.Pop();//quita )
      pila.Pop();
      expresion = (pila.Pop());//quita exprecion
      pila.Pop();
      pila.Pop();//quita (
      pila.Pop();
      pila.Pop();//quita el while
      pila.Pop();
      bloque = (pila.Pop());//quita bloque
      pila.Pop();
      pila.Pop();//quita do
    }
  }

  internal class For : Node //<Sentencia> ::= for id = <Expresion> : <Expresion> : <Expresion> <SentenciaBloque>
  {
    Node senbloque;
    Node expresion1;
    Node expresion2;
    Node expresion3;
    Node id;

    internal For(Stack<Node> pila)
    {
      pila.Pop();
      senbloque = (pila.Pop());//quita senteciabloque
      pila.Pop();
      expresion3 = (pila.Pop());//quita expresion
      pila.Pop();
      pila.Pop();//quita ;
      pila.Pop();
      expresion2 = (pila.Pop());//quita expresion
      pila.Pop();
      expresion1 = (pila.Pop());//quita expresion
      pila.Pop();
      pila.Pop();//quita =
      pila.Pop();
      id = new Id((pila.Pop()).token);//quita id
      pila.Pop();
      pila.Pop();//quita for
    }
  }

  internal class Constante : Node
  {
    internal Constante(Token _token)
    {
      token = _token;
      // clase = "cons";
    }
  }

  internal class Llamadafunc : Node//<LlamadaFunc> ::= id ( <Argumentos> )
  {
    Node id;
    Node argumentos;

    internal Llamadafunc(Stack<Node> pila)
    {
      // clase = "fun";
      pila.Pop();
      pila.Pop();//quita )
      pila.Pop();
      argumentos = (pila.Pop());//quita exprecion
      pila.Pop();
      pila.Pop();//quita (
      pila.Pop();
      id = new Id((pila.Pop()).token);//quita id
    }
  }
  /*************************************/
}