using EZCameraShake;
using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public bool player;

    public Explosion()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        int num = other.gameObject.layer;
        Vector3 vector3 = other.transform.position - base.transform.position;
        Vector3 vector31 = vector3.normalized;
        float single = Vector3.Distance(other.transform.position, base.transform.position);
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (other.gameObject.name != "Torso")
            {
                return;
            }
            RagdollController component = (RagdollController)other.transform.root.GetComponent(typeof(RagdollController));
            if (!component || component.IsRagdoll())
            {
                return;
            }
            component.MakeRagdoll(vector31 * 1100f);
            if (this.player)
            {
                PlayerMovement.Instance.Slowmo(0.35f, 0.5f);
            }
            Enemy enemy = (Enemy)other.transform.root.GetComponent(typeof(Enemy));
            if (enemy)
            {
                enemy.DropGun(Vector3.up);
            }
            return;
        }
        Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
        if (!rigidbody)
        {
            return;
        }
        if (single < 5f)
        {
            single = 5f;
        }
        rigidbody.AddForce((vector31 * 450f) / single, ForceMode.Impulse);
        rigidbody.AddTorque(new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)) * 10f);
        if (num == LayerMask.NameToLayer("Player"))
        {
            ((PlayerMovement)other.transform.root.GetComponent(typeof(PlayerMovement))).Explode();
        }
    }

    private void Start()
    {
        float single = Vector3.Distance(base.transform.position, PlayerMovement.Instance.gameObject.transform.position);
        MonoBehaviour.print(single);
        float single1 = 10f / single;
        if (single1 < 0.1f)
        {
            single1 = 0f;
        }
        CameraShaker.Instance.ShakeOnce(20f * single1 * GameState.Instance.cameraShake, 2f, 0.4f, 0.5f);
        MonoBehaviour.print(String.Concat((object)"ratio: ", single1));
    }
}