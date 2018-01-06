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

namespace MSLab1
{
    public partial class Form1 : Form
    {
        private VariationalSeriesCharacteristic _variationalSeriesCharacteristic;
        private IFileService _fileService;
        private FormService _formService;
        private NumberService _numberService;
        private ClassService _classService;
        private List<double> listFileContent;
        private List<Class> listClasses;
        private string filePath = @"C:\Users\USER\Downloads\Mat_Stat2\VP&MC\labs_своя программа\data_lab1,2\25\exp.txt";
        public Form1(IFileService fileService, FormService formService)
        {
            InitializeComponent();
            _fileService = fileService;
            _formService = formService; 
            PrepareData();
        }

        private void PrepareData()
        {
            List<string> str = _fileService.ReadFromFile(filePath);
            listFileContent = new List<double>();
            for (int i = 0; i < str.Count; i++)
            {
                string result = string.Empty;
                if (str[i].Contains("."))
                {
                    result = str[i].Replace(".", ",");
                    listFileContent.Add(Convert.ToDouble(result));
                }
                else
                {
                    listFileContent.Add(Convert.ToDouble(str[i]));
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DataBuild();
        }

        private void DataBuild()
        {
            _numberService = new NumberService(listFileContent);
            _classService = new ClassService();
            dataGridView1.DataSource = _numberService.GetAllListNumber();
            var variationalSeries = _numberService.GetAllListNumber();
            listClasses = _classService.GetListClasses(listFileContent, 0);
            Calculation();
        }

        private void Calculation()
        {
            List<Number> listNumber = _numberService.GetAllListNumber();
            dataGridView1.DataSource = listNumber;
            dataGridView2.DataSource = listClasses;
            _formService.ChartClear(chart1);            
            for (int i = 0; i < listClasses.Count; i++)
            {
                chart1.Series["Частота"].Points.AddXY($"{listClasses[i].StartLimit} ... {listClasses[i].EndLimit}", listClasses[i].Frequence);
            }
            _formService.ChartClear(chart2);
            chart2.Series.Clear();
            for (int i = 0; i < listNumber.Count; i++)
            {
                chart2.Series.Add($"ser{i}").ChartType = SeriesChartType.Line;
                chart2.Series[i].Color = Color.Chocolate;
                chart2.Series[i].BorderWidth = 5;
                chart2.Series[i].Points.AddXY(listNumber[i].VariantValue, listNumber[i].DistributionValue);
                if (i < listNumber.Count - 1)
                {
                    chart2.Series[i].Points.AddXY(listNumber[i + 1].VariantValue, listNumber[i].DistributionValue);
                    chart2.Series[i].IsVisibleInLegend = false;
                }
                else
                {
                    chart2.ChartAreas[0].AxisX.Maximum = listNumber[i].VariantValue + 3;
                    chart2.Series[i].Points.AddXY(listNumber[i].VariantValue + 3, listNumber[i].DistributionValue);
                    chart2.Series[i].LegendText = "Ряд";
                    chart2.Series[i].IsVisibleInLegend = true;
                }
            }
            _formService.ChartClear(chart3);
            chart3.Series.Clear();
            for (int i = 0; i < listClasses.Count; i++)
            {
                chart3.Series.Add($"ser{i}").ChartType = SeriesChartType.Line;
                chart3.Series[i].Color = Color.Chocolate;
                chart3.Series[i].BorderWidth = 5;
                chart3.Series[i].Points.AddXY(listClasses[i].StartLimit, listClasses[i].DistribValue);
                if (i < listClasses.Count - 1)
                {
                    chart3.Series[i].Points.AddXY(listClasses[i + 1].StartLimit, listClasses[i].DistribValue);
                    chart3.Series[i].IsVisibleInLegend = false;
                }
                else
                {
                    chart3.ChartAreas[0].AxisX.Maximum = listClasses[i].StartLimit + 3;
                    chart3.Series[i].Points.AddXY(listClasses[i].StartLimit+3, listClasses[i].DistribValue);
                    chart3.Series[i].LegendText = "Класс";
                    chart3.Series[i].IsVisibleInLegend = true;
                }
            }
            // Выборка
            PrepareData();

            var _sampleCharacteristic = new SampleCharacteristics(listFileContent);
            _formService.FillTextBox(txtAverArif, _sampleCharacteristic.GetAvarage().Meaning);
            _formService.FillTextBox(txtMediane, _sampleCharacteristic.GetMediane().Meaning);
            _formService.FillTextBox(txtAverSquare, _sampleCharacteristic.GetSquareAvarage().Meaning);
            _formService.FillTextBox(txtAssym, _sampleCharacteristic.GetUnbaisedAssemetry().Meaning);
            _formService.FillTextBox(txtDeviation, _sampleCharacteristic.GetUnbaisedDeviation().Meaning);
            _formService.FillTextBox(txtkurtosis, _sampleCharacteristic.GetKurt().Meaning);
            _formService.FillTextBox(txtContrKurt, _sampleCharacteristic.GetConrtKurt().Meaning);
            _formService.FillTextBox(txtVariation, _sampleCharacteristic.GetCoefVariation().Meaning);
            _formService.FillTextBox(txtDeviationForAssymerty, _sampleCharacteristic.GetDeviationForAssemetry());
            _formService.FillTextBox(txtDeviationForContrKurtosis, _sampleCharacteristic.GetDeviationForContrKurtosis());
            _formService.FillTextBox(txtDeviationForDeviation, _sampleCharacteristic.GetDeviationForDeviation());
            _formService.FillTextBox(txtAvarageDeviation, _sampleCharacteristic.GetDeviationForAvarage());
            _formService.FillTextBox(txtDeviationForKurtosis, _sampleCharacteristic.GetDeviationForKurtosis());
            _formService.FillTextBox(txtDeviationForVariation, _sampleCharacteristic.GetDeviationForVariation());

            // confident interval
            _formService.FillTextBox(txtAvarageLowLimit, _sampleCharacteristic.GetLowLimit(_sampleCharacteristic.GetAvarage().Meaning, _sampleCharacteristic.GetDeviationForAvarage()));
            _formService.FillTextBox(txtAvarageHighLimit, _sampleCharacteristic.GetHighLimit(_sampleCharacteristic.GetAvarage().Meaning, _sampleCharacteristic.GetDeviationForAvarage()));
            _formService.FillTextBox(txtAssymetryLowLimit, _sampleCharacteristic.GetLowLimit(_sampleCharacteristic.GetUnbaisedAssemetry().Meaning, _sampleCharacteristic.GetDeviationForAssemetry()));
            _formService.FillTextBox(txtAssymetryHighLimit, _sampleCharacteristic.GetHighLimit(_sampleCharacteristic.GetUnbaisedAssemetry().Meaning, _sampleCharacteristic.GetDeviationForAssemetry()));
            _formService.FillTextBox(txtDeviationLowLimit, _sampleCharacteristic.GetLowLimit(_sampleCharacteristic.GetUnbaisedDeviation().Meaning, _sampleCharacteristic.GetDeviationForDeviation()));
            _formService.FillTextBox(txtDeviationHighLimit, _sampleCharacteristic.GetHighLimit(_sampleCharacteristic.GetUnbaisedDeviation().Meaning, _sampleCharacteristic.GetDeviationForDeviation()));
            _formService.FillTextBox(txtKurtosisLowLimit, _sampleCharacteristic.GetLowLimit(_sampleCharacteristic.GetKurt().Meaning, _sampleCharacteristic.GetDeviationForKurtosis()));
            _formService.FillTextBox(txtKurtosisHighLimit, _sampleCharacteristic.GetHighLimit(_sampleCharacteristic.GetKurt().Meaning, _sampleCharacteristic.GetDeviationForKurtosis()));
            _formService.FillTextBox(txtContrKurtosisLowLimit, _sampleCharacteristic.GetLowLimit(_sampleCharacteristic.GetConrtKurt().Meaning, _sampleCharacteristic.GetDeviationForContrKurtosis()));
            _formService.FillTextBox(txtContrKurtosisHighLimit, _sampleCharacteristic.GetHighLimit(_sampleCharacteristic.GetConrtKurt().Meaning, _sampleCharacteristic.GetDeviationForContrKurtosis()));
            _formService.FillTextBox(txtVariationLowLimit, _sampleCharacteristic.GetLowLimit(_sampleCharacteristic.GetCoefVariation().Meaning, _sampleCharacteristic.GetDeviationForVariation()));
            _formService.FillTextBox(txtVariationHighLimit, _sampleCharacteristic.GetHighLimit(_sampleCharacteristic.GetCoefVariation().Meaning, _sampleCharacteristic.GetDeviationForVariation()));

            var anomalValues = new AnomalValues(listFileContent);
            _formService.FillTextBox(txtAnomalLow, anomalValues.FindLowLimit());
            _formService.FillTextBox(txtAnomalHigh, anomalValues.FindHighLimit());

            for (int i = 0; i < anomalValues.FindAnomal().Count(); i++)
            {

                DataGridViewTextBoxCell normalColumn = new DataGridViewTextBoxCell();
                normalColumn.Value = anomalValues.FindAnomal().ElementAt(i);
                dataGridView3.Rows.Insert(i, normalColumn.Value);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listClasses = _classService.GetListClasses(listFileContent, Convert.ToInt32(txtStepsCount.Text));
            Calculation();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog1.FileName;
                PrepareData();
                DataBuild();
            }
        }
    }
}
