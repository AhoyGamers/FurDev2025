/*
 * Camera Player Connector
 * Handles transitions between the player and the minigames.
 * If the player hasn't selected a minigame, it keeps the camera on the player.
 * If the player has selected a minigame, transition to the minigame camera position, then let the minigame control the player.
 */
    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerConnector : MonoBehaviour
{
    ObjectClicker objectClicker; //The objectClicker script that has what minigame is selected.
    [SerializeField] Transform characterCamPosition; //The position of the player's cameraPos gameObject. The camera will be moved here if no minigame selected.

    // Start is called before the first frame update
    void Start()
    {
        objectClicker = GetComponent<ObjectClicker>();
    }

    // Update is called once per frame
    void Update()
    {
        //If a minigame is not selected, then keep the camera on the player
        if (!objectClicker.hasSelectedObject())
        {
            transform.position = characterCamPosition.position;
        }
    }
}
