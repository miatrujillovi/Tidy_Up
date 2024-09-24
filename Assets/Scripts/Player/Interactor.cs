using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    //Variables Publicas
    public float interactionDistance;

    //Funcion de Interaccion
    public void Interaction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            //Crea un Raycast para Detectar el Objeto Interactable
            RaycastHit _hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out _hit, interactionDistance))
            {
                if (_hit.collider.gameObject.GetComponent<InteractableObject>() != null)
                {
                    _hit.collider.gameObject.GetComponent<InteractableObject>().Interact();
                }
            }
        }
    }
}
