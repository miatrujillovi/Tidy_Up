using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleInteractor : MonoBehaviour
{
    // Variables publicas
    public UnityEvent onObjectPlaced;

    // Variables privadas
    private PuzzleObject objectInHand;
    private PuzzlePosition puzzlePosition;

    // Start is called before the first frame update
    void Start()
    {
        objectInHand = null;
    }

    // Update is called once per frame
    void Update()
    {
        Interaction();
        MoveObjectInHand();
    }

    // Funcion para interactuar
    void Interaction()
    {
        // Si apretamos el boton izquierdo del mouse
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                // Si el objeto es una posicion del puzzle
                if(hit.collider.gameObject.GetComponent<PuzzlePosition>() != null)
                {
                    // Interactuamos con la posicion
                    Interact(hit.collider.gameObject.GetComponent<PuzzlePosition>());
                }
            }
        }
    }

    // Funcion para interactuar con la posicion
    void Interact(PuzzlePosition _position)
    {
        // guardamos la posicion
        puzzlePosition = _position;

        // Si la posicion tiene un objeto
        if (_position.puzzleObject != null)
        {
            TakeObject(_position);
        }
        // Si la posicion no tiene un objeto
        else
        {
            // Si tenemos un objeto en la mano entonces lo dejamos ahi
            if(objectInHand != null)
            {
                PlaceObject(_position);
            }
        }
    }

    // Funcion para tomar un objeto
    void TakeObject(PuzzlePosition _position)
    {
        // Si tenemos un objeto en la mano
        if(objectInHand != null)
        {
            // Tomamos el objeto
            PuzzleObject _newPuzzleObject = _position.puzzleObject;

            // Dejamos el que ya tenemos y guardamos el nuevo
            PlaceObject(_position);
            objectInHand = _newPuzzleObject;
        }
        // Si no tenemos un objeto en la mano
        else
        {
            // Tomamos el objeto
            objectInHand = _position.puzzleObject;
            _position.SetPuzzleObject(null);
        }
    }

    // Funcion para dejar un objeto
    void PlaceObject(PuzzlePosition _position)
    {
        _position.SetPuzzleObject(objectInHand);
        objectInHand = null;
        onObjectPlaced.Invoke();
    }

    // Funcion para mover el objeto en la mano
    void MoveObjectInHand()
    {
        // Si tenemos un objeto en la mano
        if (objectInHand != null)
        {
            // Lo movemos hacia donde este el mouse
            objectInHand.transform.position = gameObject.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f));
        }
    }

    // Funcion para devolver si tenemos un objeto en la mano
    public bool HasObjectInHand()
    {
        if(objectInHand != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
