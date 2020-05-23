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

    public Token(string Value, int Type)
    {
      this.Value = Value;
      this.Type = Type;
    }
  }
}
