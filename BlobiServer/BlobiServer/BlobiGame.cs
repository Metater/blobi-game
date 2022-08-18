using BlobiShared.Physics;

namespace BlobiServer;

public class BlobiGame
{
    public BlobiWorld World { get; } = new(64, 64);

    public List<BlobiPlayer> Players { get; } = new();

    public Random Random { get; } = new();

    public void Tick(ulong tickId)
    {
        //double time = tickId / (double)Constants.TicksPerSecond;
        World.Tick(Constants.SecondsPerTick);
    }
}