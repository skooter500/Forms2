using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Constrain: SteeringBehaviour
{
    public float radius = 20.0f;

    public Vector3 center;

    public void OnDrawGizmos()
    {
        if (isActiveAndEnabled)
        {
            Gizmos.color = Color.red; 
            Gizmos.DrawWireSphere(center, radius);
        }
    }

    public void Start()
    {
            //center = transform.position;
    }

    public override Vector3 Calculate()
    {
        Vector3 toTarget = transform.position - center;
        Vector3 steeringForce = Vector3.zero;
        if (toTarget.magnitude > radius)
        {
            steeringForce = Vector3.Normalize(toTarget) * (radius - toTarget.magnitude);
        }
        return steeringForce;
    }
}