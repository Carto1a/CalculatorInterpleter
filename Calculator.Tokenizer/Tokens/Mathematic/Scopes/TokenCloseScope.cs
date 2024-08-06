namespace Calculator.Tokenizer.Tokens.Mathematic.Scopes;
public class TokenCloseScope : TokenScope
{
    public TokenCloseScope(ScopeGroup scope) : base(scope)
    {
        Scope.CloseScope = this;
    }

    public override string ToString()
    {
        return ") ";
    }
}
