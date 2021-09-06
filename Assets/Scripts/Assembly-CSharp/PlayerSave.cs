using System;
using System.Runtime.CompilerServices;

public class PlayerSave
{
    public float[] times = new Single[100];

    public bool cameraShake { get; set; } = true;

    public float fov { get; set; } = 80f;

    public bool graphics { get; set; } = true;

    public bool motionBlur { get; set; } = true;

    public float music { get; set; } = 0.5f;

    public bool muted
    {
        get;
        set;
    }

    public float sensitivity { get; set; } = 1f;

    public bool slowmo { get; set; } = true;

    public float volume { get; set; } = 0.75f;

    public PlayerSave()
    {
    }
}