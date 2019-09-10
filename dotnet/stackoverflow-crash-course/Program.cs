using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace stackoverflow_crash_course
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader(File.OpenRead("data.csv")))
            {
                if (File.Exists("new-data.csv")) // If not deleted, the new data will get appeneded with old data.
                {
                    File.Delete("new-data.csv");
                }
                var ids = new List<string>();
                var questions = new List<string>();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    // Answer for 1.a
                    var longestQuestion = questions.Aggregate("", (max, cur) => max.Length > cur.Length ? max : cur); // Aggregate explained https://stackoverflow.com/questions/7105505/linq-aggregate-algorithm-explained
                    System.Console.WriteLine("The longest question from rhe data set is: " + longestQuestion);

                    // Answer for 1.b
                    var questinsSplitBySpace = values[1].Split(' ');
                    System.Console.WriteLine("The id " + values[0] + " has the question containing words of lenght " + questinsSplitBySpace.Length);


                    // Answer for 1.c
                    var csv = new StringBuilder();
                    string newLine;
                    foreach (var word in questinsSplitBySpace)
                    {
                        newLine = $"{values[0]},{word}";
                        System.Console.WriteLine("The id is " + values[0] + " word in question is " + word);
                        csv.AppendLine(newLine);
                    }
                    File.AppendAllText("new-data.csv", csv.ToString());

                    ids.Add(values[0]);
                    questions.Add(values[1]);
                }
            }
        }
    }
}
