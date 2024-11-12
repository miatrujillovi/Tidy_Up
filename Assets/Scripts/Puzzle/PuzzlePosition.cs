using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePosition : MonoBehaviour
{
    // Variables publicas
    public int ID;
    public PuzzleObject puzzleObject;

    // Pasar un nuevo objeto
    public void SetPuzzleObject(PuzzleObject _object)
    {
        if(_object != null)
        {
            puzzleObject = _object;
            puzzleObject.gameObject.transform.parent = gameObject.transform;
            puzzleObject.gameObject.transform.localPosition = Vector3.zero;
        }
        else
        {
            puzzleObject = null;
        }
    }

    // Si el objeto coinside
    public bool ObjectMatch()
    {
        if(puzzleObject != null)
        {
            if(puzzleObject.ID == ID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
}
