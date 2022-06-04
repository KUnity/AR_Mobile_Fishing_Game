using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastingBtn : MonoBehaviour
{
    [SerializeField] private GameObject data;
    [SerializeField] private GameObject gsm;
    public GameObject bobber;
    public GameObject button;
    public GameObject subCam;
    public GameObject resetBtn;
    Transform bobberTrans;
    
    // Start is called before the first frame update
    void Start()
    {
        bobberTrans = bobber.GetComponent<Transform>();
        button.SetActive(false);
    }

    public void onClickCastingBtn(){
        // GameObject waterPlane = GameObject.Find("WaterPlane");
        // bobberTrans.position = waterPlane.transform.position;
        // subCam.SetActive(true);
        // data.GetComponent<GameData>().isCasted = true;
        // button.SetActive(false);
        gsm.GetComponent<GameSceneManager>().stage = 0;
        resetBtn.SetActive(false);
    }

    public void OnClickCreateBtn(){
        if (ARFilterPlanes.instance.arPlane == null)
            return; /* No Plane Exists */
        button.SetActive(true);
    }
}
