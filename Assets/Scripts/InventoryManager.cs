using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] public GameObject itemInfo;
    private GameObject newSelectedSlot;
    private GameObject oldSelectedSlot;

    public GameObject slotParent; 
    public TextMeshProUGUI goldNum; 
    // Start is called before the first frame update
    void Start()
    {
        goldNum.text = SaveCtrl.instance.myData.gold.ToString();
        GameObject slotPrefab = Resources.Load("Prefabs/Slot") as GameObject;

        // 데이터에서 보유 아이템 불러오기 (배열로) 
       
        // UI에 맞춰 아이템  배치 + Slot 동적 생성 
        int havingFishingRodCnt=0;
        bool[] hasFishingRod = SaveCtrl.instance.myData.hasFishingRod;
        int size = hasFishingRod.Length;
        for(int i =0;i<size;i++){
            if(hasFishingRod[i])
                havingFishingRodCnt++;
        }

        // 사용자가 기존에 선택한 아이템으로 oldSelectedSlot 초기화 
        // oleSelectedSlot =

        for(int i =havingFishingRodCnt;i>-1;i--){
            GameObject slot = Instantiate(slotPrefab);
            slot.name="Slot"+i; 
            slot.transform.SetParent(slotParent.transform); 
            
            if(i==0){   
                oldSelectedSlot=slot;
                oldSelectedSlot.transform.GetChild(0).gameObject.SetActive(true);
                SetRodItemInfo(oldSelectedSlot,SaveCtrl.instance.myData.equipFishingRod);
                oldSelectedSlot.GetComponent<Outline>().enabled = true;
            }
        }



    }

    public void SetRodItemInfo(GameObject slot,int itemCode){
        bool[] hasFishingRod = SaveCtrl.instance.myData.hasFishingRod;
        (itemInfo.transform.Find("ItemName").GetComponent<TextMeshProUGUI>()).text = FishingRob.robNames[itemCode];
        (itemInfo.transform.Find("Probability").GetComponent<TextMeshProUGUI>()).text = "Prob : "+ FishingRob.probalility_datas[itemCode].ToString();
        (itemInfo.transform.Find("Power").GetComponent<TextMeshProUGUI>()).text =  "Power : "+FishingRob.power_datas[itemCode].ToString();
        (itemInfo.transform.Find("Description").GetComponent<TextMeshProUGUI>()).text = FishingRob.robDesc[itemCode];
    }

    public void OnSlotClick(){
        if(newSelectedSlot!= null){
            oldSelectedSlot = newSelectedSlot;
        }
        newSelectedSlot = EventSystem.current.currentSelectedGameObject;
        Debug.Log(newSelectedSlot.name);
        // name, probablity, power, description 가져와서 item info창에 넣어주기 
       showOutline();

    }


    // 그냥 클릭만 하는 경우
    public void showOutline(){
        // 테두리 보이게 
        newSelectedSlot.GetComponent<Outline>().enabled = true;
        oldSelectedSlot.GetComponent<Outline>().enabled = false;
    }

    // 실제 착용하는 경우
    public void onUseItemClick(){
        // 체크 표시
        newSelectedSlot.transform.GetChild(0).gameObject.SetActive(true);
        oldSelectedSlot.transform.GetChild(0).gameObject.SetActive(false);
    }
 

}
