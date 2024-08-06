namespace Calculator.Tokenizer.Tokens;
public class Token
{
    public virtual Token? NextToken { get; set; }
    public Token()
    {
        NextToken = null;
    }

    public Token(Token? next)
    {
        NextToken = next;
    }
}
