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
            double value = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                value += _list[i] * _list[i] - GetAvarageArif() * GetAvarageArif();
            }
            return Math.Sqrt((1 / Convert.ToDouble(_list.Count)) * value);
        }

        protected override double GetUnBaisedDeviation()
        {
            var value = 1 / Convert.ToDouble(_list.Count - 1);
            return Math.Sqrt(value * GetCoreExample(2));
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
            return rubbish * ((GetBaisedKurtosis() - 3) + (6 / count + 1));
        }

        protected override double GetCoreExample(int pow)
        {
            double value = 0;
            for (int i = 0; i < _list.Count; i++)
            {
                value += Math.Pow((_list[i] - GetAvarageArif()), pow);
            }
            return value;
        }

    }
}
