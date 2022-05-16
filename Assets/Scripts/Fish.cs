using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
    public static int typeNum = 2;
    public static int totalNum = 10;
    public static int[] fishNumAsType = { 5, 5 };

    public int itemCode;
    public int hp; // hp
    public float power; // 공격력
    public float probability; // 찌를 물 확률
    public int gold; // 상점 판매 금액
    public int quality; // 0 = 노멀, 1 = 레어, 2 = 에픽, 3 = 유니크, 4 = 레전드리
    public float weight; // 무게 (도감 용)
    public float width; // (크기 - 폭)
    public float height; // (크기 - 높이)
    public string name;
    public string info;

    /// <summary>
    /// 인자에 해당하는 Fish Class를 반환하는 함수 (실패 시, NULL 반환)
    /// </summary>
    /// <param name="_itemType">물고기 종류</param>
    /// <param name="_itemCode">물고기 코드</param>
    /// <returns></returns>
    static public Fish GetFish(int _itemType, int _itemCode)
    {
        Fish fish = null;

        switch (_itemType)
        {
            case 0: fish = SaveCtrl.instance.normalFish[_itemCode]; break;
            case 1: fish = SaveCtrl.instance.sharks[_itemCode]; break;
        }

        return fish;
    }

    /// <summary>
    /// Fish에 해당하는 ItemType을 반환하는 함수 (실패 시, -1 반환)
    /// </summary>
    /// <param name="fish">물고기</param>
    /// <returns></returns>
    static public int GetItemType(Fish fish)
    {
        int type = -1;

        if (fish is NormalFish) type = 0;
        else if (fish is Shark) type = 1;

        return type;
    }

    /// <summary>
    /// Type & Code를 통해 FishIndex를 구하는 함수
    /// </summary>
    /// <param name="_itemType">물고기 종류</param>
    /// <param name="_itemCode">물고기 코드</param>
    /// <returns></returns>
    static public int GetFishIndex(int _itemType, int _itemCode)
    {
        int index = 0;
        for (int i = 0; i < _itemType; i++)
            index += fishNumAsType[i];
        index += _itemCode;

        return index;
    }

    /// <summary>
    /// Fish를 통해 FishIndex를 구하는 함수
    /// </summary>
    /// <param name="fish">물고기</param>
    /// <returns></returns>
    static public int GetFishIndex(Fish fish)
    {
        int _itemType = GetItemType(fish);
        int _itemCode = fish.itemCode;

        return GetFishIndex(_itemType, _itemCode);
    }
};



public class Shark : Fish
{
    public static int[] hps = { 100, 250, 500, 1000, 2000 };
    public static float[] powers = { 10f, 25f, 50f, 100f, 200f };
    public static float[] probalilities = { 1f, 0.5f, 0.3f, 0.1f, 0.01f };
    public static int[] golds = { 0, 0, 0, 0, 0 };
    public static int[] qualities = { 2, 2, 3, 3, 4 };
    public static float[] weights = { 0f, 0f, 0f, 0f, 0f };
    public static float[] widths = { 0f, 0f, 0f, 0f, 0f };
    public static float[] heights = { 0f, 0f, 0f, 0f, 0f };
    public static string[] names = { "청상어", "적상어", "백상어", "흑상어", "금상어"};
    public static string[] infos =
    {
        "푸른 빛을 띄는 상어이다.",
        "붉은 빛을 띄는 상어이다.",
        "밝은 빛을 띄는 상어이다.",
        "검은 빛을 띄는 상어이다.",
        "금 빛을 띄는 상어이다."
    };
    public static int totalNum = 5;

    public Shark(int _itemCode)
    {
        itemCode = _itemCode;
        hp = hps[itemCode];
        power = powers[itemCode];
        probability = probalilities[itemCode];
        gold = golds[itemCode];
        quality = qualities[itemCode];
        weight = weights[itemCode];
        width = widths[itemCode];
        height = heights[itemCode];
        name = names[itemCode];
        info = infos[itemCode];
    }
};

public class NormalFish : Fish
{
    public static int[] hps = { 50, 100, 250, 500, 1000 };
    public static float[] powers = { 5f, 10f, 25f, 50f, 100f };
    public static float[] probalilities = { 1f, 0.5f, 0.3f, 0.1f, 0.01f };
    public static int[] golds = { 0, 2, 0, 0, 0 };
    public static int[] qualities = { 0, 0, 0, 1, 2 };
    public static float[] weights = { 0f, 0f, 0f, 0f, 0f };
    public static float[] widths = { 0f, 0f, 0f, 0f, 0f };
    public static float[] heights = { 0f, 0f, 0f, 0f, 0f };
    public static string[] names = { "농어", "돗돔", "돌돔", "참돔", "자바리" };
    public static string[] infos =
    {
        "농어목 농엇과에 속하는 어류의 일종이다. 여름이 별미다.",
        "어목 투어바리과에 속하는 어류의 일종이다. 대형 물고기이다.",
        "검정우럭목 돌돔과에 속하는 속하는 어류의 일종이다. 식용이나 낚시 대상으로 인기가 많다.",
        "농어목 도미과에 속하는 어류의 일종이다. 매운탕으로 최고다.",
        "농어목 바리과에 속하는 어류의 일종이다. 고급 식재로 취급되어 인기가 좋다."
    };
    public static int totalNum = 5;

    public NormalFish(int _itemCode)
    {
        itemCode = _itemCode;
        hp = hps[itemCode];
        power = powers[itemCode];
        probability = probalilities[itemCode];
        gold = golds[itemCode];
        quality = qualities[itemCode];
        weight = weights[itemCode];
        width = widths[itemCode];
        height = heights[itemCode];
        name = names[itemCode];
        info = infos[itemCode];
    }
};