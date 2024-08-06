namespace Calculator.Tokenizer.Tokens;
public class TokenList
{
    public Token? Head { get; set; }
    public Token? Tail { get; set; }

    public int Count { get; private set; }

    public TokenList()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }

    public void Add(Token token)
    {
        Count++;
        if (Head == null)
        {
            Head = token;
            Tail = token;
            return;
        }

        Tail!.NextToken = token;
        Tail = token;
    }
}
