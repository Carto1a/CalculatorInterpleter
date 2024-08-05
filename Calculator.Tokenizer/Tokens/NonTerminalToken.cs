namespace Calculator.Tokenizer.Tokens;
public class NonTerminalToken : Token
{
    public List<Token> Children { get; }
    public int Count => Children.Count;

    public NonTerminalToken(List<Token> children)
    {
        Children = children;
    }

    public void AddChild(Token child)
    {
        Children.Add(child);
    }
}
