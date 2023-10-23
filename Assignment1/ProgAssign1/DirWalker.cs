using System;
using System.IO;
using System.Diagnostics;

namespace ProgAssign1
{
  

    public class DirWalker
    {

        public void walk(String path, ref int skippedRows, ref int validRows, ref int totalFiles)
        {
            SimpleCSVParser parser = new SimpleCSVParser();
            string[] list = Directory.GetDirectories(path);

            if (list == null) return;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath, ref skippedRows, ref validRows, ref totalFiles);
                    /*Console.WriteLine("Dir:" + dirpath);*/
                }
            }
            string[] fileList = Directory.GetFiles(path);
            foreach (string filepath in fileList)
            {
                totalFiles++;
                parser.parse(filepath, ref skippedRows, ref validRows);
                /*Console.WriteLine("File:" + filepath);*/
            }
        }

        public static void Main(String[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            string dirToWalk = Path.Combine(currentDir, "..", "..", "..", "TestDirectory");
            string logFile = Path.Combine(currentDir, "..", "..", "..", "logs", "log.txt");
            
            TextWriterTraceListener log = new TextWriterTraceListener(logFile);
            Trace.Listeners.Add(log);
            DateTime startTime = DateTime.Now;
            
            int skippedRows = 0;
            int validRows = 0;
            int totalFiles = 0;
            
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Trace.WriteLine($"Program execution started at {startTime}.\n");

            DirWalker fw = new DirWalker();
            fw.walk(dirToWalk, ref skippedRows, ref validRows, ref totalFiles);

            stopwatch.Stop();
            DateTime stopTime = DateTime.Now;
            Trace.WriteLine($"Program execution stopped at {stopTime}.\n" +
                $"Time elapsed: {stopwatch.ElapsedMilliseconds} ms.\n" +
                $"Total number of valid Rows: {validRows}.\n" +
                $"Total number of skipped Rows: {skippedRows}.");

            Trace.Listeners.Remove(log);
            log.Close();

            Console.WriteLine("Skipped: " + skippedRows);
            Console.WriteLine("Valid: " + validRows);
            Console.WriteLine("Total Files: " + totalFiles);
        }

    }
}
