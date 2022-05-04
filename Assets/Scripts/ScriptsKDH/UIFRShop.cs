using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class UIFRShop : MonoBehaviour
{
    public SpriteAtlas atlas;
    public GameObject contents;
    public GameObject uiListItemPrefab;
    public Sprite soldout;
    public float iconWidth = 180;
    public float iconHeight = 180;

    string[] itemNames = {
        "bronze fishing rod",
        "silver fishing rod",
        "gold fishing rod",
        "A",
        "B",
        "C",
        "D",
        "E"
    };

    string[] itemDescription = {
        "Bronze",
        "Silver",
        "Gold",
        "A",
        "B",
        "C",
        "D",
        "E",
    };

    int[] itemPrices = {
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8
    };

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            var itemIcon = string.Format("HPBar_{0}", i+19);
            var iName = this.itemNames[i];
            var iPrice = this.itemPrices[i];
            var iDescription = this.itemDescription[i];
            var sp = atlas.GetSprite(itemIcon);
            var go = Instantiate<GameObject>(this.uiListItemPrefab, contents.transform);
            var uiListItem = go.GetComponent<UIListItem>();
            uiListItem.btn.onClick.AddListener(() =>
            {
                Debug.Log(iName.ToString() + " Buy Button Clicked");
                uiListItem.changeImage(soldout);
            });
            uiListItem.Init(sp, iName, iDescription, iPrice);
        }   
    }
}
