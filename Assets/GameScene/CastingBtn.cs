using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingBtn : MonoBehaviour
{
    [SerializeField] private GameObject data;
    public GameObject bobber;
    public GameObject button;
    public GameObject subCam;
    Transform bobberTrans;
    
    // Start is called before the first frame update
    void Start()
    {
        bobberTrans = bobber.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickCastingBtn(){
        bobberTrans.position = new Vector3(0, -3, 20);
        subCam.SetActive(true);
        data.GetComponent<GameData>().isCasted = true;
        button.SetActive(false);
    }
}
