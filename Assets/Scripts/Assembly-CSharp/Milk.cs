using System;
using UnityEngine;

public class Milk : MonoBehaviour
{
    public Milk()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (PlayerMovement.Instance.IsDead())
            {
                return;
            }
            Game.Instance.Win();
            MonoBehaviour.print("Player won");
        }
    }

    private void Update()
    {
        float single = Mathf.PingPong(Time.time, 1f);
        Vector3 vector3 = new Vector3(1f, 1f, single);
        base.transform.Rotate(vector3, 0.5f);
    }
}