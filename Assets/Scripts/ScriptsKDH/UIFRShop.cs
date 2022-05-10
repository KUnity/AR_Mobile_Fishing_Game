using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class UIFRShop : MonoBehaviour
{
    public SpriteAtlas atlas;
    public GameObject contents;
    public GameObject uiListItemPrefab;
    public TMP_Text priceText;
    public Sprite soldout;
    public float iconWidth = 180;
    public float iconHeight = 180;

    //string[] itemNames = {
    //    "Shabby fishing rod",
    //    "Bamboo fishing rod",
    //    "Steal fishing rod",
    //    "bronze fishing rod",
    //    "Silver fishing rod",
    //    "Gold fishing rod",
    //    "Diamond fishing rod",
    //    "Carbon  fishing rod",
    //};

    //string[] itemDescription = {
    //    "It's a fishing rod thrown away by the shop uncle.",
    //    "It's a fishing rod made of Bamboo",
    //    "It's a fishing rod made of Steal",
    //    "It's a fishing rod made of bronze",
    //    "It's a fishing rod made of Silver",
    //    "It's a fishing rod made of Gold",
    //    "It's a fishing rod made of Diamond",
    //    "It's a fishing rod made of Carbon",
    //};

    //int[] itemPrices = {
    //    1,
    //    2,
    //    3,
    //    4,
    //    5,
    //    6,
    //    7,
    //    8,
    //};

    void Start()
    {
        for (int i = 0; i < FishingRob.fishingRobNum; i++)
        {
            var itemIcon = string.Format("HPBar_{0}", i+19);

            string iName = SaveCtrl.instance.fishingRobs[i].name;
            long iPrice = SaveCtrl.instance.fishingRobs[i].gold;
            string iDescription = SaveCtrl.instance.fishingRobs[i].desc;

            var sp = atlas.GetSprite(itemIcon);
            var go = Instantiate<GameObject>(this.uiListItemPrefab, contents.transform);
            var uiListItem = go.GetComponent<UIListItem>();
            uiListItem.Init(sp, iName, iDescription, iPrice);

            SaveCtrl.instance.LoadData();

            if (SaveCtrl.instance.myData.hasFishingRod[i] == false){
                uiListItem.btn.interactable = false;
                SaveCtrl.instance.SaveData();
                uiListItem.changeImage(soldout);
            }

            uiListItem.btn.onClick.AddListener(() =>
            {
                if (SaveCtrl.instance.myData.gold >= iPrice){
                    Debug.Log("보유 골드가 부족합니다.");

                }
                else{
                    SaveCtrl.instance.LoadData();
                    SaveCtrl.instance.myData.gold -= iPrice;
                    switch (i)
                    {
                        case 0:
                            SaveCtrl.instance.myData.hasFishingRod[0] = true;
                            //uiListItem.btn.interactable = !SaveCtrl.instance.myData.hasFishingRod[0];
                            
                            break;
                        case 1:
                            SaveCtrl.instance.myData.hasFishingRod[1] = true;
                            //uiListItem.btn.interactable = !SaveCtrl.instance.myData.hasFishingRod[1];
                            break;
                        case 2:
                            SaveCtrl.instance.myData.hasFishingRod[2] = true;
                            //uiListItem.btn.interactable = !SaveCtrl.instance.myData.hasFishingRod[2];
                            break;
                        case 3:
                            SaveCtrl.instance.myData.hasFishingRod[3] = true;
                            //uiListItem.btn.interactable = !SaveCtrl.instance.myData.hasFishingRod[3];
                            break;
                        case 4:
                            SaveCtrl.instance.myData.hasFishingRod[4] = true;
                            //uiListItem.btn.interactable = !SaveCtrl.instance.myData.hasFishingRod[4];
                            break;
                    }
                    uiListItem.btn.interactable = false;
                    uiListItem.changeImage(soldout);
                    priceText.text = SaveCtrl.instance.myData.gold.ToString() + " G";
                    SaveCtrl.instance.SaveData();

                }
            });
        }
        priceText.text = SaveCtrl.instance.myData.gold.ToString() + " G";
    }
}


