using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameAction : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] GameObject gameData;
    [SerializeField] GameObject reel;
    [SerializeField] Slider tensionSlider;
    [SerializeField] GameSceneManager gameSceneManager;
    [SerializeField] Slider fishHPbar;
    [SerializeField] Text userdata;
    [SerializeField] private Animator animator;
    Animator reelAnimator;

    [SerializeField] private Canvas canvas;
    public RectTransform mainCirle;
    public RectTransform pointCircle;
    public float radius;

    public Vector2 currentPos;
    public Fish fish;
    public GameObject[] fishObjects;
    public int curFishHP;
    public bool isCatch;

    public float userTotalPower; // 유저의 총 파워량
    public float userTotalPercentUp; // 유저의 총 확률업

    // Start is called before the first frame update
    void Start()
    {
        radius = mainCirle.rect.width * 0.4f;
        mainCirle.gameObject.SetActive(false);
        pointCircle.gameObject.SetActive(false);

        reelAnimator = reel.GetComponent<Animator>();
        reelAnimator.enabled = false;
        // reel.GetComponent<Transform>().position = new Vector3(1f, -3f, -6f);
        for (int i = 0; i < fishObjects.Length; i++)
            fishObjects[i].SetActive(false);

        // 총 데미지, 확률 계산
        userTotalPower = Bait.power_datas[SaveCtrl.instance.myData.equipBaits] + FishingRob.power_datas[SaveCtrl.instance.myData.equipFishingRod];
        userTotalPercentUp = Bait.probalility_datas[SaveCtrl.instance.myData.equipBaits] + FishingRob.probalility_datas[SaveCtrl.instance.myData.equipFishingRod];

        for(int i = 0; i < Fish.typeNum; i++) {
            for(int j = 0; j < Fish.totalNum / Fish.typeNum; j++) {
                if (SaveCtrl.instance.myData.fish_collections[i*5 + j])
                {
                    if(i==1 && j==4) break;
                    Fish fish = Fish.GetFish(i, j);
                    userTotalPower += fish.collection_powerup;
                    userTotalPercentUp += fish.collection_percentup;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        tensionSlider.value -= 0.15f * Time.deltaTime;
        userdata.text = string.Format("총 유저 파워\n{0:F2}\n\n총 유저 확률\n{1:F0} %", userTotalPower, userTotalPercentUp * 100f);
        Debug.Log(string.Format("user total power : {0:F2}", userTotalPower));
        Debug.Log(string.Format("user total percentUp : {0:F0} %", userTotalPercentUp * 100f));
        if (fish != null)
        {
            fishHPbar.value = (float)curFishHP / (float)fish.hp;
            if (!isCatch && curFishHP <= 0)
            {
                isCatch = true;
                GetFish();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        reelAnimator.enabled = true;
    }

    public void OnDrag(PointerEventData eventData) {
        if (isCatch) return;
        Vector2 zoomVec = (eventData.position - (Vector2)mainCirle.position) / canvas.transform.localScale.x;
        zoomVec = Vector2.ClampMagnitude(zoomVec * 100, radius);
        pointCircle.localPosition = zoomVec;

        if (Vector2.Distance(currentPos, zoomVec) >= radius) {
            tensionSlider.value += 0.03f;
            currentPos = zoomVec;
            Debug.Log(curFishHP);
            
            // ����� HP ���� ����
            if (userTotalPower - fish.power > 1) {
                curFishHP -= (int)(userTotalPower - fish.power);
            } else {
                curFishHP -= 1;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        reelAnimator.enabled = false;
    }

    public void GetFish()
    {
        int fishIndex = Fish.GetFishIndex(fish);
        StartCoroutine(SetInit());
        SaveCtrl.instance.myData.fishNums[fishIndex]++;
        SaveCtrl.instance.myData.fish_collections[fishIndex] = true;
        fishObjects[fishIndex].SetActive(true);
        SaveCtrl.instance.SaveData();
        animator.SetInteger("type", 3);

        Debug.Log(SaveCtrl.instance.myData.fishNums[fishIndex]);
    }

    IEnumerator SetInit()
    {
        gameSceneManager.SetUIGotFish();
        yield return new WaitForSeconds(3f);
        gameSceneManager.initAll();
        for (int i = 0; i < fishObjects.Length; i++)
            fishObjects[i].SetActive(false);
        isCatch = false;
        pointCircle.localPosition = Vector2.zero;
        animator.SetInteger("type", 0);
    }
}
