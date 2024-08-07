namespace Calculator.Tokenizer.Tokens.Mathematic.Operators;
public class TokenOperator : IToken
{
    public IToken Left { get; set; }
    public IToken? Right
    {
        get => NextToken;
        set => NextToken = value;
    }

    public IToken? NextToken { get; set; }

    public TokenOperator(IToken left)
    {
        Left = left;
    }
}