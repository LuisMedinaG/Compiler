namespace Compiler
{
  class Token
  {
    public string value { get; set; }
    public int type { get; set; }

    public Token() { }

    public Token(string Value, int Type)
    {
      this.value = Value;
      this.type = Type;
    }

    public override string ToString()
    {
      return "Token(" + type + ", " + value + ")";
    }
  }
}
