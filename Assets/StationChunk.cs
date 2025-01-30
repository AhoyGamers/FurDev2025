using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationChunk : MonoBehaviour{
    [SerializeField] public StationChunk next;
    [SerializeField] public StationChunk previous;

    [SerializeField] public CameraPosition[] cameraPositions;
    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }
}
