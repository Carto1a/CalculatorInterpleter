using System.Text;
using Calculator.Tokenizer.Tokens;


namespace Calculator.Tokenizer.Lexers;
public class Lexer
{
    public NonTerminalToken Tokenize(string input)
    {
        var numberbuffer = new StringBuilder();

        var number0 = 48;
        var number9 = 57;

        var position = 0;

        while (input.Length >= position)
        {
            if (input[position] == ' ')
            {
                position++;
                continue;
            }

            else if (input[position] >= number0 && input[position] <= number9)
            {
                numberbuffer.Append(input[position]);
                position++;
                continue;
            }
            else
            {
                break;
            }

            /* if (inputnospace[position] == '+') */
            /* { */
            /*     var left = Tokenize(inputnospace.Substring(0, position)); */
            /*     var right = Tokenize(inputnospace.Substring(position + 1)); */
            /*     return new TokenTree(new OperatorAdd(left.Root, right.Root)); */
            /* } */
        }


        return new NonTerminalToken(new List<Token> { new Token() });
    }

    public NonTerminalToken Tokenize(string input, int start, int end)
    {
        return Tokenize(input[start..end]);
    }
}
