using System;
using System.Timers;
using System.Collections.Generic;

class Program
{
    static System.Timers.Timer timer = new System.Timers.Timer(1000); // Timer co sekundę
    static List<string> savedTimes = new List<string>(); // Lista zapisanych czasów
    static bool isRunning = true; // Flaga kontrolująca działanie aplikacji

    static void Main(string[] args)
    {
        Console.WriteLine("Aplikacja działa w trybie nieinteraktywnym.");

        // Timer wyświetlający aktualny czas co sekundę
        timer.Elapsed += ShowCurrentTime;
        timer.Start();

        // Tryb nieinteraktywny: aplikacja działa w nieskończoność
        while (isRunning)
        {
            // Symulacja automatycznego zapisu czasu co 5 sekund
            SaveCurrentTime();
            System.Threading.Thread.Sleep(5000);
        }

        timer.Stop();
        Console.WriteLine("Aplikacja została zakończona.");
    }

    private static void ShowCurrentTime(object sender, ElapsedEventArgs e)
    {
        Console.Clear();
        Console.WriteLine($"Aktualna godzina: {DateTime.Now:HH:mm:ss}");

        Console.WriteLine("\nZapisane czasy:");
        foreach (var time in savedTimes)
        {
            Console.WriteLine(time);
        }
    }

    private static void SaveCurrentTime()
    {
        string timeString = DateTime.Now.ToString("HH:mm:ss");
        savedTimes.Add(timeString);
        Console.WriteLine($"Zapisano czas: {timeString}");
    }
}
