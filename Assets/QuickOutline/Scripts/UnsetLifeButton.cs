using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnsetLifeButton : MonoBehaviour, IPushableButton
{
   [SerializeField] LifeManager lifeManager;
    public void OnMouseDown()
    {
        lifeManager.UnsetAnswerlight();
    }
}
