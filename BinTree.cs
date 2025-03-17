using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleCode
{
    class BinTree
    {
        public BinTree() { }
        // Hàm kiểm tra xem chuỗi có phải là toán tử không
        private bool IsOperator(string c)
        {
            return (c == "+" || c == "-" || c == "*" || c == "/");
        }

        // Hàm ưu tiên toán tử
        private int GetPriority(string c)
        {
            if (c == "+" || c == "-")
                return 1;
            if (c == "*" || c == "/")
                return 2;
            return 0;
        }

        // Chuyển biểu thức infix sang postfix
        private List<string> InfixToPostfix(string expression)
        {
            List<string> result = new List<string>();
            Stack<string> stack = new Stack<string>();
            string number = "";

            for (int i = 0; i < expression.Length; i++)
            {
                string c = expression[i].ToString();

                // Xử lý số (có thể là số nhiều chữ số)
                if (char.IsDigit(expression[i]))
                {
                    number += c;
                    if (i == expression.Length - 1 || !char.IsDigit(expression[i + 1]))
                    {
                        result.Add(number);
                        number = "";
                    }
                }
                // Xử lý dấu ngoặc mở
                else if (c == "(")
                {
                    stack.Push(c);
                }
                // Xử lý dấu ngoặc đóng
                else if (c == ")")
                {
                    while (stack.Count > 0 && stack.Peek() != "(")
                    {
                        result.Add(stack.Pop());
                    }
                    stack.Pop(); // Xóa dấu "("
                }
                // Xử lý toán tử
                else if (IsOperator(c))
                {
                    while (stack.Count > 0 && stack.Peek() != "(" &&
                           GetPriority(stack.Peek()) >= GetPriority(c))
                    {
                        result.Add(stack.Pop());
                    }
                    stack.Push(c);
                }
            }

            // Thêm các toán tử còn lại trong stack
            while (stack.Count > 0)
            {
                result.Add(stack.Pop());
            }

            return result;
        }

        // Xây dựng cây biểu thức từ biểu thức postfix
        private Node BuildExpressionTree(List<string> postfix)
        {
            Stack<Node> stack = new Stack<Node>();

            foreach (string token in postfix)
            {
                if (IsOperator(token))
                {
                    Node right = stack.Pop();
                    Node left = stack.Pop();
                    Node node = new Node(token);
                    node.Left = left;
                    node.Right = right;
                    stack.Push(node);
                }
                else
                {
                    stack.Push(new Node(token));
                }
            }

            return stack.Pop();
        }

        // Tính giá trị của cây biểu thức
        private double Evaluate(Node root)
        {
            if (root == null)
                return 0;

            // Nếu là lá (số), trả về giá trị
            if (root.Left == null && root.Right == null)
                return double.Parse(root.Value);

            // Tính toán các giá trị con
            double leftVal = Evaluate(root.Left);
            double rightVal = Evaluate(root.Right);

            // Thực hiện phép toán
            switch (root.Value)
            {
                case "+":
                    return leftVal + rightVal;
                case "-":
                    return leftVal - rightVal;
                case "*":
                    return leftVal * rightVal;
                case "/":
                    if (rightVal == 0)
                        throw new DivideByZeroException("Không thể chia cho 0");
                    return leftVal / rightVal;
                default:
                    return 0;
            }
        }

        // Hàm chính để tính biểu thức
        public double Calculate(string expression)
        {
            // Loại bỏ khoảng trắng
            expression = expression.Replace(" ", "");

            // Chuyển sang postfix và xây dựng cây
            List<string> postfix = InfixToPostfix(expression);
            Node root = BuildExpressionTree(postfix);

            // Tính giá trị
            return Evaluate(root);
        }
    }

}
