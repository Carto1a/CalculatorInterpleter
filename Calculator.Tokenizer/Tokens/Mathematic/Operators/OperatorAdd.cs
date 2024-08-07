namespace Calculator.Tokenizer.Tokens.Mathematic.Operators;
public class OperatorAdd : TokenOperator
{
    public OperatorAdd(IToken left) : base(left) { }

    public override string ToString()
    {
        return "+ ";
    }
}