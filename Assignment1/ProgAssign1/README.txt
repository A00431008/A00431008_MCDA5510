Assignment #1 - A00431008_MCDA5510
-------------------------------------------------------------------

The project walks through all the directories the TestDirectory recursively and takes input 
from all the csv files within the directories and combines into output.csv in the output folder.

DirWalker.cs recursively walks through the directories and sends each of the files at the 
leaf nodes of the directory tree to SimpleCSVParser.cs for parsing.

SimpleCSVParser.cs parses the file to take the input from any csv file and hence writes the 
content in output.csv. While doing so, it adds into output the date from respective directories.
While parsing, it skips the rows which have blank field and counts them. It also counts all
the valid rows that were written to output.csv.
Note: The directories within TestDirectory represent the year month and dates.

While parsing the file, if there is an error to write the row in the output.csv file because of
the output.csv file being inaccessible (is use by another process), then the function retries
recursively. The recursion runs until the row is written successfully hence there is no data loss.

The logs for the execution of the program is in Assignment1/ProgAssign1/logs/log.txt.
The data logged are execution start/end time, total skipped rows, total valid rows and
any exception that were caught during execution.

"Entire Dataset log.txt" and "Entire Dataset Output.txt" are the log and output file for 
the entire dataset on "Sample Data.zip".