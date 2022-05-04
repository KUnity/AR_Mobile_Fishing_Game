using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIListItem : MonoBehaviour
{
    public Image icon;
    public TMP_Text itemName;
    public TMP_Text itemDescription;
    public TMP_Text price;
    public Button btn;

    public void Init(Sprite sp, string itemName, string itemDescription, int price)
    {
        icon.sprite = sp;
        this.itemName.text = itemName;
        this.itemDescription.text = itemDescription;
        this.price.text = price.ToString();
    }
    public void changeImage(Sprite sp)
    {
        icon.sprite = sp;
    }
}
