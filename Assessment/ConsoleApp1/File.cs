using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleApp1
{
    class File
    {
        public const string FileName = "ScoreTable.txt";

        public static string[] LoadFile(string filename)
        {
            string[] rtn;

            //this way of using 'using' means that everything is tidy,
            //and that sr will be properly disposed of when we're done.
            using (StreamReader sr = new StreamReader(filename))
            {
                //initialize our line counter
                int NumberOfLines = 0;

                //First we count the number of lines
                //just by reading through the entire file & incrementing the counter.
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                    NumberOfLines++;
                }

                //Then we reset back to the start of the file
                sr.BaseStream.Position = 0;
                sr.DiscardBufferedData();

                //Now we can assign the correct sized array
                rtn = new string[NumberOfLines];
                //reset our counter
                NumberOfLines = 0;
                //and read in the data
                while (!sr.EndOfStream)
                    rtn[NumberOfLines++] = sr.ReadLine();
            }

            return rtn;
        }

        public static void SaveScore(string Name, int Score)
        {
            string[] rtn;
            int NumberOfLines = 0;

            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    sr.ReadLine();
                    NumberOfLines++;
                }

                sr.BaseStream.Position = 0;
                sr.DiscardBufferedData();
                rtn = new string[NumberOfLines];
                NumberOfLines = 0;

                while (!sr.EndOfStream)
                    rtn[NumberOfLines++] = sr.ReadLine();
            }

            using (StreamWriter sw = new StreamWriter(FileName))
            {
                for (int i = 0; i < NumberOfLines; i++)
                    sw.WriteLine(rtn[i]);

                sw.WriteLine(Name + " - " + Score);
            }
        }
    }
}
