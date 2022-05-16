using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class ManageFishDlg : MonoBehaviour
{

    private int fishGold ;
    // Start is called before the first frame update
    void Start()
    {
        // 사용자가 가지고 있는 물고기 배치 
        // (transform.Find("DismissBtn").gameObject as Button).onClick.AddListener(CloseDlg);
    }


    public void SetItemInfo(int itemType,int itemCode)
    {
        string name = "", info = "", prob = "", power = "", gold="";

        Fish fish = Fish.GetFish(itemType,itemCode);
        if(fish is NormalFish){
            // NormalFish normalFish = fish as NormalFish;
            name = NormalFish.names[itemCode];
            prob = "Prob : " + NormalFish.probalilities[itemCode].ToString();
            power = "Power : " + NormalFish.powers[itemCode].ToString();
            info = NormalFish.infos[itemCode];
            gold= NormalFish.golds[itemCode].ToString();
        }else if( fish is Shark) {
            // Shark shark = fish as Shark;
            name = Shark.names[itemCode];
            prob = "Prob : " + Shark.probalilities[itemCode].ToString();
            power = "Power : " + Shark.powers[itemCode].ToString();
            info = Shark.infos[itemCode];

            fishGold =  Shark.golds[itemCode];
        }

        transform.Find("FishName").GetComponent<Text>().text = name;
        transform.Find("Probability").GetComponent<Text>().text = prob;
        transform.Find("Power").GetComponent<Text>().text = power;
        transform.Find("Description").GetComponent<Text>().text = info;
        transform.Find("GoldContainer").Find("GoldNum").GetComponent<Text>().text = gold.ToString();

    }    
    
    public void OpenDlg(){
        gameObject.SetActive(true);
    }
   
    public void CloseDlg(){
        gameObject.SetActive(false);
    }

    public void SellFish(int gold){
        // 돈 추가 
        SaveCtrl.instance.myData.gold += gold;

        // 판 물고기 지워야함 
        // 데이터 저장
        SaveCtrl.instance.SaveData();
    }

}
