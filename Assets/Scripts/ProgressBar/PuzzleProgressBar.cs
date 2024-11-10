using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleProgressBar : MonoBehaviour
{
    //Variables Publicas
    public Image barraProgreso;
    public float amountPuzzles;

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

        }
    }
}
