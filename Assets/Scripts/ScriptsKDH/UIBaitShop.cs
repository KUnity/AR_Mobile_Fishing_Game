using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UIBaitShop: MonoBehaviour
{
    public SpriteAtlas atlas;
    public GameObject contents;
    public GameObject uiListItemPrefab;
    public float iconWidth = 180;
    public float iconHeight = 180;

    string[] itemNames = {
        "Shrimp",
        "Warm",
        "Dacaied fish",
        "Cabire",
        "Squid",
        "Pearl"
    };

    string[] itemDescription = {
        "It's a warm",
        "It's a Decaied fish",
        "It's a shrimps",
        "It's a Squid",
        "It's a Cabire",
        "It's a Pearl"
    };

    int[] itemPrices = {
        1,
        5,
        10,
        25,
        50,
        100
    };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            var itemIcon = string.Format("R2_{0}", i);
            var iName = this.itemNames[i];
            var iPrice = this.itemPrices[i];
            var iDescription = this.itemDescription[i];
            var sp = atlas.GetSprite(itemIcon);
            var go = Instantiate<GameObject>(this.uiListItemPrefab, contents.transform);
            var uiListItem = go.GetComponent<UIListItem>();
            uiListItem.btn.onClick.AddListener(() =>
            {
                Debug.Log(iName.ToString() + " Buy Button Clicked");
            });
            uiListItem.Init(sp, iName, iDescription, iPrice);
        }   
    }
}
