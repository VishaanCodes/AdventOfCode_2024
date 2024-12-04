using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_Day4_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "Input_AoC_D4_2024.txt";
            int xmasHori = 0, xmasVert = 0, xmasDiag = 0, xmasBackDiag = 0;
            string[,] xmasArray = new string[140, 140];
            using (StreamReader reader = new StreamReader(path))
            {
                int row = 0; 
                string line;
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    for (int j = 0; j < line.Length; j++)
                    {
                        xmasArray[row, j] = line[j].ToString();
                    }
                    row++; 
                }
            }
            int maxRows = 140, maxCols = 140; 
            for (int i = 0; i < maxRows; i++) 
            {
                for (int j = 0; j < maxCols; j++) 
                {
                    if (j + 3 < maxCols)
                    {
                        string temp = xmasArray[i, j] + xmasArray[i, j + 1] + xmasArray[i, j + 2] + xmasArray[i, j + 3];
                        if (CheckTemp(temp)) xmasHori++;
                    }
                    if (i + 3 < maxRows)
                    {
                        string temp = xmasArray[i, j] + xmasArray[i + 1, j] + xmasArray[i + 2, j] + xmasArray[i + 3, j];
                        if (CheckTemp(temp)) xmasVert++;
                    }
                    if (i + 3 < maxRows && j + 3 < maxCols)
                    {
                        string temp = xmasArray[i, j] + xmasArray[i + 1, j + 1] + xmasArray[i + 2, j + 2] + xmasArray[i + 3, j + 3];
                        if (CheckTemp(temp)) xmasDiag++;
                    }
                    if (i + 3 < maxRows && j - 3 >= 0)
                    {
                        string temp = xmasArray[i, j] + xmasArray[i + 1, j - 1] + xmasArray[i + 2, j - 2] + xmasArray[i + 3, j - 3];
                        if (CheckTemp(temp)) xmasBackDiag++;
                    }
                }
            }
            Console.WriteLine(xmasHori);
            Console.WriteLine(xmasVert);
            Console.WriteLine(xmasDiag);
            Console.WriteLine(xmasBackDiag);
            Console.WriteLine(xmasHori + xmasVert + xmasDiag + xmasBackDiag);
            Console.ReadLine();
        }
        static bool CheckTemp(string temp)
        {
            if (temp == "XMAS" || temp == "SAMX")
            {
                return true;
            }
            return false;
        }
    }
}

