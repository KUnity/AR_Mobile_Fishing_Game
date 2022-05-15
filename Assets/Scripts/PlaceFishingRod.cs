using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceFishingRod : MonoBehaviour
{
    
    public Camera arCamera;
    // Start is called before the first frame update
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(arCamera.transform.position);
    }
}
