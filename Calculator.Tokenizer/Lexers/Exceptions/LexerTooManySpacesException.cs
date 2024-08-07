namespace Calculator.Tokenizer.Lexers.Exceptions;
public class LexerTooManySpacesException : LexerException
{
    public LexerTooManySpacesException(string message) : base(message) { }
    public LexerTooManySpacesException()
        : base("Too many white spaces") { }
}
