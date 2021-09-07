using Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grappler : Pickup
{
    private Transform tip;

    private bool grappling;

    public LayerMask whatIsGround;

    private Vector3 grapplePoint;

    private SpringJoint joint;

    [SerializeField]
    private LineRenderer lr;

    private Vector3 endPoint;

    private float offsetMultiplier;

    private float offsetVel;

    public GameObject aim;

    private int positions = 2;

    private Vector3 aimVel;

    private Vector3 scaleVel;

    private Vector3 nearestPoint;

    private PlayerMovement playerMovement;

    public float range = 70f;

    private void DrawGrapple()
    {
        if (!grappling)
        {
            lr.enabled = false;
            return;
        } 

        if(grapplePoint == Vector3.zero){
            Debug.Log("out o range");
            lr.enabled = false;
            return;
        }
        if(lr.positionCount > 2){
            lr.positionCount = 2;
        }

        lr.enabled = true;
        endPoint = Vector3.Lerp(endPoint, grapplePoint, Time.deltaTime * 15f);
        lr.SetPosition(0, tip.transform.position);
        lr.SetPosition(1, endPoint);
        lr.positionCount = 2;
        // offsetMultiplier = Mathf.SmoothDamp(offsetMultiplier, 0f, ref offsetVel, 0.1f);
        // Vector3 vector3 = tip.position;
        // float single = Vector3.Distance(endPoint, vector3);
        // lr.SetPosition(positions, endPoint);
        // float single1 = single;
        // float single2 = 1f;
        // for (int i = 1; i < positions - 1; i++)
        // {
        //     float single3 = (float)i / (float)positions;
        //     float single4 = single3 * offsetMultiplier;
        //     float single5 = (Mathf.Sin(single4 * single1) - 0.5f) * single2 * (single4 * 2f);
        //     Vector3 vector31 = (endPoint - vector3).normalized;
        //     float single6 = Mathf.Sin(single3 * 180f * 0.0174532924f);
        //     float single7 = Mathf.Cos(offsetMultiplier * 90f * 0.0174532924f);

        //     Vector3 newVect = (vector3 + (((endPoint - vector3) / (float) positions) * (float)i));
        //     //Vector3 vector32 = (vector3 + (((endPoint - vector3) / (float)positions) * (float)i)), single7 * single5 * Vector2.Perpendicular(vector31), (offsetMultiplier * single6 * Vector3.down);
        //     //lr.SetPosition(i, vector32);
        // }
    }

    private Vector3 FindNearestPoint(List<RaycastHit> hits)
    {
        Transform playerCamTransform = playerMovement.GetPlayerCamTransform();
        Vector3 vector3 = Vector3.zero;
        float item = Mathf.Infinity;
        for (int i = 0; i < hits.Count; i++)
        {
            if (hits[i].distance < item)
            {
                item = hits[i].distance;
                RaycastHit raycastHit = hits[i];
                vector3 = raycastHit.collider.ClosestPoint(playerCamTransform.position + (playerCamTransform.forward * item));
            }
        }
        return vector3;
    }

    public Vector3 GetGrapplePoint()
    {
        return grapplePoint;
    }

    private void LateUpdate()
    {
        DrawGrapple();
    }

    public override void OnAim()
    {
        if (grappling)
        {
            return;
        }
        Transform playerCamTransform = playerMovement.GetPlayerCamTransform();
        List<RaycastHit> list = Physics.RaycastAll(playerCamTransform.position, playerCamTransform.forward, range, whatIsGround).ToList<RaycastHit>();
        if (list.Count > 0)
        {
            aim.SetActive(false);
            aim.transform.localScale = Vector3.zero;
            return;
        }
        int num = 50;
        int num1 = 10;
        float single = 0.035f;
        bool count = list.Count > 0;
        int num2 = 0;
        while (num2 < num1 && !count)
        {
            for (int i = 0; i < num; i++)
            {
                float single1 = 6.28318548f / (float)num * (float)i;
                float single2 = Mathf.Cos(single1);
                float single3 = Mathf.Sin(single1);
                Vector3 vector3 = (playerCamTransform.right * single2) + (playerCamTransform.up * single3);
                list.AddRange(Physics.RaycastAll(playerCamTransform.position, playerCamTransform.forward + ((vector3 * single) * (float)num2), range, whatIsGround));
            }
            if (list.Count <= 0)
            {
                num2++;
            }
            else
            {
                count = true;
                break;
            }
        }
        nearestPoint = FindNearestPoint(list);
        if (list.Count <= 0 || grappling)
        {
            aim.SetActive(false);
            aim.transform.localScale = Vector3.zero;
            return;
        }
        aim.SetActive(true);
        aim.transform.position = Vector3.SmoothDamp(aim.transform.position, nearestPoint, ref aimVel, 0.05f);
        RaycastHit item = list[0];
        Vector3 vector31 = 0.025f * item.distance * Vector3.one;
        aim.transform.localScale = Vector3.SmoothDamp(aim.transform.localScale, vector31, ref scaleVel, 0.2f);
    }

    private void Start()
    {
        tip = base.transform.GetChild(0);
        lr.positionCount = positions;
        aim.transform.parent = null;
        aim.SetActive(false);
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();

    }

    public override void StopUse()
    {
        Object.Destroy(joint);
        grapplePoint = Vector3.zero;
        grappling = false;
    }

    public override void Use(Vector3 attackDirection)
    {
        if (grappling)
        {
            return;
        }
        grappling = true;
        Transform playerCamTransform = playerMovement.GetPlayerCamTransform();
        Transform instance = playerMovement.transform;
        RaycastHit hit;        
        if(Physics.Raycast(playerCamTransform.position, playerCamTransform.forward, out hit, range, whatIsGround)){
            grapplePoint = hit.point;
        }
        else
        {
            if (nearestPoint == Vector3.zero)
            {
                return;
            }
            grapplePoint = nearestPoint;
        }
        joint = instance.gameObject.AddComponent<SpringJoint>();
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = grapplePoint;
        joint.maxDistance = Vector2.Distance(grapplePoint, instance.position) * 0.8f;
        //joint.minDistance = Vector2.Distance(grapplePoint, instance.position) * 0.25f;
        joint.minDistance = 0;
        joint.spring = 4.5f;
        joint.damper = 7f;
        joint.massScale = 4.5f;
        endPoint = tip.position;
        offsetMultiplier = 2f;
        lr.positionCount = positions;
        AudioManager.Instance.PlayPitched("Grapple", 0.2f);

        Debug.Log("use");
    }
}