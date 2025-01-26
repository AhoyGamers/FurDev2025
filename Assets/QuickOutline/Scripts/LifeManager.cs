using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class LifeManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject[] lightsRow0 = new GameObject[3];
    [SerializeField] GameObject[] lightsRow1 = new GameObject[3]; 
    [SerializeField] GameObject[] lightsRow2 = new GameObject[3];
    [SerializeField] Material unlitPlastic;
    [SerializeField] Material litPlastic;
    [SerializeField] GameObject confirmbutton;
    [SerializeField] SetLifeButton setButton;
    [SerializeField] UnsetLifeButton unSetButton;
    [SerializeField] AnswerLight answerLight;
    
    //Conways game of life has the following rules:
    //(1) Any live cell with fewer than two neighbors dies
    //(2) Any cell with two or three neighbors live
    //(3) Any cell with more than three neighbors dies
    //(4) Any cell with exactly three neighbors and is dead comes to life.

    //We are going to track a 3 x 3 grid, with on or off tracked by true or false.
    //The only state that we are modifying is the CENTER light of the grid.
    //Set turns it on, unset turns it off.
    //When player hits bottom check button, they will see if the center light is correct for the current step.
    private bool[,] lifeGrid = new bool[3,3];
    int neighbors = 0;
    bool originalMiddleCellState = true;
    
    
    void Start()
    {
        GenerateLifeGrid();
        answerLight.SetNeutralLight();
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.G)){
        //     GenerateLifeGrid();
        // }
        // if(Input.GetKeyDown(KeyCode.C)){
        //     CheckAnswer();
        // }
    }

    void GenerateLifeGrid()
    {
        for(int i = 0; i < lifeGrid.GetLength(0); i++){
            for(int j = 0; j < lifeGrid.GetLength(1); j++){
                int lightValue = Random.Range(0, 2);
                if(lightValue == 0)
                {
                    lifeGrid[i, j] = false;
                    UnsetLight(i, j);
                }
                else
                {
                    lifeGrid[i, j] = true;
                    SetLight(i, j);
                }
            }
        }
        originalMiddleCellState = lifeGrid[1,1];
        //DebugGrid();
        CheckNumberOfNeighbors();
    }

    private void DebugGrid()
    {
        string debugGrid = string.Empty;
        for(int i = 0; i < lifeGrid.GetLength(0); i++)
        {
            for (int j = 0; j < lifeGrid.GetLength(1); j++)
            {
                debugGrid += lifeGrid[i,j] + " ";
            }
            debugGrid += "\n";
        }

        Debug.Log("Our grid looks like:\n" + debugGrid);
    }

    private void UnsetLight(int i, int j)
    {
        if (i == 0)
        {
            lightsRow0[j].GetComponent<MeshRenderer>().material = unlitPlastic;
        }
        else if (i == 1)
        {
            lightsRow1[j].GetComponent<MeshRenderer>().material = unlitPlastic;
        }
        else
        {
            lightsRow2[j].GetComponent<MeshRenderer>().material = unlitPlastic;
        }

    }

    private void SetLight(int i, int j)
    {
        if (i == 0)
        {
            lightsRow0[j].GetComponent<MeshRenderer>().material = litPlastic;
        }
        else if (i == 1)
        {
            lightsRow1[j].GetComponent<MeshRenderer>().material = litPlastic;
        }
        else
        {
            lightsRow2[j].GetComponent<MeshRenderer>().material = litPlastic;
        }
    }

    public void SetCenterLight(){
        lightsRow1[1].GetComponent<MeshRenderer>().material = litPlastic;
        lifeGrid[1,1] = true;
    }

    public void UnsetCenterLight(){
        lightsRow1[1].GetComponent<MeshRenderer>().material = unlitPlastic;
        lifeGrid[1,1] = false;
    }

    public bool CheckAnswer()
    {
        //When the confirm button is pressed, we are checking for the result of the NEXT generation.
        //Our neighbors value tells us what our current conditions are!
        //So, we check our current conditions, 
        
        //Check our answer according to conways rules
        //True if alive
        //False is dead

        bool answer  = false;
        if(originalMiddleCellState == true) //if the cell was alive at generation
        {
            if (neighbors < 2)
            {
                answer = false;
            }

            else if(neighbors >= 2 && neighbors <= 3)
            {
                answer = true;
            }

            else if (neighbors > 3)
            {
                answer = false;
            }
        }

        else if(originalMiddleCellState  == false) //if cell was dead at generation
        {
            if(neighbors == 3)
            {
                answer = true;
            }
        }

        if(lifeGrid[1,1] == answer){
            Debug.Log("You got it right!");
            answerLight.SetCorrectLight();
            return true;
        }
        
        else
        {
            Debug.Log("You got it wrong!");
            gameManager.AddToFailures();
            answerLight.SetWrongLight();
            GenerateLifeGrid();
            return false;
        }

    }

    void CheckNumberOfNeighbors()
    {
        for(int i = 0; i < lifeGrid.GetLength(1); i++)
        {
            //sum row 0 and 2's neighbors first.
            if(lifeGrid[0, i] == true)
            {
                neighbors += 1;
            }

            if(lifeGrid[2, i] == true)
            {
                neighbors += 1;
            } 
        }

        if(lifeGrid[1, 0])
        {
            neighbors += 1;
        } 

        if (lifeGrid[1, 2])
        {
            neighbors += 1;
        }
    }

    void TurnOnAnswerLight()
    {

    }

    void TurnOffAnswerLight()
    {

    }
}
