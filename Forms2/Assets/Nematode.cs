using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nematode : MonoBehaviour
{
    public int length = 5;

    public Material material;

    NematodeSchool school;

    Constrain constrain;


    void Awake()
    {

        school = FindObjectOfType<NematodeSchool>();
        
        length = Random.Range(10,70);
        GameObject head = null;
        float r = Random.Range(2.0f,2.50f);
        for(int i = 0 ; i < length ; i ++)
        {            
            GameObject seg = GameObject.CreatePrimitive(PrimitiveType.Sphere);
         
            if (i == 0)
            {
                head = seg;
            }

            Vector3 pos = new Vector3(0, 0, -i);
            pos = transform.TransformPoint(pos);
            seg.transform.position = pos;
            float range = 0.2f; 
            float s = Mathf.Sin(Utilities.Map(i, 0, length - 1, range, Mathf.PI - range)) * r;
            seg.transform.localScale = new Vector3(s, s, 1);
            seg.transform.rotation = this.transform.rotation;
            seg.transform.parent = this.transform;
            seg.GetComponent<Renderer>().material = material;
            seg.layer = this.gameObject.layer;
            //seg.GetComponent<Renderer>().material.color = Color.HSVToRGB(i / (float) length, 1.0f, 1.0f); 
            //seg.GetComponent<Renderer>().material.color = Color.HSVToRGB(col, 1.0f, 1.0f); 
        }

        Boid b = head.AddComponent<Boid>();
        b.maxSpeed = 3;
        ObstacleAvoidance oo = head.AddComponent<ObstacleAvoidance>();
        oo.weight = 3;
        Constrain c = head.AddComponent<Constrain>();
        c.weight = 0.4f;
        c.radius = 20;
        constrain = c;

        NoiseWander nw2 = head.AddComponent<NoiseWander>();
        nw2.axis = NoiseWander.Axis.Vertical;
        nw2.weight = 6;

        NoiseWander nw3 = head.AddComponent<NoiseWander>();
        nw3.axis = NoiseWander.Axis.Horizontal;
        nw3.weight = 6;
    }
    


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        constrain.radius = school.radius;
    }
}
