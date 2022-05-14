using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemBtnClick : MonoBehaviour
{
    public enum ItemType
    {
        ROD = 1,
        BAIT
    }
    public Button dlgOpenBtn;
    public Button fishingRodBtn;
    public Button baitBtn;
    public GameObject itemInfo;
    public GameObject slotParent;

    private ItemType itemType;
    private GameObject slotPrefab;
    private GameObject newSelectedSlot, oldSelectedSlot;



    // Start is called before the first frame update
    void Start()
    {
        slotPrefab = Resources.Load("Prefabs/Slot") as GameObject;
        Debug.Log(dlgOpenBtn.name);
        Debug.Log(fishingRodBtn.name);
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

    private void SetItemType(GameObject clickedObj)
    {
        if (clickedObj == fishingRodBtn)
        {
            itemType = ItemType.ROD;
        }
        else
        {
            itemType = ItemType.BAIT;
        }
    }


    private void InitRod()
    {
        bool[] hasFishingRod = SaveCtrl.instance.myData.hasFishingRod;
        int rodTotalNum = hasFishingRod.Length;
        for (int i = 0; i < rodTotalNum; i++)
        {
            if (hasFishingRod[i])
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.name = "RodSlot" + i; // Slot + 아이템 코드 
                slot.transform.SetParent(slotParent.transform);

                // 사용자가 기존에 선택한 아이템으로 oldSelectedSlot 초기화 
                if (oldSelectedSlot == null)
                {
                    oldSelectedSlot = slot;
                    oldSelectedSlot.transform.GetChild(0).gameObject.SetActive(true);
                    SetItemInfo(oldSelectedSlot, SaveCtrl.instance.myData.equipFishingRod);
                    oldSelectedSlot.GetComponent<Outline>().enabled = true;
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
            Debug.Log(fishBaits[i]);
            if (fishBaits[i] > 0)
            {
                GameObject slot = Instantiate(slotPrefab);
                slot.name = "BaitSlot" + i; // Slot + 아이템 코드 
                slot.transform.SetParent(slotParent.transform);
                slot.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = fishBaits[i].ToString();

                // 사용자가 기존에 선택한 아이템으로 oldSelectedSlot 초기화 
                if (oldSelectedSlot == null)
                {
                    oldSelectedSlot = slot;
                    oldSelectedSlot.transform.GetChild(0).gameObject.SetActive(true);
                    SetItemInfo(oldSelectedSlot, SaveCtrl.instance.myData.equipBaits);
                    oldSelectedSlot.GetComponent<Outline>().enabled = true;
                }
            }
        }
    }

    public void SetItemInfo(GameObject slot, int itemCode)
    {
        bool[] hasFishingRod = SaveCtrl.instance.myData.hasFishingRod;
        string name = "", desc = "", prob = "", power = "";

        switch (itemType)
        {
            case ItemType.ROD:
                name = FishingRob.robNames[itemCode];
                prob = "Prob : " + FishingRob.probalility_datas[itemCode].ToString();
                power = "Power : " + FishingRob.power_datas[itemCode].ToString();
                desc = FishingRob.robDesc[itemCode];
                break;
            case ItemType.BAIT:
                name = Bait.baitNames[itemCode];
                prob = "Prob : " + Bait.probalility_datas[itemCode].ToString();
                power = "Power : " + Bait.power_datas[itemCode].ToString();
                desc = Bait.baitDesc[itemCode];
                break;
        }

       (itemInfo.transform.Find("ItemName").GetComponent<TextMeshProUGUI>()).text = name;
        (itemInfo.transform.Find("Probability").GetComponent<TextMeshProUGUI>()).text = prob;
        (itemInfo.transform.Find("Power").GetComponent<TextMeshProUGUI>()).text = power;
        (itemInfo.transform.Find("Description").GetComponent<TextMeshProUGUI>()).text = desc;

    }

    public void OnSlotClick()
    {
        if (newSelectedSlot != null)
        {
            oldSelectedSlot = newSelectedSlot;
        }
        newSelectedSlot = EventSystem.current.currentSelectedGameObject;
        Debug.Log(newSelectedSlot.name);

        // name, probablity, power, description 가져와서 item info창에 넣어주기 
        showOutline();
    }

    public void showOutline()
    {
        // 테두리 보이게 
        newSelectedSlot.GetComponent<Outline>().enabled = true;
        oldSelectedSlot.GetComponent<Outline>().enabled = false;

    }

    // 실제 착용하는 경우
    public void onUseItemClick()
    {
        // 체크 표시
        newSelectedSlot.transform.GetChild(0).gameObject.SetActive(true);
        oldSelectedSlot.transform.GetChild(0).gameObject.SetActive(false);

    }


}
