using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BGE.Forms;

public class OculusController : MonoBehaviour {
    public Transform leftHand;
    public Transform rightHand;

    GameObject leftEngine;
    GameObject rightEngine;

    private JetFire leftJet;
    private JetFire rightJet;
    private Rigidbody rb;

    public GameObject head;
    public float maxSpeed = 250.0f;
    public float power = 1000.0f;

    public bool haptics = false;


}
