using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    //Variables Privadas
    private bool isOpen, isLocked;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interaction()
    {
        if (isOpen)
        {
            SetIsOpen(false);
        } else
        {
            SetIsOpen(true);
        }
    }
    public void SetIsOpen(bool _state)
    {
        if (!isLocked)
        {
            isOpen = _state;
            UpdateAnimator();
        }
    }

    public void SetLocked(bool _state)
    {
        if (!isOpen)
        {
            isLocked = _state;
        }
    }

    //Funcion para actualizar al Animator
    void UpdateAnimator()
    {
        animator.SetBool("isOpen", isOpen);
    }

}
