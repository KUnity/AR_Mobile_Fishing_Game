using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
    protected int itemCode;
    protected float power; // 공격력
    protected float probability; // 찌를 물 확률
    protected int gold; // 상점 판매 금액
    protected int quality; // 0 = 노멀, 1 = 레어, 2 = 에픽, 3 = 유니크, 4 = 레전드리
    protected float weight; // 무게 (도감 용)
    protected float width; // (크기 - 폭)
    protected float height; // (크기 - 높이)
    protected string name;
    protected string info;
};



public class Shark : Fish
{
    public static float[] powers = { };
    public static float[] probalilities = { };
    public static int[] golds = { };
    public static int[] qualities = { 2, 2, 3, 3, 4 };
    public static float[] weights = { };
    public static float[] widths = { };
    public static float[] heights = { };
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
    public static float[] powers = { };
    public static float[] probalilities = { };
    public static int[] golds = { };
    public static int[] qualities = { 0, 0, 0, 1, 2 };
    public static float[] weights = { };
    public static float[] widths = { };
    public static float[] heights = { };
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