using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {

            int[] tcal = new int[protein.Length];
            int[] result = new int[dietPlans.Length];

            for(int i=0; i<protein.Length; i++)
            {
                tcal[i] = protein[i]* 5 + carbs[i]* 5 + fat[i]* 9;
            }

            for(int i=0; i<dietPlans.Length; i++)
            {
                if (dietPlans[i] == "")
                {
                    result[i] = 0;
                }
                else
                {
                    List<int> indexes = new List<int>();
                    result[i] = findres(protein, carbs, fat, tcal, dietPlans[i], indexes);
                }
            }

            return result;

            throw new NotImplementedException();
        }

        public static int findres(int[] protein, int[] carbs, int[] fat, int[] tcal, string dietPlan, List<int> indexes)
        {
            if(dietPlan.Length > 0)
            {
                switch (dietPlan[0])
                {
                    case 'P' :  indexes = maxIndexes(protein, indexes);
                        if (indexes.Count > 1)
                        {
                            return findres(protein, carbs, fat, tcal, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case 'p':   indexes = minIndexes(protein, indexes);
                        if (indexes.Count > 1)
                        {
                            return findres(protein, carbs, fat, tcal, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case 'C':   indexes = maxIndexes(carbs, indexes);
                        if (indexes.Count > 1)
                        {
                            return findres(protein, carbs, fat, tcal, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case 'c':   indexes = minIndexes(carbs, indexes);
                        if (indexes.Count > 1)
                        {
                            return findres(protein, carbs, fat, tcal, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case 'F':   indexes = maxIndexes(fat, indexes);
                        if (indexes.Count > 1)
                        {
                            return findres(protein, carbs, fat, tcal, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case 'f':   indexes = minIndexes(fat, indexes);
                        if (indexes.Count > 1)
                        {
                            return findres(protein, carbs, fat, tcal, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case 'T':  indexes = maxIndexes(tcal, indexes);
                        if (indexes.Count > 1)
                        {
                            return findres(protein, carbs, fat, tcal, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case 't':   indexes = minIndexes(tcal, indexes);
                        if (indexes.Count > 1)
                        {
                            return findres(protein, carbs, fat, tcal, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }
                }
            }
            else
            {
                return indexes[0];
            }

            return 0;
            throw new NotImplementedException();
        }

        public static List<int> maxIndexes(int[] nutri, List<int> indexes)
        {
            int max;
            
            List<int> indexesb = new List<int>();

            if(indexes.Count > 0)
            {
                max = nutri[indexes[0]];
                foreach(int i in indexes)
                {
                    if (max < nutri[i])
                        max = nutri[i];
                }

                foreach (int i in indexes)
                    if (max == nutri[i])
                        indexesb.Add(i);
            }
            else
            {
                max = nutri[0];
                for(int i=1; i<nutri.Length; i++)
                {
                    if(max < nutri[i])
                        max = nutri[i];
                }

                for (int i = 0; i < nutri.Length; i++)
                {
                    if (max == nutri[i])
                        indexesb.Add(i);
                }
            }

            return indexesb;
        }

        public static List<int> minIndexes(int[] nutri, List<int> indexes)
        {
            int min;

            List<int> indexesb = new List<int>();

            if (indexes.Count > 0)
            {
                min = nutri[indexes[0]];
                foreach (int i in indexes)
                {
                    if (min > nutri[i])
                        min = nutri[i];
                }

                foreach (int i in indexes)
                    if (min == nutri[i])
                        indexesb.Add(i);
            }
            else
            {
                min = nutri[0];
                for (int i = 1; i < nutri.Length; i++)
                {
                    if (min > nutri[i])
                        min = nutri[i];
                }

                for (int i = 0; i < nutri.Length; i++)
                {
                    if (min == nutri[i])
                        indexesb.Add(i);
                }
            }

            return indexesb;
        }
    }
}
