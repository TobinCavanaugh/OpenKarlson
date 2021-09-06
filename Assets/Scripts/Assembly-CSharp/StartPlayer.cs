using System;
using UnityEngine;

public class StartPlayer : MonoBehaviour
{
    public StartPlayer()
    {
    }

    private void Awake()
    {
        for (int i = base.transform.childCount - 1; i >= 0; i--)
        {
            MonoBehaviour.print(String.Concat((object)"removing child: ", i));
            base.transform.GetChild(i).parent = null;
        }
        Object.Destroy(base.gameObject);
    }
}