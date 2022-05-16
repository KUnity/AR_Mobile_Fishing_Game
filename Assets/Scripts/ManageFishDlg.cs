using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class ManageFishDlg : MonoBehaviour
{

    private int fishGold ;
    private GameObject clickedFish;
    private GameObject fishUI;
    public GameObject goldUI;
    public GameObject FishUIContainer;

    private GameObject[] normalFishUIs;

    
    // Start is called before the first frame update
    void Start()
    {
        normalFishUIs = Resources.LoadAll<GameObject>("Prefabs/Fishes/NormalFish/UI");
    }


    public void SetItemInfo(int itemType,int itemCode)
    {
        string name = "", info = "", prob = "", power = "", goldStr="";

        Fish fish = Fish.GetFish(itemType,itemCode);
        if(fish is NormalFish){
            // NormalFish normalFish = fish as NormalFish;
            name = NormalFish.names[itemCode];
            prob = "Prob : " + NormalFish.probalilities[itemCode].ToString();
            power = "Power : " + NormalFish.powers[itemCode].ToString();
            info = NormalFish.infos[itemCode];
            goldStr= NormalFish.golds[itemCode].ToString();
            fishGold = NormalFish.golds[itemCode];
        }else if( fish is Shark) {
            // Shark shark = fish as Shark;
            name = Shark.names[itemCode];
            prob = "Prob : " + Shark.probalilities[itemCode].ToString();
            power = "Power : " + Shark.powers[itemCode].ToString();
            info = Shark.infos[itemCode];
            goldStr = Shark.golds[itemCode].ToString();
            fishGold =  Shark.golds[itemCode];
        }

        transform.Find("FishName").GetComponent<Text>().text = name;
        transform.Find("Probability").GetComponent<Text>().text = prob;
        transform.Find("Power").GetComponent<Text>().text = power;
        transform.Find("Description").GetComponent<Text>().text = info;
        transform.Find("GoldContainer").Find("GoldNum").GetComponent<Text>().text = goldStr;

        // UI에 추가 
        fishUI=Instantiate(normalFishUIs[Fish.GetFishIndex(itemType,itemCode)]);
        fishUI.transform.SetParent(FishUIContainer.transform);
        fishUI.transform.localPosition = Vector3.zero;
    }    
    
    public void OpenDlg(){
        gameObject.SetActive(true);
    }
   
    public void CloseDlg(){
        gameObject.SetActive(false);
        Destroy(fishUI,.1f);
    }

    public void SellFish(){
        // 돈 추가 
        SaveCtrl.instance.myData.gold += fishGold;
        goldUI.GetComponent<Text>().text = SaveCtrl.instance.myData.gold.ToString();

        // 판 물고기 지워야함 
        int index = clickedFish.GetComponent<MoveFish>().GetItemIndexFromName();
        clickedFish.GetComponent<MoveFish>().RemoveFish();
        SaveCtrl.instance.myData.fishNums[index]-=1;

        // 데이터 저장
        SaveCtrl.instance.SaveData();
        CloseDlg();
    }

    public void GetClickedFish(GameObject obj){
        clickedFish = obj;
    }

}
