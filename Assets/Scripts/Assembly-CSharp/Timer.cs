using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private TextMeshProUGUI text;

    private float timer;

    private bool stop;

    public static Timer Instance
    {
        get;
        set;
    }

    public Timer()
    {
    }

    private void Awake()
    {
        Timer.Instance = this;
        this.text = base.GetComponent<TextMeshProUGUI>();
        this.stop = false;
    }

    public string GetFormattedTime(float f)
    {
        if (f == 0f)
        {
            return "nan";
        }
        float single = Mathf.Floor(f / 60f);
        string str = single.ToString("00");
        single = Mathf.Floor(f % 60f);
        string str1 = single.ToString("00");
        single = f * 100f % 100f;
        string str2 = single.ToString("00");
        if (str2.Equals("100"))
        {
            str2 = "99";
        }
        return String.Format("{0}:{1}:{2}", (object)str, str1, str2);
    }

    public int GetMinutes()
    {
        return (int)Mathf.Floor(this.timer / 60f);
    }

    public float GetTimer()
    {
        return this.timer;
    }

    public void StartTimer()
    {
        this.stop = false;
        this.timer = 0f;
    }

    private string StatusText(float f)
    {
        if (f < 2f)
        {
            return "very easy";
        }
        if (f < 4f)
        {
            return "easy";
        }
        if (f < 8f)
        {
            return "medium";
        }
        if (f < 12f)
        {
            return "hard";
        }
        if (f < 16f)
        {
            return "very hard";
        }
        if (f < 20f)
        {
            return "impossible";
        }
        if (f < 25f)
        {
            return "oh shit";
        }
        if (f < 30f)
        {
            return "very oh shit";
        }
        return "f";
    }

    public void Stop()
    {
        this.stop = true;
    }

    private void Update()
    {
        if (!Game.Instance.playing || this.stop)
        {
            return;
        }
        this.timer += Time.deltaTime;
        this.text.text = this.GetFormattedTime(this.timer);
    }
}