using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private readonly string filePath = @"C:\Users\USER\Downloads\Mat_Stat2\VP&MC\labs_своя программа\data_lab1,2\25\exp.txt";
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
            _numberService = new NumberService(listFileContent);
            _classService = new ClassService();
            dataGridView1.DataSource = _numberService.GetAllListNumber();
            var variationalSeries = _numberService.GetAllListNumber();
            listClasses = _classService.GetListClasses(variationalSeries, 0);
            Calculation();
        }

        private void Calculation()
        {
            dataGridView1.DataSource = _numberService.GetAllListNumber();
            dataGridView2.DataSource = listClasses;
            
            for (int i = 0; i < listClasses.Count; i++)
            {
                chart1.Series["Частота"].Points.AddXY($"{listClasses[i].StartLimit} ... {listClasses[i].EndLimit}", listClasses[i].Frequence);
            }
            _formService.ChartClear(chart2);
            _formService.ChartClear(chart3);
            for (int i = 0; i < _numberService.GetAllListNumber().Count; i++)
            {
                chart2.Series["Ряд"].Points.AddXY(_numberService.GetAllListNumber()[i].VariantValue, _numberService.GetAllListNumber()[i].DistributionValue);
            }
            for (int i = 0; i < listClasses.Count; i++)
            {
                chart3.Series["Классы"].Points.AddXY(listClasses[i].StartLimit, listClasses[i].DistribValue);
            }

            //Варицационный ряд
            _variationalSeriesCharacteristic = new VariationalSeriesCharacteristic(_numberService.GetAllListNumber(), _numberService.GetFileCount());
            _formService.FillTextBox(textBox1, _variationalSeriesCharacteristic.GetAvarage().Meaning);
            _formService.FillTextBox(textBox2, _variationalSeriesCharacteristic.GetMediane().Meaning);
            _formService.FillTextBox(textBox3, _variationalSeriesCharacteristic.GetSquareAvarage().Meaning);
            _formService.FillTextBox(textBox4, _variationalSeriesCharacteristic.GetUnbaisedAssemetry().Meaning);
            _formService.FillTextBox(textBox5, _variationalSeriesCharacteristic.GetUnbaisedDeviation().Meaning);
            _formService.FillTextBox(textBox6, _variationalSeriesCharacteristic.GetKurt().Meaning);
            _formService.FillTextBox(textBox7, _variationalSeriesCharacteristic.GetConrtKurt().Meaning);
            _formService.FillTextBox(textBox8, _variationalSeriesCharacteristic.GetCoefVariation().Meaning);

            // Выборка
            PrepareData();

            var _sampleCharacteristic = new SampleCharacteristics(listFileContent);
            _formService.FillTextBox(textBox9, _sampleCharacteristic.GetAvarage().Meaning);
            _formService.FillTextBox(textBox10, _sampleCharacteristic.GetMediane().Meaning);
            _formService.FillTextBox(textBox11, _sampleCharacteristic.GetSquareAvarage().Meaning);
            _formService.FillTextBox(textBox12, _sampleCharacteristic.GetUnbaisedAssemetry().Meaning);
            _formService.FillTextBox(textBox13, _sampleCharacteristic.GetUnbaisedDeviation().Meaning);
            _formService.FillTextBox(textBox14, _sampleCharacteristic.GetKurt().Meaning);
            _formService.FillTextBox(textBox15, _sampleCharacteristic.GetConrtKurt().Meaning);
            _formService.FillTextBox(textBox16, _sampleCharacteristic.GetCoefVariation().Meaning);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listClasses = _classService.GetListClasses(_numberService.GetAllListNumber(), Convert.ToInt32(txtStepsCount.Text));
            Calculation();
        }
    }
}
