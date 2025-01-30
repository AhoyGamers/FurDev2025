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
}
