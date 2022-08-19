using BlobiShared.Packets;
using LiteNetLib;

namespace BlobiServer;

public class Server
{
    public BlobiNet Net { get; } = new();
    public BlobiGame Game { get; } = new();
    public Dictionary<NetPeer, BlobiPlayer> Peers { get; } = new();

    public Server()
    {
        
    }

    public void Start()
    {   
        Net.Server.Start(8080);
    }

    public void PollEvents()
    {
        Net.Server.PollEvents();
    }

    public void Tick(ulong tickId)
    {
        Game.Tick(tickId);
    }

    public void Stop()
    {

    }
}