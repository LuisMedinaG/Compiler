namespace Compiler
{
  internal class SymbolTable : Node
  {
    public string id;
    public char tipo;
    //public string ambito;
    public string stpara;

    public SymbolTable(string _id, char _tipo, string _ambito, string _stpara)
    {
      id = _id;
      tipo = _tipo;
      ambito = _ambito;
      stpara = _stpara;
    }
    public SymbolTable(string _id, char _tipo, string _ambito)
    {
      id = _id;
      tipo = _tipo;
      ambito = _ambito;
      stpara = "";
    }

    public SymbolTable(string ambito)
    {
      this.ambito = ambito;
    }
  }
}