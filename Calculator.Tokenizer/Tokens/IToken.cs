namespace Calculator.Tokenizer.Tokens;
public interface IToken
{
    public IToken? NextToken { get; set; }
}