using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject data;
    [SerializeField] private GameObject subCam;
    private GameData gameData;
    private float waitedTime;
    private float battleTime = 30.0f;

    public GameObject castingBtn;
    public GameObject bobber;
    public GameObject biteSignal;
    public GameObject subCamRect;
    public GameObject timeRect;
    public Text timeText;
    
    public GameObject HookingBtn;
    // Start is called before the first frame update
    void Start()
    {
        gameData = data.GetComponent<GameData>();
        subCam.GetComponent<Transform>().position = new Vector3(0, -2.5f, 17.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(gameData.isCasted) {
            waitedTime += Time.deltaTime;
            if(waitedTime > 5.0f) {
                gameData.isBited = true;
            }
        }

        if(gameData.isBited) {
            bobber.GetComponent<Transform>().position = new Vector3(0, -3.5f, 20);
            biteSignal.SetActive(true);
            
            // 챔질 동작 우선 버튼 대체
            HookingBtn.SetActive(true);
            
        }

        if(gameData.isHooking) {
            subCamRect.SetActive(false);
            HookingBtn.SetActive(false);
            timeRect.SetActive(true);

            if (battleTime < 0) {
                //모든 값 초기화 후, 원래화면으로 돌아감
                gameData.isCasted = false;
                gameData.isBited = false;
                gameData.isHooking = false;
                waitedTime = 0;
                battleTime = 30.0f;

                castingBtn.SetActive(true);
                timeRect.SetActive(false);
            }

            timeText.text = string.Format("{0:D2} : {1:D2}", (int)battleTime, (int)((battleTime - (int)battleTime)*100*0.6f));
            battleTime -= Time.deltaTime;
        }
    }

    // 챔질 동작 우선 버튼 대체
    public void onClickHookBtn(){
        gameData.isHooking = true;
    }
}
