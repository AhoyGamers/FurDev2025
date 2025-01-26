using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceBoxController : MonoBehaviour{
    [SerializeField] Animator animator;
    [SerializeField] bool selected;
    [SerializeField] CameraPosition cameraPosition;
    [SerializeField] BoxCollider boxCollider;


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
        boxCollider.enabled = false; //Turn off the collider for the outside of the box.
        return true;
    }
    public bool Close(){
        animator.SetBool("BoolOpen", false);
        boxCollider.enabled = true; //Turn on the collider for the outside of the box.
        return true;
    }

    public void SetSelected(bool selected){
        this.selected = selected;
        if(selected) Open();
        else Close();
    }
}
