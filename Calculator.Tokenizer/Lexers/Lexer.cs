using System.Text;
using Calculator.Tokenizer.Tokens;
using Calculator.Tokenizer.Tokens.Mathematic;

namespace Calculator.Tokenizer.Lexers;
public class Lexer
{
    private bool IsNumber(char c)
    {
        var number0 = 48;
        var number9 = 57;

        return c >= number0 && c <= number9;
    }

    public string Untokenize(Token root)
    {
        var buffer = new StringBuilder();
        var current = root;

        while (current != null)
        {
            buffer.Append(current.ToString());
            current = current.Next;
        }

        return buffer.ToString();
    }

    public string UntokenizeRecursive(
        Token? root, StringBuilder? buffer = null)
    {
        buffer ??= new StringBuilder();
        if (root == null)
        {
            return buffer.ToString();
        }

        return UntokenizeRecursive(root.Next, buffer.Append(root.ToString()));
    }

    public Token Tokenize(string input)
    {
        var root = new Token();
        var position = 0;


        while (input.Length >= position)
        {
            if (input[position] == ' ')
            {
                position++;
                continue;
            }
            else if (IsNumber(input[position]))
            {
                var numberbuffer = new StringBuilder();

                while (IsNumber(input[position]))
                {
                    numberbuffer.Append(input[position]);
                    position++;
                }

                var number = decimal.Parse(numberbuffer.ToString());
                root.Next = new TokenNumber(number);
                continue;
            }
            else if (input[position] == '+')
            {
                var left = Tokenize(input.Substring(0, position));
                var right = Tokenize(input.Substring(position + 1));
            }
            else
            {
                throw new Exception("Invalid token");
            }
        }

        return root;
    }

    public Token Tokenize(string input, int start, int end)
    {
        return Tokenize(input[start..end]);
    }
}
