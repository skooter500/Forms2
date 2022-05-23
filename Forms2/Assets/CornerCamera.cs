using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;


public class CornerCamera : MonoBehaviour
{
    public Transform cam;
    public Vector3 target;

    public Quaternion from;
    public float fromDistance;
    public float toDistance;
    public Quaternion to;

    public float angle = 45.0f;

    public float transitionTime = 2.0f;

    public float elapsed; 

    public float step = 25;
    public float min = 0;
    public float max = 200;
    public enum Transition {rotation, movement, time, shaderTime};

    public Transition transition = Transition.rotation; 

    Queue<Transition> transitions = new Queue<Transition>();

    NematodeSchool ns;

    public Utilities.EASE ease;

    float lastf = 0.5f;

    float oldTime = 2.5f;
    float newTime = 0;
    float oldShaderTime = 0; 
    float newShaderTime = 0; 

    float oldTransitionTime = 0;

    bool stopped = true;

    public Light directionalLight;

    void OnDestroy()
    {
        Debug.Log("On Destroy Called");
    }


    public void StopStart(InputAction.CallbackContext context)
    {

        if (elapsed != transitionTime)
        {
            return;
        }
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        
        if (stopped)
        {
            Debug.Log("Starting");
            newTime = CornerCamera.timeScale;;
            oldTime = 0;            
            newShaderTime = oldShaderTime;            
            oldShaderTime = 0;
            elapsed = 0.0f;
            transition = Transition.time;
            
            //Invoke("ShaderTimeTransition", transitionTime);
        }
        else
        {
            Debug.Log("Stopping");            
            newTime = 0;
            oldTime = ns.ts;            
            newShaderTime = 0;
            oldShaderTime = ns.material.GetFloat("_TimeMultiplier");       
            //ns.material.SetFloat("_TimeMultiplier", 0);
            elapsed = 0.0f;
            transition = Transition.time;
            oldTransitionTime = transitionTime;

            //Invoke("ShaderTimeTransition", transitionTime);
        }
    }

    public void Quit(InputAction.CallbackContext context)
    {
        shouldIgnore = true;
        Application.Quit();
    } 
    public void Light(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>() * 200;    
        Debug.Log("Center Light: " + f);    
        ns.material.SetFloat("_CI", f);
    }

    public void Smoothness(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();    
        Debug.Log("Smoothness: " + f);    
        ns.material.SetFloat("_Glossiness", f);
    }
    
    public void Temp(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = (context.ReadValue<float>() - 0.5f) * 200;    
        Debug.Log("Temp: " + f);    
        colorGrading.temperature.Override(f);
    }
    
    public void Tint(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = (context.ReadValue<float>() - 0.5f) * 200;    
        Debug.Log("Tint: " + f);    
        colorGrading.tint.Override(f);    
    }

    public void Metalic(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();    
        Debug.Log("Metalic: " + f);    
        ns.material.SetFloat("_Metallic", f);
    }

    /*public void FogStart(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>() * 200;    
        Debug.Log("Fog STart: " + f);    
        RenderSettings.fogStartDistance = f;
        //ns.material.SetFloat("_CI", f);
    }
    */

    public void FogEnd(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>() * 200;    
        Debug.Log("Fog End: " + f);    
        RenderSettings.fogEndDistance = f;
        //ns.material.SetFloat("_CI", f);
    }



    void OnApplicationFocus(bool f)
    {
        shouldIgnore = ! f;
    }

    private static bool shouldIgnore = false;
    private bool ShouldIgnore(InputAction.CallbackContext context)
    {
        //bool b = Mathf.Abs(Time.time - (float) context.time) > 600;
        if (shouldIgnore)
        {
            Debug.Log("Ignoring: " + context);
        }
        return shouldIgnore;
    }

    public void DisplayValues(InputAction.CallbackContext context)
    {
        if (context.phase != InputActionPhase.Started)
        {
            return;
        }
        Debug.Log("Radius: " + ns.radius);
        Debug.Log("Probe Length: " + ns.feelerDepth);

        Debug.Log("Center Light: " + ns.material.GetFloat("_CI"));
        Debug.Log("Directional Light: " + directionalLight.intensity);

        Debug.Log("Alpha: " + ns.material.GetFloat("_Alpha"));
        Debug.Log("Bloom: " + bloom.intensity);

        Debug.Log("Fog: " + desiredAlpha);
        Debug.Log("Smoothness: " + ns.material.GetFloat("_Glossiness"));
        
        Debug.Log("Metalic: " + ns.material.GetFloat("_Metallic"));
        Debug.Log("Speed: " + CornerCamera.timeScale);
        

        Debug.Log("Range: " + ns.material.GetFloat("_PositionScale"));
        Debug.Log("Color width: " + ns.material.GetFloat("_ColorWidth"));
        
        
        Debug.Log("Shift: " + colorGrading.hueShift.value);
        Debug.Log("Temperature: " + colorGrading.temperature.value);

        Debug.Log("Tint: " + colorGrading.tint.value);
        Debug.Log("Shader Time: " + ns.material.GetFloat("_TimeMultiplier"));
        
        Debug.Log("Camera Position: " + cam.transform.position);
        Debug.Log("Camera Rotation: " + cam.transform.rotation);
        
    }


    float desiredAlpha = 0.5f;
    public void Alpha(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();    
        desiredAlpha = f;
        Debug.Log("Desired Alpha: " + desiredAlpha);    
        
    }
    public void AmbientLight(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        
        float f = context.ReadValue<float>() * 50;        
        Debug.Log("Directional Light: " + f);
        directionalLight.intensity = f;
    }

    private static float distance = -150;

    public void RestartScene(InputAction.CallbackContext context)
    {
        CornerCamera.timeScale = ns.ts;
        CornerCamera.distance = cam.transform.localPosition.z;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void FeelerLength(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        
        float f = context.ReadValue<float>() * 50;        
        Debug.Log("Probe Length: " + f);
        ns.feelerDepth = f;
        ns.sideFeelerDepth  = f;
    }

    private Bloom bloom;
    private ColorGrading colorGrading;
    public PostProcessVolume volume;

    void OnApplicationQuit()
    {
        Debug.Log("Quit");
        shouldIgnore = true;
    }

    float desiredBloom = 0;

    public void Bloom(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>() * 20;        
        Debug.Log("Bloom: " + f); 
        desiredBloom = f;        
    }

    public void ColorWidth(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();        
        Debug.Log("Color Width : " + f);
        ns.material.SetFloat("_ColorWidth", f);
        
    }


    public void ColorStart(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();        
;
        Debug.Log("Color Start : " + f);
        ns.material.SetFloat("_ColorStart", f);
        
    }
    public void ColorEnd(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();       
        Debug.Log("Color End : " + f);
        ns.material.SetFloat("_ColorEnd", f);
        
    }



    public void ColorShift(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>();        

        f = Utilities.Map(f, 0, 1, -180, 180);
        
        Debug.Log("Color Shift: " + f);
        colorGrading.hueShift.Override(f);
        //ns.material.SetFloat("_ColorShift", f);

    }



    public void TimeChanged(InputAction.CallbackContext context)
    {

        if (ShouldIgnore(context))
        {
            return;
        }        
        
        Debug.Log("Speed: " + context.ReadValue<float>());
        tTimeChanged = context.ReadValue<float>();
        if (! stopped)
        {
            ns.ts = tTimeChanged;
            newTime = tTimeChanged;
        }   
        else
        {
            newTime = tTimeChanged;
            CornerCamera.timeScale = newTime;
        }     
    }
    float tTimeChanged = 0; 

    public void Forwards(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        fromDistance = -cam.transform.localPosition.z;
        toDistance = Mathf.Clamp(fromDistance - step, min, max);            
        elapsed = 0;
        transition = Transition.movement;
    }

    public void Backwards(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        fromDistance = -cam.transform.localPosition.z;
        toDistance = Mathf.Clamp(fromDistance + step, min, max);            
        elapsed = 0;
        transition = Transition.movement;
    }

    public void PitchClock(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(angle, transform.right) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void PitchCount(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(-angle, transform.right) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void YawClock(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(angle, transform.up) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void YawCount(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(-angle, transform.up) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void RollClock(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(angle, transform.forward) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }
    public void RollCount(InputAction.CallbackContext context)
    {
        if (elapsed < transitionTime || context.phase != InputActionPhase.Performed)
        {
            return;
        }
        from = transform.rotation;
        to = Quaternion.AngleAxis(-angle, transform.forward) * transform.rotation;
        elapsed = 0;
        transition = Transition.rotation;
    }

    public void Radius(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>() * 100;
        Debug.Log("Radius: " + f);
        ns.radius = f;
    }



    public void ColorRange(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = 1.0f + context.ReadValue<float>() * 200;
        Debug.Log("Desired Range: " + f);
        desiredRange = f;
    }

    float desiredRange = 100;

    float desiredShaderTime = 50;

    public void ShaderTime(InputAction.CallbackContext context)
    {
        if (ShouldIgnore(context))
        {
            return;
        }
        float f = context.ReadValue<float>() * 100;
        Debug.Log("Desired Time: " + f);
        desiredShaderTime = f;
    }

    private static float timeScale = 2.5f;


    void Awake()
    {
        Debug.Log("Awake");
        ns = FindObjectOfType<NematodeSchool>();
        ns.ts = 0;
        oldShaderTime = 1;
        oldTime = CornerCamera.timeScale;                      
    }
            
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        //float v = 100.0f;
        //RenderSettings.ambientLight = new Color(v,v,v,1);
        elapsed = transitionTime;
        fromDistance = -cam.transform.localPosition.z;
        toDistance = fromDistance;

        bloom = volume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.Bloom>();
        colorGrading = volume.profile.GetSetting<UnityEngine.Rendering.PostProcessing.ColorGrading>();
        oldTime = CornerCamera.timeScale;
        
        newTime = 0;

        Vector3 lp = cam.transform.localPosition;
        lp.z = CornerCamera.distance; 
        cam.transform.localPosition = lp;
        
        //ns.material.SetFloat("_TimeMultiplier", 0);
    }


    // Update is called once per frame
    void Update()
    {        
        ns.material.SetFloat("_TimeMultiplier", Mathf.Lerp(ns.material.GetFloat("_TimeMultiplier"), desiredShaderTime, Time.deltaTime));
        ns.material.SetFloat("_PositionScale", Mathf.Lerp(ns.material.GetFloat("_PositionScale"), desiredRange, Time.deltaTime));
        ns.material.SetFloat("_Alpha", Mathf.Lerp(ns.material.GetFloat("_Alpha"), desiredAlpha, Time.deltaTime));


        float oldBloom = bloom.intensity.GetValue<float>();
        float lerpedBloom = Mathf.Lerp(oldBloom, desiredBloom, Time.deltaTime);
        bloom.intensity.Override(lerpedBloom);
        if (elapsed < transitionTime)
        {
            elapsed += Time.deltaTime;
            if (elapsed > transitionTime)
            {
                elapsed = transitionTime;
            }

            float t = Utilities.Map2(elapsed, 0, transitionTime, 0, 1
                    , ease, 
                    Utilities.EASE.EASE_IN_OUT
                    );
            switch (transition)
            {
                case Transition.movement:
                {
                    float z = Mathf.Lerp(fromDistance, toDistance, t);
                    Vector3 camLocal = cam.transform.localPosition;
                    camLocal.z = -z;
                    cam.transform.localPosition = camLocal;
                    break;
                }
                case Transition.rotation:
                {
                    transform.rotation = Quaternion.Slerp(from, to, t);
                    break;
                }
                case Transition.time:
                {
                    Debug.Log("New Time: " + newTime);
                    ns.ts = Mathf.Lerp(oldTime, newTime, t);         
                    //float timeM = Mathf.Lerp(oldShaderTime, newShaderTime, t);                     
                    //ns.material.SetFloat("_TimeMultiplier", timeM);      
                    if (elapsed == transitionTime)
                    {
                        stopped = ! stopped;
                    }   
                    break;                    
                }                
            }            
        }        
    }
}
