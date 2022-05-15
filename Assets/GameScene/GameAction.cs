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
    [SerializeField] GameObject gameSceneManager;
    [SerializeField] Slider fishHPbar;
    Animator reelAnimator;

    [SerializeField] private Canvas canvas;
    public RectTransform mainCirle;
    public RectTransform pointCircle;
    public float radius;

    public Vector2 currentPos;
    public Fish fish;
    public int curFishHP;


    // Start is called before the first frame update
    void Start()
    {
        radius = mainCirle.rect.width * 0.35f;
        mainCirle.gameObject.SetActive(false);
        pointCircle.gameObject.SetActive(false);

        reelAnimator = reel.GetComponent<Animator>();
        reelAnimator.StartPlayback();
        reel.GetComponent<Transform>().position = new Vector3(1f, -3f, -6f);
    }

    // Update is called once per frame
    void Update()
    {
        tensionSlider.value -= 0.1f * Time.deltaTime;

        fishHPbar.value = (float)curFishHP / (float)fish.hp;
    }

    public void OnPointerDown(PointerEventData eventData) {
        reelAnimator.StopPlayback();
    }

    public void OnDrag(PointerEventData eventData) {
        Vector2 zoomVec = (eventData.position - (Vector2)mainCirle.position) / canvas.transform.localScale.x;
        zoomVec = Vector2.ClampMagnitude(zoomVec * 100, radius);
        pointCircle.localPosition = zoomVec;

        if (Vector2.Distance(currentPos, zoomVec) >= radius)
        {
            tensionSlider.value += 0.05f;
            currentPos = zoomVec;
            
            // 물고기 HP 감소 동작
            if (FishingRob.power_datas[SaveCtrl.instance.myData.equipFishingRod] > fish.power) {
                curFishHP -= (int)FishingRob.power_datas[SaveCtrl.instance.myData.equipFishingRod];
            } else {
                curFishHP -= 1;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        reelAnimator.StartPlayback();
    }

    public void GetFish()
    {
        int fishIndex = Fish.GetFishIndex(fish);
        SaveCtrl.instance.myData.fishNums[fishIndex]++;
    }
}
