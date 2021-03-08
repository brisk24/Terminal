using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Обратная_связь_для_терминала
{
    public partial class Form1 : Form
    {
        string abc;
        Button[] button;
        Button buttonSpace;
        public Form1()
        {
            InitializeComponent();
            Init();
        }
        private void Init()
        {
            abc = "йцукенгшщзхъфывапролджэячсмитьбю";
            button = new Button[32];
            buttonSpace = new Button();
            int x = 0;
            int y = 100;
            int z = 0;
            for (int i = 0; i < abc.Length; i++)
            {
                button[i] = new Button();
                button[i].Location = new System.Drawing.Point(x, y);
                button[i].Size = new System.Drawing.Size(50, 50);
                button[i].Text = abc[i].ToString();
                button[i].TabStop = false;       
                Controls.Add(this.button[i]);               
                x += 50;
                z++;
                if(z == 12)
                {
                    y += 50;
                    x = 25;
                }
                if(z == 23)
                {
                    y += 50;
                    x = 75;
                }
                button[i].MouseClick += KeyDownTest;
            }

            buttonSpace.Location = new System.Drawing.Point(x = 125, y += 50);
            buttonSpace.Size = new System.Drawing.Size(350, 50);
            buttonSpace.Text = " ";
            buttonSpace.TabStop = false;
            Controls.Add(this.buttonSpace);
            buttonSpace.MouseClick += KeyDownTest;

        }

        private void KeyDownTest(object sender, MouseEventArgs e)
        {
            textBox1.Text += (sender as Button).Text;
        }
    }
}
