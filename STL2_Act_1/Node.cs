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
    internal static string ambito { get; set; }
    internal static char tipodato { get; set; }
    internal static string cadenapa { get; set; }

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

    public Node(string _ambito)
    {
      ambito = _ambito;
      state = -1;
    }

    /* SEMANTIC */
    public char dimetipo(Node tipo)
    {
      if(tipo.symbol.value == "int")
        return 'i';
      else if(tipo.symbol.value == "float")
        return 'f';
      else if(tipo.symbol.value == "char")
        return 'c';
      return 'v';
    }

    public virtual void validatipos(List<TableSymbol> tabsim, List<string> errores)
    {
      if(next != null) next.validatipos(tabsim, errores);

      if(symbol != null) {
        if(Int32.TryParse(symbol.value, out int res) == true) {
          tipodato = 'i';
        } else if(float.TryParse(symbol.value, out float res1) == true) {
          tipodato = 'f';
        } else {
          tipodato = existe(tabsim, symbol.value, ambito);
        }
      }
    }

    public char existe(List<TableSymbol> tabsim, string id, string ambito)
    {
      foreach(var s in tabsim)
        if(s.id == id && s.ambito == ambito)
          return s.tipo;
      return '\0';
    }

    public char buscartipo(List<TableSymbol> tabsim, string id)
    {
      foreach(var sT in tabsim) {
        if(sT.id == id)
          return sT.tipo;
      }
      return '\0';
    }
  }

  internal class Programa : Node
  {
    public Programa(Stack<Node> stack)
    {
      stack.Pop();
      next = stack.Pop();
    }
  }

  internal class Rule : Node
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
    internal Id(Token symbol)
    {
      type = "Id";
      this.symbol = symbol;
    }
    public override void validatipos(List<TableSymbol> tab, List<string> errores)
    {
      tab.Add(new TableSymbol(symbol.value, tipodato, ambito));
      if(next != null) next.validatipos(tab, errores);
    }
  }

  internal class Tipo : Node
  {
    internal Tipo(Token _symbol)
    {
      symbol = _symbol;
    }
  }

  internal class DefVar : Node
  {
    Tipo tipo;
    Id id;
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

    public override void validatipos(List<TableSymbol> tab, List<string> errores)
    {
      tipodato = dimetipo(tipo);

      if(existe(tab, id.symbol.value, ambito) != '\0')
        errores.Add("La variable " + id.symbol.value + " de la funcion " + ambito + " ya existe");
      else
        tab.Add(new TableSymbol(id.symbol.value, tipodato, ambito));

      if(lvar != null)
        lvar.validatipos(tab, errores);
      if(next != null)
        next.validatipos(tab, errores);
    }
  }

  internal class DefFunc : Node
  {
    Tipo tipo;
    Id id;
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

    public override void validatipos(List<TableSymbol> symbolTable, List<string> errors)
    {
      tipodato = dimetipo(tipo);
      ambito = id.symbol.value;

      if(parametros != null)
        parametros.validatipos(symbolTable, errors);

      if(existe(symbolTable, id.symbol.value, ambito) != '\0')
        errors.Add(" La funcion " + id.symbol.value + " ya existe.");
      else
        symbolTable.Add(new TableSymbol(id.symbol.value, tipodato, ambito, cadenapa));

      cadenapa = "";
      if(bloqFunc != null) bloqFunc.validatipos(symbolTable, errors);
      ambito = "";
      if(next != null) next.validatipos(symbolTable, errors);
    }
  }

  internal class Parametros : Node //<Parametros> ::= tipo id <ListaParam> 
  {
    Tipo tipo;
    Id id;

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

    public override void validatipos(List<TableSymbol> tabsim, List<string> errores)
    {
      tipodato = dimetipo(tipo);
      if(existe(tabsim, id.symbol.value, ambito) != '\0')
        errores.Add("la variable " + id.symbol.value + " ya fue declarada");
      else
        tabsim.Add(new TableSymbol(id.symbol.value, tipodato, ambito));
      cadenapa += tipo.symbol.value[0];
      if(lparametros != null) lparametros.validatipos(tabsim, errores);
      if(next != null) next.validatipos(tabsim, errores);
    }
  }

  internal class Asignacion : Node//<Sentencia> ::= id = <Expresion> ; 
  {
    Id id;
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

    public override void validatipos(List<TableSymbol> tabsim, List<string> errores)
    {
      tipodato = existe(tabsim, id.symbol.value, ambito);
      char tipo1 = tipodato;
      if(expresion != null) expresion.validatipos(tabsim, errores);
      char tipo2 = tipodato;
      if(tipo1 != tipo2) errores.Add("El tipo de dato de " + id.symbol.value + " en la funcion " + ambito + " es diferente de la expresion");
      if(next != null) next.validatipos(tabsim, errores);
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

    public override void validatipos(List<TableSymbol> tabsim, List<string> errores)
    {
      if(expresion.symbol != null) {
        if(Int32.TryParse(expresion.symbol.value, out int res) == true) {
          tipodato = 'i';
        } else if(float.TryParse(expresion.symbol.value, out float res1) == true) {
          tipodato = 'f';
        } else {
          tipodato = existe(tabsim, expresion.symbol.value, ambito);
        }
      }
      char tmpTipoDato = buscartipo(tabsim, ambito);
      if(tipodato != tmpTipoDato)
        errores.Add("El tipo de dato que regresa " + expresion.symbol.value + " no es el mismo que el de la funcion " + ambito);
    }
  }

  internal class Llamadafunc : Node//<LlamadaFunc> ::= id ( <Argumentos> )
  {
    Id id;
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

    public override void validatipos(List<TableSymbol> tabsim, List<string> errores)
    {
      Node aux = new Node();
      tipodato = buscartipo(tabsim, id.symbol.value);
      aux = argumentos;
      string cadena = "";
      if(aux.symbol != null) {
        while(aux != null && aux.symbol != null) {
          if(Int32.TryParse(aux.symbol.value, out int res) == true) {
            cadena += 'i';
          } else if(float.TryParse(aux.symbol.value, out float res1) == true) {
            cadena += 'f';
          } else {
            cadena += existe(tabsim, aux.symbol.value, ambito);
          }
          aux = aux.next;
        }
      }
      if(argumentos != null) argumentos.validatipos(tabsim, errores);

      if(id.symbol.value == "print")
        id.validatipos(tabsim, errores);
      else if(existefunc(tabsim, id.symbol.value, ambito, cadena, errores)) {
        id.validatipos(tabsim, errores);
      }
      if(next != null)
        next.validatipos(tabsim, errores);
    }

    public bool existefunc(List<TableSymbol> tabsim, string _id, string ambito, string _cadenapa, List<string> errores)
    {
      bool existe = false;
      foreach(var s in tabsim) {
        if(s.id == _id) {
          existe = true;
          if(s.stpara == _cadenapa)
            return true;
          else
            errores.Add("los parametros de la funcion " + _id + " son incorrectos");
        }
      }
      if(!existe) errores.Add("la funcion " + _id + " no existe");
      return false;
    }
  }

  internal class Expresion : Node //Expresion -> Expresion opSuma Expresion
  {
    Node left;
    Node right;

    internal Expresion(Stack<Node> stack)
    {
      stack.Pop();
      left = (Node)stack.Pop();
      stack.Pop();
      symbol = stack.Pop().symbol;
      stack.Pop();
      right = (Node)stack.Pop();
    }

    public override void validatipos(List<TableSymbol> tabsim, List<string> errores)
    {
      char tipodato1 = tipodato;
      left.validatipos(tabsim, errores);
      char tipodato2 = tipodato;
      right.validatipos(tabsim, errores);
      char tipodato3 = tipodato;
      if(tipodato1 == tipodato2 && tipodato2 == tipodato3) {
        if(next != null) next.validatipos(tabsim, errores);
      }
    }
  }

  internal class ListVar : Node
  {
    Id id;
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

    public override void validatipos(List<TableSymbol> tasimb, List<string> errores)
    {
      if(sentenciaBloque != null) sentenciaBloque.validatipos(tasimb, errores);
      if(otro != null) otro.validatipos(tasimb, errores);
      if(next != null) next.validatipos(tasimb, errores);
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

    public override void validatipos(List<TableSymbol> tabsim, List<string> errores)
    {
      if(bloque != null) bloque.validatipos(tabsim, errores);
      if(next != null) next.validatipos(tabsim, errores);
    }
  }

  internal class TableSymbol : Node
  {
    public string id;
    public char tipo;
    public string stpara;
    public string ambito;

    public TableSymbol(string _id, char _tipo, string _ambito, string _stpara)
    {
      id = _id;
      tipo = _tipo;
      ambito = _ambito;
      stpara = _stpara;
    }
    public TableSymbol(string _id, char _tipo, string _ambito)
    {
      id = _id;
      tipo = _tipo;
      ambito = _ambito;
      stpara = "";
    }

    public TableSymbol(string ambito)
    {
      this.ambito = ambito;
    }
  }
}