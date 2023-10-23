using System;
using System.IO;

namespace ProgAssign1
{
  

    public class DirWalker
    {

        public void walk(String path, ref int skippedRows, ref int validRows)
        {
            SimpleCSVParser parser = new SimpleCSVParser();
            string[] list = Directory.GetDirectories(path);

            if (list == null) return;

            foreach (string dirpath in list)
            {
                if (Directory.Exists(dirpath))
                {
                    walk(dirpath, ref skippedRows, ref validRows);
                    /*Console.WriteLine("Dir:" + dirpath);*/
                }
            }
            string[] fileList = Directory.GetFiles(path);
            foreach (string filepath in fileList)
            {
                parser.parse(filepath, ref skippedRows, ref validRows);
                Console.WriteLine("File:" + filepath);
            }
        }

        public static void Main(String[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            string dirToWalk = Path.Combine(currentDir, "..", "..", "..", "TestDirectory");
            int skippedRows = 0;
            int validRows = 0;
            DirWalker fw = new DirWalker();
            fw.walk(dirToWalk, ref skippedRows, ref validRows);
            Console.WriteLine("Skipped: " + skippedRows);
            Console.WriteLine("Valid: " + validRows);
        }

    }
}
