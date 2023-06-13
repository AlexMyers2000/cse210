using System;
using System.Collections.Generic;
using System.IO;

class JournalEntry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }

    public override string ToString()
    {
        return $"Date: {Date.ToString("yyyy-MM-dd")}\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

class Journal
{
    private List<JournalEntry> entries = new List<JournalEntry>();

    public void AddEntry(JournalEntry entry)
    {
        entries.Add(entry);
    }

    public void DisplayJournal()
    {
        if (entries.Count == 0)
        {
            Console.WriteLine("Journal is empty.");
        }
        else
        {
            foreach (var entry in entries)
            {
                Console.WriteLine(entry.ToString());
            }
        }
    }

    public void SaveJournal(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine(entry.ToString());
            }
        }
    }

    public void LoadJournal(string filename)
    {
        entries.Clear();
        try
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                JournalEntry entry = null;
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.StartsWith("Date:"))
                    {
                        entry = new JournalEntry();
                        entry.Date = DateTime.Parse(line.Replace("Date: ", ""));
                    }
                    else if (line.StartsWith("Prompt:"))
                    {
                        entry.Prompt = line.Replace("Prompt: ", "");
                    }
                    else if (line.StartsWith("Response:"))
                    {
                        entry.Response = line.Replace("Response: ", "");
                    }
                    else if (line == "")
                    {
                        entries.Add(entry);
                        entry = null;
                    }
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Program
{
    static string ShowRandomPrompt()
    {
        List<string> prompts = new List<string>
        {
            "Describe your favorite childhood memory.",
            "What are your goals for the next five years?",
            "Write about a book that has had a significant impact on you.",
            "Describe a place you've always wanted to visit and why.",
            "Reflect on a challenging experience you've overcome.",
            // Add more prompts here
        };
        Random random = new Random();
        return prompts[random.Next(prompts.Count)];
    }

    static void Main(string[] args)
    {
        Journal journal = new Journal();
        string choice;

        do
        {
            Console.WriteLine("Journal Menu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display journal");
            Console.WriteLine("3. Save journal to a file");
            Console.WriteLine("4. Load journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Enter your choice (1-5): ");
            choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string prompt = ShowRandomPrompt();
                    Console.WriteLine("Prompt: " + prompt);
                    Console.Write("Enter your response: ");
                    string response = Console.ReadLine();
                    JournalEntry entry = new JournalEntry
                    {
                        Prompt = prompt,
                        Response = response,
                        Date = DateTime.Now
                    };
                    journal.AddEntry(entry);
                    Console.WriteLine("Entry added.");
                    break;
                case "2":
                    journal.DisplayJournal();
                    break;
                case "3":
                    Console.Write("Enter the filename to save the journal to: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveJournal(saveFilename);
                    Console.WriteLine("Journal saved.");
                    break;
                case "4":
                    Console.Write("Enter the filename to load the journal from: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadJournal(loadFilename);
                    Console.WriteLine("Journal loaded.");
                    break;
                case "5":
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            Console.WriteLine();
        } while (choice != "5");
    }
}
