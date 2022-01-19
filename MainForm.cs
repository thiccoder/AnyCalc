using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace AnyCalc
{
    public partial class MainForm : Form
    {
        private int CurrentBase = 10;
        private bool Overflow = false; 
        private readonly List<StoredObject> objects = new();
        private static readonly Font ButtonFont = new("OCR A Extended", 30F, FontStyle.Bold, GraphicsUnit.Pixel);
        private static readonly string[] operators = { "+", "-", "*", "/", "^", "%", "=", "Clr", "CE", "⌫" };
        private readonly List<Button> NumButtons = new();
        private readonly List<Button> OpButtons = new();
        private readonly List<Button> ClearButtons = new();

        private Number Calculate()
        {
            Number res = objects[0];
            Number num;
            for (int i = 1; i < objects.Count - 1; i++)
            {

                if (objects[i].IsOperator)
                {
                    string op = objects[i];
                    num = objects[++i];

                    switch (op)
                    {
                        case "+":
                            res += num;
                            break;
                        case "-":
                            res -= num;
                            break;
                        case "*":
                            res *= num;
                            break;
                        case "/":
                            res /= num;
                            break;
                        case "^":
                            res ^= num;
                            break;
                        case "%":
                            res %= num;
                            break;
                    }
                }
            }
            if (res.ToString().Length > Out.MaxLength) 
            {
                Overflow = true;
            }
            return res;
        }
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var ctrl = (Control)sender;
            var s = 50;
            int minTabIdx = 4;
            int xoff = (ctrl.Size.Width / 2) - (s * 4);
            int yoff = (ctrl.Size.Height / 2) - (s * 3);
            Button button;
            for (int i = 0; i < 3; i++)
            {
                button = new();
                button.BackgroundImageLayout = ImageLayout.None;
                button.Font = ButtonFont;
                button.Location = new Point((i % 3 * 2 * s) + xoff,  yoff);
                button.Name = $"{operators[7 + i]}";
                button.Size = new Size(2 * s, s);
                button.TabIndex = i + minTabIdx;
                button.Text = button.Name;
                button.UseVisualStyleBackColor = true;
                button.Click += ClearButton_Click;
                ClearButtons.Add(button);
                Controls.Add(button);
            }
            for (var i = 0; i < 7; i++)
            {
                button = new();
                button.BackgroundImageLayout = ImageLayout.None;
                button.Font = ButtonFont;
                button.Location = new Point(xoff +6* s, (i % 7 * s) + yoff);
                button.Name = $"{operators[i]}";
                button.Size = new Size(s, s);
                button.TabIndex = i + minTabIdx + 3;
                button.Text = button.Name;
                button.UseVisualStyleBackColor = true;
                button.Click += OpButton_Click;
                OpButtons.Add(button);
                Controls.Add(button);
            }
            
            for (var i = 0; i < 36; i++)
            {

                button = new();
                button.BackgroundImageLayout = ImageLayout.None;
                button.Font = ButtonFont;
                button.Location = new Point((i % 6 * s) + xoff, (1+i / 6) * s + yoff);
                button.Name = $"{Number.digitSet[i]}";
                button.Size = new Size(s, s);
                button.TabIndex = i + minTabIdx + 9;
                button.Text = button.Name;
                button.UseVisualStyleBackColor = true;
                button.Click += NumButton_Click;
                NumButtons.Add(button);
                Controls.Add(button);
            }
            
            
        }

        private void HandleNumber(string text) 
        {
            if (Number.Fits(Out.Text + text, CurrentBase) && Out.Text.Length < 10)
                Out.Text += text;
            else if (Out.Text == "0" && text != "0")
                Out.Text = text;
            else
                SystemSounds.Asterisk.Play();
        }

        private void HandleOperator(string text)
        {
            bool doOperator = true;
            if (doOperator)
            {
                Out.PlaceholderText = "";
                try
                {
                    if (Out.TextLength > 0)
                    {
                        objects.Add(new StoredObject(new Number(Out.Text, CurrentBase)));
                    }
                    else
                    {
                        objects.Add(new StoredObject(new Number(Out.PlaceholderText, CurrentBase)));
                    }
                }
                catch { if (objects.Last().IsOperator) { objects.RemoveAt(objects.Count -1); } }

                var obj = new StoredObject(text);
                if (text == "=")
                {
                    Number res = Calculate();
                    objects.Clear();

                    if (Overflow)
                    {
                        Overflow = false;
                        obj = new StoredObject(Number.Zero(CurrentBase));
                        SystemSounds.Hand.Play();
                        Out.PlaceholderText = "OVERFLOW";
                    }
                    else
                    {
                        obj = new StoredObject(res);
                        Out.PlaceholderText = res.ToString();
                    }
                    // FullString.Text = "";
                }
                //else FullString.Text += Number.Formal(Out.Text,CurrentBase);
                //FullString.Text += obj.ToString();
                objects.Add(obj);

            }
            else
            {
                SystemSounds.Hand.Play();
                Out.PlaceholderText = "INCORRECT";
            }
            FullString.Text = string.Join("", objects.ToArray());
            Out.Text = "";
        }
        private void HandleClear(string text)
        {
            if ((text == "\b" || text == "⌫") && Out.Text.Length > 0)
                Out.Text = Out.Text.Remove(Out.Text.Length - 1, 1);
            else if (text == "Clr" && Out.Text.Length > 0)
                Out.Text = "";
            else if (text == "CE") 
            {
                Out.Text = "";
                Out.PlaceholderText = "";
                FullString.Text = "";
                objects.Clear();
            }

        }

        private void NumButton_Click(object sender, EventArgs e)
        {
            HandleNumber(((Button)sender).Text);
        }
        private void OpButton_Click(object sender, EventArgs e)
        {
            HandleOperator(((Button)sender).Text);
        }
        private void ClearButton_Click(object sender, EventArgs e)
        {
            HandleClear(((Button)sender).Text);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool isBaseEnter = ActiveControl.Equals(BaseUpDown);
            
            if (!isBaseEnter)
            {
                string keyChar = e.KeyChar.ToString().ToUpper();
                if (operators.Contains(keyChar))
                {
                    HandleOperator(keyChar);
                }
                else if (keyChar == "\n")
                {
                    HandleOperator("=");
                }
                else if (Number.digitSet.Contains(keyChar[0]))
                {
                    HandleNumber(keyChar);
                }
                else { HandleClear(keyChar); }
                e.Handled = true;
            }
        }
        private void MainForm_KeyDown(object sender, KeyEventArgs e) 
        {
            bool isBaseEnter = ActiveControl.Equals(BaseUpDown);
            if (!isBaseEnter) 
            {
                if (e.KeyCode == Keys.Delete) HandleClear("del");
            }
        }
        private void ChangeBase_Click(object sender, EventArgs e)
        {
            CurrentBase = (int)BaseUpDown.Value;
            if (objects.Any())
            {
                Number res;
                if (objects.Count > 1)
                {
                    res = Calculate() << CurrentBase;
                    if (Overflow)
                    {
                        Overflow = false;
                        objects.Clear();
                        objects.Add(new StoredObject(Number.Zero(CurrentBase)));
                        SystemSounds.Hand.Play();
                        Out.PlaceholderText = "OVERFLOW";
                        FullString.Text = "";
                        return;
                    }
                }
                else
                {
                    res = (Number)objects[0] << CurrentBase;
                }
                objects.Clear();
                objects.Add(new StoredObject(res));
                Out.PlaceholderText = ((Number)objects[0]).ToString();
                FullString.Text = "";
            }
        }
    }
}
