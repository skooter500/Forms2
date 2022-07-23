using UnityEngine;
using System.Collections;
using System;

namespace BGE.Forms
{

    public class OffsetPursue : SteeringBehaviour
    {
        public GameObject leaderGameObject;
        public Boid leader;
        Vector3 targetPos;
        Vector3 worldTarget;
        public Vector3 offset;

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(targetPos, 2);
        }

        // Start is called before the first frame update
        public void CalculateOffset()
        {
            if (leaderGameObject != null)
            {
                leader = leaderGameObject.GetComponentInChildren<Boid>();
            }

            offset = transform.position - leader.transform.position;
            offset = Quaternion.Inverse(leader.transform.rotation) * offset;
            Debug.Log("In Start: " + offset);            
        }

        // Update is called once per frame
        public override Vector3 Calculate()
        {
            worldTarget = leader.TransformPoint(offset);
            float dist = Vector3.Distance(boid.position, worldTarget);
            float time = dist / boid.maxSpeed;

            targetPos = worldTarget + (leader.velocity * time);
            return boid.ArriveForce(targetPos);
        }
    }

}