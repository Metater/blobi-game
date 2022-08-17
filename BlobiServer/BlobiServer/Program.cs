using System.Diagnostics;
using System.Runtime.InteropServices;

using BlobiServer;

Console.WriteLine("Hello, World!");

Server server = new();

long systemTicksPerSecond = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 10_000_000L : 1_000_000_000L;
long systemTicksPerTick = systemTicksPerSecond / Constants.TicksPerSecond;

long tickStartTime;
long tickCarryoverTime = 0;
Stopwatch timer = Stopwatch.StartNew();
long GetDeltaTime() => (timer.ElapsedTicks - tickStartTime) + tickCarryoverTime;
ulong tickId = 0;

while (true)
{
    if (Console.KeyAvailable)
    {
        ConsoleKey key = Console.ReadKey().Key;
        if (key == ConsoleKey.X)
        {
            break;
        }
    }

    tickStartTime = timer.ElapsedTicks;

    server.PollEvents();

    //Console.WriteLine($"Tick {tickId}: {timer.Elapsed}");
    server.Tick(tickId);

    while (GetDeltaTime() < systemTicksPerTick)
    {
        Thread.Sleep(1);
        server.PollEvents();
    }

    tickCarryoverTime = GetDeltaTime() - systemTicksPerTick;

    tickId++;
}

server.Stop();