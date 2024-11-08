using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour
{
    // Variables publicas
    public Camera puzzleCamera;
    public PuzzlePosition[] positions;
    public bool _debug;
    public UnityEvent onStart, onExit, onCompleted;

    // Variables privadas
    private bool isActive, isCompleted;

    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        isCompleted = false;
        if (_debug)
        {
            foreach (PuzzlePosition _position in positions)
            { 
                _position.ObjectMatch();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Si esta activo y presionamos la tecla F salimos
        if (isActive && Input.GetKeyDown(KeyCode.F))
        {
            ExitPuzzle();
        }
    }

    public void StartPuzzle()
    {
        // Si el puzzle no esta terminado podemos interactuar con el
        if (!isCompleted)
        {
            onStart.Invoke();
            isActive = true;
        }
    }

    public void ExitPuzzle()
    {
        // Si el puzzle esta activo, si no tenemos objeto en la mano
        if (isActive && !puzzleCamera.GetComponent<PuzzleInteractor>().HasObjectInHand())
        {
            onExit.Invoke();
            isActive = false;
        }
    }

    // Funcion para revisar que el puzzle este resuelto
    public void CheckMatch()
    {
        // Revisamos cada posicion del puzzle
        foreach (PuzzlePosition _position in positions)
        {
            if (!_position.ObjectMatch())
            {
                Debug.Log("No está resuelto");
                return;
            }
        }

        // Si esta resuelto entonces activamos el evento y salimos del puzzle
        Debug.Log("Resuelto");
        onCompleted.Invoke();
        isCompleted = true;
        ExitPuzzle();
    }
}
