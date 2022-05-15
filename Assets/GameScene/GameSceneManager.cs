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
    
    
    public int stage;


    // Start is called before the first frame update
    void Start()
    {
        gameData = data.GetComponent<GameData>();
        subCam.GetComponent<Transform>().position = new Vector3(0, -2.5f, 17.0f);
    }

    // Update is called once per frame
    void Update()
    {
        switch(stage){
            case 0: // 던지기 감지
                gameData.isCasted = MotionBlur.CheckThrow();
                if(gameData.isCasted)
                    stage = 1;
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
                if(gameData.isHooking){
                    stage = 3;

                     // 잡힌 물고기 선택
                    float[] percents = null;
                    itemType = Random.Range(0, Fish.fishNum);
                    switch (itemType)
                    {
                        case 0: percents = NormalFish.probalilities; break;
                        case 1: percents = Shark.probalilities; break;
                    }

                    for (int i = percents.Length - 1; i >= 0; i--)
                    {
                        if (GetRandFlag(percents[i]))
                        {
                            itemCode = i;
                            break;
                        }
                    }
                    FindObjectOfType<GameAction>().fish = Fish.GetFish(itemType, itemCode);
                }
                break;
            case 3: // 롤링
                subCamRect.SetActive(false);
                HookingBtn.SetActive(false);
                timeRect.SetActive(true);
                reelRect.SetActive(true);
                tensionSlider.SetActive(true);
                fishHPbar.SetActive(true);
                reel.SetActive(true);

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
    void initAll() {
        stage = 0;
        gameData.isCasted = false;
        gameData.isBited = false;
        gameData.isHooking = false;
        waitedTime = 0;
        battleTime = 30.0f;
        warningTime = 0;
        tensionSlider.GetComponent<Slider>().value = 0;

        reel.SetActive(false);
        castingBtn.SetActive(true);
        timeRect.SetActive(false);
        reelRect.SetActive(false);
        tensionSlider.SetActive(false);
        warningIMG.SetActive(false);
        fishHPbar.SetActive(false);
        bobber.GetComponent<Transform>().position = new Vector3(0, 2.0f, 1.0f);
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
