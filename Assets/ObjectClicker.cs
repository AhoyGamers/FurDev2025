using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectClicker : MonoBehaviour{
    [SerializeField] GameManager gameManager;
    static GameObject hoveredObject;
    static GameObject selectedObject;

    [SerializeField] GameObject debugHoveredObject;
    [SerializeField] GameObject debugSelectedObject;

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        if(gameManager.AreControlsActive()){
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
                debugSelectedObject = hoveredObject;
                if(isObjectAncestor(hoveredObject, selectedObject)){
                    //Current selection is an ancestor of attempted selection. Doing nothing.
                    Debug.Log("ancestor check succeeded.");
                }else{
                    setSelectedObject(hoveredObject);
                }
            }else if(Input.GetMouseButtonDown((int)MouseButton.Right)){
                setSelectedObject(null);
                debugSelectedObject = null;
            }
        }
    }

    GameObject getAncestorWithTag(GameObject gameObject, string tag){
        if(!gameObject) return null;
        if(gameObject.CompareTag(tag)) return gameObject;
        if(!(gameObject.transform.parent)) return null;
        return getAncestorWithTag(gameObject.transform.parent.gameObject, tag);
    }

    bool isObjectAncestor(GameObject child, GameObject ancestor){
        if(!child || !ancestor) return false;
        if(child.transform.parent.gameObject == ancestor) return true;
        return isObjectAncestor(child.transform.parent.gameObject, ancestor);
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
        if(gameObject == selectedObject) return;
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
