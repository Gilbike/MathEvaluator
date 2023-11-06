using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class Evaluator
    {
        static char[] operators = new char[] { '+', '-', '*', '/', '%' };

        public static double Evaluate(string input)
        {
            string[] operands = input.Split(new char[] { '+', '-', '*', '/', '%' });
            if (operands.Length < 2)
            {
                throw new ArgumentException();
            }

            List<string> tokens = ExtractTokens(input);
            do
            {
                int highestOrder = tokens.Count(x => x == "*" || x == "/" || x == "%");
                for (int i = 1; i < tokens.Count; i += 2)
                {
                    if (char.Parse(tokens[i]) == '*' || char.Parse(tokens[i]) == '/' || char.Parse(tokens[i]) == '%')
                    {
                        double result = PerformBasicCalculation(double.Parse(tokens[i - 1]), double.Parse(tokens[i + 1]), char.Parse(tokens[i]));
                        tokens[i - 1] = result.ToString();
                        tokens.RemoveRange(i, 2);
                    }
                    else if (highestOrder == 0)
                    {
                        double result = PerformBasicCalculation(double.Parse(tokens[i - 1]), double.Parse(tokens[i + 1]), char.Parse(tokens[i]));
                        tokens[i - 1] = result.ToString();
                        tokens.RemoveRange(i, 2);
                    }
                }
            } while (tokens.Count != 1);


            return double.Parse(tokens[0]);
        }

        public static List<string> ExtractTokens(string input)
        {
            List<string> tokens = new List<string>();
            int streak = 0;
            int index = 0;
            foreach (char c in input.Replace(" ", ""))
            {
                bool result = double.TryParse(c.ToString(), out double value);
                if (!result && !operators.Contains(c))
                {
                    throw new ArgumentException();
                }
                else if (!result && operators.Contains(c))
                {
                    string number = input.Substring(index - streak, streak);
                    streak = 0;
                    tokens.Add(number);
                    tokens.Add(c.ToString());
                }
                else
                {
                    streak++;
                }

                index++;
            }
            if (streak != 0)
            {
                string number = input.Substring(input.Length - streak, streak);
                tokens.Add(number);
            }
            return tokens;
        }

        public static double PerformBasicCalculation(double left, double right, char op)
        {
            switch (op)
            {
                case '+':
                    return left + right;
                case '-':
                    return left - right;
                case '*':
                    return left * right;
                case '/':
                    if (right == 0) throw new DivideByZeroException();
                    return left / right;
                case '%':
                    if (right == 0) throw new DivideByZeroException();
                    return left % right;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
