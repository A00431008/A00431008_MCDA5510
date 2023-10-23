using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace ProgAssign1
{
    public class SimpleCSVParser
    {

        /*public static void Main(String[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            string dirToWalk = Path.Combine(currentDir, "..", "..", "..", "TestDirectory");
            SimpleCSVParser parser = new SimpleCSVParser();
            int skippedRows = 0, validRows = 0;
            parser.parse(Path.Combine(dirToWalk, "CustomerData0.csv"), ref skippedRows, ref validRows);
            Console.WriteLine("Skipped : " + skippedRows);
            Console.WriteLine("Valid: " + validRows);
        }*/


        public void parse(String fileName, ref int skippedRows, ref int validRows)
        {
            try { 
            using (TextFieldParser parser = new TextFieldParser(fileName))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Process row
                    List<String> row = new List<String>();
                    bool rowValid = true;
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        if (!String.IsNullOrEmpty(field))
                        {
                            row.Add(field);
                        } else
                        {
                            rowValid = false;
                            skippedRows++;
                            break;
                        }
                    }
                    if (rowValid)
                    {       // Assumption: Header of CSV is skipped if header already exists
                            if (row[0].Contains("First Name"))
                            {
                                continue;
                            }
                            row.Add(Environment.NewLine);
                            string outputFilePath = Path.Combine(Environment.CurrentDirectory,
                                "..", "..", "..", "Output", "output.csv");
                            File.AppendAllText(outputFilePath, String.Join(",", row));
                            validRows++;
                        }
                }
            }
        
        }catch(IOException ioe){
                Console.WriteLine(ioe.StackTrace);
         }

    }


    }
}
