using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerLight : MonoBehaviour
{
    [SerializeField] Color wrongColor;
    [SerializeField] Color correctColor;
    [SerializeField] Color neutralColor;
    Material lightMaterial;

    void Start()
    {
        lightMaterial = this.GetComponent<MeshRenderer>().material;
    }

    public void SetCorrectLight()
    {
        lightMaterial.color = correctColor;
    }

    public void SetWrongLight()
    {
        lightMaterial.color = wrongColor;
    }

    public void SetNeutralLight()
    {
        lightMaterial.color = neutralColor;
    }
}
