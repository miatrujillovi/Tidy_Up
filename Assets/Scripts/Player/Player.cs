using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables Publicas
    public PlayerMovement playerMovement;
    public CameraMovement cameraMovement;

    // Update is called once per frame
    void Update()
    {
        cameraMovement.Rotation();
    }

    private void FixedUpdate()
    {
        playerMovement.Movement();
    }
}
