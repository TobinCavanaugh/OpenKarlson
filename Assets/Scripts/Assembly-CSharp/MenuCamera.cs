using EZCameraShake;
using System;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    private Vector3 startPos;

    private Vector3 options = new Vector3(0f, 3.6f, 8f);

    private Vector3 play = new Vector3(1f, 4.6f, 5.5f);

    private Vector3 about = new Vector3(1f, 5.5f, 5.5f);

    private Vector3 desiredPos;

    private Vector3 posVel;

    private Vector3 startRot;

    private Vector3 playRot;

    private Vector3 aboutRot;

    private Quaternion desiredRot;

    public MenuCamera()
    {
    }

    public void About()
    {
        this.desiredPos = this.about;
        this.desiredRot = Quaternion.Euler(this.aboutRot);
    }

    public void Main()
    {
        this.desiredPos = this.startPos;
        this.desiredRot = Quaternion.Euler(this.startRot);
    }

    public void Options()
    {
        this.desiredPos = this.options;
    }

    public void Play()
    {
        this.desiredPos = this.play;
        this.desiredRot = Quaternion.Euler(this.playRot);
    }

    private void Start()
    {
        this.startPos = base.transform.position;
        this.desiredPos = this.startPos;
        this.options += this.startPos;
        this.play += this.startPos;
        this.about += this.startPos;
        CameraShaker.Instance.StartShake(1f, 0.04f, 0.1f);
        this.startRot = Vector3.zero;
        this.playRot = new Vector3(0f, 90f, 0f);
        this.aboutRot = new Vector3(-90f, 0f, 0f);
    }

    private void Update()
    {
        base.transform.position = Vector3.SmoothDamp(base.transform.position, this.desiredPos, ref this.posVel, 0.4f);
        base.transform.rotation = Quaternion.Slerp(base.transform.rotation, this.desiredRot, Time.deltaTime * 4f);
    }
}