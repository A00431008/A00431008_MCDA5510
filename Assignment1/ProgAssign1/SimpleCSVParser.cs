using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Microsoft.VisualBasic.FileIO;

namespace ProgAssign1
{
    public class SimpleCSVParser
    {
        public static async void writeOutput(List<string> row)
        {
            try
            {
                string outputFilePath = Path.Combine(Environment.CurrentDirectory,
                                "..", "..", "..", "Output", "output.csv");
                await File.AppendAllTextAsync(outputFilePath, String.Join(',', row));
            } catch (Exception ioe)
            {
                Trace.WriteLine($"Error found: An exception was thrown " +
                    $"with the following message:\n {ioe.Message}\nTrying again...\n");
                writeOutput(row);
                // since row is valid, if an exception is throws while trying to write the file
                // try to write it again recursively
                // Note: Upon numerous execution of the program the only reason for exception is
                // output.csv not accessible as it is being used by other process. Hence, simply
                // try again to write it recursively.
            }
            
        }

        /*public static void Main(String[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            string dirToWalk = Path.Combine(currentDir, "..", "..", "..", "TestDirectory","1995","7","22");
            SimpleCSVParser parser = new SimpleCSVParser();
            int skippedRows = 0, validRows = 0;
            parser.parse(Path.Combine(dirToWalk, "CustomerData0.csv"), ref skippedRows, ref validRows);
            Console.WriteLine("Skipped : " + skippedRows);
            Console.WriteLine("Valid: " + validRows);
        }*/

        public static string findDate(string filePath)
        {
            try
            {
                string[] splitPath = filePath.Split('\\');
                String year, month, day;
                year = splitPath[splitPath.Length - 4];
                month = splitPath[splitPath.Length - 3];
                day = splitPath[splitPath.Length - 2];
                return (
                        year + "/" +
                        (int.Parse(month) < 10 ? "0" : "") + month + "/" +
                        (int.Parse(day) < 10 ? "0" : "") + day
                    );
            } catch (Exception err)
            {
                return "";
            }
            
        }


        public void parse(String fileName, 
            ref int skippedRows, 
            ref int validRows) 
        {
            try { 
                using (TextFieldParser parser = new TextFieldParser(fileName))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    int rowNum = 1;
                    
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
                                rowNum++;
                                break;
                            }
                        }
                        if (rowValid)
                        {   /* ASSUMPTION: Header of all the other files are skipped except the first one
                            * if a valid row is already read and output, that would be the first
                            * row of first file which would be the header, hence we continue
                            */
                            if (row[0].Contains("First Name") && validRows > 0)
                            {
                                continue;
                            } else if (row[0].Contains("First Name") && validRows == 0)
                            {
                                // If row is a header of the first file, add "Date"
                                // column and write on output.csv then skip to next row 
                                row.Add("Date");
                                row.Add("\n");
                                writeOutput(row);
                                continue;
                            }
                            // for normal rows, add date for the row and write on output.csv
                            row.Add(findDate(fileName));
                            row.Add("\n");
                            writeOutput(row);
                            validRows++;
                            rowNum++;
                         }
                }
                }
        
            }catch(IOException ioe){
                Trace.WriteLine($"Error found: An exception was thrown with the following message:\n" +
                    $"{ioe.Message}\n");
                Console.WriteLine(ioe.StackTrace);
            }

        }   


    }
}
