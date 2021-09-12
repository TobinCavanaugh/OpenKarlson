using System;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    private CharacterJoint[] c;

    private Vector3[] axis;

    private Vector3[] anchor;

    private Vector3[] swingAxis;

    public GameObject hips;

    private float[] mass;

    public GameObject[] limbs;

    private bool isRagdoll;

    public Transform leftArm;

    public Transform rightArm;

    public Transform head;

    public Transform hand;

    public Transform hand2;

    public RagdollController()
    {
    }

    private void AddRigid(int i, Vector3 dir)
    {
        GameObject gameObject = this.limbs[i];
        Rigidbody rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.mass = this.mass[i];
        //rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        rigidbody.AddForce(dir);
        if (i != 0)
        {
            CharacterJoint characterJoint = gameObject.AddComponent<CharacterJoint>();
            characterJoint.autoConfigureConnectedAnchor = true;
            characterJoint.connectedBody = this.FindConnectedBody(i);
            characterJoint.axis = this.axis[i];
            characterJoint.anchor = this.anchor[i];
            characterJoint.swingAxis = this.swingAxis[i];
        }
    }

    private Rigidbody FindConnectedBody(int i)
    {
        int num = 0;
        if (i == 2)
        {
            num = 1;
        }
        if (i == 4)
        {
            num = 3;
        }
        if (i == 7)
        {
            num = 6;
        }
        if (i == 9)
        {
            num = 8;
        }
        if (i == 10)
        {
            num = 5;
        }
        return this.limbs[num].GetComponent<Rigidbody>();
    }

    public bool IsRagdoll()
    {
        return this.isRagdoll;
    }

    private void LateUpdate()
    {
    }

    public void MakeRagdoll(Vector3 dir)
    {
        if (this.isRagdoll)
        {
            return;
        }
        Object.Destroy(base.GetComponent<UnityEngine.AI.NavMeshAgent>());
        Object.Destroy(base.GetComponent("NavTest"));
        this.isRagdoll = true;
        Object.Destroy(base.GetComponent<Rigidbody>());
        base.GetComponentInChildren<Animator>().enabled = false;
        for (int i = 0; i < (int)this.limbs.Length; i++)
        {
            this.AddRigid(i, dir);
            this.limbs[i].gameObject.layer = LayerMask.NameToLayer("Object");
            this.limbs[i].AddComponent(typeof(Object));
        }
    }

    private void MakeStatic()
    {
        int length = (int)this.limbs.Length;
        this.c = new CharacterJoint[length];
        Rigidbody[] component = new Rigidbody[length];
        this.mass = new Single[length];
        for (int i = 0; i < (int)this.limbs.Length; i++)
        {
            component[i] = this.limbs[i].GetComponent<Rigidbody>();
            this.mass[i] = component[i].mass;
            this.c[i] = this.limbs[i].GetComponent<CharacterJoint>();
        }
        this.axis = new Vector3[length];
        this.anchor = new Vector3[length];
        this.swingAxis = new Vector3[length];
        for (int j = 0; j < (int)this.c.Length; j++)
        {
            if (this.c[j] != null)
            {
                this.axis[j] = this.c[j].axis;
                this.anchor[j] = this.c[j].anchor;
                this.swingAxis[j] = this.c[j].swingAxis;
                Object.Destroy(this.c[j]);
            }
        }
        Rigidbody[] rigidbodyArray = component;
        for (int k = 0; k < (int)rigidbodyArray.Length; k++)
        {
            Object.Destroy(rigidbodyArray[k]);
        }
    }

    private void Start()
    {
        this.MakeStatic();
    }
}