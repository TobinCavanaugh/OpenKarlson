using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public Transform player;

    private Vector3 offset;

    public Camera cam;

    public GameState gameState;


    public MoveCamera()
    {
    }

    private void Start()
    {
        this.cam = base.transform.GetChild(0).GetComponent<Camera>();
        this.cam.fieldOfView = gameState.fov;
        this.offset = base.transform.position - this.player.transform.position;
    }

    private void Update()
    {
        base.transform.position = this.player.transform.position;
    }

    public void UpdateFov()
    {
        this.cam.fieldOfView = gameState.fov;
    }
}