using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemBtnClick : MonoBehaviour
{
    public Button fishingRod;
    public Button bait;
   
     // Start is called before the first frame update
    void Start()
    {
    }
    
    public void openDlg(){
        gameObject.SetActive(true);
        GameObject clickedObj = EventSystem.current.currentSelectedGameObject;

        if (clickedObj.name == fishingRod.name){
            // 낚싯대 리스트 가져오기


        }else if(clickedObj.name == bait.name){
            // 미끼 리스트 가져오기 


        }
    }

    public void closeDlg(){
        gameObject.SetActive(false);
    }

}
