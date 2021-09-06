using System;
using UnityEngine;

public class ExplosiveBullet : MonoBehaviour
{
    private Rigidbody rb;

    public ExplosiveBullet()
    {
    }

    private void OnCollisionEnter(Collision other)
    {
        Object.Destroy(base.gameObject);
        Object.Instantiate<GameObject>(PrefabManager.Instance.explosion, base.transform.position, Quaternion.identity);
    }

    private void Start()
    {
        this.rb = base.GetComponent<Rigidbody>();
        Object.Instantiate<GameObject>(PrefabManager.Instance.thumpAudio, base.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        this.rb.AddForce((Vector3.up * Time.deltaTime) * 1000f);
    }
}