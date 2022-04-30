using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private float power;
    private float probability;
    private int gold ; 
    private float weight;
    private float width;
    private float height;
    private string name;
    private string rate;
    private string type;

    public Fish(float power, float probability, int gold, float weight, float width,float height, string name, string rate, string type){
        this.power=power;
        this.probability=probability;
        this.gold = gold;
        this.weight=weight;
        this.width =width;
        this.height = height;
        this.name = name;
        this.rate = rate;
        this.type = type;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
