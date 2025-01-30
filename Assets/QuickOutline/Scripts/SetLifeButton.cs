using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetLifeButton : MonoBehaviour, IPushableButton
{
   [SerializeField] LifeManager lifeManager;

    public void OnMouseDown(){
        lifeManager.SetCenterLight();
    }
}
