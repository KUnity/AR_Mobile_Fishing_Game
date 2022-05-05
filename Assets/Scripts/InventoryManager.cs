using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] public GameObject itemInfo;
    private GameObject newSelectedSlot;
    private GameObject oldSelectedSlot;
    // Start is called before the first frame update
    void Start()
    {
        // 데이터에서 보유 아이템 불러오기 (배열로) 
        // 사용자가 기존에 선택한 아이템으로 oldSelectedSlot 초기화 
        // oleSelectedSlot =
        // UI에 맞춰 아이템  배치 + Slot 동적 생성 


    }

    public void OnSlotClick(){
        newSelectedSlot = EventSystem.current.currentSelectedGameObject;
        // name, probablity, power, description 가져와서 item info창에 넣어주기 

    }

    public void onUseItemClick(){
        // 테두리
        newSelectedSlot.GetComponent<Outline>().enabled = true;
        oldSelectedSlot.GetComponent<Outline>().enabled =false;


        //
    }
    // public void UnuseItem(){

    //     // 기본적으로 선택되는 아이템을 지정할지 
    //     // 
    // }


}
