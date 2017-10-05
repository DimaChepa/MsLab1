using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSLab1
{
    public class SampleCharacteristic
    {
        private IList<Number> _fileContent;
        public SampleCharacteristic(IList<Number> fileContent)
        {
            _fileContent = fileContent;
        }

        // среднее арифметическое
        public StatPoint GetAvarage()
        {
            return new StatPoint()
            {
                Meaning = this.GetAvarageArif()
            };
        }


        public StatPoint GetMediane()
        {
            return new StatPoint()
            {
                Meaning = _fileContent[Convert.ToInt32(_fileContent.Count / 2)].VariantValue
            };
        }

        //среднее квадратическое
        public StatPoint GetSquareAvarage()
        {
            double sumSquareItems = 0;
            for (int i = 0; i < _fileContent.Count; i++)
            {
                sumSquareItems += Math.Pow(_fileContent[i].VariantValue, 2);
            }
            return new StatPoint()
            {
                Meaning = Math.Sqrt(sumSquareItems / _fileContent.Count)
            };
        }

        public StatPoint GetUnbaisedDeviation()
        {
            return new StatPoint()
            {
                Meaning = GetUnBaisedDeviation()
            };
        }

        public StatPoint GetUnbaisedAssemetry()
        {
            return new StatPoint()
            {
                Meaning = GetUnBaisedAssemetry()
            };
        }

        public StatPoint GetKurt()
        {
            return new StatPoint()
            {
                Meaning = GetUnBaisedKurtosis()
            };
        }

        public StatPoint GetConrtKurt()
        {
            return new StatPoint()
            {
                Meaning = GetContrCurtosis()
            };
        }

        public StatPoint GetCoefVariation()
        {
            return new StatPoint()
            {
                Meaning = this.GetCoeficientVariation()
            };
        }

        //отклонения
        //смещенное
        private double GetBaisedDeviation()
        {
            double value = 0;
            for (int i = 0; i < _fileContent.Count; i++)
            {
                value += ((Math.Pow(_fileContent[i].VariantValue, 2) * _fileContent[i].RelatedFrequency) - Math.Pow(GetAvarageArif(), 2));
            }
            return Math.Sqrt(Math.Abs(value));
        }

        //несмещенное
        private double GetUnBaisedDeviation()
        {
            double rubbish = _fileContent.Count / Convert.ToDouble(_fileContent.Count - 1);
            return Math.Sqrt(rubbish * GetCoreExample(2));
        }

        //коэффициент ассиметрии
        //смещенный
        private double GetBaisedAssemetry()
        {
            return GetCoreExample(3) / Math.Pow(GetBaisedDeviation(), 3);
        }

        //несмещенное
        private double GetUnBaisedAssemetry()
        {
            double rubbish = (Math.Sqrt(_fileContent.Count * (_fileContent.Count - 1)) / (_fileContent.Count - 2));
            return rubbish * GetBaisedAssemetry();
        }
        //эксцесс
        //смещенный
        private double GetBaisedKurtosis()
        {
            return GetCoreExample(4) / Math.Pow(GetBaisedDeviation(), 4);
        }
        //несмещенный
        private double GetUnBaisedKurtosis()
        {
            var count = _fileContent.Count;
            double rubbish = (count * count - 1) / ((count - 2) * (count - 3));
            return rubbish * ((GetBaisedKurtosis() - 3) + (6 / count + 1));
        }
        // контрэксцесс
        private double GetContrCurtosis()
        {
            return 1 / Math.Sqrt(Math.Abs(GetBaisedKurtosis()));
        }
        //коэффицеинт вариации
        private double GetCoeficientVariation()
        {
            return GetUnBaisedDeviation() / GetAvarageArif();
        }
        // сума произведения разницы квадратов на вероятность
        private double GetCoreExample(int pow)
        {
            double value = 0;
            for (int i = 0; i < _fileContent.Count; i++)
            {
                value += Math.Pow((_fileContent[i].VariantValue - GetAvarageArif()), pow) * _fileContent[i].RelatedFrequency;
            }
            return value;
        }

        private double GetAvarageArif()
        {
            double value = 0;
            for (int i = 0; i < _fileContent.Count; i++)
            {
                value += _fileContent[i].VariantValue * _fileContent[i].RelatedFrequency;
            }
            return value;
        }
    }

    public class StatPoint
    {
        public double Meaning
        {
            get; set;
        }
        public double SquareDeviation { get; set; }
        public double ConvinienceIntervalStart { get; set; }
        public double ConvinienceIntervalEnd { get; set; }
    }
}
