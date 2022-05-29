using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectIncText : MonoBehaviour
{
    public GameObject[] fishes;
    public Text incrementText;
    private float totalPowerUp;
    private float totalProbUp;
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < fishes.Length; i++) {
            if(SaveCtrl.instance.myData.fish_collections[i]) {
                CollectFishData curFishData = fishes[i].GetComponent<CollectFishData>();
                totalPowerUp += curFishData.powerUp;
                totalProbUp += curFishData.probUp;
            }
        }

        incrementText.text = string.Format("총 파워 업 : +{0:F2}\n총 확률 업 : +{1:F0} %", totalPowerUp, totalProbUp * 100f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
