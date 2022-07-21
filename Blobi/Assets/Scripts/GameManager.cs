using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BlobiNet Net { get; private set; }

    private void Start()
    {
        Net = new();

        Net.Client.Start();
        Net.Client.Connect("localhost", 8080, "ConnectionKey");
    }

    private void Update()
    {
        Net.Client.PollEvents();
    }

    private void OnApplicationQuit()
    {
        Net.Client.Stop();
    }
}
