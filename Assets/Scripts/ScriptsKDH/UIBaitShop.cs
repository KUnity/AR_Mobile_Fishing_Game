using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

public class UIBaitShop: MonoBehaviour
{
    public SpriteAtlas atlas;
    public GameObject contents;
    public GameObject uiListItemPrefab;
    public TMP_Text priceText;
    public float iconWidth = 180;
    public float iconHeight = 180;

    //string[] itemNames = {
    //    "Shrimp",
    //    "Warm",
    //    "Dacaied fish",
    //    "Cabire",
    //    "Squid",
    //    "Pearl"
    //};

    //string[] itemDescription = {
    //    "It's a warm",
    //    "It's a Decaied fish",
    //    "It's a shrimps",
    //    "It's a Squid",
    //    "It's a Cabire",
    //    "It's a Pearl"
    //};

    //int[] itemPrices = {
    //    1,
    //    5,
    //    10,
    //    25,
    //    50,
    //    100
    //};

    void Start()
    {
        for (int i = 0; i < Bait.BaitNum; i++)
        {
            var itemIcon = string.Format("R2_{0}", i);
            string iName = SaveCtrl.instance.baits[i].name;
            long iPrice = SaveCtrl.instance.baits[i].gold;
            string iDescription = SaveCtrl.instance.baits[i].desc;
            var sp = atlas.GetSprite(itemIcon);
            var go = Instantiate<GameObject>(this.uiListItemPrefab, contents.transform);
            var uiListItem = go.GetComponent<UIListItem>();
            uiListItem.btn.onClick.AddListener(() =>
            {
                if (SaveCtrl.instance.myData.gold < iPrice)
                {
                    Debug.Log("보유 골드가 부족합니다.");
                }
                else
                {
                    SaveCtrl.instance.myData.gold -= iPrice;
                    priceText.text = SaveCtrl.instance.myData.gold.ToString() + " G";
                }
            });
            uiListItem.Init(sp, iName, iDescription, iPrice);
        }
        priceText.text = SaveCtrl.instance.myData.gold.ToString() + " G";
    }
}
