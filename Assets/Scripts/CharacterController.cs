/*
 * Character Controller
 * Placed on the main camera to track how the mouse moves. When mouse moved, changes the orientation of the camera to match how the mouse moved.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    //How sensitive the camera is on the x and y axis
    public float sensX;
    public float sensY;

    //How much the camera should rotate on the x and y axis on the next frame
    float xRotation;
    float yRotation;

    public Transform orientation; //The player's orientation. Will be changed according to the x and y rotation vars

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get how much the mouse has moved between frames
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        //Calculate how much the character should rotate based on mouse movement
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //Prevent player from looking farther than straight up or down.

        //Actually apply the transformation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);


    }
}
