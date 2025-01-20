/*
 * DeviceBoxController
 * The physical box gameObjects that, when clicked, open up and reveal the minigame inside.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceBoxController : MonoBehaviour{
    [SerializeField] Animator animator;
    [SerializeField] bool selected;
    [SerializeField] CameraPosition cameraPosition; //Which cameraPosition object actually looks at this DeviceBox.


    // Start is called before the first frame update
    void Start(){
    }

    // Update is called once per frame
    void Update(){
        
    }

    bool IsInTransition(){
        return (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
    }

    public bool Open(){
        animator.SetBool("BoolOpen", true);
        return true;
    }
    public bool Close(){
        animator.SetBool("BoolOpen", false);
        return true;
    }

    public void NaturalizeCamera(){
        cameraPosition.NaturalizeCamera(transform);
    }

    public void SetSelected(bool selected){
        this.selected = selected;
        cameraPosition.setActivity(selected);
        if(selected) Open();
        else Close();
    }
}
