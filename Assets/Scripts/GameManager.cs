using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine.Experimental.AI;

public class GameManager : MonoBehaviour
{   //Parameters
    [SerializeField] float emergencyTimeLimit;
    [SerializeField] float normalModeTimeLimit;
    [SerializeField] int maxFailures;
    [SerializeField] int maxSuccesses;
    [SerializeField] int numberOfTimesMessageIsFlashed;
    
    //Manager References
    [SerializeField] LifeManager lifeManager;
    
    [Header("UI Panel References")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject alertPanel;
    [SerializeField] GameObject[] puzzleManagers;

    //State
    [Header("States")]
    [SerializeField] int currentFailures = 0;
    [SerializeField] int currentSuccesses = 0;
    [SerializeField] float currentTimeSinceStartOfNormalPhase = 0f;
    [SerializeField] float currentTimeSinceStartOfEmergencyPhase = 0f;
    [SerializeField] bool controlsActive = false;
    [SerializeField] bool emergencyState = false;

    void Start()
    {
        controlsActive = true;
        StartCoroutine(StartNormalGameTimer());
    }
    void Update()
    {
        //Give player time to explore. Once time is up, then move into emergency mode and activate a puzzle.
        if(!emergencyState && currentTimeSinceStartOfNormalPhase >= normalModeTimeLimit){ 
            emergencyState = true;
            StopNormalGameTimer(); 
            StartCoroutine(StartEmergencyGameTimer());
            StartCoroutine(ShowAlertPanel());
        } 

        else if (emergencyState && currentSuccesses >= maxSuccesses) //If we succeed at our puzzles, we move into normal mode.
        {
            emergencyState = false;
            StopEmergencyGameTime();
            StartCoroutine(StartNormalGameTimer());
            currentFailures = 0;
            currentSuccesses = 0;
        }  
        //Gameover Via failures
        else if(currentFailures >= maxFailures || currentTimeSinceStartOfEmergencyPhase > emergencyTimeLimit)
        {
            print("You have lost.");
            DisableControls();
            ShowGameOverText();
        }
    }
    public void AddToFailures(){
        currentFailures++;
    }
    public void RemoveFromFailures(){
        currentFailures--;
    }

    public void AddToSuccesses(){
        currentSuccesses++;
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

    //This is called via a Try Again or Restart button.
    public void RestartGame(){
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void ShowGameOverText()
    {
        gameOverPanel.SetActive(true);
    }

    private IEnumerator StartNormalGameTimer(){
        while(true){
            yield return new WaitForSeconds(1);
            currentTimeSinceStartOfNormalPhase++;
        }
    }

    private void StopNormalGameTimer(){
        StopCoroutine("StartNormalGameTimer");
        currentTimeSinceStartOfNormalPhase = 0f;
    }

    private IEnumerator StartEmergencyGameTimer(){
        while(true){
            yield return new WaitForSeconds(1);
            currentTimeSinceStartOfEmergencyPhase++;
        }
    }

    private void StopEmergencyGameTime()
    {
        StopCoroutine("StartEmergencyGameTimer");
        currentTimeSinceStartOfEmergencyPhase = 0f;
    }

    private IEnumerator ShowAlertPanel(){
        int messageDisplayed = 0;
        while(messageDisplayed < numberOfTimesMessageIsFlashed){
            alertPanel.SetActive(true);
            yield return new WaitForSeconds(1);
            alertPanel.SetActive(false);
            messageDisplayed++;
            yield return new WaitForSeconds(2);
        }
        StopCoroutine(ShowAlertPanel());        
    }
}
