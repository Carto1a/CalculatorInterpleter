namespace Calculator.Tokenizer.Lexers.Exceptions;
public class LexerOperatorTokenException : LexerInvalidTokenException
{
    public LexerOperatorTokenException(string message) : base(message) { }
    public LexerOperatorTokenException()
        : base("Operator token is invalid") { }
}
