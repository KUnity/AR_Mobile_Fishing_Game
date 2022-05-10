using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIFRShop : MonoBehaviour
{
    public SpriteAtlas atlas;
    public GameObject contents;
    public GameObject uiListItemPrefab;
    public TMP_Text priceText;
    public Sprite soldout;

    void Start()
    {
        Debug.Log("Shop Start() Starts");
        for (int i = 0; i < FishingRob.fishingRobNum; i++)
        {
            int temp = i;
            var itemIcon = string.Format("HPBar_{0}", i + 19);
            string iName = SaveCtrl.instance.fishingRobs[i].name;
            long iPrice = SaveCtrl.instance.fishingRobs[i].gold;
            string iDescription = SaveCtrl.instance.fishingRobs[i].desc;

            var sp = atlas.GetSprite(itemIcon);
            var go = Instantiate<GameObject>(this.uiListItemPrefab, contents.transform);
            var uiListItem = go.GetComponent<UIListItem>();
            uiListItem.Init(sp, iName, iDescription, iPrice);
            uiListItem.btn.onClick.AddListener(() =>
            {
                if (SaveCtrl.instance.myData.gold < iPrice){
                    Debug.Log("보유 골드가 부족합니다.");
                }
                else{
                    Debug.Log(temp + " temp");
                    SaveCtrl.instance.myData.gold -= iPrice;
                    if(SaveCtrl.instance.myData.hasFishingRod[temp] == false)
                    {
                        SaveCtrl.instance.myData.hasFishingRod[temp] = true;
                    }
                    uiListItem.btn.interactable = false;
                    uiListItem.changeImage(soldout);
                    priceText.text = SaveCtrl.instance.myData.gold.ToString() + " G";
                }
                SaveCtrl.instance.SaveData();
            });
            if (SaveCtrl.instance.myData.hasFishingRod[i] == true)
            {
                uiListItem.btn.interactable = false;
                uiListItem.changeImage(soldout);
            }
        }
        priceText.text = SaveCtrl.instance.myData.gold.ToString() + " G";
    }
}


