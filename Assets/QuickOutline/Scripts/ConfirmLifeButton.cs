using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmLifeButton : MonoBehaviour, IPushableButton
{
    [SerializeField] LifeManager lifeManager;
    public void OnMouseDown()
    {
        lifeManager.CheckAnswer();
    }

    public void SetSelected(bool selected)
    {
        if(selected){
            print("The confirm button was selected");
        }
        else
        {
            print("The confirm button was deselected");
        }
    }
}
