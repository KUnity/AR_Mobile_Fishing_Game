using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Numerics;

public class CollectIncText : MonoBehaviour
{
    public GameObject[] fishes;
    public Text incrementText;
    public float totalPowerUp;
    public float totalProbUp;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < fishes.Length-2; i++) {
            if(SaveCtrl.instance.myData.fish_collections[i]) {
                CollectFishData curFishData = fishes[i].GetComponent<CollectFishData>();
                totalPowerUp += curFishData.powerUp;
                totalProbUp += curFishData.probUp;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        incrementText.text = string.Format("총 파워 업 : +{0:F2}\n총  확률 업 : +{0:F0}%", totalPowerUp, totalProbUp * 100f);
    }
}
