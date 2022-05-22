using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NematodeSchool : MonoBehaviour
{

    public float sv1, ev1;
float posScale = 0;
    public float endValue;
public float ts = 1.0f;
public float shaderTs = 1.0f;
    public static float timeScale = 2.5f;  
    public GameObject prefab;

    [Range(1, 5000)]
    public float radius = 50;

    public int count = 10;

    public float speed = 2.0f;

    public float feelerDepth = 5;

    public float sideFeelerDepth = 5;

    public float minRange = 38;
    public float maxRange = 200;

    float t = 0;

    public Material material;
    // Start is called before the first frame update

    string ps = "_PositionScale";


    void Start()
    {
        posScale = material.GetFloat(ps);
        endValue = posScale;
        timeScale = ts;
        t = transitionTime;

    }
    void Awake()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 pos = Random.insideUnitSphere * 5;
            pos = transform.TransformPoint(pos);
            Quaternion q = Quaternion.Euler(Random.Range(0.0f, 360), Random.Range(0.0f, 360), Random.Range(0.0f, 360));
            GameObject nematode = GameObject.Instantiate<GameObject>(prefab, pos, q);
            nematode.transform.parent = this.transform;
        }
    }

    public float transitionTime = 2.0f;

    public float maxJump = 10;

    float startValue;

    int[] rads = {1, 30, 60, 90, 120};
    int iR = 0;

    bool lastClicked = false;

    public enum Transition {scale, speed};

    public Transition transition = Transition.scale; 

    // Update is called once per frame
    void Update()
    {
        
        if (t < transitionTime)
        {
            t += Time.deltaTime;
            if (t > transitionTime)
            {
                t = transitionTime;
            }
            switch(transition)
            {
                case Transition.scale:
                {
                    float y = Utilities.Map2(t, 0, transitionTime, startValue, endValue, Utilities.EASE.CUBIC, Utilities.EASE.EASE_IN_OUT);
                    posScale = y;
                    material.SetFloat(ps, y);
                    break;
                }
                case Transition.speed:
                {
                    //float y = Utilities.Map2(t, 0, transitionTime, sv1, ev1, Utilities.EASE.CUBIC, Utilities.EASE.EASE_IN_OUT);
                    //shaderTs = y;
                    //material.SetFloat("_TimeMultiplier", y);
                    break;

                }
            }            
        }

        float p = 0.02f;

        timeScale = ts;      
   
    }    
}
