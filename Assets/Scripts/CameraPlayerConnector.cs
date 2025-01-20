/*
 * Camera Player Connector
 * Handles transitions between the player and the minigames.
 * If the player hasn't selected a minigame, it keeps the camera on the player and enables movement.
 * If the player has selected a minigame, transition to the minigame camera position and disable player movement.
 */
    

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerConnector : MonoBehaviour
{
    ObjectClicker objectClicker; //The objectClicker script that has what minigame is selected.
    [SerializeField] PlayerMovement playerMovement; //The PlayerMovement script on the player object that will be disabled if player is in a minigame, and enabled if not in a minigame.
    [SerializeField] Transform characterCamPosition; //The position of the player's cameraPos gameObject. The camera will be moved here if no minigame selected.
    bool playerCanMove = true;

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
            if (!playerCanMove)
            {
                playerMovement.enabled = true;
                playerCanMove = true;
            }
            transform.position = characterCamPosition.position;
        }
        else if (playerCanMove)
        {
            playerMovement.enabled = false;
            playerCanMove = false;
        }
    }
}
