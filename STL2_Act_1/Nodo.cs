using System.Collections.Generic;

namespace STL2_Act_1
{
  public class Nodo
  {
    public string simbolo;
    public Nodo siguiente;
    public static string ambito;
    public char tipodato;
    public int TipoNodo;

    public Nodo()
    {
      simbolo = "";
      ambito = "";
      siguiente = null;
    }

    public Nodo(string _ambito)
    {
      simbolo = "";
      ambito = _ambito;
      siguiente = null;
    }
  }
  /*-----------------------------------------------*/
  public class programa : Nodo
  {
    public programa(Stack<Nodo> pila)
    {
      pila.Pop();//quita estado
      siguiente = (pila.Pop());
    }
  }

  public class Id : Nodo
  {
    public Id(string _simbolo)
    {
      simbolo = _simbolo;
      // clase = "id";
    }
  }

  public class Tipo : Nodo
  {
    public Tipo(string _simbolo)
    {
      simbolo = _simbolo;
    }
  }

  public class DefVar : Nodo
  {
    Nodo tipo;
    Nodo id;
    Nodo lvar;

    public DefVar(Stack<Nodo> pila)//<DefVar> ::= tipo id <ListaVar> ; 
    {
      pila.Pop();//quita estado
      pila.Pop(); //quita  ;
      pila.Pop(); //quita estado estado
      lvar = (pila.Pop()); //quita ListaVar
      pila.Pop(); //quita estado
      id = new Id((pila.Pop()).simbolo); //quita Id
      pila.Pop(); //quita estado
      tipo = new Tipo((pila.Pop()).simbolo); //quita tipo
    }
  }

  public class DefFunc : Nodo
  {
    Nodo tipo;
    Nodo id;
    Nodo parametros;
    Nodo bloqFunc;
    public static string varlocal;
    public DefFunc(Stack<Nodo> pila)//<DefFunc> ::= tipo id ( <Parametros> ) <BloqFunc> 
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
      id = new Id((pila.Pop()).simbolo);//quita id
      pila.Pop();//quita estado
      tipo = new Tipo((pila.Pop()).simbolo);//quita el tipo
    }
  }

  public class Parametros : Nodo //<Parametros> ::= tipo id <ListaParam> 
  {
    Nodo tipo;
    Nodo id;
    public Nodo lparametros;
    public Parametros(Stack<Nodo> pila)
    {
      pila.Pop();//quita estado
      lparametros = (pila.Pop());//quita la lista de aprametros
      pila.Pop();//quita estado
      id = new Id((pila.Pop()).simbolo);//quita el id
      pila.Pop(); //quita estado
      tipo = new Tipo((pila.Pop()).simbolo);//quita el tipo
    }
  }

  public class Asignacion : Nodo//<Sentencia> ::= id = <Expresion> ; 
  {
    Nodo id;
    Nodo expresion;

    public Asignacion(Stack<Nodo> pila)//<Sentencia> ::= id = <Expresion> ;
    {
      pila.Pop();
      pila.Pop();//quita la ;
      pila.Pop();
      expresion = (pila.Pop());//quita expresion
      pila.Pop();
      pila.Pop();//quita =
      pila.Pop();
      id = new Id((pila.Pop()).simbolo);//quita id
    }
  }

  public class While : Nodo //<Sentencia> ::= while ( <Expresion> ) <Bloque> 
  {
    Nodo expresion;
    Nodo bloque;

    public While(Stack<Nodo> pila)
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
  public class Dowhile : Nodo
  {
    Nodo bloque;
    Nodo expresion;

    public Dowhile(Stack<Nodo> pila)
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

  public class For : Nodo //<Sentencia> ::= for id = <Expresion> : <Expresion> : <Expresion> <SentenciaBloque>
  {
    Nodo senbloque;
    Nodo expresion1;
    Nodo expresion2;
    Nodo expresion3;
    Nodo id;

    public For(Stack<Nodo> pila)
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
      id = new Id((pila.Pop()).simbolo);//quita id
      pila.Pop();
      pila.Pop();//quita for
    }
  }

  public class Constante : Nodo
  {
    public Constante(string _simbolo)
    {
      simbolo = _simbolo;
      // clase = "cons";
    }
  }

  public class Llamadafunc : Nodo//<LlamadaFunc> ::= id ( <Argumentos> )
  {
    Nodo id;
    Nodo argumentos;

    public Llamadafunc(Stack<Nodo> pila)
    {
      // clase = "fun";
      pila.Pop();
      pila.Pop();//quita )
      pila.Pop();
      argumentos = (pila.Pop());//quita exprecion
      pila.Pop();
      pila.Pop();//quita (
      pila.Pop();
      id = new Id((pila.Pop()).simbolo);//quita id
    }
  }

  public class Operacion1 : Nodo//<Expresion> ::= opSuma <Expresion> 
  {
    Nodo der;

    public Operacion1(Stack<Nodo> pila)
    {
      pila.Pop();
      der = (pila.Pop());//quita exprsion
      pila.Pop();
      simbolo = (pila.Pop()).simbolo;//quita el operador
    }
  }

  public class Operacion2 : Nodo
  {
    Nodo der;
    Nodo izq;
    public Operacion2(Stack<Nodo> pila)
    {
      pila.Pop();
      der = (pila.Pop());//quita exprsion
      pila.Pop();
      simbolo = (pila.Pop()).simbolo;//quita el operador
      pila.Pop();
      izq = (pila.Pop());//quita expresion
    }
  }

  public class Constructores
  {

    public Constructores()
    {

    }
  }

  public class TEMPORALCLASS
  {
    /// <summary>
    /// Cambar comportamineto normal de tabla de analisis sintacitico
    /// </summary>
    public void ARBOL_SINTACTICO_TABLA()
    {
      /*aux - no originales*/
      Stack<Nodo> pila = new Stack<Nodo>();
      int accion = 0;
      int regla = 0;

      if (accion < 0) {
        regla = -(accion + 2);
        Nodo nodo = new Nodo();
        nodo = null;

        switch (regla + 1) {
          case 1:  //<programa> ::= <Definiciones> 
            nodo = new programa(pila);
            break;
          case 3://<Definiciones> ::= <Definicion> <Definiciones>
          case 16://<DefLocales> ::= <DefLocal> <DefLocales> 	
          case 20://<Sentencias> ::= <Sentencia> <Sentencias>
          case 32://<Argumentos> ::= <Expresion> <ListaArgumentos> 
            pila.Pop();//quita estado
            Nodo aux = (pila.Pop());//quita <definiciones>
            pila.Pop();//quita estado
            nodo = (pila.Pop());//quita <definicion>
            nodo.siguiente = aux;
            break;
          //case 1:
          case 4://<Definicion> ::= <DefVar>
          case 5://<Definicion> ::= <DefFunc> 
          case 17://<DefLocal> ::= <DefVar> 
          case 18://<DefLocal> ::= <Sentencia> 
          case 35://<Atomo> ::= <LlamadaFunc> 
          case 39://<SentenciaBloque> ::= <Sentencia> 
          case 40://<SentenciaBloque> ::= <Bloque> 
          case 50://<Expresion> ::= <Atomo>
            pila.Pop();//quita estado
            nodo = (pila.Pop());//quita defvar
            break;

          case 6:// <DefVar> ::= tipo id <ListaVar> ;
            nodo = new DefVar(pila);
            break;
          case 8:  //<ListaVar> ::= , id <ListaVar>
            pila.Pop();//quita estado
            Nodo lvar = (pila.Pop());
            pila.Pop();//quita estado
            nodo = new Id((pila.Pop()).simbolo);//quita id
            nodo.siguiente = lvar;
            pila.Pop();//quita estado
            pila.Pop();//quita la coma
            break;

          case 9://<DefFunc> ::= tipo id ( <Parametros> ) <BloqFunc>
            nodo = new DefFunc(pila);
            break;

          case 11://<Parametros> ::= tipo id <ListaParam>
            nodo = new Parametros(pila);
            break;
          case 13://<ListaParam> ::= , tipo id <ListaParam>
            nodo = new Parametros(pila);
            pila.Pop();//quita estado;
            pila.Pop();//quita la coma
            break;
          case 14://<BloqFunc> ::= { <DefLocales> }
          case 30://<Bloque> ::= { <Sentencias> } 
          case 41://<Expresion> ::= ( <Expresion> ) 
            pila.Pop();//quita estado
            pila.Pop();//quita }
            pila.Pop();//quita estado
            nodo = (pila.Pop());//quita <deflocales> o <sentencias>
            pila.Pop();
            pila.Pop();//quita la {
            break;

          case 21: //<Sentencia> ::= id = <Expresion> ;
            nodo = new Asignacion(pila);
            break;

          case 22://<Sentencia> ::= if ( <Expresion> ) <SentenciaBloque> <Otro>
                  //nodo = new If(pila) // TODO : Agregar la clase para If con su constructor
            break;

          case 23://<Sentencia> ::= while ( <Expresion> ) <Bloque> 
            nodo = new While(pila);
            break;

          case 24://<Sentencia> ::= do <Bloque> while ( <Expresion> ) ;
            nodo = new Dowhile(pila);
            break;

          case 25://<Sentencia> ::= for id = <Expresion> : <Expresion> : <Expresion> <SentenciaBloque>
            nodo = new For(pila);
            break;

          case 26://<Sentencia> ::= return <Expresion> ;
            // nodo = new Return(pila);
            break;

          case 27://<Sentencia> ::= <LlamadaFunc> ; 
            pila.Pop();
            pila.Pop();//quita ;
            pila.Pop();
            nodo = (pila.Pop());//quita llamadafunc
            break;

          case 29://<Otro> ::= else <SentenciaBloque> 
            pila.Pop();
            nodo = (pila.Pop());//quita sentencia bloque
            pila.Pop();
            pila.Pop();//quita el else
            break;
          case 34:// <ListaArgumentos> ::= , <Expresion> <ListaArgumentos> 
            pila.Pop();
            aux = (pila.Pop());//quita la lsta de argumentos
            pila.Pop();
            nodo = (pila.Pop());//quita expresion
            pila.Pop();
            pila.Pop();//quita la ,
            nodo.siguiente = aux;
            break;

          //aqui cae R2,R7,R10,R12,R15,R19,R28,R31,R33,
          default:
            // for (int i = 0; i < lonreglas[regla] * 2; i++) pila.Pop();
            break;
        }

        Nodo fila = pila.Peek();
        // columna = idreglas[regla];
        // transicion = tabla[fila][columna];
        // NoTerminal NT = new NoTerminal(idreglas[regla]);
        // NT = nodo;
        // pila.Push(NT);
        // pila.Push(new Estado(transicion));
        // accion = tabla[transicion][(int)lex.tiposimlr];
      }
    }

  }

  public class Estado : Nodo
  {
    internal int numestado;
  }
}

