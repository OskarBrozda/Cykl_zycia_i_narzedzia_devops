using System;
using System.Timers; 
using System.Collections.Generic;

class Program
{
    static System.Timers.Timer timer = new System.Timers.Timer(1000); 
    static List<string> savedTimes = new List<string>(); 

    static void Main()
    {
        timer.Elapsed += ShowCurrentTime;
        timer.Start();

        Console.WriteLine("Press [Enter] to save the current time or [Esc] to exit.");
        while (true)
        {
            var key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.Enter)
            {
                SaveCurrentTime();
            }
            else if (key == ConsoleKey.Escape)
            {
                break;
            }
        }
    }

    private static void ShowCurrentTime(object sender, ElapsedEventArgs e)
    {
        Console.SetCursorPosition(0, 0); 
        Console.WriteLine($"Aktualna godzina: {DateTime.Now:HH:mm:ss}");
    
        Console.SetCursorPosition(0, 2); 
        foreach (var time in savedTimes)
        {
            Console.WriteLine(time);
        }
    }

    private static void SaveCurrentTime()
    {
        string timeString = DateTime.Now.ToString("HH:mm:ss");
        savedTimes.Add(timeString);  // Dodajemy zapisany czas do listy
    }
}
