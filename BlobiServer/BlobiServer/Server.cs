namespace BlobiServer;

public class Server
{
    public BlobiNet Net { get; } = new();
    public BlobiGame Game { get; } = new();

    public Server()
    {

    }

    public void Start()
    {
        //net.SubscribeReusable<>
        
        net.Start(8080);
    }

    public void PollEvents()
    {
        Net.Server.PollEvents();
    }

    public void Tick(ulong tickId)
    {
        world.Tick(Constants.SPT);
    }

    public void Stop()
    {

    }
}