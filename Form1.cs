using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TestConsoleCode;

namespace Final_Project
{
    public partial class Form1 : Form
    {
        public String input = "";
        public double result = 0;
        private BinTree binTree = new BinTree();
        frmHistory frmHistory;
        HistoryRepository repository;
        public Form1()
        {
            InitializeComponent();
            repository = new HistoryRepository();
            frmHistory = new frmHistory(this);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "0";
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "1";
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "2";
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "3";
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "4";
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "5";
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "6";
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "7";
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "8";
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtNhap.Text += "9";
        }

        private void btnDiv_Click(object sender, EventArgs e)
        {
            txtNhap.Text += " / ";
        }

        private void btnMul_Click(object sender, EventArgs e)
        {
            txtNhap.Text += " * ";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            txtNhap.Text += " + ";
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            txtNhap.Text += " - ";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            txtNhap.Text += " ( ";
        }

        private void txtClose_Click(object sender, EventArgs e)
        {
            txtNhap.Text += " ) ";
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            input = txtNhap.Text;
            //input = input.Replace('.', ',');
            result = binTree.Calculate(input);
            //result = Math.Round(result, 2);
            //MessageBox.Show("Result: " + result.ToString());
            txtXuat.Text = result.ToString();
            History history = new History(input, result.ToString());
            repository.SaveToExcel(history);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtXuat.Text = "";
            txtNhap.Text = "";
        }

        private void btndot_Click(object sender, EventArgs e)
        {
            
            txtNhap.Text += ".";
        }

        private void btnPercent_Click(object sender, EventArgs e)
        {
            txtNhap.Text += " % ";
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(txtNhap.Text.Length > 1)
            {
                txtNhap.Text = txtNhap.Text.Remove(txtNhap.Text.Length - 1);
                txtXuat.Text = "";
            }
                

            else
            {
                txtNhap.Text = "";
            }
                
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (frmHistory == null || frmHistory.IsDisposed)
            {
                frmHistory = new frmHistory(this);
            }
            frmHistory.Show();
        }
        public void UpdateInput(string input)
        {
            txtNhap.Text = input;
        }

        private void txtNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                if (e.KeyChar == (char)Keys.Enter)
                {
                    e.Handled = true;
                    
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    e.Handled = true;
                    textBox.Clear();
                }
                else if (e.KeyChar == (char)Keys.Back)
                {
                    e.Handled = false; // Cho phép hành vi Backspace mặc định
                }
                //else if (e.KeyChar == (char)Keys.Delete)
                //{
                //    e.Handled = true;
                //    textBox.Clear();
                //}
                else
                {
                    // Cho phép số (0-9), toán tử (+, -, *, /), dấu ngoặc ((), dấu chấm (.)
                    char c = e.KeyChar;
                    if ((c >= '0' && c <= '9') || c == '+' || c == '-' || c == '*' || c == '/' ||
                        c == '(' || c == ')' || c == '.')
                    {
                        e.Handled = false; // Cho phép nhập
                    }
                    else
                    {
                        e.Handled = true; // Chặn ký tự không hợp lệ
                    }
                }
            }
        }

        private void btnAns_Click(object sender, EventArgs e)
        {
            txtNhap.Text = frmHistory.HistorySorted()[0].Intput;
        }
    }
}
