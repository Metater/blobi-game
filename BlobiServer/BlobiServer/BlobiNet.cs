using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BlobiShared;
using BlobiShared.Packets;
using LiteNetLib;
using LiteNetLib.Utils;

namespace BlobiServer;

public class BlobiNet : INetEventListener
{
    public Server Server { get; }
    public NetManager Manager { get; }
    public NetPacketProcessor Processor { get; }

    public BlobiNet(Server server)
    {
        Server = server;
        Manager = new(this);
        Processor = new();

        Processor.SubscribeReusable<RequestSetPlayerNamePacket>((data, peer) =>
        {
            
        });
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

        /*
        Send(peer, new PrintPacket
        {
            Message = "Hello, Client!"
        }, DeliveryMethod.ReliableOrdered);
        */

        var player = Server.Game.NewPlayer(peer);
        Server.Peers.Add(peer, player);
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
        Manager.SendToAll(Processor.Write(packet), deliveryMethod);
    }
    #endregion Sending
}