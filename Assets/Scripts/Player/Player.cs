using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Variables Publicas
    public PlayerMovement playerMovement;
    public CameraMovement cameraMovement;
    public Interactor interactor;

    // Update is called once per frame
    void Update()
    {
        cameraMovement.Rotation();
        interactor.Interaction();
    }

    private void FixedUpdate()
    {
        playerMovement.Movement();
    }
}
