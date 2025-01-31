using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerLight : MonoBehaviour
{
    [SerializeField] Color wrongColor;
    [SerializeField] Color correctColor;
    [SerializeField] Color neutralColor;
    [SerializeField] Color litColor;
    [SerializeField] Color unlitColor;
    Material lightMaterial;

    bool answer = false;

    void Start()
    {
        lightMaterial = this.GetComponent<MeshRenderer>().material;
    }

    public IEnumerator FlashCorrect(float timeToFlash){
        SetCorrectLight();
        yield return new WaitForSeconds(timeToFlash);
        SetLightUnlit();
    }

    public IEnumerator FlashIncorrect(float timeToFlash){
        SetWrongLight();
        yield return new WaitForSeconds(timeToFlash);
        SetLightUnlit();
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

    public void SetLightLit(){
        lightMaterial.color = litColor;
        answer = true;
    }

    public void SetLightUnlit(){
        lightMaterial.color = unlitColor;
        answer = false;
    }

    public bool IsLightLit(){
        return answer;
    }


}
