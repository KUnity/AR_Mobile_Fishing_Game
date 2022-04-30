using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int gold; // 가격
    private float probability; // 낚시 확률
    private float power; // 강도
    private int type; // 1 : 낚싯대,  2: 미끼 
    private string name; // 이름 
    private int count; // 개수 
    private bool isUsing; // 현재 사용중인지 
    private int useType; // 1: 일회성 2: 다회성 

    public Item(int gold, float probability, float power, int type, string name, int count, bool isUsing,int useType){
        this.gold = gold;
        this.probability = probability;
        this.power = power;
        this.type = type;
        this.name = name; 
        this.count = count;
        this.isUsing = isUsing;
        this.useType = useType;
    }

    public void save(){
        // 데이터 저장하는 구문
    }

    public void useItem(){
        count--;
        // 사용자의 확률, power 높이기 

    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

  
}
