using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BlobiShared.Packets;
using LiteNetLib;
using LiteNetLib.Utils;

namespace BlobiServer;

public class BlobiNet : INetEventListener
{
    public NetManager Server { get; private set; }
    public NetPacketProcessor Processor { get; private set; }

    public BlobiNet()
    {
        Server = new(this);
        Processor = new();
    }

    public void SubscribeReusable<T>(Action<T> onReceive) where T : class, new()
    {
        Processor.SubscribeReusable(onReceive);
    }

    public void Start(int port)
    {   
        Server.Start(port);
    }

    #region NetEvents
    public void OnConnectionRequest(ConnectionRequest request)
    {
        request.AcceptIfKey(SharedConstants.ConnectionKey);
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
        
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
        
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
        Processor.ReadAllPackets(reader, peer);
        reader.Recycle();
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        
    }

    public void OnPeerConnected(NetPeer peer)
    {
        Console.WriteLine($"Peer {peer.Id}: Connected!");

        Send(peer, new PrintPacket
        {
            Message = "Hello, Client!"
        }, DeliveryMethod.ReliableOrdered);
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Console.WriteLine($"Peer {peer.Id}: Disconnected!");
    }
    #endregion NetEvents

    #region Sending
    public void Send<T>(NetPeer peer, T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        peer.Send(Processor.Write(packet), deliveryMethod);
    }

    public void SendToAll<T>(T packet, DeliveryMethod deliveryMethod) where T : class, new()
    {
        Server.SendToAll(Processor.Write(packet), deliveryMethod);
    }
    #endregion Sending
}