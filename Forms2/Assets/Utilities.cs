using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Utilities
{
    public static Color RandomColor()
    {
        return Color.HSVToRGB(Random.Range(0.0f, 1.0f), 1, 1);
    }

    public static float Map(float value, float r1, float r2, float m1, float m2)
    {
        float dist = value - r1;
        float range1 = r2 - r1;
        float range2 = m2 - m1;
        return m1 + ((dist / range1) * range2);
    }

    public static void AssignmaterialRecorsive(GameObject root, Material m)
    {
        Renderer[] rs = root.GetComponentsInChildren<Renderer>();
        foreach(Renderer r in rs)
        {
            r.material = m;
        }

    }

    static public bool checkNaN(Quaternion v)
    {
        if (float.IsNaN(v.x) || float.IsInfinity(v.x))
        {
            return true;
        }
        if (float.IsNaN(v.y) || float.IsInfinity(v.y))
        {
            return true;
        }
        if (float.IsNaN(v.z) || float.IsInfinity(v.z))
        {
            return true;
        }
        if (float.IsNaN(v.w) || float.IsInfinity(v.w))
        {
            return true;
        }
        return false;
    }

    static public bool checkNaN(Vector3 v)
    {
        if (float.IsNaN(v.x) || float.IsInfinity(v.x))
        {
            return true;
        }
        if (float.IsNaN(v.y) || float.IsInfinity(v.y))
        {
            return true;
        }
        if (float.IsNaN(v.z) || float.IsInfinity(v.z))
        {
            return true;
        }
        return false;
    }

    // From: https://raw.githubusercontent.com/sighack/easing-functions/master/code/easing/easing.pde



    public enum EASE { LINEAR = 0, QUADRATIC = 1, CUBIC = 2, QUARTIC = 3, QUINTIC = 4, SINUSOIDAL = 5, EXPONENTIAL = 6, CIRCULAR = 7, SQRT = 8 , EASE_IN = 0, EASE_OUT = 1, EASE_IN_OUT = 2};


    
    /* The map2() function supports the following easing types */

    /*
    * A map() replacement that allows for specifying easing curves
    * with arbitrary exponents.
    *
    * value :   The value to map
    * start1:   The lower limit of the input range
    * stop1 :   The upper limit of the input range
    * start2:   The lower limit of the output range
    * stop2 :   The upper limit of the output range
    * type  :   The type of easing (see above)
    * when  :   One of EASE_IN, EASE_OUT, or EASE_IN_OUT
    */
    public static float Map2(float value, float start1, float stop1, float start2, float stop2, EASE type, EASE when)
    {
        float b = start2;
        float c = stop2 - start2;
        float t = value - start1;
        float d = stop1 - start1;
        float p = 0.5f;
        switch (type)
        {
            case EASE.LINEAR:
                return c * t / d + b;
            case EASE.SQRT:
                if (when == EASE.EASE_IN)
                {
                    t /= d;
                    return c * Mathf.Pow(t, p) + b;
                }
                else if (when == EASE.EASE_OUT)
                {
                    t /= d;
                    return c * (1 - Mathf.Pow(1 - t, p)) + b;
                }
                else if (when == EASE.EASE_IN_OUT)
                {
                    t /= d / 2;
                    if (t < 1) return c / 2 * Mathf.Pow(t, p) + b;
                    return c / 2 * (2 - Mathf.Pow(2 - t, p)) + b;
                }
                break;
            case EASE.QUADRATIC:
                if (when == EASE.EASE_IN)
                {
                    t /= d;
                    return c * t * t + b;
                }
                else if (when == EASE.EASE_OUT)
                {
                    t /= d;
                    return -c * t * (t - 2) + b;
                }
                else if (when == EASE.EASE_IN_OUT)
                {
                    t /= d / 2;
                    if (t < 1) return c / 2 * t * t + b;
                    t--;
                    return -c / 2 * (t * (t - 2) - 1) + b;
                }
                break;
            case EASE.CUBIC:
                if (when == EASE.EASE_IN)
                {
                    t /= d;
                    return c * t * t * t + b;
                }
                else if (when == EASE.EASE_OUT)
                {
                    t /= d;
                    t--;
                    return c * (t * t * t + 1) + b;
                }
                else if (when == EASE.EASE_IN_OUT)
                {
                    t /= d / 2;
                    if (t < 1) return c / 2 * t * t * t + b;
                    t -= 2;
                    return c / 2 * (t * t * t + 2) + b;
                }
                break;
            case EASE.QUARTIC:
                if (when == EASE.EASE_IN)
                {
                    t /= d;
                    return c * t * t * t * t + b;
                }
                else if (when == EASE.EASE_OUT)
                {
                    t /= d;
                    t--;
                    return -c * (t * t * t * t - 1) + b;
                }
                else if (when == EASE.EASE_IN_OUT)
                {
                    t /= d / 2;
                    if (t < 1) return c / 2 * t * t * t * t + b;
                    t -= 2;
                    return -c / 2 * (t * t * t * t - 2) + b;
                }
                break;
            case EASE.QUINTIC:
                if (when == EASE.EASE_IN)
                {
                    t /= d;
                    return c * t * t * t * t * t + b;
                }
                else if (when == EASE.EASE_OUT)
                {
                    t /= d;
                    t--;
                    return c * (t * t * t * t * t + 1) + b;
                }
                else if (when == EASE.EASE_IN_OUT)
                {
                    t /= d / 2;
                    if (t < 1) return c / 2 * t * t * t * t * t + b;
                    t -= 2;
                    return c / 2 * (t * t * t * t * t + 2) + b;
                }
                break;
            case EASE.SINUSOIDAL:
                if (when == EASE.EASE_IN)
                {
                    return -c * Mathf.Cos(t / d * (Mathf.PI / 2)) + c + b;
                }
                else if (when == EASE.EASE_OUT)
                {
                    return c * Mathf.Sin(t / d * (Mathf.PI / 2)) + b;
                }
                else if (when == EASE.EASE_IN_OUT)
                {
                    return -c / 2 * (Mathf.Cos(Mathf.PI * t / d) - 1) + b;
                }
                break;
            case EASE.EXPONENTIAL:
                if (when == EASE.EASE_IN)
                {
                    return c * Mathf.Pow(2, 10 * (t / d - 1)) + b;
                }
                else if (when == EASE.EASE_OUT)
                {
                    return c * (-Mathf.Pow(2, -10 * t / d) + 1) + b;
                }
                else if (when == EASE.EASE_IN_OUT)
                {
                    t /= d / 2;
                    if (t < 1) return c / 2 * Mathf.Pow(2, 10 * (t - 1)) + b;
                    t--;
                    return c / 2 * (-Mathf.Pow(2, -10 * t) + 2) + b;
                }
                break;
            case EASE.CIRCULAR:
                if (when == EASE.EASE_IN)
                {
                    t /= d;
                    return -c * (Mathf.Sqrt(1 - t * t) - 1) + b;
                }
                else if (when == EASE.EASE_OUT)
                {
                    t /= d;
                    t--;
                    return c * Mathf.Sqrt(1 - t * t) + b;
                }
                else if (when == EASE.EASE_IN_OUT)
                {
                    t /= d / 2;
                    if (t < 1) return -c / 2 * (Mathf.Sqrt(1 - t * t) - 1) + b;
                    t -= 2;
                    return c / 2 * (Mathf.Sqrt(1 - t * t) + 1) + b;
                }
                break;
        };
        return 0;
    }

    /*
    * A map() replacement that allows for specifying easing curves
    * with arbitrary exponents.
    *
    * value :   The value to map
    * start1:   The lower limit of the input range
    * stop1 :   The upper limit of the input range
    * start2:   The lower limit of the output range
    * stop2 :   The upper limit of the output range
    * v     :   The exponent value (e.g., 0.5, 0.1, 0.3)
    * when  :   One of EASE_IN, EASE_OUT, or EASE_IN_OUT
    */
    public static float map3(float value, float start1, float stop1, float start2, float stop2, float v, EASE when)
    {
        float b = start2;
        float c = stop2 - start2;
        float t = value - start1;
        float d = stop1 - start1;
        float p = v;
        float output = 0;
        if (when == EASE.EASE_IN)
        {
            t /= d;
        output = c * Mathf.Pow(t, p) + b;
        }
        else if (when == EASE.EASE_OUT)
        {
            t /= d;
        output = c * (1 - Mathf.Pow(1 - t, p)) + b;
        }
        else if (when == EASE.EASE_IN_OUT)
        {
            t /= d / 2;
            if (t < 1) return c / 2 * Mathf.Pow(t, p) + b;
        output = c / 2 * (2 - Mathf.Pow(2 - t, p)) + b;
        }
        return output;
    }


}