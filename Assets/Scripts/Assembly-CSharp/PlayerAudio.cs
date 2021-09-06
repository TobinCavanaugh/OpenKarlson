using System;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    private Rigidbody rb;

    public AudioSource wind;

    public AudioSource foley;

    private float currentVol;

    private float volVel;

    public PlayerAudio()
    {
    }

    private void Start()
    {
        this.rb = PlayerMovement.Instance.GetRb();
    }

    private void Update()
    {
        if (!this.rb)
        {
            return;
        }
        float single = this.rb.velocity.magnitude;
        if (!PlayerMovement.Instance.grounded)
        {
            single = (single - 10f) / 30f;
        }
        else
        {
            if (single < 20f)
            {
                single = 0f;
            }
            single = (single - 20f) / 30f;
        }
        if (single > 1f)
        {
            single = 1f;
        }
        single *= 1f;
        this.currentVol = Mathf.SmoothDamp(this.currentVol, single, ref this.volVel, 0.2f);
        if (PlayerMovement.Instance.paused)
        {
            this.currentVol = 0f;
        }
        this.foley.volume = this.currentVol;
        this.wind.volume = this.currentVol * 0.5f;
    }
}