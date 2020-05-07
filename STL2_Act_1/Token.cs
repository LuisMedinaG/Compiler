namespace Compiler
{
  class Token
  {
    public string Value { get; set; }
    public int Type { get; set; }

    public Token()
    {
      Value = "";
      Type = 0;
    }

    public Token(string Data, int Type)
    {
      this.Value = Data; this.Type = Type;
    }
  }
}
