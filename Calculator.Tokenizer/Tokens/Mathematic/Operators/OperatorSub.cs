namespace Calculator.Tokenizer.Tokens.Mathematic.Operators;
public class OperatorSub : TokenOperator
{
    public OperatorSub(IToken left) : base(left) { }

    public override string ToString()
    {
        return "- ";
    }
}