using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.iOS;

public class ObjectClicker : MonoBehaviour{
    static GameObject hoveredObject;
    static GameObject selectedObject;

    [SerializeField] GameObject debugHoveredObject;
    [SerializeField] GameObject debugSelectedObject;

    // Start is called before the first frame update
    void Start(){
        
    }

    public bool hasSelectedObject()
    {
        return (selectedObject != null);
    }

    // Update is called once per frame
    void Update(){
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        float nearestDistance = 0f;
        GameObject nearestObject = null;
        foreach(RaycastHit hit in hits){
            GameObject raycastObject = hit.transform.gameObject;
            print(raycastObject.name);
            GameObject selectableGameObject = getAncestorWithTag(raycastObject, "Selectable");
            if(selectableGameObject){
                float raycastDistance = (selectableGameObject.transform.position - Camera.main.transform.position).magnitude;
                if(nearestObject == null || raycastDistance < nearestDistance){
                    nearestDistance = raycastDistance;
                    nearestObject = selectableGameObject;
                }
            }
        }
        if(nearestObject){
            setHoveredObject(nearestObject);
            debugHoveredObject = nearestObject;
        }else{
            setHoveredObject(null);
            debugHoveredObject = null;
        }

        if(Input.GetMouseButtonDown((int)MouseButton.Left)){
            setSelectedObject(hoveredObject);
            debugSelectedObject = hoveredObject;
        }else if(Input.GetMouseButtonDown((int)MouseButton.Right)){
            setSelectedObject(null);
            debugSelectedObject = null;
        }
    }

    GameObject getAncestorWithTag(GameObject gameObject, string tag){
        if(!gameObject) return null;
        if(gameObject.CompareTag(tag)) return gameObject;
        if(!(gameObject.transform.parent)) return null;
        return getAncestorWithTag(gameObject.transform.parent.gameObject, tag);
    }

    void setHoveredObject(GameObject gameObject){
        if(hoveredObject){
            Outline selectOutline = hoveredObject.GetComponent<Outline>();
            if(selectOutline){
                //selectOutline.enabled = false;
            }
        }
        hoveredObject = gameObject;
        if(hoveredObject){
            Outline selectOutline = hoveredObject.GetComponent<Outline>();
            if(selectOutline){
                //selectOutline.enabled = true;
            }
        }
    }

    void setSelectedObject(GameObject gameObject){
        if(selectedObject){
            selectedObject.SendMessage("SetSelected", false);
            /*
            Outline selectOutline = selectedObject.GetComponentInChildren<Outline>();
            if(selectOutline){
                selectOutline.enabled = false;
            }
            */
        }
        selectedObject = gameObject;
        if(selectedObject){
            selectedObject.SendMessage("SetSelected", true);
            /*
            Outline selectOutline = selectedObject.GetComponentInChildren<Outline>();
            if(selectOutline){
                selectOutline.enabled = true;
            }
            */
        }
    }
}
