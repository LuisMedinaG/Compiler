using System;
using System.Collections.Generic;

namespace Compiler
{
  internal class Node
  {
    internal Token symbol { get; set; }
    internal Node next { get; set; }
    internal int state { get; set; }
    internal string type { get; set; }

    internal string ambito { get; set; }
    internal char tipodato { get; set; }
    internal string cadenapa { get; set; }

    internal Node()
    {
      state = -1;
    }

    internal Node(int State)
    {
      this.state = State;
    }

    internal Node(Token Token)
    {
      this.symbol = Token;
      state = -1;
    }

    internal Node(Rule Rule)
    {
      type = Rule.type;
      state = -1;
    }

    public Node(string ambito)
    {
      this.ambito = ambito;
    }

    public virtual void validatipos(List<SymbolTable> tabsim, List<string> errores)
    {
      if(next != null) next.validatipos(tabsim, errores);
    }

    public char dimetipo(Node tipo)
    {
      if(tipo.symbol.value == "int") {
        return 'i';
      } else if(tipo.symbol.value == "float") {
        return 'f';
      } else if(tipo.symbol.value == "char") {
        return 'c';
      } else if(tipo.symbol.value == "void") {
        return 'v';
      } else
        return 'v';
    }

    internal bool existe(List<SymbolTable> tabsim, string value, char tipodato, string ambito)
    {
      foreach(var item in tabsim) {
        /*check if exists*/
      }
      return true;
    }

    public override string ToString()
    {
      return "Node(" + type + ", " + state + ")";
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
      this.type = Type;
    }
  }

  internal class Id : Node
  {
    internal Id(Token Token)
    {
      type = "Id";
      this.symbol = Token;
    }
  }

  internal class Tipo : Node
  {
    internal Tipo(Token token)
    {
      this.symbol = token;
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
      id = new Id(pila.Pop().symbol); //quita Id
      pila.Pop(); //quita estado
      tipo = new Tipo(pila.Pop().symbol); //quita tipo
      next = lvar;
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
      id = new Id(pila.Pop().symbol); // quita id
      pila.Pop(); // quita estado
      tipo = new Tipo(pila.Pop().symbol);//quita el tipo
      next = bloqFunc;
    }

    public override void validatipos(List<SymbolTable> tabsim, List<string> errores)
    {
      tipodato = dimetipo(tipo);
      ambito = id.symbol.value;
      if(parametros != null) parametros.validatipos(tabsim, errores);
      if(!existe(tabsim, id.symbol.value, tipodato, ambito)) {
        tabsim.Add(new SymbolTable(id.symbol.value, tipodato, ambito,
        cadenapa));
      } else {
        errores.Add(" La funcion " + id.symbol.value + " ya existe");
      }
      cadenapa = " ";
      if(bloqFunc != null) bloqFunc.validatipos(tabsim, errores);
      ambito = " ";
      if(next != null) next.validatipos(tabsim, errores);
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
      id = new Id(pila.Pop().symbol);//quita el id
      pila.Pop(); // quita estado
      tipo = new Tipo(pila.Pop().symbol);//quita el tipo
      next = lparametros;
    }

    public override void validatipos(List<SymbolTable> tabsim, List<string> errores)
    {
      tipodato = dimetipo(tipo);
      if(!existe(tabsim, id.symbol.value, tipodato, ambito)) {
        tabsim.Add(new SymbolTable(id.symbol.value, tipodato, ambito));
      } else
        errores.Add("la variable " + id.symbol.value + " ya fue declarada");
      cadenapa += tipo.symbol.value[0];
      if(lparametros != null) lparametros.validatipos(tabsim, errores);
      if(next != null) next.validatipos(tabsim, errores);
    }
  }

  internal class Asignacion : Node//<Sentencia> ::= id = <Expresion> ; 
  {
    Node id;
    Node expresion;

    internal Asignacion(Stack<Node> pila)//<Sentencia> ::= id = <Expresion> ;
    {
      type = "Asignacion";
      pila.Pop();
      pila.Pop();//quita la ;
      pila.Pop();
      expresion = pila.Pop();//quita expresion
      pila.Pop();
      pila.Pop();//quita =
      pila.Pop();
      id = new Id(pila.Pop().symbol);//quita id
      next = expresion;
    }

    public override void validatipos(List<SymbolTable> tabsim, List<string> errores)
    {
      id.validatipos(tabsim, errores);
      expresion.validatipos(tabsim, errores);
      if(id.tipodato == 'c' && expresion.tipodato == 'c') {
        tipodato = 'c';

      } else {
        if(id.tipodato == 'i' && expresion.tipodato == 'i') {
          tipodato = 'i';
        } else {
          if(id.tipodato == 'f' && expresion.tipodato == 'f') {
            tipodato = 'f';
          } else {
            tipodato = 'e';
            errores.Add(" el tipo de dato de " + id.symbol.value + " es diferente al de la expresion");
            }
        }
      }
      if(expresion.next != null) expresion.next.validatipos(tabsim,
     errores);
      if(next != null) next.validatipos(tabsim, errores);
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
      expresion.next = bloque;
      pila.Pop();
      pila.Pop(); //quita (
      pila.Pop();
      pila.Pop(); //quita while
      type = "While";
    }
  }

  internal class Return : Node
  {
    Node expresion;

    public Return(Stack<Node> stack)
    {
      type = "Return";
      stack.Pop();
      stack.Pop();
      stack.Pop();
      expresion = stack.Pop();
      stack.Pop();
      stack.Pop();
      next = expresion;
    }
  }


  internal class Constante : Node
  {
    internal Constante(Token _token)
    {
      symbol = _token;
    }
  }

  internal class If : Node
  {
    Node otro;
    Node sentenciaBloque;
    Node expresion;

    internal If(Stack<Node> pila)//<Sentencia> ::= if ( <Expresion> ) <SentenciaBloque> <Otro>
    {
      type = "If";
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
      next = sentenciaBloque;
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
      id = new Id((pila.Pop()).symbol);//quita id
      next = argumentos;
    }

    public override void validatipos(List<SymbolTable> tabsim, List<string> errores)
    {
      Node aux = new Node(ambito);
      //tipodato = buscartipo(tabsim, id.symbol.value);
      aux = argumentos;
      //string cadena = "";
      while(aux != null) {
        char tipo2;
        //tipo2 = buscartipo2(tabsim, aux.symbol.value, ambito);

        //cadena += tipo2;
        //aux = aux.next;
      }

      if(argumentos != null) argumentos.validatipos(tabsim, errores);
      if(id.symbol.value == "print")
        id.validatipos(tabsim, errores);
      else {
        Console.WriteLine("entra a ver si existe la funcion");
        //if(existefunc(tabsim, id.simbolo, ambito, cadena, errores)) {
        //  id.validatipos(tabsim, cadenapa, errores);
        //}
      }
      if(next != null) next.validatipos(tabsim, errores);
    }

    public bool existefunc(List<SymbolTable> tabsim, string id, string ambito, string cadenapa, List<string> errores)
    {
      bool existe = false;
      foreach(var s in tabsim) {
        if(s.id == id) {
          existe = true;

          Console.WriteLine("en la funcion " + id);
          Console.WriteLine("stpara=" + s.stpara + " === cadenapa=" +
          cadenapa);
          if(s.stpara == cadenapa)
            return true;
          else {
            errores.Add("los parametros de la funcion " + id + " son incorrectos");
          }
        }
      }
      if(!existe) errores.Add("la funcion " + id + " no existe");
      return false;
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
      id = new Id(stack.Pop().symbol);
      id.next = listVar;
      stack.Pop();
      stack.Pop();
      next = listVar;
    }
  }
  /*************************************/
}