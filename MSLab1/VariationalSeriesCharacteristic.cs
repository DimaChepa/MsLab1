using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSLab1
{
    public class VariationalSeriesCharacteristic
    {
        private IList<Number> _fileContent;
        int _countFiles;
        double laplasCoefFunction = 2.06;
        public VariationalSeriesCharacteristic(IList<Number> fileContent, int countFiles)
        {
            _fileContent = fileContent;
            _countFiles = countFiles;
        }

        public VariationalSeriesCharacteristic(IList<double> fileContent)
        {
        }
        // среднее арифметическое
        public StatPoint GetAvarage()
        {
            return new StatPoint()
            {
                Meaning = this.GetAvarageArif()
            };
        }


        public virtual StatPoint GetMediane()
        {
            return new StatPoint()
            {
                Meaning = _fileContent[Convert.ToInt32(_fileContent.Count / 2)].VariantValue
            };
        }

        //среднее квадратическое
        public virtual StatPoint GetSquareAvarage()
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

        public StatPoint GetStartConfidenceInterval()
        {
            return new StatPoint()
            {
                Meaning = GetStartConfidence()
            };
        }

        public StatPoint GetEndConfidenceInterval()
        {
            return new StatPoint()
            {
                Meaning = GetEndConfidence()
            };
        }

        protected virtual double GetStartConfidence()
        {
            return GetAvarageArif() - laplasCoefFunction * (GetBaisedDeviation() / Convert.ToDouble(Math.Sqrt(_countFiles)));
        }

        protected virtual double GetEndConfidence()
        {
            return GetAvarageArif() + laplasCoefFunction * (GetBaisedDeviation() / Convert.ToDouble(Math.Sqrt(_countFiles)));
        }
        //отклонения
        //смещенное
        protected virtual double GetBaisedDeviation()
        {
            double value = 0;
            for (int i = 0; i < _fileContent.Count; i++)
            {
                value += (Math.Pow(_fileContent[i].VariantValue - GetAvarageArif(), 2) * _fileContent[i].RelatedFrequency);
            }
            return Convert.ToDouble(Math.Sqrt(value));
        }

        //несмещенное
        protected virtual double GetUnBaisedDeviation()
        {
            double rubbish = _countFiles / Convert.ToDouble(_countFiles - 1);
            return Convert.ToDouble(Math.Sqrt(rubbish * GetCoreExample(2)));
        }

        //коэффициент ассиметрии
        //смещенный
        protected virtual double GetBaisedAssemetry()
        {
            return GetCoreExample(3) / Math.Pow(GetBaisedDeviation(), 3);
        }

        //несмещенное
        protected virtual double GetUnBaisedAssemetry()
        {
            double rubbish = (Math.Sqrt(_countFiles * (_countFiles - 1)) / (_countFiles - 2));
             return rubbish * GetBaisedAssemetry();
        }
        //эксцесс
        //смещенный
        protected virtual double GetBaisedKurtosis()
        {
            return GetCoreExample(4) / Convert.ToDouble(Math.Pow(GetBaisedDeviation(), 4));
        }
        //несмещенный
        protected virtual double GetUnBaisedKurtosis()
        {
             var count = _countFiles;
             double rubbish = (count * count - 1) / Convert.ToDouble(((count - 2) * (count - 3)));
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
        protected virtual double GetCoreExample(int pow)
        {
            double value = 0;
            for (int i = 0; i < _fileContent.Count; i++)
            {
                value += Math.Pow((_fileContent[i].VariantValue - GetAvarageArif()), pow) * _fileContent[i].RelatedFrequency;
            }
            return value;
        }

        protected virtual double GetAvarageArif()
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
