using System;
using System.Collections.Generic;

class Program
{
    static int Precedence(char op)
    {
        switch (op)
        {
            case '+':
            case '-':
                return 1;
            case '*':
            case '/':
                return 2;
            case '^':
                return 3;
            default:
                return -1;
        }
    }

    static string InfixToPostfix(string expression)
    {
        string result = "";
        Stack<char> stack = new Stack<char>();

        foreach (char c in expression)
        {
            if (Char.IsLetterOrDigit(c))
            {
                result += c;
            }
            else if (c == '(')
            {
                stack.Push(c);
            }
            else if (c == ')')
            {
                while (stack.Count > 0 && stack.Peek() != '(')
                {
                    result += stack.Pop();
                }
                if (stack.Count > 0 && stack.Peek() == '(')
                {
                    stack.Pop();
                }
            }
            else
            {
                while (stack.Count > 0 && Precedence(c) <= Precedence(stack.Peek()))
                {
                    result += stack.Pop();
                }
                stack.Push(c);
            }
        }

        while (stack.Count > 0)
        {
            result += stack.Pop();
        }

        return result;
    }

    static string InfixToPrefix(string expression)
    {
        char[] arr = expression.ToCharArray();
        Array.Reverse(arr);

        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == '(')
            {
                arr[i] = ')';
                i++;
            }
            else if (arr[i] == ')')
            {
                arr[i] = '(';
                i++;
            }
        }

        string postfix = InfixToPostfix(new string(arr));
        char[] prefixArr = postfix.ToCharArray();
        Array.Reverse(prefixArr);
        return new string(prefixArr);
    }

    static void Main()
    {
        Console.WriteLine("Ingresa una expresión en notación infija:");
        string infixExpression = Console.ReadLine();
        string postfixExpression = InfixToPostfix(infixExpression);
        string prefixExpression = InfixToPrefix(infixExpression);

        Console.WriteLine("Expresión Infija: " + infixExpression);
        Console.WriteLine("Expresión Posfija: " + postfixExpression);
        Console.WriteLine("Expresión Prefija: " + prefixExpression);
    }
}
