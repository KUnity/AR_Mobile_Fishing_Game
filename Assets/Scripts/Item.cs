using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int itemCode; // 아이템 코드
    public string name; // 이름
    public int gold; // 가격
}

public class FishingRob : Item
{
    private static int[] gold_datas = { 1000, 10000, 50000, 100000 };
    private static float[] probalility_datas = { 0.1f, 0.2f, 0.3f, 0.5f };
    private static float[] power_datas = { 1f, 2f, 3f, 5f };
    public static int fishingRobNum = 4; // 게임 내 존재하는 낚시대의 개수

    public float probability; // 낚시 확률
    public float power; // 강도

    public FishingRob(int _itemCode)
    {
        itemCode = _itemCode;
        gold = gold_datas[itemCode];
        probability = probalility_datas[itemCode];
        power = power_datas[itemCode];
    }
};

public class Bait : Item
{
    private static int[] gold_datas = { 100, 1000, 5000, 10000 };
    private static float[] probalility_datas = { 0.1f, 0.2f, 0.3f, 0.5f };
    private static float[] power_datas = { 1f, 2f, 3f, 5f };
    public static int BaitNum = 4; // 게임 내 존재하는 미끼의 개수

    public float probability; // 낚시 확률
    public float power; // 강도

    public Bait(int _itemCode)
    {
        itemCode = _itemCode;
        gold = gold_datas[itemCode];
        probability = probalility_datas[itemCode];
        power = power_datas[itemCode];
    }
};
