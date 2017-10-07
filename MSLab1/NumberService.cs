using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSLab1
{
    public class NumberService
    {
        // content of text file
        public List<double> _list;
        public NumberService(List<double> list)
        {
            _list = list;
        }
        public List<Number> GetAllListNumber()
        {
            //list of variations according to the table
            List<Number> listStatData = new List<Number>();

            // dictionary that store variant and count of each in the text file
            Dictionary<double, int> dictionary = new Dictionary<double, int>();

            _list.Sort();
            foreach (var a in _list)
            {
                if (!dictionary.Keys.Contains(a))
                {
                    dictionary.Add(a, 1);
                }
                else
                {
                    var c = dictionary.Where(i => i.Key == a).First();
                    dictionary.Remove(a);
                    dictionary.Add(a, c.Value + 1);
                    
                }
            }
            for (int i = 0; i < dictionary.Count(); i++)
            {
                double distrib = 0;
                for (int j = 0; j < i; j++)
                {
                    distrib += dictionary.ElementAt(j).Value / Convert.ToDouble(_list.Count());
                }
                listStatData.Add(
                    new Number()
                    {
                        Id = i + 1,
                        VariantValue = dictionary.ElementAt(i).Key,
                        AbsoluteFrequency = dictionary.ElementAt(i).Value,
                        RelatedFrequency = dictionary.ElementAt(i).Value / Convert.ToDouble(_list.Count()),
                        DistributionValue = distrib
                    }
                );
            }

            return listStatData;            
        }

        public int GetFileCount()
        {
            return _list.Count;
        }
    }
}
