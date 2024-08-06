namespace Calculator.Tokenizer.Tokens.Mathematic.Scopes;
public class TokenOpenScope : TokenScope
{
    public TokenOpenScope(int level)
    {
        Level = level;
        Scope.OpenScope = this;
    }

    public void Add(Token token)
    {
        Scope.Add(token);
    }

    public override string ToString()
    {
        return "( ";
    }
}
