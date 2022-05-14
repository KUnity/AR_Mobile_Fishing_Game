using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class ManageFishDlg : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // (transform.Find("DismissBtn").gameObject as Button).onClick.AddListener(CloseDlg);
    }


    public void SetItemInfo(int itemCode)
    {
        string name = "", desc = "", prob = "", power = "";

        // switch (itemType)
        // {
        //     case ItemType.ROD:
        //         name = FishingRob.robNames[itemCode];
        //         prob = "Prob : " + FishingRob.probalility_datas[itemCode].ToString();
        //         power = "Power : " + FishingRob.power_datas[itemCode].ToString();
        //         desc = FishingRob.robDesc[itemCode];
        //         break;
        //     case ItemType.BAIT:
        //         name = Bait.baitNames[itemCode];
        //         prob = "Prob : " + Bait.probalility_datas[itemCode].ToString();
        //         power = "Power : " + Bait.power_datas[itemCode].ToString();
        //         desc = Bait.baitDesc[itemCode];
        //         break;
        // }
        // name = Fish;
        // prob = "Prob : " + FishingRob.probalility_datas[itemCode].ToString();
        // power = "Power : " + FishingRob.power_datas[itemCode].ToString();
        // desc = FishingRob.robDesc[itemCode];

        (transform.Find("FishName").GetComponent<TextMeshProUGUI>()).text = name;
        (transform.Find("Probability").GetComponent<TextMeshProUGUI>()).text = prob;
        (transform.Find("Power").GetComponent<TextMeshProUGUI>()).text = power;
        (transform.Find("Description").GetComponent<TextMeshProUGUI>()).text = desc;
        (transform.Find("GoldContainer").Find("GoldNum").GetComponent<TextMeshProUGUI>()).text = desc;

    }    
    
    public void OpenDlg(){
        gameObject.SetActive(true);
    }
   
    public void CloseDlg(){
        gameObject.SetActive(false);
    }

}
