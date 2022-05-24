using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemBtnClick : MonoBehaviour
{
    public Button dlgOpenBtn;
    public Button fishingRodBtn;
    public Button baitBtn;
    public GameObject itemInfo;
    public GameObject slotParent;

    public Button onButton;

    private GameObject slotPrefab;
    private GameObject newSelectedSlot, oldSelectedSlot;
    private GameObject equippedSlot;

    public bool isRod;
    private Sprite[] rodSprites;
    private Sprite[] baitSprites;

    // private string RodSlotStr="RodSlot";
    // private string BaitSlotStr="BaitSlot";

    // Start is called before the first frame update
    void Start()
    {
        slotPrefab = Resources.Load("Prefabs/Slot") as GameObject;
        // 이미지 
        rodSprites = Resources.LoadAll<Sprite>("FishingRod");
        baitSprites = Resources.LoadAll<Sprite>("Bait");

        if (System.Object.ReferenceEquals(dlgOpenBtn, fishingRodBtn))
        {
            InitRod();
        }
        else
        {
            InitBait();
        }
        

    }

    public void OpenDlg()
    {
        gameObject.SetActive(true);
    }
    public void CloseDlg(){
        gameObject.SetActive(false);
    }


    private void InitRod()
    {
        // bool[] hasFishingRod = SaveCtrl.instance.myData.hasFishingRod;
        int rodTotalNum = SaveCtrl.instance.myData.hasFishingRod.Length;
        for (int i = 0; i < rodTotalNum; i++)
        {
            if (SaveCtrl.instance.myData.hasFishingRod[i])
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.name = "RodSlot " + i; // Slot +' '+  아이템 코드 
                slot.transform.SetParent(slotParent.transform);

                // z좌표 1로 설정
                Vector3 pos = slot.GetComponent<RectTransform>().anchoredPosition3D;
                slot.GetComponent<RectTransform>().localPosition = new Vector3(pos.x,pos.y,1);
                slot.transform.Find("Num").gameObject.SetActive(false);
                slot.transform.GetComponent<Button>().onClick.AddListener(OnSlotClick);
                
                slot.transform.Find("Image").gameObject.GetComponent<Image>().sprite = rodSprites[i];
                slot.GetComponent<RectTransform>().localScale = Vector3.one;

                // 사용자가 기존에 선택한 아이템으로 oldSelectedSlot 초기화 
                if (oldSelectedSlot == null)
                {
                    oldSelectedSlot = slot;
                    oldSelectedSlot.transform.Find("Check").gameObject.SetActive(true);
                    SetItemInfo(oldSelectedSlot, SaveCtrl.instance.myData.equipFishingRod);
                    oldSelectedSlot.GetComponent<Outline>().enabled = true;
                    equippedSlot = slot;
                }
            }
        }

    }

    private void InitBait()
    {
        int[] fishBaits = SaveCtrl.instance.myData.fishBaits;
        int baitTypeNum = fishBaits.Length;

        for (int i = 0; i < baitTypeNum; i++)
        {
        
            if ((i!=0 && fishBaits[i] > 0)|| i ==0)
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.name = "BaitSlot " + i; // Slot +' '+ 아이템 코드 
                slot.transform.SetParent(slotParent.transform);

                // z좌표 1로 설정
                Vector3 pos = slot.GetComponent<RectTransform>().anchoredPosition3D;
                slot.GetComponent<RectTransform>().localPosition = new Vector3(pos.x,pos.y,1);
                slot.GetComponent<RectTransform>().localScale = Vector3.one;
                slot.transform.Find("Image").gameObject.GetComponent<Image>().sprite = baitSprites[i];
                slot.transform.GetComponent<Button>().onClick.AddListener(OnSlotClick);
                if( i !=0){
                    slot.transform.Find("Num").GetComponent<TextMeshProUGUI>().text = fishBaits[i].ToString();
                }else{
                    slot.transform.Find("Num").gameObject.SetActive(false);
                }

                // 사용자가 기존에 선택한 아이템으로 oldSelectedSlot 초기화 
                if (oldSelectedSlot == null)
                {
                    oldSelectedSlot = slot;
                    oldSelectedSlot.transform.Find("Check").gameObject.SetActive(true);
                    SetItemInfo(oldSelectedSlot, SaveCtrl.instance.myData.equipBaits);
                    oldSelectedSlot.GetComponent<Outline>().enabled = true;
                    equippedSlot = slot;
                }
            }
        }
    }

    public void SetItemInfo(GameObject slot, int itemCode)
    {
        bool[] hasFishingRod = SaveCtrl.instance.myData.hasFishingRod;
        string name = "", desc = "", prob = "", power = "";


        if (isRod){
            name = FishingRob.robNames[itemCode];
            prob = "Prob : " + FishingRob.probalility_datas[itemCode].ToString();
            power = "Power : " + FishingRob.power_datas[itemCode].ToString();
            desc = FishingRob.robDesc[itemCode];
            itemInfo.transform.Find("ItemImage").GetComponent<Image>().sprite = rodSprites[itemCode];
        }else{
             name = Bait.baitNames[itemCode];
            prob = "Prob : " + Bait.probalility_datas[itemCode].ToString();
            power = "Power : " + Bait.power_datas[itemCode].ToString();
            desc = Bait.baitDesc[itemCode];
            itemInfo.transform.Find("ItemImage").GetComponent<Image>().sprite = baitSprites[itemCode];
        }

        (itemInfo.transform.Find("ItemName").GetComponent<Text>()).text = name;
        (itemInfo.transform.Find("Probability").GetComponent<Text>()).text = prob;
        (itemInfo.transform.Find("Power").GetComponent<Text>()).text = power;
        (itemInfo.transform.Find("Description").GetComponent<Text>()).text = desc;

    }

    public void OnSlotClick()
    {
       
        if (newSelectedSlot != null)
            oldSelectedSlot = newSelectedSlot;
        
        // 클릭된 슬롯 - new slot으로 할당 
        newSelectedSlot = EventSystem.current.currentSelectedGameObject;

        if(newSelectedSlot.Equals(oldSelectedSlot)) 
            return;
        
        // name, probablity, power, description 가져와서 item info창에 넣어주기 
        int itemCode = GetItemCodeFromName();
        SetItemInfo(newSelectedSlot,itemCode);
        showOutline();
    }

    public void showOutline()
    {
        // 테두리 보이게 
        newSelectedSlot.GetComponent<Outline>().enabled = true;
        oldSelectedSlot.GetComponent<Outline>().enabled = false;
    }

    // 실제 착용하는 경우
    public void OnUseItemClick()
    {
        if (newSelectedSlot == null || newSelectedSlot.Equals(equippedSlot))
            return;

        int itemCode = GetItemCodeFromName();

        // 체크 표시
        equippedSlot.transform.Find("Check").gameObject.SetActive(false);
        newSelectedSlot.transform.Find("Check").gameObject.SetActive(true);
        equippedSlot = newSelectedSlot;

        // 서버데이터 수정 
        if(isRod){
            SaveCtrl.instance.myData.equipFishingRod = itemCode;
        }else{
            SaveCtrl.instance.myData.equipBaits = itemCode;
        }
        
        SaveCtrl.instance.SaveData();

     

    }

    private int GetItemCodeFromName(){
        string[] split_data = newSelectedSlot.name.Split(' ');
        int itemCode =  int.Parse(split_data[1]);
        return itemCode;
    }


}
