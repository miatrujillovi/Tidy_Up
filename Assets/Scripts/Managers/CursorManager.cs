using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    //Variables Publicas
    public bool startCursorState;

    //Singleton
    public static CursorManager Instance { get; private set; }

    private void Awake()
    {
        //Verificacion de Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        SetCursorState(startCursorState);
    }

    //Funcion para Cambiar el Estado del Mouse
    public void SetCursorState(bool _state)
    {
        if (_state)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
