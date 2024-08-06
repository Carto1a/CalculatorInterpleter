namespace Calculator.Tokenizer.Tokens.Mathematic.Scopes;
public class ScopeGroup : Token
{
    public List<Token> Children { get; private set; }
    public int Count => Children.Count;

    public TokenOpenScope OpenScope { get; set; }
    public TokenCloseScope CloseScope { get; set; }

    public ScopeGroup()
    {
        Children = new List<Token>();
    }

    public void Add(Token token)
    {
        Children.Add(token);
    }
}
