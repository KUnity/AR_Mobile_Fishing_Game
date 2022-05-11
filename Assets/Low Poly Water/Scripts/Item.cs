using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemCode; // ?„ì´??ì½”ë“œ
    public string name; // ?´ë¦„
    public string desc;//°­µ¿Çö Ãß°¡, ¾ÆÀÌÅÛ ¼³¸í
    public long gold; // ê°€ê²?
}

public class FishingRob : Item
{
    public static string[] robNames = {
        "Shabby fishing rod",
        "Bamboo fishing rod",
        "Steal fishing rod",
        "bronze fishing rod",
        "Silver fishing rod", 
    };
    public static string[] robDesc = {
        "It's a fishing rod thrown away by the shop uncle.",
        "It's a fishing rod made of Bamboo",
        "It's a fishing rod made of Steal",
        "It's a fishing rod made of bronze",
        "It's a fishing rod made of Silver",
    };
    public static long[] gold_datas = { 1, 2, 3, 4, 5 };
    private static float[] probalility_datas = { 0.1f, 0.2f, 0.3f, 0.5f, 0.7f };
    private static float[] power_datas = { 1f, 2f, 3f, 5f, 7f };
    public static int fishingRobNum = 5; // ê²Œì„ ??ì¡´ì¬?˜ëŠ” ?šì‹œ?€??ê°œìˆ˜

    public float probability; // ?šì‹œ ?•ë¥ 
    public float power; // ê°•ë„

    public FishingRob(int _itemCode)
    {
        itemCode = _itemCode;
        name = robNames[itemCode];
        desc = robDesc[itemCode];
        gold = gold_datas[itemCode];
        probability = probalility_datas[itemCode];
        power = power_datas[itemCode];
    }
};

public class Bait : Item
{
    public static string[] baitNames = {
        "Shrimp",
        "Warm",
        "Dacaied fish",
        "Cabire",
        "Squid",
    };
    public static string[] baitDesc = {
        "It's a warm",
        "It's a Decaied fish",
        "It's a shrimps",
        "It's a Squid",
        "It's a Cabire",
    };
    public static long[] gold_datas = { 1, 2, 3, 4, 5 };
    private static float[] probalility_datas = { 0.1f, 0.2f, 0.3f, 0.5f, 0.7f };
    private static float[] power_datas = { 1f, 2f, 3f, 5f, 7f };
    public static int BaitNum = 5; // ê²Œì„ ??ì¡´ì¬?˜ëŠ” ë¯¸ë¼??ê°œìˆ˜

    public float probability; // ?šì‹œ ?•ë¥ 
    public float power; // ê°•ë„

    public Bait(int _itemCode)
    {
        itemCode = _itemCode;
        name = baitNames[itemCode];
        desc = baitDesc[itemCode];
        gold = gold_datas[itemCode];
        probability = probalility_datas[itemCode];
        power = power_datas[itemCode];
    }
};
