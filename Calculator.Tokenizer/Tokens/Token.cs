namespace Calculator.Tokenizer.Tokens;
public class Token
{
    public Token? Next { get; set; }
    public Token()
    {
        Next = null;
    }

    public Token(Token? next)
    {
        Next = next;
    }
}
