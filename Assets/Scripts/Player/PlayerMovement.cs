using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables Publicas
    public float walkSpeed, runSpeed, jumpForce;
    public bool canMove;

    //Variables Privadas
    private Vector3 movementVector, verticalForce;
    private float speed, currentSpeed;
    private bool isGrounded;
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        // Inicializacion de Variables
        characterController = GetComponent<CharacterController>();
        speed = 0;
        currentSpeed = 0;
        verticalForce = Vector3.zero;
        movementVector = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            Walk();
            Run();
        }
    }

    //Funcion para caminar
    public void Walk()
    {
        //Conseguir inputs
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.z = Input.GetAxis("Vertical");

        //Normalizamos el vector de movimiento
        movementVector = movementVector.normalized;

        //Nos movemos en direccion de la camara
        movementVector = transform.TransformDirection(movementVector);

        //Guardamos la velocidad actual con suavizado
        currentSpeed = Mathf.Lerp(currentSpeed, movementVector.magnitude * speed, 10f * Time.deltaTime);

        //Nos movemos
        characterController.Move(movementVector * currentSpeed * Time.deltaTime);
    }

    //Funcion para correr
    public void Run()
    {
        //Si presionamos el boton para correr, modificamos la velocidad
        if (Input.GetAxis("Run") > 0)
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }
}
