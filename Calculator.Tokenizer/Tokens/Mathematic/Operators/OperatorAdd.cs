namespace Calculator.Tokenizer.Tokens.Mathematic.Operators;
public class OperatorAdd : TokenOperator
{
    public OperatorAdd(Token left)
    : base(left) { }
    public OperatorAdd(Token left, Token right)
    : base(left, right) { }

    public override string ToString()
    {
        return "+ ";
    }
}
