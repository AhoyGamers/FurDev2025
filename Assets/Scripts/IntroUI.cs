using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMOD.Studio;

public class IntroUI : MonoBehaviour
{
    //references we need
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    private void Start()
    {
        startButton.onClick.AddListener(startGame);
        quitButton.onClick.AddListener(quitGame);

        AudioManager.StartBGM("test_bgm");
    }

    private void startGame()
    {
        LoadFirstLevel();
        AudioManager.StopBGM();
    }

    private void quitGame()
    {
        Debug.Log("Quitter!");
        Application.Quit();
    }

    private void LoadFirstLevel()
    {
        int nextScene = 1;
        SceneManager.LoadScene(nextScene);
    }
}
