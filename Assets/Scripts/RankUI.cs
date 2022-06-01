using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankUI : MonoBehaviour
{
    [SerializeField] private GameObject rankContent;
    [SerializeField] private GameObject topRankObject;
    [SerializeField] private GameObject normalRankObject;
    [SerializeField] private GameObject myRankObject;

    public GameObject audioManagerObj;
    private AudioManager audioManager;

    private bool isOnOff;

    // Temp Variable
    UIBox[] uIBoxs;
    UIBox uIBox;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = audioManagerObj.GetComponent<AudioManager>();
        rankContent.SetActive(false);
    }

    public void OnOffRankUI()
    {
        audioManager.ClickBtn();
        isOnOff = !isOnOff;
        rankContent.SetActive(isOnOff);
        if (isOnOff)
        {
            SetContent();
        }
    }

    private void SetContent()
    {
        // TopRank Setting
        List<UserData> userDatas = SaveCtrl.instance.userDatas;
        Debug.Log("랭킹 수 : " + userDatas.Count);

        uIBoxs = topRankObject.GetComponentsInChildren<UIBox>();
        for (int i = 0; i < uIBoxs.Length; i++)
        {
            if (i < userDatas.Count)
            {
                uIBoxs[i].texts[0].text = userDatas[i].ID;
                uIBoxs[i].texts[1].text = userDatas[i].rank_score + " 점";
            }
            else
            {
                uIBoxs[i].gameObject.SetActive(false);
            }
        }

        // NormalRank Setting
        uIBoxs = normalRankObject.GetComponentsInChildren<UIBox>();
        for (int i = 0; i < uIBoxs.Length; i++)
        {
            if (i < userDatas.Count - 3)
            {
                uIBoxs[i].texts[0].text = userDatas[i + 3].rank + "등";
                uIBoxs[i].texts[1].text = userDatas[i + 3].ID;
                uIBoxs[i].texts[2].text = userDatas[i + 3].rank_score + " 점";
            }
            else
            {
                uIBoxs[i].gameObject.SetActive(false);
            }
        }

        // My Rank Setting
        uIBox = myRankObject.GetComponentInChildren<UIBox>();
        uIBox.texts[0].text = SaveCtrl.instance.myData.rank + "등";
        uIBox.texts[1].text = "(내 정보) " + SaveCtrl.instance.myData.ID;
        uIBox.texts[2].text = SaveCtrl.instance.myData.rank_score + " 점";
    }
}
