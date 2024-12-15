using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using System.Threading;

class Program
{
    static List<string> savedTimes = new List<string>();
    static bool isRunning = true;

    static string connectionString = "Server=localhost;Database=timeServer_db;User=user;Password=zaq1@WSX;";

    static void Main(string[] args)
    {
        MigrateDatabase(); 
        LoadTimesFromDatabase(); 

        while (isRunning)
        {
            Console.Clear();

            Console.WriteLine($"Aktualna godzina: {DateTime.Now:HH:mm:ss}\n");

            Console.WriteLine("Zapisane czasy:");

            Console.WriteLine($"Times length: {savedTimes.Count}");

            foreach (var time in savedTimes)
            {
                Console.WriteLine(time);
            }

            Thread.Sleep(1000);

            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(intercept: true);

                if (key.Key == ConsoleKey.Enter)
                {
                    SaveCurrentTime();
                    continue;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    isRunning = false;
                }
            }
        }
    }

    private static void MigrateDatabase()
{
    using (var connection = new MySqlConnection(connectionString))
    {
        connection.Open();
        string query = @"
            CREATE TABLE IF NOT EXISTS times (
                id INT AUTO_INCREMENT PRIMARY KEY,
                time_value VARCHAR(8) NOT NULL
            );";

        using (var cmd = new MySqlCommand(query, connection))
        {
            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine("Migration completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration error: {ex.Message}");
            }
        }
    }
}


    private static void SaveCurrentTime()
    {
        string timeString = DateTime.Now.ToString("HH:mm:ss");
        savedTimes.Add(timeString);
        SaveTimeToDatabase(timeString);
    }

    private static void SaveTimeToDatabase(string time)
    {
        try{
        using (var connection = new MySqlConnection(connectionString))
        {
            connection.Open();
            string query = "INSERT INTO times (time_value) VALUES (@time)";
            using (var cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@time", time);
                cmd.ExecuteNonQuery();
            }
        }
        }
        catch (Exception ex)
    {
        Console.WriteLine($"Error: {ex.Message}");  
    }
    }

    private static void LoadTimesFromDatabase()
    {
        savedTimes.Clear();
        using (var connection = new MySqlConnection(connectionString))
        {
            Console.WriteLine("Connected to db");

            connection.Open();
            string query = "SELECT time_value FROM times";
            using (var cmd = new MySqlCommand(query, connection))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        savedTimes.Add(reader.GetString("time_value"));
                    }
                }
            }
        }
    }
}
