using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public GameObject target; 
    BGE.Forms.Boid b;
    // Start is called before the first frame update
    void Start()
    {
        b = target.GetComponentInChildren<BGE.Forms.Boid>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(b.transform);
    }
}
