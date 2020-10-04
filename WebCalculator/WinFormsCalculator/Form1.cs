using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsCalculator.ServiceReference1;

namespace WinFormsCalculator
{
    public partial class Form1 : Form
    {
        WebCalculatorSoapClient obj;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double res =obj.Add(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            textBox3.Text = Convert.ToString(res); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            obj = new WebCalculatorSoapClient();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double res = obj.Subtract(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            textBox3.Text = Convert.ToString(res);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double res = obj.Multiply(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            textBox3.Text = Convert.ToString(res);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double res = obj.Divide(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text));
            textBox3.Text = Convert.ToString(res);
        }
    }
}
