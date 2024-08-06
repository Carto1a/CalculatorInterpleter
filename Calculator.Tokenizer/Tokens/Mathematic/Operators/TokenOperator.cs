namespace Calculator.Tokenizer.Tokens.Mathematic.Operators;
public class TokenOperator : Token
{
    public Token Right { get; set; }
    public Token Left { get; set; }

    public TokenOperator(Token left, Token right)
    : base(right)
    {
        Left = left;
        Right = right;
    }
}
