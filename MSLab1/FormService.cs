using System;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MSLab1
{
    public class FormService
    {
        public void ChartClear(Chart chart)
        {
            foreach (var a in chart.Series)
            {
                a.Points.Clear();
            }
        }

        public void FillTextBox(TextBox box, double value)
        {
            box.Text = Convert.ToString(value);
        }
    }
}
