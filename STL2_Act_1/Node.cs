using System.Collections.Generic;

namespace Compiler
{
  internal class Node
  {
    internal Token token { get; set; }
    internal Node Next { get; set; }
    internal int State { get; set; }
    internal string Type { get; set; }

    internal Node()
    {
      token = null;
      Next = null;
      State = -1;
    }

    internal Node(int State)
    {
      token = null;
      Next = null;
      this.State = State;
    }

    internal Node(Token token)
    {
      this.token = token;
      Next = null;
      State = -1;
    }

    internal Node(Rule rule)
    {
      token = null;
      Next = null;
      State = -1;
      Type = rule.Type;
    }
  }

  /*************************************/
  class Rule : Node
  {
    public int Id { get; set; }
    public int Column { get; set; }
    public int PopNum { get; set; }
    public string Detail { get; set; }

    public Rule(int Id, int Column, int PopNum, string Detail, string Type)
    {
      this.Id = Id;
      this.Column = Column;
      this.PopNum = PopNum;
      this.Detail = Detail;
      this.Type = Type;
    }
  }

  internal class Id : Node
  {
    internal Id(Token token)
    {
      Type = "Id";
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
      lvar = pila.Pop(); //quita ListaVar
      pila.Pop(); //quita estado
      id = new Id(pila.Pop().token); //quita Id
      pila.Pop(); //quita estado
      tipo = new Tipo(pila.Pop().token); //quita tipo
      Next = lvar;
    }
  }

  internal class DefFunc : Node
  {
    Node tipo;
    Node id;
    Node parametros;
    Node bloqFunc;

    internal DefFunc(Stack<Node> pila)//<DefFunc> ::= tipo id ( <Parametros> ) <BloqFunc> 
    {
      pila.Pop(); // quita estado
      bloqFunc = pila.Pop(); // quita <BloqFunc>
      pila.Pop(); // quita estado
      pila.Pop(); // quita )
      pila.Pop(); // quita estado
      parametros = pila.Pop(); // quita <parametros>
      pila.Pop(); // quita estado
      pila.Pop(); // quita (
      pila.Pop(); // quita estado
      id = new Id(pila.Pop().token); // quita id
      pila.Pop(); // quita estado
      tipo = new Tipo(pila.Pop().token);//quita el tipo
      Next = bloqFunc;
    }
  }

  internal class Parametros : Node //<Parametros> ::= tipo id <ListaParam> 
  {
    Node tipo;
    Node id;

    internal Node lparametros;
    internal Parametros(Stack<Node> pila)
    {
      pila.Pop(); // quita estado
      lparametros = pila.Pop();//quita la lista de aprametros
      pila.Pop(); // quita estado
      id = new Id(pila.Pop().token);//quita el id
      pila.Pop(); // quita estado
      tipo = new Tipo(pila.Pop().token);//quita el tipo
      Next = lparametros;
    }
  }

  internal class Asignacion : Node//<Sentencia> ::= id = <Expresion> ; 
  {
    Node id;
    Node expresion;

    internal Asignacion(Stack<Node> pila)//<Sentencia> ::= id = <Expresion> ;
    {
      Type = "Asignacion";
      pila.Pop();
      pila.Pop();//quita la ;
      pila.Pop();
      expresion = pila.Pop();//quita expresion
      pila.Pop();
      pila.Pop();//quita =
      pila.Pop();
      id = new Id(pila.Pop().token);//quita id
      Next = expresion;
    }
  }

  internal class While : Node //<Sentencia> ::= while ( <Expresion> ) <Bloque> 
  {
    Node expresion;
    Node bloque;

    internal While(Stack<Node> pila)
    {
      pila.Pop();
      bloque = pila.Pop();//quita bloque
      pila.Pop();
      pila.Pop(); //quita )
      pila.Pop();
      expresion = pila.Pop();//quita expresion
      expresion.Next = bloque;
      pila.Pop();
      pila.Pop(); //quita (
      pila.Pop();
      pila.Pop(); //quita while
      Type = "While";
    }
  }

  internal class Return : Node
  {
    Node expresion;

    public Return(Stack<Node> stack)
    {
      Type = "Return";
      stack.Pop();
      stack.Pop();
      stack.Pop();
      expresion = stack.Pop();
      stack.Pop();
      stack.Pop();
      Next = expresion;
    }
  }


  internal class Constante : Node
  {
    internal Constante(Token _token)
    {
      token = _token;
    }
  }

  internal class If : Node
  {
    Node otro;
    Node sentenciaBloque;
    Node expresion;

    internal If(Stack<Node> pila)//<Sentencia> ::= if ( <Expresion> ) <SentenciaBloque> <Otro>
    {
      Type = "If";
      pila.Pop();
      otro = pila.Pop();//quita Otro
      pila.Pop();
      sentenciaBloque = pila.Pop();//quita SentenciaBloque
      pila.Pop();
      pila.Pop(); // )
      pila.Pop();
      expresion = pila.Pop();
      pila.Pop();
      pila.Pop(); // (
      pila.Pop();
      pila.Pop(); // if
      Next = sentenciaBloque;
    }
  }

  internal class Llamadafunc : Node//<LlamadaFunc> ::= id ( <Argumentos> )
  {
    Node id;
    Node argumentos;

    internal Llamadafunc(Stack<Node> pila)
    {
      pila.Pop();
      pila.Pop();//quita )
      pila.Pop();
      argumentos = (pila.Pop());//quita expresion
      pila.Pop();
      pila.Pop();//quita (
      pila.Pop();
      id = new Id((pila.Pop()).token);//quita id
      Next = argumentos;
    }
  }

  internal class Expresion : Node //Expresion -> Expresion opSuma Expresion
  {
    Node exp1;
    Node ope;
    Node exp2;

    internal Expresion(Stack<Node> stack)
    {
      stack.Pop();
      exp1 = stack.Pop();
      stack.Pop();
      ope = stack.Pop();
      stack.Pop();
      exp2 = stack.Pop();
    }
  }

  internal class ListVar : Node
  {
    Node id;
    Node listVar;

    public ListVar(Stack<Node> stack)//<ListaVar> ::= , id <ListaVar>
    {
      stack.Pop();
      listVar = stack.Pop();
      stack.Pop();
      id = new Id(stack.Pop().token);
      id.Next = listVar;
      stack.Pop();
      stack.Pop();
      Next = listVar;
    }
  }
  /*************************************/
}