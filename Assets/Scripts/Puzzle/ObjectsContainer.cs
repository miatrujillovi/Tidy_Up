using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectsContainer : MonoBehaviour
{
    // Variables publicas
    public string ObjectName;
    public bool needsOrder;
    public Transform[] positions;
    public float grabSpeed;
    public UnityEvent onFull;

    // Variables privadas
    private int currentIndex = 0, currentID = 0;
    private bool isFull = false;
    private GameObject _currentObject;

    private void Update()
    {
        MoveCurrentObject();
    }

    // Funcion para recibir un objeto
    public bool PlaceObject(GameObject _object)
    {
        // Si no está lleno el container, es del mismo tipo y el ID es igual al index
        if (!isFull && _object.GetComponent<GrabbableObject>().ObjectName == ObjectName && currentID == currentIndex)
        {
            // Guardamos el objeto y aumentamos el index
            _currentObject = _object;
            _currentObject.transform.parent = positions[currentIndex];
            currentIndex++;

            // Si necesitamos un orden revisamos que sea el correcto
            if (needsOrder)
            {
                // Si tiene el ID necesario entonces lo aceptamos como correcto
                if(_currentObject.GetComponent<GrabbableObject>().ID == currentID)
                {
                    currentID++;
                }
            }
            // Si no necesitamos un orden entonces lo mandamos como correcto
            else
            {
                currentID++;
            }

            // Si ya llegamos a la capacidad del container entonces estamos llenos
            if (currentIndex == positions.Length)
            {
                isFull = true;
                onFull.Invoke();
            }
            return true;
        }
        // Si estamos llenos entonces no pude dejarlo
        else
        {
            return false;
        }
    }

    // Funcion para devolver un objeto
    public GameObject TakeObject()
    {
        // Si hay al menos un objeto en el contenedor entonces vemos si lo podemos devolver
        if (currentIndex != 0)
        {
            // Si hay uno equivocado entonces lo puede tomar
            if (currentIndex > currentID)
            {
                currentIndex--;
                GameObject _returnObject = _currentObject;
                _currentObject = null;
                return _returnObject;
            }
            // Si no entonces no puede tomarlo
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }

    // Funcion para mover el ultimo objeto hacia su posicion
    void MoveCurrentObject()
    {
        if(_currentObject != null)
        {
            _currentObject.transform.localPosition = Vector3.Lerp(_currentObject.transform.localPosition, Vector3.zero, grabSpeed * Time.deltaTime);
            _currentObject.transform.localRotation = Quaternion.Lerp(_currentObject.transform.localRotation, Quaternion.identity, grabSpeed * Time.deltaTime);
        }
    }
}
