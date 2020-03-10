namespace STL2_Act_1
{
  internal class Node
  {
    public string Symbol { get; set; }
    public Nodo Next { get; set; }
    public int Type { get; set; }

    public Node()
    {
      Symbol = "";
      Next = null;
      Type = 0;
    }

    public Node(string Symbol, int Type)
    {
      this.Symbol = Symbol;
      this.Type = Type;
    }
  }
}