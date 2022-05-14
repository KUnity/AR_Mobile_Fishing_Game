using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
namespace InvManager{
    // public enum ItemType {
    //     ROD = 1,
    //     BAIT
    // };

    public class InventoryManager : MonoBehaviour
    {
    

        // [SerializeField] public GameObject rodInfo;
        // [SerializeField] public GameObject baitInfo;

        // public Button fishingRod;
        // public Button bait;
        // public GameObject rodDlg, baitDlg; // Dlg

        
        // private GameObject newSelectedRod, oldSelectedRod;

        // private GameObject newSelectedBait, oldSelectedBait;

        // public GameObject rodSlotParent, baitSlotParent; // content box
        public TextMeshProUGUI goldNum; 



        //GameObject rodSlotPrefab, baitSlotPrefab;

        // Start is called before the first frame update
        void Start()
        {
            goldNum.text = SaveCtrl.instance.myData.gold.ToString();
            // rodSlotPrefab = Resources.Load("Prefabs/RodSlot") as GameObject;
            // baitSlotPrefab = Resources.Load("Prefabs/BaitSlot") as GameObject;

            // /*데이터에서 보유 아이템 불러오기 */
        
            // // UI에 맞춰 낚싯대 배치 + Slot 동적 생성 
            // InitRod();

            // UI에 맞춰 미끼 배치 + Slot 동적 생성 
            // int baitNum = SaveCtrl.instance.myData.fishBaits;
            // for(int i =0;i<baitNum;i++){
                
            //     GameObject slot = Instantiate(baitSlotPrefab);
            //     slot.name="BaitSlot"+i; // Slot + 아이템 코드 
            //     slot.transform.SetParent(baitdSlotParent.transform); 
                
            //     // 사용자가 기존에 선택한 아이템으로 oldSelectedSlot 초기화 
            //     if(oldSelectedBait == null){   
            //         oldSelectedBait=slot;
            //         oldSelectedBait.transform.GetChild(0).gameObject.SetActive(true);
            //         SetItemInfo(oldSelectedBait,SaveCtrl.instance.myData.equipFishingRod,ItemType.BAIT);
            //         oldSelectedBait.GetComponent<Outline>().enabled = true;
            //     }
                
            // }

        }

        // private void InitRod(){
            
        //     bool[] hasFishingRod = SaveCtrl.instance.myData.hasFishingRod;
        //     int rodTotalNum = hasFishingRod.Length;

        //     for(int i =0;i<rodTotalNum;i++){
        //         if(hasFishingRod[i]){
        //             GameObject slot = Instantiate(rodSlotPrefab);
        //             slot.name="RodSlot"+i; // Slot + 아이템 코드 
        //             slot.transform.SetParent(rodSlotParent.transform); 
                    
        //             // 사용자가 기존에 선택한 아이템으로 oldSelectedSlot 초기화 
        //             if(oldSelectedRod == null){   
        //                 oldSelectedRod=slot;
        //                 oldSelectedRod.transform.GetChild(0).gameObject.SetActive(true);
        //                 SetItemInfo(oldSelectedRod,SaveCtrl.instance.myData.equipFishingRod,ItemType.ROD);
        //                 oldSelectedRod.GetComponent<Outline>().enabled = true;
        //             }
        //         }
        //     }

        // }

        // public void SetRodInfo(GameObject slot,int itemCode, GameObject itemInfo){

        // }

        // public void SetItemInfo(GameObject slot,int itemCode, GameObject itemInfo,ItemType itemType){
        //     bool[] hasFishingRod = SaveCtrl.instance.myData.hasFishingRod;
        //     string name="",desc="",prob="",power="";
            
        //     switch(itemType){
        //         case ItemType.ROD:
        //             name = FishingRob.robNames[itemCode];
        //             prob= "Prob : "+ FishingRob.probalility_datas[itemCode].ToString();
        //             power=  "Power : "+FishingRob.power_datas[itemCode].ToString();
        //             desc = FishingRob.robDesc[itemCode];
        //             break;
        //         case ItemType.BAIT:
        //             name = Bait.baitNames[itemCode];
        //             prob= "Prob : "+ Bait.probalility_datas[itemCode].ToString();
        //             power=  "Power : "+Bait.power_datas[itemCode].ToString();
        //             desc = Bait.baitDesc[itemCode];
        //             break;
        //     }

        //     (itemInfo.transform.Find("ItemName").GetComponent<TextMeshProUGUI>()).text = name;
        //     (itemInfo.transform.Find("Probability").GetComponent<TextMeshProUGUI>()).text = prob;
        //     (itemInfo.transform.Find("Power").GetComponent<TextMeshProUGUI>()).text =  power;
        //     (itemInfo.transform.Find("Description").GetComponent<TextMeshProUGUI>()).text = desc;

        // }


        // public void OnRodSlotClick(){
        //     if(newSelectedRod!= null){
        //         oldSelectedRod = newSelectedRod;
        //     }
        //     newSelectedRod = EventSystem.current.currentSelectedGameObject;
        //     Debug.Log(newSelectedRod.name);

        //     // name, probablity, power, description 가져와서 item info창에 넣어주기 
        //     showOutline(ItemType.ROD);
        // }

        // public void OnBaitSlotClick(){
        //     if(newSelectedBait!= null){
        //         oldSelectedBait = newSelectedBait;
        //     }
        //     newSelectedBait = EventSystem.current.currentSelectedGameObject;
        //     Debug.Log(newSelectedBait.name);

        //     // name, probablity, power, description 가져와서 item info창에 넣어주기 
        // showOutline(ItemType.BAIT);

        // }


        // // 그냥 클릭만 하는 경우
        // public void showOutline(ItemType itemType){
        //     // 테두리 보이게 
        //     if(itemType == ItemType.ROD){
        //         newSelectedRod.GetComponent<Outline>().enabled = true;
        //         oldSelectedRod.GetComponent<Outline>().enabled = false;
        //     }else if(itemType == ItemType.BAIT) {
        //         newSelectedBait.GetComponent<Outline>().enabled = true;
        //         oldSelectedBait.GetComponent<Outline>().enabled = false;
        //     }
        // }

        // // 실제 착용하는 경우
        // public void onUseItemClick(ItemType itemType){
        //     // 체크 표시
        //     if(itemType == ItemType.ROD){
        //         newSelectedRod.transform.GetChild(0).gameObject.SetActive(true);
        //         oldSelectedRod.transform.GetChild(0).gameObject.SetActive(false);
        //     }else if(itemType == ItemType.BAIT){
        //         newSelectedBait.transform.GetChild(0).gameObject.SetActive(true);
        //         oldSelectedBait.transform.GetChild(0).gameObject.SetActive(false);
        //     }
        // }
    

    }
}