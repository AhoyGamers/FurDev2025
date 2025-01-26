using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{   //Parameters
    [SerializeField] float timeLimit;
    [SerializeField] int maxFailures;
    
    //Manager References
    [SerializeField] LifeManager lifeManager;
    
    //Other references
    [SerializeField] GameObject gameOverPanel;
    
    //State
    [SerializeField] int currentFailures = 0;
    [SerializeField] float currentTimeSinceStartOfScene = 0f;
    [SerializeField] bool controlsActive = false;

    void Start()
    {
        controlsActive = true;
        StartCoroutine("StartGameTimer");
    }
    void Update()
    {
        //Gameover Via failures
        if(currentFailures > maxFailures || currentTimeSinceStartOfScene > timeLimit)
        {
            print("You have lost.");
            DisableControls();
            ShowGameOverText();
        }

        //Once loss condition is hit, disable controls, pop up game over text.
        //else if victory condition is met, (currently) enter cooldown time.
    }
    public void AddToFailures(){
        currentFailures++;
    }
    public void RemoveFromFailures(){
        currentFailures--;
    }

    public bool AreControlsActive(){
        return controlsActive;
    }  

    public void DisableControls(){
        controlsActive = false;
    }
    public void EnableControls(){
        controlsActive = true;
    }

    private void ShowGameOverText()
    {
        gameOverPanel.SetActive(true);
    }

    private IEnumerator StartGameTimer(){
        while(true){
            yield return new WaitForSeconds(1);
            currentTimeSinceStartOfScene++;
        }
    }
}
