using System.Diagnostics;
using System.Runtime.InteropServices;

using BlobiServer;

Console.WriteLine("Hello, World!");

BlobiNet net = new();

long systemTicksPerSecond = RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? 10_000_000L : 1_000_000_000L;
long systemTicksPerTick = systemTicksPerSecond / 25;

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

    net.Server.PollEvents();

    Tick(tickId);

    while (GetDeltaTime() < systemTicksPerTick)
    {
        Thread.Sleep(1);
        net.Server.PollEvents();
    }

    tickCarryoverTime = GetDeltaTime() - systemTicksPerTick;

    tickId++;
}

void Tick(ulong tickId)
{
    //Console.WriteLine($"Tick {tickId}: {timer.Elapsed}");
}