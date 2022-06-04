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
    public GameObject NOMONEY;
   
    public GameObject audioManagerObj;
    private AudioManager audioManager; 


    void Start()
    {   
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        for (int i = 0; i < Bait.BaitNum; i++)
        {
            int temp = i;
            var itemIcon = string.Format("bait_{0}_0", i+1);
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
                    audioManager.NoCoin();
                    CancelInvoke("noMoney");
                    NOMONEY.SetActive(true);
                    Debug.Log("골드가 부족합니다.");
                    Invoke("noMoney", 1.4f);

                }
                else
                {
                    audioManager.Coin();
                    SaveCtrl.instance.myData.gold -= iPrice;
                    SaveCtrl.instance.myData.fishBaits[temp] += 1;
                    Debug.Log(SaveCtrl.instance.myData.fishBaits[temp] + temp.ToString());
                    SaveCtrl.instance.SaveData();
                    priceText.text = SaveCtrl.instance.myData.gold.ToString() + " G";
                }
            });
            uiListItem.Init(sp, iName, iDescription, iPrice);
        }
        priceText.text = SaveCtrl.instance.myData.gold.ToString() + " G";
    }
    public void noMoney()
    {
        NOMONEY.SetActive(false);
    }
}
