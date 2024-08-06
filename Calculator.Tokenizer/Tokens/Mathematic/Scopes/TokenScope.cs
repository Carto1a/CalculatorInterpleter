namespace Calculator.Tokenizer.Tokens.Mathematic.Scopes;
public abstract class TokenScope : Token
{
    public ScopeGroup Scope { get; set; }
    public int Level { get; set; } = 0;
    public int Count => Scope.Count;

    public TokenScope(ScopeGroup scope)
    {
        Scope = scope;
    }

    public TokenScope()
    {
        Scope = new ScopeGroup();
    }
}
