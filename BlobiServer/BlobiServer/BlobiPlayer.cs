using BlobiShared.Physics;

namespace BlobiServer;

public class BlobiPlayer
{
    public BlobiGame Game { get; }
    public BlobiEntity Entity { get; }
    public string Name { get; set; }

    public BlobiPlayer(BlobiGame game, BlobiEntity entity)
    {
        Game = game;
        Entity = entity;

        Name = $"Player {Game.Random.Next(0, 100_000)}"; 
    }
}