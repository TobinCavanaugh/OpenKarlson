using System;
using TMPro;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    public TextMeshProUGUI[] maps;

    public PlayUI()
    {
    }

    private void Start()
    {
        float[] instance = SaveManager.Instance.state.times;
        for (int i = 0; i < (int)this.maps.Length; i++)
        {
            MonoBehaviour.print(String.Concat((object)"i: ", instance[i]));
            this.maps[i].text = Timer.Instance.GetFormattedTime(instance[i]);
        }
    }
}