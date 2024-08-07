namespace Calculator.Tokenizer.Lexers.Exceptions;
public class LexerNoDataException : LexerException
{
    public LexerNoDataException() : base("No data to tokenize") { }
}
