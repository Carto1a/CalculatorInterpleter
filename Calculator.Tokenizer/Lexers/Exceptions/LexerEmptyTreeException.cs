namespace Calculator.Tokenizer.Lexers.Exceptions;
public class LexerEmptyTreeException : LexerException
{
    public LexerEmptyTreeException() : base("The tree is empty") { }
}
