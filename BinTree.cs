using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestConsoleCode
{
    class BinTree
    {
        // Chuyển từ infix sang postfix với hỗ trợ dấu ngoặc
        private string[] InfixToPostfix(string expression)
        {
            Stack<string> operators = new Stack<string>();
            List<string> output = new List<string>();
            string[] tokens = expression.Split(' ',(char) StringSplitOptions.RemoveEmptyEntries);

            foreach (string token in tokens)
            {
                if (double.TryParse(token, out _)) // Nếu là số
                {
                    output.Add(token);
                }
                else if (token == "(") // Dấu ngoặc mở
                {
                    operators.Push(token);
                }
                else if (token == ")") // Dấu ngoặc đóng
                {
                    while (operators.Count > 0 && operators.Peek() != "(")
                    {
                        output.Add(operators.Pop());
                    }
                    if (operators.Count == 0) throw new ArgumentException("Mismatched parentheses");
                    operators.Pop(); // Loại bỏ "("
                }
                else if (IsOperator(token)) // Nếu là toán tử
                {
                    while (operators.Count > 0 && operators.Peek() != "(" &&
                           GetPrecedence(operators.Peek()) >= GetPrecedence(token))
                    {
                        output.Add(operators.Pop());
                    }
                    operators.Push(token);
                }
                else
                {
                    throw new ArgumentException($"Invalid token: {token}");
                }
            }

            // Xử lý các toán tử còn lại trong stack
            while (operators.Count > 0)
            {
                if (operators.Peek() == "(") throw new ArgumentException("Mismatched parentheses");
                output.Add(operators.Pop());
            }

            return output.ToArray();
        }

        // Xây dựng cây từ postfix
        public Node BuildTree(string[] postfix)
        {
            Stack<Node> stack = new Stack<Node>();

            foreach (string token in postfix)
            {
                if (IsOperator(token))
                {
                    Node right = stack.Pop();
                    Node left = stack.Pop();
                    Node node = new Node(token)
                    {
                        Left = left,
                        Right = right
                    };
                    stack.Push(node);
                }
                else
                {
                    if (!double.TryParse(token, out _))
                        throw new ArgumentException($"Invalid number: {token}");
                    stack.Push(new Node(token));
                }
            }
            return stack.Pop();
        }

        // Tính toán giá trị của cây
        public double Evaluate(Node node)
        {
            if (node.Left == null && node.Right == null)
            {
                return double.Parse(node.Value);
            }

            double leftValue = Evaluate(node.Left);
            double rightValue = Evaluate(node.Right);

            switch (node.Value)
            {
                case "+": return leftValue + rightValue;
                case "-": return leftValue - rightValue;
                case "*": return leftValue * rightValue;
                case "/":
                    if (rightValue == 0) throw new DivideByZeroException();
                    return leftValue / rightValue;
                default: throw new InvalidOperationException("Invalid operator");
            }
        }

        // Xử lý chuỗi đầu vào và trả về kết quả
        public double Calculate(string expression)
        {
            string[] postfix = InfixToPostfix(expression);
            Node root = BuildTree(postfix);
            return Evaluate(root);
        }

        // Kiểm tra toán tử
        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "*" || token == "/";
        }

        // Độ ưu tiên của toán tử
        private int GetPrecedence(string op)
        {
            switch (op)
            {
                case "+":
                case "-": return 1;
                case "*":
                case "/": return 2;
                default: return 0;
            }
        }
    }
}
