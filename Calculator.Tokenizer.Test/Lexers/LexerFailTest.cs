using System.Text;
using Calculator.Tokenizer.Lexers;
using Calculator.Tokenizer.Lexers.Exceptions;

namespace Calculator.Tokenizer.Test.Lexers;
public class LexerFailTest
{
    [Theory]
    [InlineData("-")]
    [InlineData("+")]
    [InlineData("1-")]
    [InlineData("1+")]
    [InlineData("--")]
    [InlineData("++")]
    [InlineData("-+")]
    [InlineData("+-")]
    public void ThrowIfTokenPlusOrMinusIsWrong(string calculation)
    {
        // Arrange
        var input = new MemoryStream(
            Encoding.UTF8.GetBytes(calculation));

        var lexer = new Lexer(input);

        // Assert
        Assert.ThrowsAny<LexerInvalidTokenException>(() =>
            lexer.Tokenization());
    }
}
