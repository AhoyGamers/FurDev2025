using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//Followed along with https://www.youtube.com/watch?v=_nRzoTzeyxU&t=2s
// 
public class DialogManager : MonoBehaviour
{
    private string outputText = "Hello world! I am a demo string, here to bring you joy! Get rid of me when we add more dialog!";
    [SerializeField] TMP_Text dialogText;
    [SerializeField] TMP_Text nameText;
    [SerializeField] Dialog dialogObject;
    [SerializeField] Button continueButton;
    [SerializeField] GameObject dialogPanel;
    Queue<string> dialogSentences;

    [SerializeField] float printSpeed = 1f;
    void Start()
    {
        //just a little prep work before we display anything.
        dialogSentences = new Queue<string>();
        ClearDialogText();
        ClearOutputText();
        StartDialog(dialogObject);
        continueButton.onClick.AddListener(DisplayNextSentence);
    }

    private IEnumerator PrintDialog()
    {
        for(int i = 0; i < outputText.Length; i++)
        {
            dialogText.text += outputText[i];
            yield return new WaitForSeconds(printSpeed);
        }
    }

    private void ClearOutputText()
    {
        outputText = string.Empty;
    }
    private void ClearDialogText()
    {
        dialogText.text = string.Empty;
    }
    public void SetOutputText(string text)
    {
        outputText = text;
    }

    public void SetSpeakerName(string name)
    {
        nameText.text = name;
    }

    public void StartDialog(Dialog dialog)
    {
        dialogSentences.Clear();
        SetSpeakerName(dialog.speakerName);
        foreach (string s in dialog.dialogs)
        {
            dialogSentences.Enqueue(s);
        }
        dialogPanel.SetActive(true);
        DisplayNextSentence();
        Debug.Log("We have: " + dialogSentences.Count + " sentences!");
    }

    public void DisplayNextSentence()
    {
        StopAllCoroutines();
        if(dialogSentences.Count == 0)
        {
            EndDialog();
            return;
        }
        ClearDialogText();
        SetOutputText(dialogSentences.Dequeue());
        StartCoroutine("PrintDialog");
    }

    public void EndDialog()
    {
        Debug.Log("Ending Dialog");
        ClearDialogText();
        dialogPanel.SetActive(false);
    }

}
