using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //Variables Publicas
    public float rotationSpeed, minRotation, maxRotation;
    public bool canRotate;

    //Variables Privadas
    private float xRotation, yRotation;

    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    public void Rotation()
    {
        //Si podemos rotar
        if (canRotate)
        {
            //Conseguimos la rotación del mouse
            xRotation += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            yRotation += Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            //Agregamos limite de rotacion en Y
            yRotation = Mathf.Clamp(yRotation, minRotation, maxRotation);

            //Rotar objeto y camara
            transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
            Camera.main.transform.localRotation = Quaternion.Euler(-yRotation, 0f, 0f);
        }
    }
}
