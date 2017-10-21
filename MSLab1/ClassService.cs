using System;
using System.Collections.Generic;
using System.Linq;

namespace MSLab1
{
    public class ClassService
    {
        public List<Class> GetListClasses(List<double> listNumbers, int countSteps)
        {
            // countItems = кол-во элементов в ввыборке
            listNumbers.Sort();
            List<Class> list = new List<Class>();
            if (countSteps == 0)
            {

                if (listNumbers.Count >= 100)
                {
                    countSteps = Convert.ToInt32(Math.Pow(listNumbers.Count, (1.0 / 3.0)));
                }
                else
                {
                    countSteps = Convert.ToInt32(Math.Sqrt(listNumbers.Count));
                }
            }
            double stepLength = (FindMaxInList(listNumbers) - FindMinInList(listNumbers)) / Convert.ToDouble(countSteps);
            var min = listNumbers[0];
            var listCountVariationOnStep = new List<int>();
            for (int j = 0; j < countSteps; j++)
            {
                var list1 = new List<double>();
                for (int i = 0; i < listNumbers.Count; i++)
                {
                    if (listNumbers[i] >= min + stepLength * j && listNumbers[i] <= min + stepLength * (j + 1))
                    {
                        list1.Add(listNumbers[i]);
                    }
                }
                listCountVariationOnStep.Add(list1.Count);
                double distrib = 0;
                for (int i = 0; i < j + 1; i++)
                {
                    distrib += listCountVariationOnStep[i] / (double)listNumbers.Count;
                }
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

        private double FindMaxInList(List<double> listNumbers)

        {
            return listNumbers.Last();
        }

        private double FindMinInList(List<double> listNumbers)
        {
            return listNumbers.First();
        }
    }
}
