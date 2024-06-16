using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        char[] letterValues = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

        Console.Write("Enter a word: ");
        string word = Console.ReadLine();

        int numericValue = GetNumericValue(word, letterValues);

        // Create the file if it doesn't exist
        using (StreamWriter createFileSW = new StreamWriter("NumericStrings.txt", true))
        {
        }

        WriteNumValueToNotepad(word, numericValue);

        using (StreamWriter sw = new StreamWriter("NumericStrings.txt", true))
        {
            WriteNumValueToFile(sw, word, numericValue);
        }

        using (StreamReader sr = new StreamReader("NumericStrings.txt"))
        {
            ReadNumValuefromFile(sr);
        }

        Console.ReadLine();
    }

    static int GetNumericValue(string s, char[] letterValues)
    {
        int totalValue = 0;

        foreach (char c in s)
        {
            if (char.IsLetter(c))
            {
                if (char.IsUpper(c))
                {
                    return 0;
                }

                int letterValue = Array.IndexOf(letterValues, char.ToLower(c)) + 1; // Convert to lowercase and then find the index
                totalValue += letterValue;
            }
            else
            {
                return 0;
            }
        }

        return totalValue;
    }

    static void WriteNumValueToFile(StreamWriter sw, string s, int numericValue)
    {

        if (numericValue > 0)
        { 
            sw.WriteLine($"Numeric value of '{s}' is {numericValue}");
        }
        else
        {
            sw.WriteLine($"'{s}' contains non-lower case character(s)");
        }
    }

    static void ReadNumValuefromFile(StreamReader sr)
    {
        Console.WriteLine("Contents of NumericStrings.txt:");
        Console.WriteLine(sr.ReadToEnd());
    }

    static void WriteNumValueToNotepad(string s, int numericValue)
    {
        using (StreamWriter sw = new StreamWriter("TempNotepadFile.txt"))
        {
            if (numericValue > 0)
            {
                sw.WriteLine($"Numeric value of '{s}' is {numericValue}");
            }
            else
            {
                sw.WriteLine($"'{s}' contains non-lower case character(s)");
            }

            string existingContent = File.ReadAllText("NumericStrings.txt");
            sw.Write(existingContent);
        }

        Process.Start("notepad.exe", "TempNotepadFile.txt");
    }
}
