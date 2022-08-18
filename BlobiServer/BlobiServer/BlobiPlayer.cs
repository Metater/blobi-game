using BlobiShared.Physics;

namespace BlobiServer;

public class BlobiPlayer
{
    public NetPeer Peer { get; }
    public BlobiGame Game { get; }
    public BlobiEntity Entity { get; }
    public string Name { get; set; }

    public BlobiPlayer(NetPeer peer, BlobiGame game, BlobiEntity entity)
    {
        Peer = peer;
        Game = game;
        Entity = entity;

        Name = $"Player {Game.Random.Next(0, 100_000)}"; 
    }
}