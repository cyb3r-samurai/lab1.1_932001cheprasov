using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace lab1._1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable table = new DataTable();
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            table.Columns.Add("1", typeof(string));
            table.Columns.Add("2", typeof(string));
            table.Columns.Add("3", typeof(string));
            table.Columns.Add("4", typeof(string));
            table.Columns.Add("5", typeof(string));
            table.Columns.Add("6", typeof(string));
            table.Columns.Add("7", typeof(string));
            table.Columns.Add("8", typeof(string));
            table.Columns.Add("9", typeof(string));
            table.Columns.Add("10", typeof(string));

            for (int i = 0; i <4 ; i++)
            {   
                table.Rows.Add();

            }
            dataGridView1.DataSource= table;
            dataGridView1.Rows[0].HeaderCell.Value = "Time step";
            dataGridView1.Rows[1].HeaderCell.Value = "Distance";
            dataGridView1.Rows[2].HeaderCell.Value = "Max height";
            dataGridView1.Rows[3].HeaderCell.Value = "Speed at the end point";
            dataGridView1.AutoResizeRowHeadersWidth(
        DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }


        double  k, angle, area, weight, dt, vx, vy,x,t,y,v0,cosa,sina,maxheight;

        int count = 0;

        const double g = 9.81, C = 0.15, rho = 1.29;
        private void button1_Click(object sender, EventArgs e)
        {
            if (!timer1.Enabled)
            {
                dt = (double)inputStep.Value;
                if (dt == 0) return;
                string ind = (count + 1).ToString();
                chart1.ChartAreas[0].AxisX.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Minimum = 0;
                chart1.ChartAreas[0].AxisY.Maximum = 25;
                chart1.Series.Add(ind);
                chart1.Series[ind].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                y = (double)inputHeight.Value;
                angle = (double)inputAngle.Value;
                area = (double)inputArea.Value;
                weight = (double)inputWeight.Value;
               
                
                v0 = (double)inputSpeed.Value;

                t = 0; x = 0; maxheight = -1;

                cosa = Math.Cos(angle * Math.PI / 180);
                sina = Math.Sin(angle * Math.PI / 180);

                k = 0.5 * C * rho * area / weight;

                vx = v0 * cosa; vy = v0 * sina;

                chart1.Series[count].BorderWidth = 4;
                chart1.Series[count].Points.AddXY(x, y);
                table.Rows[0][count] = dt;
                timer1.Start();



                count++;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            t = t + dt;
            double v = Math.Sqrt(vx* vx + vy * vy); 
            vx = vx - k * vx * v * dt;
            vy = vy - (g + k * vy * v) * dt;
            x = x + vx * dt;
            y = y + vy * dt;
            if (y > maxheight) maxheight = y;
            chart1.Series[count -1].Points.AddXY(x, y);
            if (y <= 0) timer1.Stop();
            table.Rows[1][count-1] = x;
            table.Rows[2][count-1] = maxheight;
            table.Rows[3][count - 1] = v;
        }
    }
}
