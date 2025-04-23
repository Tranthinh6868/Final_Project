using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestConsoleCode
{
    //class BinTree
    //{
    //    // Chuyển từ infix sang postfix với hỗ trợ dấu ngoặc
    //    private string[] InfixToPostfix(string textInputDisplay)
    //    {
    //        Stack<string> operators = new Stack<string>();
    //        List<string> output = new List<string>();
    //        string[] tokens = textInputDisplay.Split(' ',(char) StringSplitOptions.RemoveEmptyEntries);

    //        foreach (string token in tokens)
    //        {
    //            if (double.TryParse(token, out _)) // Nếu là số
    //            {
    //                output.Add(token);
    //            }
    //            else if (token == "(") // Dấu ngoặc mở
    //            {
    //                operators.Push(token);
    //            }
    //            else if (token == ")") // Dấu ngoặc đóng
    //            {
    //                while (operators.Count > 0 && operators.Peek() != "(")
    //                {
    //                    output.Add(operators.Pop());
    //                }
    //                if (operators.Count == 0) throw new ArgumentException("Mismatched parentheses");
    //                operators.Pop(); // Loại bỏ "("
    //            }
    //            else if (IsOperator(token)) // Nếu là toán tử
    //            {
    //                while (operators.Count > 0 && operators.Peek() != "(" &&
    //                       GetPrecedence(operators.Peek()) >= GetPrecedence(token))
    //                {
    //                    output.Add(operators.Pop());
    //                }
    //                operators.Push(token);
    //            }
    //            else
    //            {
    //                throw new ArgumentException($"Invalid token: {token}");
    //            }
    //        }

    //        // Xử lý các toán tử còn lại trong stack
    //        while (operators.Count > 0)
    //        {
    //            if (operators.Peek() == "(") throw new ArgumentException("Mismatched parentheses");
    //            output.Add(operators.Pop());
    //        }

    //        return output.ToArray();
    //    }

    //    // Xây dựng cây từ postfix
    //    public Node BuildTree(string[] postfix)
    //    {
    //        Stack<Node> stack = new Stack<Node>();

    //        foreach (string token in postfix)
    //        {
    //            if (IsOperator(token))
    //            {
    //                Node right = stack.Pop();
    //                Node left = stack.Pop();
    //                Node node = new Node(token)
    //                {
    //                    Left = left,
    //                    Right = right
    //                };
    //                stack.Push(node);
    //            }
    //            else
    //            {
    //                if (!double.TryParse(token, out _))
    //                    throw new ArgumentException($"Invalid number: {token}");
    //                stack.Push(new Node(token));
    //            }
    //        }
    //        return stack.Pop();
    //    }

    //    // Tính toán giá trị của cây
    //    public double Evaluate(Node node)
    //    {
    //        if (node.Left == null && node.Right == null)
    //        {
    //            return double.Parse(node.Value);
    //        }

    //        double leftValue = Evaluate(node.Left);
    //        double rightValue = Evaluate(node.Right);

    //        switch (node.Value)
    //        {
    //            case "+": return leftValue + rightValue;
    //            case "-": return leftValue - rightValue;
    //            case "*": return leftValue * rightValue;
    //            case "/":
    //                if (rightValue == 0) throw new DivideByZeroException();
    //                return leftValue / rightValue;
    //            default: throw new InvalidOperationException("Invalid operator");
    //        }
    //    }

    //    // Xử lý chuỗi đầu vào và trả về kết quả
    //    public double Calculate(string textInputDisplay)
    //    {
    //        string[] postfix = InfixToPostfix(textInputDisplay);
    //        Node root = BuildTree(postfix);
    //        return Evaluate(root);
    //    }

    //    // Kiểm tra toán tử
    //    private bool IsOperator(string token)
    //    {
    //        return token == "+" || token == "-" || token == "*" || token == "/";
    //    }

    //    // Độ ưu tiên của toán tử
    //    private int GetPrecedence(string op)
    //    {
    //        switch (op)
    //        {
    //            case "+":
    //            case "-": return 1;
    //            case "*":
    //            case "/": return 2;
    //            default: return 0;
    //        }
    //    }
    //}
    //class BinTree
    //{
    //    // Tách biểu thức thành các token (số, dấu ngoặc, toán tử)
    //    //Đây là cách viết quốc tế hoá hoặc gọi là biểu thức chính qui (giống Java :)) )
    //    private string[] Tokenize(string textInputDisplay)
    //    {
    //        string pattern = @"((-?\d*\.\d+|-?\d+|[+\-*/()]))"; 
    //        MatchCollection matches = Regex.Matches(textInputDisplay, pattern);
    //        List<string> tokens = new List<string>();
    //        foreach (Match match in matches)
    //        {
    //            tokens.Add(match.Value);
    //        }
    //        return tokens.ToArray();
    //    }

    //    // Chuyển biểu thức từ infix sang postfix
    //    private string[] InfixToPostfix(string expression)
    //    {
    //        string[] tokens = Tokenize(expression);
    //        Stack<string> operators = new Stack<string>();
    //        List<string> output = new List<string>();

    //        foreach (string token in tokens)
    //        {
    //            // Kiểm tra xem token có phải là số hay không với tất cả kiểu cài đặt của máy
    //            if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
    //            {
    //                output.Add(token);
    //            }
    //            else if (token == "(")
    //            {
    //                operators.Push(token);
    //            }
    //            else if (token == ")")
    //            {
    //                while (operators.Count > 0 && operators.Peek() != "(")
    //                {
    //                    output.Add(operators.Pop());
    //                }
    //                if (operators.Count == 0)
    //                    throw new ArgumentException("Mismatched parentheses");
    //                operators.Pop(); // loại bỏ dấu '('
    //            }
    //            else if (IsOperator(token))
    //            {
    //                while (operators.Count > 0 && operators.Peek() != "(" &&
    //                       GetPrecedence(operators.Peek()) >= GetPrecedence(token))
    //                {
    //                    output.Add(operators.Pop());
    //                }
    //                operators.Push(token);
    //            }
    //            else
    //            {
    //                throw new ArgumentException("Invalid token: " + token);
    //            }
    //        }

    //        while (operators.Count > 0)
    //        {
    //            if (operators.Peek() == "(")
    //                throw new ArgumentException("Mismatched parentheses");
    //            output.Add(operators.Pop());
    //        }

    //        return output.ToArray();
    //    }

    //    // Xây dựng cây từ biểu thức hậu tố
    //    public Node BuildTree(string[] postfix)
    //    {
    //        Stack<Node> stack = new Stack<Node>();

    //        foreach (string token in postfix)
    //        {
    //            if (IsOperator(token))
    //            {
    //                Node right = stack.Pop();
    //                Node left = stack.Pop();
    //                Node node = new Node(token);
    //                node.Left = left;
    //                node.Right = right;
    //                stack.Push(node);
    //            }
    //            else
    //            {
    //                if (!double.TryParse(token, out _))
    //                    throw new ArgumentException("Invalid number: " + token);
    //                stack.Push(new Node(token));
    //            }
    //        }

    //        return stack.Pop();
    //    }

    //    // Đánh giá giá trị của biểu thức
    //    public double Evaluate(Node node)
    //    {
    //        if (node.Left == null && node.Right == null)
    //        {
    //            return double.Parse(node.Value);
    //           //return Convert.ToDouble(node.Value);
    //        }
    //        //Đệ quy
    //        double left = Evaluate(node.Left);
    //        double right = Evaluate(node.Right);

    //        switch (node.Value)
    //        {
    //            case "+": return left + right;
    //            case "-": return left - right;
    //            case "*": return left * right;
    //            case "/":
    //                if (right == 0) throw new DivideByZeroException();
    //                return left / right;
    //            default:
    //                throw new InvalidOperationException("Invalid operator: " + node.Value);
    //        }
    //    }

    //    // Tính toán biểu thức từ chuỗi
    //    public double Calculate(string expression)
    //    {
    //        string[] postfix = InfixToPostfix(expression);
    //        Node root = BuildTree(postfix);
    //        return Evaluate(root);
    //    }

    //    // Kiểm tra toán tử
    //    private bool IsOperator(string token)
    //    {
    //        return token == "+" || token == "-" || token == "*" || token == "/";
    //    }

    //    // Độ ưu tiên toán tử
    //    private int GetPrecedence(string op)
    //    {
    //        switch (op)
    //        {
    //            case "+":
    //            case "-": return 1;
    //            case "*":
    //            case "/": return 2;
    //            default: return 0;
    //        }
    //    }
    //}
    public class BinTree
    {
        // Tách biểu thức thành các token
        private string[] Tokenize(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentException("Expression cannot be empty");

            // Mẫu regex hỗ trợ số âm, số thực (.5, 2.5), số nguyên, toán tử, dấu ngoặc
            string pattern = @"((-?\d*\.\d+|-?\d+|[+\-*/()]))";
            MatchCollection matches = Regex.Matches(expression, pattern);
            List<string> tokens = new List<string>();

            foreach (Match match in matches)
            {
                tokens.Add(match.Value);
            }

            if (tokens.Count == 0)
                throw new ArgumentException("No valid tokens found in expression");

            return tokens.ToArray();
        }

        // Chuyển biểu thức từ infix sang postfix
        private string[] InfixToPostfix(string expression)
        {
            string[] tokens = Tokenize(expression);
            Stack<string> operators = new Stack<string>();
            List<string> output = new List<string>();

            foreach (string token in tokens)
            {
                if (double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                {
                    output.Add(token);
                }
                else if (token == "(")
                {
                    operators.Push(token);
                }
                else if (token == ")")
                {
                    while (operators.Count > 0 && operators.Peek() != "(")
                    {
                        output.Add(operators.Pop());
                    }
                    if (operators.Count == 0)
                        throw new ArgumentException("Mismatched parentheses");
                     operators.Pop(); // Loại bỏ dấu '('
                }
                else if (IsOperator(token))
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

            while (operators.Count > 0)
            {
                if (operators.Peek() == "(")
                    throw new ArgumentException("Mismatched parentheses");
                output.Add(operators.Pop());
            }

            return output.ToArray();
        }

        // Xây dựng cây từ biểu thức hậu tố
        public Node BuildTree(string[] postfix)
        {
            if (postfix == null || postfix.Length == 0)
                throw new ArgumentException("Invalid postfix expression");

            Stack<Node> stack = new Stack<Node>();

            foreach (string token in postfix)
            {
                if (IsOperator(token))
                {
                    if (stack.Count < 2)
                        throw new ArgumentException("Invalid postfix expression");
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
                    if (!double.TryParse(token, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                        throw new ArgumentException($"Invalid number: {token}");
                    stack.Push(new Node(token));
                }
            }

            if (stack.Count != 1)
                throw new ArgumentException("Invalid postfix expression");

            return stack.Pop();
        }

        // Đánh giá giá trị của biểu thức
        public double Evaluate(Node node)
        {
            if (node == null)
                throw new ArgumentException("Invalid tree structure");

            if (node.Left == null && node.Right == null)
            {
                if (!double.TryParse(node.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double value))
                    throw new ArgumentException($"Invalid number: {node.Value}");
                return value;
            }

            if (node.Left == null || node.Right == null)
                throw new ArgumentException("Invalid tree structure");

            double left = Evaluate(node.Left);
            double right = Evaluate(node.Right);

            switch (node.Value)
            {
                case "+": return left + right;
                case "-": return left - right;
                case "*": return left * right;
                case "/":
                    if (right == 0) throw new DivideByZeroException("Division by zero");
                    return left / right;
                default:
                    throw new InvalidOperationException($"Invalid operator: {node.Value}");
            }
        }

        // Tính toán biểu thức từ chuỗi
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

        // Độ ưu tiên toán tử
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
