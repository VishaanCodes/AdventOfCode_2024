using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day1_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "Input_AoC_D1_2024.txt";
            List<int> left = new List<int>(),
                      right = new List<int>();
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string leftSubstring = line.Substring(0, 5);
                    left.Add(int.Parse(leftSubstring.Trim()));
                    string rightSubstring = line.Substring(8);
                    right.Add(int.Parse(rightSubstring.Trim()));
                }
            }
            left.Sort();
            right.Sort();
            int temp = 0, difference = 0;
            for (int i = 0; i < left.Count; i++)
            {
                temp = Math.Abs(left[i] - right[i]);
                difference += temp;
            }
            Console.WriteLine(difference);

            int similarity = 0;
            for (int i = 0; i < left.Count; i++)
            {
                temp = 0;
                for (int j = 0; j < right.Count; j++)
                {
                    if (left[i] == right[j])
                    {
                        temp++;
                    }
                }
                similarity += (left[i] * temp);
            }
            Console.WriteLine(similarity);
            Console.ReadLine();
        }
    }
}
