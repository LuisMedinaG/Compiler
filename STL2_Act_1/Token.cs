namespace Compiler
{
  class Token
  {
    public string Data { get; set; }
    public int Type { get; set; }

    public Token()
    {
      Data = "";
      Type = 0;
    }

    public Token(string Data, int Type)
    {
      this.Data = Data; this.Type = Type;
    }
  }
}
