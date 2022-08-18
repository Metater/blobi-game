using BlobiShared.Physics;

namespace BlobiServer;

public class BlobiGame
{
    public BlobiWorld World { get; } = new(64, 64);

    public List<BlobiPlayer> Players { get; } = new();

    public Random Random { get; } = new();

    // Constants
    public static BlobiAABB WorldBounds { get; } = new BlobiAABB(new Vector2(-100, -100), new Vector2(100, 100));
    public static float PlayerStartingRadius { get; } = 0.5f;

    public void Tick(ulong tickId)
    {
        //double time = tickId / (double)Constants.TicksPerSecond;
        World.Tick(Constants.SecondsPerTick);
    }

    public BlobiPlayer NewPlayer(NetPeer peer)
    {
        var position = WorldBounds.RandomPointInside(Random);
        var entity = World.SpawnEntity()

        var player = new BlobiPlayer(peer, this, BlobiEntity entity);

        return player;
    }
}