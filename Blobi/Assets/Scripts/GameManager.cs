using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BlobiShared.Physics;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject entityASprite;
    [SerializeField] private GameObject entityBSprite;

    public BlobiNet Net { get; private set; }

    private BlobiWorld world;
    private BlobiEntity entityA;
    private BlobiEntity entityB;

    private void Awake()
    {
        world = new(64, 64);

        entityA = world.SpawnEntity(new Vector2(0, 3.75f).To(), 1)
        .WithBounds(true, new BlobiAABB(new Vector2(-5, -5).To(), new Vector2(5, 5).To()), 0.95f)
        .WithDrag(true, 0.125f);

        entityA.AddForce(new Vector2(4, 0).To());

        entityB = world.SpawnEntity(new Vector2(-3, 3.75f).To(), 1)
        .WithBounds(true, new BlobiAABB(new Vector2(-5, -5).To(), new Vector2(5, 5).To()), 0.95f)
        .WithDrag(true, 0.125f);

        entityB.AddForce(new Vector2(-4, 0).To());
    }

    private void Start()
    {
        Net = new();

        Net.Client.Start();
        Net.Client.Connect("localhost", 8080, "ConnectionKey");
    }

    private void FixedUpdate()
    {
        entityA.AddForce(new Vector2(0, -9.8f).To(), Time.fixedDeltaTime);
        entityB.AddForce(new Vector2(0, -9.8f).To(), Time.fixedDeltaTime);

        world.Tick(Time.fixedDeltaTime);

        entityASprite.transform.position = entityA.Position.To();
        entityBSprite.transform.position = entityB.Position.To();
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
