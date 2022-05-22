using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject data;
    [SerializeField] private GameObject subCam;
    [SerializeField] private GameObject reelRect;
    [SerializeField] private GameObject tensionSlider;
    [SerializeField] private GameObject warningIMG;
    [SerializeField] private GameObject fishHPbar;
    [SerializeField] private GameObject reel;
    private GameData gameData;
    private GameAction gameAction;
    private float waitedTime;
    private float battleTime = 30.0f;
    private float warningTime;
    private float maxWarningTime = 3.0f;

    public int itemType;
    public int itemCode;

    public GameObject castingBtn;
    public GameObject bobber;
    public GameObject biteSignal;
    public GameObject subCamRect;
    public GameObject timeRect;
    public Text timeText;
    public GameObject HookingBtn;
    public GameObject menuSet;
    public GameObject EquipmentSet;


    public int stage;

    public float userTotalPercent; // 유저의 총 잡히는 물고기 확률업 수치


    // Start is called before the first frame update
    void Start()
    {
        gameData = data.GetComponent<GameData>();
        gameAction = FindObjectOfType<GameAction>();
        subCam.GetComponent<Transform>().position = new Vector3(0, -2.5f, 17.0f);
    }

    // Update is called once per frame
    void Update()
    {
        switch (stage){
            case -1:
                break;
            case 0: // 던지기 감지
                castingBtn.SetActive(false);
                gameData.isCasted = MotionBlur.CheckThrow();
                if(gameData.isCasted){
                    GameObject waterPlane = GameObject.Find("WaterPlane");
                    bobber.transform.position = waterPlane.transform.position;
                    subCamRect.SetActive(true);
                    data.GetComponent<GameData>().isCasted = true;
                    stage = 1;
                }
                break;
            case 1: // 던진 후
                    waitedTime += Time.deltaTime;
                    if(waitedTime > 5.0f) {
                        gameData.isBited = true;
                        stage = 2;

                        bobber.GetComponent<Transform>().position = new Vector3(0, -3.5f, 20);
                        biteSignal.SetActive(true);

                        // 챔질 동작 우선 버튼 대체
                        HookingBtn.SetActive(true);
                    }
                break;
            case 2: // 훅킹 감지
                gameData.isHooking = MotionBlur.CheckChamjil();
                if (gameData.isHooking){
                    // 잡힌 물고기 선택
                    float[] percents = null;
                    itemType = Random.Range(0, Fish.typeNum);
                    switch (itemType)
                    {
                        case 0: percents = NormalFish.probalilities; break;
                        case 1: percents = Shark.probalilities; break;
                    }

                    for (int i = percents.Length - 1; i >= 0; i--)
                    {
                        if (GetRandFlag(percents[i] * (1f + gameAction.userTotalPercentUp)))
                        {
                            itemCode = i;
                            break;
                        }
                    }
                    gameAction.fish = Fish.GetFish(itemType, itemCode);
                    gameAction.curFishHP = Fish.GetFish(itemType, itemCode).hp;
                    Debug.Log("HP : " + Fish.GetFish(itemType, itemCode).hp + " | " + itemType + " / " + itemCode);

                    stage = 3;
                }
                break;
            case 3: // 릴링
                if (gameAction.isCatch) return;
                subCamRect.SetActive(false);
                HookingBtn.SetActive(false);
                timeRect.SetActive(true);
                reelRect.SetActive(true);
                tensionSlider.SetActive(true);
                fishHPbar.SetActive(true);
                reel.SetActive(true);
                reel.transform.position = new Vector3(0.5244f,0.7086f,1.651f);
                gameAction.mainCirle.gameObject.SetActive(true);
                gameAction.pointCircle.gameObject.SetActive(true);

                if (tensionSlider.GetComponent<Slider>().value > 0.75f)
                {
                    warningIMG.SetActive(true);
                    warningTime += Time.deltaTime;
                    if (warningTime > maxWarningTime)
                    {
                        initAll();
                    }
                }
                else
                {
                    warningIMG.SetActive(false);
                    warningTime = 0;
                }

                if (battleTime < 0)
                {
                    initAll();
                }

                timeText.text = string.Format("{0:D2} : {1:D2}", (int)battleTime, (int)((battleTime - (int)battleTime) * 100 * 0.6f));
                battleTime -= Time.deltaTime;
                break;
        }



        // if(gameData.isCasted) {
        //     waitedTime += Time.deltaTime;
        //     if(waitedTime > 5.0f) {
        //         gameData.isBited = true;
        //     }
        // }

        // if(gameData.isBited) {
        //     bobber.GetComponent<Transform>().position = new Vector3(0, -3.5f, 20);
        //     biteSignal.SetActive(true);
            
        //     // 챔질 동작 우선 버튼 대체
        //     HookingBtn.SetActive(true);
            
        // }

        // if(gameData.isHooking) {
        //     subCamRect.SetActive(false);
        //     HookingBtn.SetActive(false);
        //     timeRect.SetActive(true);
        //     reelRect.SetActive(true);
        //     tensionSlider.SetActive(true);
        //     fishHPbar.SetActive(true);
        //     reel.SetActive(true);

        //     // 잡힌 물고기 선택
        //     float[] percents = null;
        //     itemType = Random.Range(0, Fish.fishNum);
        //     switch(itemType){
        //         case 0: percents = NormalFish.probalilities; break;
        //         case 1: percents = Shark.probalilities; break;
        //     }

        //     for (int i = percents.Length - 1; i >= 0; i--) {
        //         if (GetRandFlag(percents[i])){
        //             itemCode = i;
        //             break;
        //         }
        //     }
        //     FindObjectOfType<GameAction>().fish = Fish.GetFish(itemType, itemCode);

        //     if (tensionSlider.GetComponent<Slider>().value > 0.75f) {
        //         warningIMG.SetActive(true);
        //         warningTime += Time.deltaTime;
        //         if(warningTime > maxWarningTime) {
        //             initAll();
        //         }
        //     }
        //     else {
        //         warningIMG.SetActive(false);
        //         warningTime = 0;
        //     }

        //     if (battleTime < 0) {
        //         initAll();
        //     }

        //     timeText.text = string.Format("{0:D2} : {1:D2}", (int)battleTime, (int)((battleTime - (int)battleTime)*100*0.6f));
        //     battleTime -= Time.deltaTime;
        // }
    }

    //모든 값 초기화 후, 원래화면으로 돌아감
    public void initAll() {
        stage = -1;
        gameData.isCasted = false;
        gameData.isBited = false;
        gameData.isHooking = false;
        waitedTime = 0;
        battleTime = 30.0f;
        warningTime = 0;
        tensionSlider.GetComponent<Slider>().value = 0;

        biteSignal.SetActive(false);
        reel.SetActive(false);
        castingBtn.SetActive(true);
        timeRect.SetActive(false);
        reelRect.SetActive(false);
        menuSet.SetActive(true);
        EquipmentSet.SetActive(true);
        tensionSlider.SetActive(false);
        warningIMG.SetActive(false);
        fishHPbar.SetActive(false);
        gameAction.mainCirle.gameObject.SetActive(false);
        gameAction.pointCircle.gameObject.SetActive(false);
        bobber.GetComponent<Transform>().position = new Vector3(0, 2.0f, 1.0f);
    }

    public void SetUIGotFish()
    {
        tensionSlider.SetActive(false);
        warningIMG.SetActive(false);
        fishHPbar.SetActive(false);
        gameAction.mainCirle.gameObject.SetActive(false);
        gameAction.pointCircle.gameObject.SetActive(false);
    }

    // 챔질 동작 우선 버튼 대체
    public void onClickHookBtn(){
        gameData.isHooking = true;
    }

    static public bool GetRandFlag(float percent)
    {
        double data = Mathf.Round((float)((double)percent * 10000000f)) / 10000000f;

        if (data >= 1f)
            return true;
        else if (data <= 0f)
            return false;

        int count = 0;

        while(data != (int)data) // data가 소수인가?
        {
            data *= 10f;
            data = Mathf.Round((float)(data * 10000000f)) / 10000000f;
            count++;

            if (count > 6)
                break;
        }

        int criticalX = (int)data;
        int criticalY = (int)Mathf.Pow(10, count);

        int rand = UnityEngine.Random.Range(1, criticalY + 1);
        if (1 <= rand && rand <= criticalX)
            return true;
        else
            return false;
    }
}
