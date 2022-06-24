using UnityEngine;
using System.Collections.Generic;

namespace BGE.Forms
{
    public class SpineAnimator : MonoBehaviour
    {

        

        public List<Vector3> offsets = new List<Vector3>();
        public List<Transform> children = new List<Transform>();
        public float damping = 10.0f;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        // This iterates through all the children transforms

        Transform parent;
        parent = (transform.parent.childCount > 1) ? transform.parent : transform.parent.parent;

        for (int i = 0; i < parent.childCount; i++)
        {
            Transform current = parent.GetChild(i);
            if (i > 0)
            {
                Transform prev = parent.GetChild(i - 1);
                // Offset from previous to current
                Vector3 offset = current.transform.position - prev.transform.position; 
                
                // Rotating from world back to local
                offset = Quaternion.Inverse(prev.transform.rotation) * offset;
                offsets.Add(offset);                
            }            
            children.Add(current);
        }
    }

        int skippedFrames = 0;

        void Update()
        {
            float dt = Time.deltaTime * NematodeSchool.timeScale;
            for (int i = 1; i < children.Count; i++)
            {
                Transform prev = children[i - 1];
                Transform current = children[i];
                Vector3 wantedPosition = prev.position + ((prev.rotation * offsets[i - 1]));
                Quaternion wantedRotation = Quaternion.LookRotation(prev.transform.position - current.position, prev.transform.up);

                Vector3 lerpedPosition = Vector3.Lerp(current.position, wantedPosition, dt * damping);

                // Dont move the segments too far apart
                Vector3 clampedOffset = lerpedPosition - prev.position;
                clampedOffset = Vector3.ClampMagnitude(clampedOffset, offsets[i - 1].magnitude);
                current.position = prev.position + clampedOffset;


                current.rotation = Quaternion.Slerp(current.rotation, wantedRotation, dt * damping);
            }
        }


        /*
        public void FixedUpdate()
        {
            if (useSpineAnimatorSystem)
            {
                return;
            }
            if (suspended)
            {
                return;
            }
            time = Time.deltaTime;
            Transform previous;
            for (int i = 0; i < bones.Count; i++)
            {
                if (i == 0)
                {
                    previous = this.transform;
                }
                else
                {
                    previous = children[i - 1];
                }

                Transform current = children[i];

                DelayedMovement(previous, current, offsets[i], i);
            }

        }


        void DelayedMovement(Transform previous, Transform current, Vector3 bondOffset, int i)
        {
            Vector3 wantedPosition = previous.TransformPointUnscaled(bondOffset);
            Vector3 newPos = Vector3.Lerp(current.position, wantedPosition, time * bondDamping);
            current.transform.position = newPos;
            Quaternion wantedRotation;
            Quaternion newRotation = Quaternion.identity;
            switch (alignmentStrategy)
            {

                case AlignmentStrategy.LookAt:
                    wantedRotation = Quaternion.LookRotation(previous.position - newPos, previous.up);
                    current.transform.rotation = Quaternion.Slerp(current.transform.rotation, wantedRotation, time * angularBondDamping);
                    break;
                case AlignmentStrategy.AlignToHead:
                    wantedRotation = previous.transform.rotation;
                    current.transform.rotation = Quaternion.Slerp(current.transform.rotation, wantedRotation, time * angularBondDamping);
                    break;
            }
        }
        */
    }
}
