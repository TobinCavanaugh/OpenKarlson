using System;
using UnityEngine;
using UnityEngine.AI;

public class NavTest : MonoBehaviour
{
    private NavMeshAgent agent;

    public NavTest()
    {
    }

    private void Start()
    {
        this.agent = base.GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!PlayerMovement.Instance)
        {
            return;
        }
        Vector3 instance = PlayerMovement.Instance.transform.position;
        if (this.agent.isOnNavMesh)
        {
            this.agent.destination = instance;
            MonoBehaviour.print("goin");
        }
    }
}