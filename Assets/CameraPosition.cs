using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CameraPosition : MonoBehaviour{
    [Header("Game Manager")]
    [SerializeField] GameManager gameManager;
    [Header("Primary Settings")]
    [Tooltip("The camera we are controlling.")]
    [SerializeField] Camera cam;
    [Tooltip("Should we draw debug text?")]
    [SerializeField] bool drawDebugText;
    [Tooltip("If enabled, this will disable the renderer of this and all child objects at runtime.")]
    [SerializeField] bool disableRuntimeRendering;
    [Header("Camera Settings")]
    [SerializeField] float maxRange = 100f;
    [SerializeField] float minRange = 0.1f;
    //[SerializeField] float aspect = 1920f/1080f;
    [SerializeField] float fieldOfView = 90f;
    [SerializeField] GameObject focusObject;
    [SerializeField] bool alignRotationToFocusObject = true;
    [Header("Neighboring Cameras")]
    [SerializeField] CameraPosition leftCam = null;
    [SerializeField] CameraPosition rightCam = null;
    [SerializeField] CameraPosition upCam = null;
    [SerializeField] CameraPosition downCam = null;
    [Header("Debug Only... Set the starting position's activity to 1.")]
    [SerializeField] float activity;

    [SerializeField] StationChunk stationChunk;

    static float reactionSpeed = 5f;
    static float camDistance;
    static Vector3 camFocusPosition;
    static Quaternion camRotation;
    bool cameraChangedThisFrame = false;

    // Start is called before the first frame update
    void Start(){
        if(activity == 1f){
            cam.transform.position = transform.position;
            cam.transform.rotation = transform.rotation;
            camFocusPosition = focusObject.transform.position;
            camDistance = (focusObject.transform.position - transform.position).magnitude;
            camRotation = transform.rotation;
            cam.fieldOfView = fieldOfView;
            cam.nearClipPlane = minRange;
            cam.farClipPlane = maxRange;
        }
        if(disableRuntimeRendering){
            foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>()){
                mr.enabled = false;
            }
        }

        int myIndex = -1;
        for(int i=0; i<stationChunk.cameraPositions.Length; i++){
            if(stationChunk.cameraPositions[i] == this){
                myIndex = i;
            }
        }
        if(stationChunk){
            StationChunk scLeft = null;
            StationChunk scRight = null;
            if(myIndex < 5){
                scLeft = stationChunk.previous;
                scRight = stationChunk.next;
            }else{
                scLeft = stationChunk.next;
                scRight = stationChunk.previous;
            }

            if(!leftCam && scLeft){
                leftCam = scLeft.cameraPositions[myIndex];
            }
            if(!rightCam && scRight){
                rightCam = scRight.cameraPositions[myIndex];
            }
        }
    }
    void OnGUI(){
        if(!drawDebugText) return;
        if(activity < 1f) return;
        
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        GUI.Label(new Rect(10, 0, 0, 0), gameObject.name, style);
        if(Input.GetKey(KeyCode.A)){
            GUI.Label(new Rect(10, 25, 0, 0), "A (Left)", style);
        }
        if(Input.GetKey(KeyCode.D)){
            GUI.Label(new Rect(10, 25, 0, 0), "D (Right)", style);
        }

    }

    // Update is called once per frame
    void Update(){
        Debug.Log("The Controls are: " + gameManager.AreControlsActive());
        if(gameManager.AreControlsActive()){
            MoveCamera();
        }
    }

    void MoveCamera()
    {
        if(alignRotationToFocusObject && focusObject!=null){
            transform.rotation = Quaternion.LookRotation(focusObject.transform.position - transform.position, Vector3.up);
        }
        if(cameraChangedThisFrame){
            cameraChangedThisFrame = false;
            return;
        }
        
        if(activity > 0f && gameManager.AreControlsActive()){
            if(leftCam!=null && Input.GetKeyDown(KeyCode.A)){
                leftCam.cameraChangedThisFrame = true;
                leftCam.activity = 1f;
                activity = 0f;
            }
            if(rightCam!=null && Input.GetKeyDown(KeyCode.D)){
                rightCam.cameraChangedThisFrame = true;
                rightCam.activity = 1f;
                activity = 0f;
            }
            if(upCam!=null && Input.GetKeyDown(KeyCode.W)){
                upCam.cameraChangedThisFrame = true;
                upCam.activity = 1f;
                activity = 0f;
            }
            if(downCam!=null && Input.GetKeyDown(KeyCode.S)){
                downCam.cameraChangedThisFrame = true;
                downCam.activity = 1f;
                activity = 0f;
            }

            camFocusPosition = Vector3.Lerp(camFocusPosition, focusObject.transform.position, activity*Time.deltaTime*reactionSpeed);
            camDistance = Mathf.Lerp(camDistance, (focusObject.transform.position-transform.position).magnitude, activity*Time.deltaTime*reactionSpeed);
            camRotation = Quaternion.Lerp(camRotation, transform.rotation, activity*Time.deltaTime*reactionSpeed);

            cam.transform.position = camFocusPosition - camRotation * new Vector3(0, 0, camDistance);
            cam.transform.rotation = camRotation;

            //cam.transform.position = Vector3.Lerp(cam.transform.position, transform.position, activity*Time.deltaTime);
            //cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation , transform.rotation, activity*Time.deltaTime);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, fieldOfView, activity*Time.deltaTime*reactionSpeed);
            cam.nearClipPlane = Mathf.Lerp(cam.nearClipPlane, minRange, activity*Time.deltaTime*reactionSpeed);
            cam.farClipPlane = Mathf.Lerp(cam.farClipPlane, maxRange, activity*Time.deltaTime*reactionSpeed);
            //cam.aspect = Mathf.Lerp(cam.aspect, aspect, activity*Time.deltaTime);
        }
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos(){
        if(Application.isPlaying && disableRuntimeRendering){
            Gizmos.DrawCube(transform.position, gameObject.transform.localScale);
        }
        if(alignRotationToFocusObject && focusObject!=null){
            transform.rotation = Quaternion.LookRotation(focusObject.transform.position - transform.position, Vector3.up);
        }
        if(activity == 1f){
            Handles.color = Color.yellow;
            Handles.DrawWireDisc(camFocusPosition, Vector3.up, 1f);
            Handles.DrawDottedLine(camFocusPosition, cam.transform.position, 5);
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, Vector3.up, 1f);
        }
        if(focusObject!=null){
            Handles.color = Color.yellow;
            Handles.DrawDottedLine(transform.position, focusObject.transform.position, 5);
        }
        if(leftCam!=null){
            Handles.color = Color.cyan;
            Handles.DrawDottedLine(transform.position, Vector3.Lerp(transform.position, leftCam.transform.position, 0.5f), 5);
        }
        if(rightCam!=null){
            Handles.color = Color.red;
            Handles.DrawDottedLine(transform.position, Vector3.Lerp(transform.position, rightCam.transform.position, 0.5f), 5);
        }
        if(upCam!=null){
            Handles.color = Color.green;
            Handles.DrawDottedLine(transform.position, Vector3.Lerp(transform.position, upCam.transform.position, 0.5f), 5);
        }
        if(downCam!=null){
            Handles.color = Color.magenta;
            Handles.DrawDottedLine(transform.position, Vector3.Lerp(transform.position, downCam.transform.position, 0.5f), 5);
        }
        //Gizmos.matrix = transform.localToWorldMatrix;
        //Gizmos.DrawFrustum(Vector3.zero, fieldOfView, maxRange, minRange, aspect);
    }
}
