using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day2_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "Input_AoC_D2_2024.txt";
            int safe = 0;
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                bool first = false;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] strings = line.Split(' ');
                    for (int i = 0; i < strings.Length - 1; i++)
                    {
                        if (int.Parse(strings[i]) > int.Parse(strings[i + 1]))
                        {
                            first = true;
                        }
                        else
                        {
                            first = false;
                            break;
                        }
                    }
                    for (int i = 0; i < strings.Length - 1; i++)
                    {
                        if (int.Parse(strings[i]) < int.Parse(strings[i + 1]))
                        {
                            first = true;
                        }
                        else
                        {
                            first = false;
                            break;
                        }
                    }
                    if (first)
                    {
                        for (int i = 0; i < strings.Length - 1; i++)
                        {
                            int current = int.Parse(strings[i].Trim());
                            int next = int.Parse(strings[i + 1].Trim());
                            for (int diff = -3; diff <= 3; diff++)
                            {
                                if (current + diff == next)
                                {
                                    safe++;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(safe);
            Console.ReadLine();
        }
    }
}
