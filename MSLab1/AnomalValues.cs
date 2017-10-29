using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSLab1
{
    public class AnomalValues
    {
        private double Coef = 1.5;
        private IEnumerable<double> _fileContent;
        public AnomalValues(IList<double> fileContent)
        {
            _fileContent = fileContent;
            _fileContent = _fileContent.OrderBy(x => x);
        }
        public double GetFirstQuartil()
        {
            return _fileContent.ElementAt((int)(_fileContent.Count() / 4));
        }

        public double GetThirdQuartil()
        {
            return _fileContent.ElementAt((int)(_fileContent.Count() * 0.75));
        }

        public double FindLowLimit()
        {
            var quartFirst = GetFirstQuartil();
            var quartThird = GetThirdQuartil();
            return quartFirst - Coef * (quartThird - quartFirst);
        }

        public double FindHighLimit()
        {
            var quartFirst = GetFirstQuartil();
            var quartThird = GetThirdQuartil();
            return quartThird + Coef * (quartThird - quartFirst);
        }

        public IEnumerable<double> FindAnomal()
        {
            var lowLimit = FindLowLimit();
            var highLimit = FindHighLimit();
            return _fileContent.Where(i => i > highLimit || i < lowLimit);
        }

    }
}
