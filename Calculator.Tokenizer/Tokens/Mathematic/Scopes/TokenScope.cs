namespace Calculator.Tokenizer.Tokens.Mathematic.Scopes;
public abstract class TokenScope : Token
{
    public static readonly int NoLevel = -1;

    public ScopeGroup? Scope { get; set; }
    public int Level { get; set; } = NoLevel;
    public int Count => Scope?.Count ?? 0;

    public TokenScope(ScopeGroup scope)
    {
        Scope = scope;
    }

    public TokenScope() { }
}