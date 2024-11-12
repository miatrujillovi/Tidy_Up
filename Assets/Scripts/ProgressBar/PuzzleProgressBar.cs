using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PuzzleProgressBar : MonoBehaviour
{
    //Variables Publicas
    public Image barraProgreso;
    public float amountPuzzles;
    public UnityEvent whenDone;

    //Variables Privadas
    private float progressAdvance;

    public void PuzzleDone()
    {
        progressAdvance = 1f/amountPuzzles;
        barraProgreso.fillAmount += progressAdvance;
    }

    public void Update()
    {
        if (barraProgreso.fillAmount == 1)
        {
            whenDone.Invoke();
        }
    }
}
