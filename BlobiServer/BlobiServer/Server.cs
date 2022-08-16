namespace BlobiServer;

public class Server
{
    private readonly BlobiNet net;
    private readonly BlobiWorld world;

    public Server(BlobiNet net)
    {
        this.net = net;
    }

    public void Start()
    {
        //net.SubscribeReusable<>
        
        net.Start(8080);
    }

    public void Tick(ulong tickId)
    {
        world.Tick(Constants.SPT);
    }

    public void Stop()
    {

    }
}