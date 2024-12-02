using System;
using System.Collections.Generic;
using System.IO;

namespace AoC_Day2_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ReadFile("Input_AoC_D2_2024.txt");
            Console.ReadLine();
        }
        static void ReadFile(string path)
        {
            int safe = 0;
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (IsSafe(line))
                    {
                        safe++;
                    }
                    else
                    {
                        if (CanBeSafeByRemovingOne(line))
                        {
                            safe++;
                        }
                    }
                }
            }
            Console.WriteLine($"Safe reports: {safe}");
        }
        static bool IsSafe(string line)
        {
            int[] levels = Array.ConvertAll(line.Split(' '), int.Parse);
            bool isIncreasing = true,
                 isDecreasing = true;
            for (int i = 0; i < levels.Length - 1; i++)
            {
                int diff = levels[i + 1] - levels[i];
                if (Math.Abs(diff) < 1 || Math.Abs(diff) > 3)
                {
                    return false;
                }
                if (diff > 0)
                {
                    isDecreasing = false;
                }
                else if (diff < 0)
                {
                    isIncreasing = false;
                }
            }
            return isIncreasing || isDecreasing;
        }
        static bool CanBeSafeByRemovingOne(string line)
        {
            int[] levels = Array.ConvertAll(line.Split(' '), int.Parse);

            for (int i = 0; i < levels.Length; i++)
            {
                List<int> modifiedLevels = new List<int>(levels);
                modifiedLevels.RemoveAt(i);

                if (IsSafe(string.Join(" ", modifiedLevels)))
                {
                    return true; 
                }
            }
            return false;
        }
    }
}
