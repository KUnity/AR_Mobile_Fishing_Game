using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectFishData : MonoBehaviour
{
    [SerializeField] private Text fishNumText;
    [SerializeField] private Text fishNameText;
    [SerializeField] private Text powerUpText;
    [SerializeField] private Text probUpText;
    public int fishNum;
    public string fishName;
    public float powerUp;
    public float probUp;
    // Start is called before the first frame update
    void Start()
    {
        fishNumText.text = string.Format("NO. {0}", fishNum);
        fishNameText.text = fishName;
        powerUpText.text = string.Format("파워 업 : +{0:F1}", powerUp);
        probUpText.text = string.Format("확률 업 : +{0:F1}%", probUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
