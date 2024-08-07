namespace Calculator.Tokenizer.Tokens;
public class Token : IToken
{
    public virtual IToken? NextToken { get; set; }
    public Token()
    {
        NextToken = null;
    }

    public Token(IToken? next)
    {
        NextToken = next;
    }
}