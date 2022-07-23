using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAt : MonoBehaviour
{
    public GameObject target; 
    public BGE.Forms.Boid b;
    // Start is called before the first frame update
    void AssignBoid()
    {
        b = target.GetComponentInChildren<BGE.Forms.Boid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (b != null)
        {
            transform.LookAt(b.transform);
        }
    }
}
