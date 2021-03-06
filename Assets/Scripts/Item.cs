using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemCode; // ?�이??코드
    public string name; // ?�름
    public string desc;//������ �߰�, ������ ����
    public long gold; // 가�?
}

public class FishingRob : Item
{
    public static string[] robNames = {
        "Woody fishing rod",
        "Carbon fishing rod",
        "Bronze fishing rod",
        "Silver fishing rod",
        "Gold fishing rod", 
    };
    public static string[] robDesc = {
        "It's a Wood fishing rod thrown away by the shop uncle.",
        "It's a fishing rod made of Carbon. So hard but, Slightly heavy",
        "It's a fishing rod made of Bronze. So light but, not Strong",
        "It's a fishing rod made of Silver. So hard and light weighted",
        "Gold Fishing rod can makes you catch the legendary fish",
    };
    public static long[] gold_datas = { 1, 2, 3, 4, 5 };
    public static float[] probalility_datas = { 0.1f, 0.2f, 0.5f, 1f, 2f };
    public static float[] power_datas = { 20f, 50f, 100f, 250f, 500f };
    public static int fishingRobNum = 5; // 게임 ??존재?�는 ?�시?�??개수

    public float probability; // ?�시 ?�률
    public float power; // 강도

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
        "Paste Bait",
        "Warm",
        "Burger",
        "Shrimp",
        "Cabire",
        "Pearl",
        "Zade",
    };
    public static string[] baitDesc = {
        "It's a Paste Bait sold at the nearby supermarket.",
        "It's a fresh Earthworm",
        "Hamburger from McDonuld's.",
        "It's a Shrimp. favorite of Tuna, Whale",
        "A Cabire. Eating it rather than using it as bait.",
        "pearl of my grandmother's pearl necklace",
        "Zade",
    };
    public static long[] gold_datas = { 1, 2, 3, 4, 5, 6, 7 };
    public static float[] probalility_datas = { 0.1f, 0.25f, 0.5f, 0.7f, 1f, 1.5f, 2f };
    public static float[] power_datas = { 5f, 10f, 15f, 20f, 25f, 30f, 50f };
    public static int BaitNum = 7; // 게임 ??존재?�는 미끼??개수

    public float probability; // ?�시 ?�률
    public float power; // 강도

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
