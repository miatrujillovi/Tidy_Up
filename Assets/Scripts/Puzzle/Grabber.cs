using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    // Variables publicas
    public Transform grabPoint;
    public GameObject objectInHand;
    public float grabSpeed, pushForce;

    private void Start()
    {
        objectInHand = null;
    }

    private void Update()
    {
        Interaction();

        // Si tenemos un objeto en la mano lo movemos hacia el punto
        if (objectInHand != null)
        {
            MoveObject();

            // Si apretamos Q lo soltamos
            if (Input.GetMouseButtonDown(1))
            {
                DropObject();
            }
        }
    }

    // Funcion de interacion
    public void Interaction()
    {
        // Si apretamos la tecla E
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            // Creamos un rayo desde la camara hacia el frente de ella
            RaycastHit _hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hit, 5f))
            {
                // Si el rayo colisionó con un objeto agarrable entonces lo agarramos
                if (_hit.collider.gameObject.GetComponent<GrabbableObject>() != null)
                {
                    GrabObject(_hit.collider.gameObject);
                }

                // Si el rayo colisionó con un contenedor
                if (_hit.collider.gameObject.GetComponent<ObjectsContainer>() != null)
                {
                    // Si tenemos un objeto en la mano entonces vemos si lo podemos dejar
                    if (objectInHand != null)
                    {
                        if (_hit.collider.gameObject.GetComponent<ObjectsContainer>().PlaceObject(objectInHand))
                        {
                            objectInHand = null;
                        }
                        // Si no pudimos dejarlo entonces tratamos de cambiarlo
                        else
                        {
                            GameObject _newObjectInHand = null;
                            if (_newObjectInHand = _hit.collider.gameObject.GetComponent<ObjectsContainer>().TakeObject())
                            {
                                // Si no podemos dejar el objeto que tenemos en la mano entonces lo soltamos
                                if (!_hit.collider.gameObject.GetComponent<ObjectsContainer>().PlaceObject(objectInHand))
                                {
                                    DropObject();
                                }
                                objectInHand = _newObjectInHand;
                                objectInHand.transform.parent = grabPoint;
                            }
                        }
                    }
                    // Si no tenemos objeto en la mano vemos si podemos tomar el objeto que hay en el container
                    else
                    {
                        if(objectInHand = _hit.collider.gameObject.GetComponent<ObjectsContainer>().TakeObject())
                        objectInHand.transform.parent = grabPoint;
                    }
                }
            }
        }
    }

    void GrabObject(GameObject _object)
    {
        // Si ya tenemos un objeto en la mano entonces lo soltamos
        if(objectInHand != null)
        {
            DropObject();
        }

        // Tomamos el nuevo objeto
        objectInHand = _object;
        objectInHand.transform.parent = grabPoint;

        // checamos si tiene un rigidbody para desactivarselo
        if (objectInHand.GetComponent<Rigidbody>())
        {
            Destroy(objectInHand.GetComponent<Rigidbody>());
        }
        objectInHand.GetComponent<Collider>().enabled = false;
    }

    void DropObject()
    {
        // Si tenemos un objeto en la mano entonces lo soltamos
        if(objectInHand != null)
        {
            objectInHand.transform.parent = null;
            objectInHand.AddComponent<Rigidbody>();
            objectInHand.GetComponent<Collider>().enabled = true;
            objectInHand.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * pushForce);
            objectInHand = null;
        }
    }

    void MoveObject()
    {
        // Acercamos el objeto a la mano
        objectInHand.transform.localPosition = Vector3.Lerp(objectInHand.transform.localPosition, Vector3.zero, grabSpeed * Time.deltaTime);
    }
}
