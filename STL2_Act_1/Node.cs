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
    internal static string scoup { get; set; }
    internal static char dataType { get; set; }
    internal static string paramStr { get; set; }

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
      scoup = _ambito;
      state = -1;
    }

    /* SEMANTIC */
    public char GetType(Node tipo)
    {
      if(tipo.symbol.value == "int")
        return 'i';
      else if(tipo.symbol.value == "float")
        return 'f';
      else if(tipo.symbol.value == "char")
        return 'c';
      return 'v';
    }

    public virtual void TypeCheck(List<TableSymbol> symbolTable, List<string> errores)
    {
      if(next != null) next.TypeCheck(symbolTable, errores);

      if(symbol != null) {
        if(Int32.TryParse(symbol.value, out int res) == true) {
          dataType = 'i';
        } else if(float.TryParse(symbol.value, out float res1) == true) {
          dataType = 'f';
        } else {
          dataType = SearchSymbol(symbolTable, symbol.value, scoup);
        }
      }
    }

    public char SearchSymbol(List<TableSymbol> symbolTable, string id, string ambito)
    {
      foreach(var s in symbolTable)
        if(s.tsId == id && s.ambito == ambito)
          return s.tsType;
      return '\0';
    }

    public char SearchSymbol(List<TableSymbol> symbolTable, string id)
    {
      foreach(var sT in symbolTable) {
        if(sT.tsId == id)
          return sT.tsType;
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
    public override void TypeCheck(List<TableSymbol> symbolTable, List<string> errores)
    {
      symbolTable.Add(new TableSymbol(symbol.value, dataType, scoup));
      if(next != null) next.TypeCheck(symbolTable, errores);
    }
  }

  internal class Type : Node
  {
    internal Type(Token _symbol)
    {
      symbol = _symbol;
    }
  }

  internal class DefVar : Node
  {
    Type _type;
    Id _id;
    Node lvar;

    internal DefVar(Stack<Node> pila)//<DefVar> ::= tipo id <ListaVar> ; 
    {
      pila.Pop(); //quita estado
      pila.Pop(); //quita  ;
      pila.Pop(); //quita estado estado
      lvar = pila.Pop(); //quita ListaVar
      pila.Pop(); //quita estado
      _id = new Id(pila.Pop().symbol); //quita Id
      pila.Pop(); //quita estado
      _type = new Type(pila.Pop().symbol); //quita tipo
    }

    public override void TypeCheck(List<TableSymbol> tab, List<string> errores)
    {
      dataType = GetType(_type);

      if(SearchSymbol(tab, _id.symbol.value, scoup) != '\0')
        errores.Add("La variable " + _id.symbol.value + " de la funcion " + scoup + " ya existe");
      else
        tab.Add(new TableSymbol(_id.symbol.value, dataType, scoup));

      if(lvar != null)
        lvar.TypeCheck(tab, errores);
      if(next != null)
        next.TypeCheck(tab, errores);
    }
  }

  internal class DefFunc : Node
  {
    Type _type;
    Id _id;
    Node _parameters;
    Node _bloqFunc;

    internal DefFunc(Stack<Node> pila)//<DefFunc> ::= tipo id ( <Parametros> ) <BloqFunc> 
    {
      pila.Pop(); // quita estado
      _bloqFunc = pila.Pop(); // quita <BloqFunc>
      pila.Pop(); // quita estado
      pila.Pop(); // quita )
      pila.Pop(); // quita estado
      _parameters = pila.Pop(); // quita <parametros>
      pila.Pop(); // quita estado
      pila.Pop(); // quita (
      pila.Pop(); // quita estado
      _id = new Id(pila.Pop().symbol); // quita id
      pila.Pop(); // quita estado
      _type = new Type(pila.Pop().symbol);//quita el tipo
    }

    public override void TypeCheck(List<TableSymbol> symbolTable, List<string> errors)
    {
      dataType = GetType(_type);
      char tmpTipodato = dataType;
      scoup = _id.symbol.value;

      if(_parameters != null)
        _parameters.TypeCheck(symbolTable, errors);

      if(SearchSymbol(symbolTable, _id.symbol.value, scoup) != '\0')
        errors.Add(" La funcion " + _id.symbol.value + " ya existe.");
      else
        symbolTable.Add(new TableSymbol(_id.symbol.value, tmpTipodato, scoup, paramStr));

      paramStr = "";
      if(_bloqFunc != null) _bloqFunc.TypeCheck(symbolTable, errors);
      scoup = "";
      if(next != null) next.TypeCheck(symbolTable, errors);
    }
  }

  internal class Parametros : Node //<Parametros> ::= tipo id <ListaParam> 
  {
    Type _type;
    Id _id;

    internal Node lparametros;
    internal Parametros(Stack<Node> pila)
    {
      pila.Pop(); // quita estado
      lparametros = pila.Pop();//quita la lista de aprametros
      pila.Pop(); // quita estado
      _id = new Id(pila.Pop().symbol);//quita el id
      pila.Pop(); // quita estado
      _type = new Type(pila.Pop().symbol);//quita el tipo
    }

    public override void TypeCheck(List<TableSymbol> symbolTable, List<string> errores)
    {
      dataType = GetType(_type);

      if(SearchSymbol(symbolTable, _id.symbol.value, scoup) != '\0')
        errores.Add("la variable " + _id.symbol.value + " ya fue declarada");
      else
        symbolTable.Add(new TableSymbol(_id.symbol.value, dataType, scoup));
      paramStr += _type.symbol.value[0];
      if(lparametros != null) lparametros.TypeCheck(symbolTable, errores);
      if(next != null) next.TypeCheck(symbolTable, errores);
    }
  }

  internal class Asignacion : Node//<Sentencia> ::= id = <Expresion> ; 
  {
    Id _id;
    Node _expresion;

    internal Asignacion(Stack<Node> pila)//<Sentencia> ::= id = <Expresion> ;
    {
      type = "Asignacion";
      pila.Pop();
      pila.Pop();//quita la ;
      pila.Pop();
      _expresion = pila.Pop();//quita expresion
      pila.Pop();
      pila.Pop();//quita =
      pila.Pop();
      _id = new Id(pila.Pop().symbol);//quita id
    }

    public override void TypeCheck(List<TableSymbol> symbolTable, List<string> errores)
    {
      dataType = SearchSymbol(symbolTable, _id.symbol.value, scoup);
      char tipo1 = dataType;
      if(_expresion != null) _expresion.TypeCheck(symbolTable, errores);
      char tipo2 = dataType;
      if(tipo1 != tipo2) errores.Add("El tipo de dato de " + _id.symbol.value + " en la funcion " + scoup + " es diferente de la expresion.");
      if(next != null) next.TypeCheck(symbolTable, errores);
    }
  }

  internal class Return : Node
  {
    Node _expresion;

    public Return(Stack<Node> stack)
    {
      type = "Return";
      stack.Pop();
      stack.Pop();
      stack.Pop();
      _expresion = stack.Pop();
      stack.Pop();
      stack.Pop();
    }

    public override void TypeCheck(List<TableSymbol> symbolTable, List<string> errores)
    {
      if(_expresion.symbol != null) {
        if(Int32.TryParse(_expresion.symbol.value, out int res) == true) {
          dataType = 'i';
        } else if(float.TryParse(_expresion.symbol.value, out float res1) == true) {
          dataType = 'f';
        } else {
          dataType = SearchSymbol(symbolTable, _expresion.symbol.value, scoup);
        }
      }
      char tmpTipoDato = SearchSymbol(symbolTable, scoup);
      if(dataType != tmpTipoDato)
        errores.Add("El tipo de dato que regresa " + _expresion.symbol.value + " no es el mismo que el de la funcion " + scoup);
    }
  }

  internal class Llamadafunc : Node//<LlamadaFunc> ::= id ( <Argumentos> )
  {
    Id _id;
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
      _id = new Id((pila.Pop()).symbol);//quita id
    }

    public override void TypeCheck(List<TableSymbol> symbolTable, List<string> errores)
    {
      Node aux = new Node();
      dataType = SearchSymbol(symbolTable, _id.symbol.value);
      aux = argumentos;
      string cadena = "";
      if(aux.symbol != null) {
        while(aux != null && aux.symbol != null) {
          if(Int32.TryParse(aux.symbol.value, out int res) == true) {
            cadena += 'i';
          } else if(float.TryParse(aux.symbol.value, out float res1) == true) {
            cadena += 'f';
          } else {
            cadena += SearchSymbol(symbolTable, aux.symbol.value, scoup);
          }
          aux = aux.next;
        }
      }
      if(_id.symbol.value == "print")
        _id.TypeCheck(symbolTable, errores);
      else if(existefunc(symbolTable, _id.symbol.value, scoup, cadena, errores)) {
        _id.TypeCheck(symbolTable, errores);
      }
      if(next != null)
        next.TypeCheck(symbolTable, errores);
    }

    public bool existefunc(List<TableSymbol> symbolTable, string _id, string _ambito, string _cadenapa, List<string> errores)
    {
      bool existe = false;
      foreach(var s in symbolTable) {
        if(s.tsId == _id) {
          existe = true;
          if(s.strPara == _cadenapa)
            return true;
          else
            errores.Add("Los parametros de la funcion " + _id + " son incorrectos.");
        }
      }
      if(!existe) errores.Add("La funcion " + _id + " no existe.");
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

    public override void TypeCheck(List<TableSymbol> symbolTable, List<string> errores)
    {
      char tipodato1 = dataType;
      left.TypeCheck(symbolTable, errores);
      char tipodato2 = dataType;
      right.TypeCheck(symbolTable, errores);
      char tipodato3 = dataType;
      if(tipodato1 == tipodato2 && tipodato2 == tipodato3) {
        if(next != null) next.TypeCheck(symbolTable, errores);
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
      //id.next = listVar;
      stack.Pop();
      stack.Pop();
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
    }

    public override void TypeCheck(List<TableSymbol> tasimb, List<string> errores)
    {
      if(sentenciaBloque != null) sentenciaBloque.TypeCheck(tasimb, errores);
      if(otro != null) otro.TypeCheck(tasimb, errores);
      if(next != null) next.TypeCheck(tasimb, errores);
    }
  }

  internal class While : Node //<Sentencia> ::= while ( <Expresion> ) <Bloque> 
  {
    Node expresion;
    Node bloque;

    internal While(Stack<Node> pila)
    {
      type = "While";
      pila.Pop();
      bloque = pila.Pop();//quita bloque
      pila.Pop();
      pila.Pop(); //quita )
      pila.Pop();
      expresion = pila.Pop();//quita expresion
      pila.Pop();
      pila.Pop(); //quita (
      pila.Pop();
      pila.Pop(); //quita while
    }

    public override void TypeCheck(List<TableSymbol> symbolTable, List<string> errores)
    {
      if(bloque != null) bloque.TypeCheck(symbolTable, errores);
      if(next != null) next.TypeCheck(symbolTable, errores);
    }
  }

  internal class TableSymbol : Node
  {
    public string tsId;
    public char tsType;
    public string strPara;
    public string ambito;

    public TableSymbol(string _id, char _tipo, string _ambito, string _stpara)
    {
      tsId = _id;
      tsType = _tipo;
      ambito = _ambito;
      strPara = _stpara;
    }
    public TableSymbol(string _id, char _tipo, string _ambito)
    {
      tsId = _id;
      tsType = _tipo;
      ambito = _ambito;
      strPara = "";
    }

    public TableSymbol(string ambito)
    {
      this.ambito = ambito;
    }
  }
}