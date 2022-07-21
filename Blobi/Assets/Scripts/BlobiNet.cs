using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;

using LiteNetLib;
using LiteNetLib.Utils;

using BlobiShared;

public class BlobiNet : INetEventListener
{
    public NetManager Client { get; private set; }
    public NetPacketProcessor Processor { get; private set; }

    public BlobiNet()
    {
        Client = new(this);
        Processor = new();

        #region Processors
        Processor.SubscribeReusable<PrintPacket>(p =>
        {
            Debug.Log($"{p.Message}");
        });
        #endregion Processors
    }

    #region NetEvents
    public void OnConnectionRequest(ConnectionRequest request)
    {
        
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
        Debug.Log("Connected!");
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Debug.Log("Disconnected!");
    }
    #endregion NetEvents
}
