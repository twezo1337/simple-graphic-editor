using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graphic_editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool startPaint = false;
        Graphics g;
        Bitmap b;

        int? initX = null;
        int? initY = null;
        bool drawSquare = false;
        bool drawRectangle = false;
        bool drawCircle = false;

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.pictureBox1.BackColor;
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.pictureBox1.BackColor = this.colorDialog1.Color;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.pictureBox2.BackColor;
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.panel1.BackColor = this.colorDialog1.Color;
                this.pictureBox2.BackColor = this.colorDialog1.Color;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            drawSquare = true;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            drawRectangle = true;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            drawCircle = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            startPaint = false;
            initX = null;
            initY = null;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (startPaint)
            {
                Pen p = new Pen(pictureBox1.BackColor, float.Parse(numericUpDown1.Text));
                //p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;
                this.g.DrawLine(p, new Point(initX ?? e.X, initY ?? e.Y), new Point(e.X, e.Y));
                initX = e.X;
                initY = e.Y;
                this.panel1.Invalidate();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            startPaint = true;
            if (drawSquare)
            {
                SolidBrush sb = new SolidBrush(pictureBox1.BackColor);
               
                this.g.FillRectangle(sb, e.X, e.Y, int.Parse(textBox1.Text), int.Parse(textBox1.Text));
                startPaint = false;
                drawSquare = false;
                this.panel1.Invalidate();
            }
            if (drawRectangle)
            {
                SolidBrush sb = new SolidBrush(pictureBox1.BackColor);
                this.g.FillRectangle(sb, e.X, e.Y, 2 * int.Parse(textBox1.Text), int.Parse(textBox1.Text));
                startPaint = false;
                drawRectangle = false;
                this.panel1.Invalidate();
            }
            if (drawCircle)
            {
                SolidBrush sb = new SolidBrush(pictureBox1.BackColor);
                this.g.FillEllipse(sb, e.X, e.Y, int.Parse(textBox1.Text), int.Parse(textBox1.Text));
                startPaint = false;
                drawCircle = false;
                this.panel1.Invalidate();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.b = new Bitmap(panel1.Size.Width, panel1.Size.Height);
            this.panel1.Image = b;
            this.g = Graphics.FromImage(b);
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            panel1.BackColor = Color.White;
            pictureBox2.BackColor = Color.White;
            panel1.Invalidate();
           
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.g.Clear(Color.White);
                this.g.DrawImage(Image.FromFile(this.openFileDialog1.FileName), new Point(0, 0));
                this.panel1.Invalidate();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.b.Save(this.saveFileDialog1.FileName,
                System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }
    }
}
