namespace BlobiServer;

public class Server
{
    private readonly BlobiNet net;

    #region Lifecycle
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

    }

    public void Stop()
    {

    }
    #endregion Lifecycle
}