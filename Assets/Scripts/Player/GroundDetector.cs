using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    //Variables Publicas
    public float radius;
    public LayerMask detectedLayers;

    //Variables Privadas
    private bool isGrounded;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckGround();
    }

    public void CheckGround()
    {
        isGrounded = Physics.CheckSphere(transform.position, radius, detectedLayers);
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }
}
