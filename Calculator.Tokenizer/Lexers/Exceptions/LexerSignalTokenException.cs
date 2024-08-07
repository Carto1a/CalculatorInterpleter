namespace Calculator.Tokenizer.Lexers.Exceptions;
public class LexerSignalTokenException : LexerInvalidTokenException
{
    public LexerSignalTokenException(string message) : base(message) { }
    public LexerSignalTokenException()
        : base("Signal token is invalid") { }
}
