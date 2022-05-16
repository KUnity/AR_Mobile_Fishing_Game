using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using TMPro;

public class equipScript : MonoBehaviour
{
    public SpriteAtlas atlasFishingRod;
    public SpriteAtlas atlasBait;
    public Image imageFishingRod;
    public Image imageBait;
    public TMP_Text baitNumber;

    void Start()
    {
        switch (SaveCtrl.instance.myData.equipBaits)
        {
            case -1:
                imageBait.sprite = atlasBait.GetSprite("None");
                break;
            case 0:
                imageBait.sprite = atlasBait.GetSprite("bait_1_0");
                break;
            case 1:
                imageBait.sprite = atlasBait.GetSprite("bait_2_0");
                break;
            case 2:
                imageBait.sprite = atlasBait.GetSprite("bait_3_0");
                break;
            case 3:
                imageBait.sprite = atlasBait.GetSprite("bait_4_0");
                break;
            case 4:
                imageBait.sprite = atlasBait.GetSprite("bait_5_0");
                break;
            case 5:
                imageBait.sprite = atlasBait.GetSprite("bait_6_0");
                break;
            case 6:
                imageBait.sprite = atlasBait.GetSprite("bait_7_0");
                break;
        }
        switch (SaveCtrl.instance.myData.equipFishingRod)
        {
            case -1:
                imageFishingRod.sprite = atlasFishingRod.GetSprite("None");
                break;
            case 0:
                imageFishingRod.sprite = atlasFishingRod.GetSprite("fishignrod_1_0");
                break;
            case 1:
                imageFishingRod.sprite = atlasFishingRod.GetSprite("fishignrod_2_0");
                break;
            case 2:
                imageFishingRod.sprite = atlasFishingRod.GetSprite("fishignrod_3_0");
                break;
            case 3:
                imageFishingRod.sprite = atlasFishingRod.GetSprite("fishignrod_4_0");
                break;
            case 4:
                imageFishingRod.sprite = atlasFishingRod.GetSprite("fishignrod_5_0");
                break;
        }
    }
}
