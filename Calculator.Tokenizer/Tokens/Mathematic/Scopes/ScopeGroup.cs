namespace Calculator.Tokenizer.Tokens.Mathematic.Scopes;
public class ScopeGroup : Token
{
    public List<IToken> Children { get; private set; }
    public int Level { get; set; } = TokenScope.NoLevel;
    public int Count => Children.Count;

    public TokenOpenScope OpenScope { get; set; }
    public TokenCloseScope? CloseScope { get; set; }

    public ScopeGroup(TokenOpenScope openScope)
    {
        Children = [];
        OpenScope = openScope;
    }

    public void Add(IToken token)
    {
        Children.Add(token);
    }
}