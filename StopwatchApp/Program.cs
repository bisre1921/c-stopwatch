using System;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch stopwatch = new Stopwatch();

        stopwatch.OnStarted += message => DisplayMessage(message, ConsoleColor.Green);
        stopwatch.OnStopped += message => DisplayMessage(message, ConsoleColor.Yellow);
        stopwatch.OnReset += message => DisplayMessage(message, ConsoleColor.Cyan);

        DisplayHeader();

        bool exit = false;

        while (!exit)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                switch (key)
                {
                    case ConsoleKey.S:
                        stopwatch.Start();
                        break;
                    case ConsoleKey.T:
                        stopwatch.Stop();
                        break;
                    case ConsoleKey.R:
                        stopwatch.Reset();
                        break;
                    case ConsoleKey.Q:
                        exit = true;
                        break;
                    default:
                        DisplayMessage("Invalid key! Please press S, T, R, or Q.", ConsoleColor.Red);
                        break;
                }
            }

            if (stopwatch.IsRunning)
            {
                stopwatch.Tick();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"⏱  Time Elapsed: {stopwatch.TimeElapsed} seconds");
                Console.ResetColor();
                Thread.Sleep(1000);
            }
        }

        DisplayMessage("Thank you for using the Stopwatch App! Goodbye.", ConsoleColor.Gray);
    }

    static void DisplayHeader()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("=========================================");
        Console.WriteLine("         🕒 Console Stopwatch App         ");
        Console.WriteLine("=========================================");
        Console.ResetColor();

        Console.WriteLine("Press:");
        WriteColoredLine("  S - Start", ConsoleColor.Green);
        WriteColoredLine("  T - Stop", ConsoleColor.Yellow);
        WriteColoredLine("  R - Reset", ConsoleColor.Cyan);
        WriteColoredLine("  Q - Quit", ConsoleColor.Red);
        Console.WriteLine();
    }

    static void DisplayMessage(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    static void WriteColoredLine(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}

class Stopwatch
{
    public int TimeElapsed { get; private set; } = 0;
    public bool IsRunning { get; private set; } = false;

    public delegate void StopwatchEventHandler(string message);

    public event StopwatchEventHandler OnStarted;
    public event StopwatchEventHandler OnStopped;
    public event StopwatchEventHandler OnReset;

    public void Start()
    {
        if (!IsRunning)
        {
            IsRunning = true;
            OnStarted?.Invoke("✅ Stopwatch Started!");
        }
        else
        {
            OnStarted?.Invoke("⚠️  Stopwatch is already running.");
        }
    }

    public void Stop()
    {
        if (IsRunning)
        {
            IsRunning = false;
            OnStopped?.Invoke("⏹️  Stopwatch Stopped.");
        }
        else
        {
            OnStopped?.Invoke("⚠️  Stopwatch is not running.");
        }
    }

    public void Reset()
    {
        TimeElapsed = 0;
        IsRunning = false;
        OnReset?.Invoke("🔄 Stopwatch Reset!");
    }

    public void Tick()
    {
        if (IsRunning)
        {
            TimeElapsed++;
        }
    }
}
