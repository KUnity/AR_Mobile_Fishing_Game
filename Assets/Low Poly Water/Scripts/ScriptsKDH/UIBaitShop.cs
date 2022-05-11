using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;
using UnityEngine.Sprites;
using TMPro;

public class UIBaitShop: MonoBehaviour
{
    public SpriteAtlas atlas;
    public GameObject contents;
    public GameObject uiListItemPrefab;
    public TMP_Text priceText;

    void Start()
    {
        for (int i = 0; i < Bait.BaitNum; i++)
        {
            var itemIcon = string.Format("HPBar_{0}", i);
            string iName = SaveCtrl.instance.baits[i].name;
            long iPrice = SaveCtrl.instance.baits[i].gold;
            string iDescription = SaveCtrl.instance.baits[i].desc;
            Sprite sp = atlas.GetSprite(itemIcon);
            GameObject go = Instantiate<GameObject>(this.uiListItemPrefab, contents.transform);
            UIListItem uiListItem = go.GetComponent<UIListItem>();
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
