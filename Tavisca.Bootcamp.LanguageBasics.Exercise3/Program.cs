﻿using System;
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

            int[] totalCalories = new int[protein.Length];
            int[] result = new int[dietPlans.Length];

            const int FIVE = 5;
            const int NINE = 5;

            for (int i=0; i<protein.Length; i++)
            {
                totalCalories[i] = protein[i]* FIVE + carbs[i]* FIVE + fat[i]* NINE;
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
                    result[i] = FindResult(protein, carbs, fat, totalCalories, dietPlans[i], indexes);
                }
            }

            return result;

            throw new NotImplementedException();
        }

        public static int FindResult(int[] protein, int[] carbs, int[] fat, int[] totalCalories, string dietPlan, List<int> indexes)
        {
            const char HIGH_PROTEIN = 'P';
            const char LOW_PROTEIN = 'p';

            const char HIGH_CARBS = 'C';
            const char LOW_CARBS = 'c';

            const char HIGH_FAT = 'F';
            const char LOW_FAT = 'f';

            const char HIGH_TOTAL_CALORIES = 'T';
            const char LOW_TOTAL_CALORIES = 't';

            if (dietPlan.Length > 0)
            {
                switch (dietPlan[0])
                {
                    case HIGH_PROTEIN:  indexes = SearchMaxIndexes(protein, indexes);
                        if (indexes.Count > 1)
                        {
                            return FindResult(protein, carbs, fat, totalCalories, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case LOW_PROTEIN:  indexes = SearchMinIndexes(protein, indexes);
                        if (indexes.Count > 1)
                        {
                            return FindResult(protein, carbs, fat, totalCalories, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case HIGH_CARBS:   indexes = SearchMaxIndexes(carbs, indexes);
                        if (indexes.Count > 1)
                        {
                            return FindResult(protein, carbs, fat, totalCalories, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case LOW_CARBS:   indexes = SearchMinIndexes(carbs, indexes);
                        if (indexes.Count > 1)
                        {
                            return FindResult(protein, carbs, fat, totalCalories, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case HIGH_FAT:   indexes = SearchMaxIndexes(fat, indexes);
                        if (indexes.Count > 1)
                        {
                            return FindResult(protein, carbs, fat, totalCalories, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case LOW_FAT:   indexes = SearchMinIndexes(fat, indexes);
                        if (indexes.Count > 1)
                        {
                            return FindResult(protein, carbs, fat, totalCalories, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case HIGH_TOTAL_CALORIES:  indexes = SearchMaxIndexes(totalCalories, indexes);
                        if (indexes.Count > 1)
                        {
                            return FindResult(protein, carbs, fat, totalCalories, dietPlan.Substring(1), indexes);
                        }
                        else
                        {
                            return indexes[0];
                        }

                    case LOW_TOTAL_CALORIES:   indexes = SearchMinIndexes(totalCalories, indexes);
                        if (indexes.Count > 1)
                        {
                            return FindResult(protein, carbs, fat, totalCalories, dietPlan.Substring(1), indexes);
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
        }

        public static List<int> SearchMaxIndexes(int[] nutrient, List<int> indexes)
        {
            int max;
            
            List<int> indexes_updated = new List<int>();

            if(indexes.Count > 0)
            {
                max = nutrient[indexes[0]];
                foreach(int index in indexes)
                {
                    if (max < nutrient[index])
                        max = nutrient[index];
                }

                foreach (int index in indexes)
                    if (max == nutrient[index])
                        indexes_updated.Add(index);
            }
            else
            {
                max = nutrient[0];
                for(int i=1; i< nutrient.Length; i++)
                {
                    if(max < nutrient[i])
                        max = nutrient[i];
                }

                for (int i = 0; i < nutrient.Length; i++)
                {
                    if (max == nutrient[i])
                        indexes_updated.Add(i);
                }
            }

            return indexes_updated;
        }

        public static List<int> SearchMinIndexes(int[] nutrient, List<int> indexes)
        {
            int min;

            List<int> indexes_updated = new List<int>();

            if (indexes.Count > 0)
            {
                min = nutrient[indexes[0]];
                foreach (int index in indexes)
                {
                    if (min > nutrient[index])
                        min = nutrient[index];
                }

                foreach (int index in indexes)
                    if (min == nutrient[index])
                        indexes_updated.Add(index);
            }
            else
            {
                min = nutrient[0];
                for (int i = 1; i < nutrient.Length; i++)
                {
                    if (min > nutrient[i])
                        min = nutrient[i];
                }

                for (int i = 0; i < nutrient.Length; i++)
                {
                    if (min == nutrient[i])
                        indexes_updated.Add(i);
                }
            }

            return indexes_updated;
        }
    }
}
