using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSLab1
{
    public class ClassService
    {
        public List<Class> GetListClasses(List<Number> listNumbers, int countSteps)
        {
            List<Class> list = new List<Class>();
            if (countSteps == 0)
            {
                countSteps = Convert.ToInt32(1 + 3.32 * Math.Log(listNumbers.Count));
            }
            double stepLength = (FindMaxInList(listNumbers) - FindMinInList(listNumbers)) / Convert.ToDouble(countSteps);
            var min = listNumbers[0].VariantValue;
            var listCountVariationOnStep = new List<double>();
            for (int j = 0; j < countSteps; j++)
            {
                var list1 = new List<double>();
                for (int i = 0; i < listNumbers.Count; i++)
                {
                    if (listNumbers[i].VariantValue >= min + stepLength * j && listNumbers[i].VariantValue <= min + stepLength * (j + 1))
                    {
                        list1.Add(listNumbers[i].VariantValue);
                    }
                }
                double distrib = 0;
                for (int i = 0; i < j; i++)
                {
                    distrib += listCountVariationOnStep[i] / Convert.ToDouble(listNumbers.Count);
                }
                listCountVariationOnStep.Add(list1.Count);
                list.Add(new Class()
                {
                    Id = j + 1,
                    StartLimit = min + stepLength * j,
                    EndLimit = min + stepLength * (j + 1),
                    Frequence = listCountVariationOnStep[j],
                    RelatedFrequence = listCountVariationOnStep[j] / Convert.ToDouble(listNumbers.Count),
                    DistribValue = distrib
                });


            }
            return list;
        }

        private double FindMaxInList(List<Number> listNumbers)

        {
            return listNumbers.Last().VariantValue;
        }

        private double FindMinInList(List<Number> listNumbers)
        {
            return listNumbers.First().VariantValue;
        }
    }
}
