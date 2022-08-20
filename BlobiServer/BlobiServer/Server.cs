using BlobiShared.Packets;
using LiteNetLib;

namespace BlobiServer;

public class Server
{
    public BlobiNet Net { get; }
    public BlobiGame Game { get; } = new();
    public Dictionary<NetPeer, BlobiPlayer> Peers { get; } = new();

    public Server()
    {
        Net = new(this);

        Net.On
    }

    public void Start()
    {   
        Net.Manager.Start(8080);
    }

    public void PollEvents()
    {
        Net.Manager.PollEvents();
    }

    public void Tick(ulong tickId)
    {
        Game.Tick(tickId);
    }

    public void Stop()
    {

    }
}