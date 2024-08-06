namespace Calculator.Tokenizer.Tokens.Mathematic.Operators;
public class OperatorSub : TokenOperator
{
    public OperatorSub(Token left)
    : base(left) { }
    public OperatorSub(Token left, Token right)
    : base(left, right) { }

    public override string ToString()
    {
        return "- ";
    }
}
