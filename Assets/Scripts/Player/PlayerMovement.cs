using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables Publicas
    public float walkSpeed, runSpeed, jumpForce;
    public bool canMove;
    public GroundDetector groundDetector;

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
        speed = walkSpeed;
        currentSpeed = 0;
        verticalForce = Vector3.zero;
        movementVector = Vector3.zero;
    }

    // Update is called once per frame
    public void Movement()
    {
        if (canMove)
        {
            Walk();
            Run();
            Jump();
        }
        Gravity();
        CheckGround();
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

    //Funcion para saltar
    void Jump()
    {
        if (isGrounded && Input.GetAxis("Jump") > 0f)
        {
            verticalForce = new Vector3(0f, jumpForce, 0f);
            isGrounded = false;
        }
    }

    //Funcion de gravedad
    void Gravity()
    {
        if (!isGrounded)
        {
            verticalForce += Physics.gravity * Time.deltaTime;
        }
        else
        {
            verticalForce = new Vector3(0f, -2f, 0f);
        }
        characterController.Move(verticalForce * Time.deltaTime);
    }

    //Funcion para ver si estamos tocando el piso
    void CheckGround()
    {
        isGrounded = groundDetector.GetIsGrounded();
    }

    //Funcion para devolver la velocidad actual del jugador
    public float GetCurrentSpeed()
    {
        return currentSpeed; 
    }
}
