using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE.Forms
{
    [RequireComponent(typeof(Boid))]
    public abstract class SteeringBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public bool active = true;

        public float weight = 1.0f;

        [HideInInspector]
        public Vector3 force;

        public float forceMagnitude;


        public abstract Vector3 Calculate();

        [HideInInspector]
        public Boid boid;

        public void Awake()
        {
            boid = GetComponent<Boid>();
            active = isActiveAndEnabled;
        }

        public virtual void Update()
        {
            active = isActiveAndEnabled;
        }

        public void SetActive(bool b)
        {
            active = b;
            enabled = b;
        }
    }
}