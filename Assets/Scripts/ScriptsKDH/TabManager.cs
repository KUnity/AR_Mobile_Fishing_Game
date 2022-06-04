using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabManager: MonoBehaviour
{
    public GameObject[] Tab;
    public Image[] TabBtnIamge;
    public Sprite[] IdleSprite, SelectSprite;
    public GameObject audioManagerObj;
    AudioManager audioManager;

    void Awake()
    {
        audioManager = audioManagerObj.GetComponent<AudioManager>();
    }
    private void Start() => TabClick(0);
      
    public void TabClick(int n)
    {
        audioManager.ClickBtn();
        for(int i = 0; i <Tab.Length; i++)
        {
            if(i == n)
            {
                TabBtnIamge[i].color = new Color(1f, 1f, 1f, 1f);
                Tab[i].SetActive(true);
            }
            else
            {
                TabBtnIamge[i].color = new Color(0f, 0f, 0f, 0.5f);
                Tab[i].SetActive(false);
            }
            TabBtnIamge[i].sprite = i == n ? SelectSprite[i] : IdleSprite[i];
        }
    }

}
