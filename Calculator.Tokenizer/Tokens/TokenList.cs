namespace Calculator.Tokenizer.Tokens;
public class TokenList
{
    public IToken? Head { get; set; }
    public IToken? Tail { get; set; }

    public int Count { get; private set; }

    public bool IsSyntaxAnalyzed { get; set; } = false;

    public TokenList()
    {
        Head = null;
        Tail = null;
        Count = 0;
    }

    public void Add(IToken token)
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