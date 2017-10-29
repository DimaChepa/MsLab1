using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSLab1
{
    public class SampleCharacteristics: VariationalSeriesCharacteristic
    {
        private IList<double> _list;
        //t1-a/2,v
        private double laplasCoef = 2.06;
        public SampleCharacteristics(IList<double> list):base(list)
        {
            _list = list;
        }

        public override StatPoint GetMediane()
        {
            return new StatPoint()
            {
                Meaning = _list[Convert.ToInt32(_list.Count / 2)]
            };
        }

        public override StatPoint GetSquareAvarage()
        {
            double sumSquareItems = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                sumSquareItems += Math.Pow(_list[i], 2);
            }
            return new StatPoint()
            {
                Meaning = Math.Sqrt(sumSquareItems / _list.Count)
            };
        }
        public double GetDeviationForAvarage()
        {
            return GetUnBaisedDeviation() / (double)Math.Sqrt(_list.Count);
        }

        public double GetDeviationForDeviation()
        {
            return GetUnBaisedDeviation() / (double)Math.Sqrt(2 * _list.Count);
        }

        public double GetDeviationForVariation()
        {
            var value = GetCoefVariation().Meaning;
            return value * Math.Sqrt((1 + 2 * value * value) / (double)(2 * _list.Count));
        }

        public double GetDeviationForAssemetry()
        {
            var firstPart = 6 / (double)_list.Count;
            var secondPart = 1 - 12 / (double)(2 * _list.Count + 7);
            return Math.Sqrt(firstPart * secondPart);
        }

        public double GetDeviationForKurtosis()
        {
            var firstPart = 24 / (double)_list.Count;
            var secondPart = 1 - (225 / (double)(15 * _list.Count + 124));
            return Math.Sqrt(firstPart * secondPart);
        }

        public double GetDeviationForContrKurtosis()
        {
            var kurtosis = GetBaisedKurtosis();
            var firstPart = Math.Sqrt(kurtosis / ((double)29 * _list.Count));
            var secondPart = Math.Pow(Math.Pow(Math.Abs(kurtosis * kurtosis - 1), 3), 0.25);
            return firstPart * secondPart;
        }

        public double GetLowLimit(double parameter, double mark)
        {
            return parameter - mark * laplasCoef;
        }

        public double GetHighLimit(double parameter, double mark)
        {
            return parameter + mark * laplasCoef;
        }

        protected override double GetAvarageArif()
        {
            double value = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                value += _list[i];
            }
            return value/Convert.ToDouble(_list.Count);
        }

        protected override double GetBaisedDeviation()
        {
            var value = 1 / Convert.ToDouble(_list.Count);
            return Math.Sqrt(value * GetCoreExample(2));
        }

        protected override double GetUnBaisedDeviation()
        {
            return Math.Sqrt(GetCoreExample(2) / Convert.ToDouble(_list.Count - 1));
        }

        protected override double GetBaisedAssemetry()
        {
            return GetCoreExample(3) / Convert.ToDouble(_list.Count * Math.Pow(GetBaisedDeviation(), 3));
        }

        protected override double GetUnBaisedAssemetry()
        {
            int count = _list.Count;
            return (Math.Sqrt(count * (count - 1)) * GetBaisedAssemetry()) / (count - 2);
        }

        protected override double GetBaisedKurtosis()
        {
            return GetCoreExample(4) / Convert.ToDouble(_list.Count * Math.Pow(GetBaisedDeviation(), 4));
        }

        protected override double GetUnBaisedKurtosis()
        {
            int count = _list.Count;
            double high = Math.Pow(count, 2) - 1;
            double low = (count - 2) * (count - 3);
            double rubbish = high / low;
            double baisedKurt = GetBaisedKurtosis();
            return rubbish * ((baisedKurt - 3) + (6 / (double)(count + 1)));
        }

        protected override double GetCoreExample(int pow)
        {
            double value = 0;
            double avarageArif = GetAvarageArif();
            for (int i = 0; i < _list.Count; i++)
            {
                value += Math.Pow((_list[i] - avarageArif), pow);
            }
            return value;
        }

    }
}
