using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using LitJson;
using System.Text;

/// <summary> User Data 저장 Class </summary>
public class UserData
{
    /// <summary> User Data InDate </summary>
    public string data_inDate;
    /// <summary> User ID </summary>
    public string ID;
    /// <summary> User가 가진 재화 </summary>
    public long gold;
    /// <summary> User가 잡은 물고기의 갯수 </summary>
    public int[] fishNums;
    /// <summary> User가 소유한 미끼의 갯수 </summary>
    public int[] fishBaits;
    /// <summary> User가 소유한 낚시대의 유무 </summary>
    public bool[] hasFishingRod;
    /// <summary> User가 착용 중인 낚시대 </summary>
    public int equipFishingRod;
    /// <summary> User의 현재 rank </summary>
    [Obsolete] // 2차 구현에서 랭크 시스템 도입 시 사용 예정
    public int rank;

    public UserData()
    {
        fishNums = new int[5];
        fishBaits = new int[5];
        hasFishingRod = new bool[5];
    }
};

public class SaveCtrl : MonoBehaviour
{
    public static SaveCtrl instance = null;

    public UserData myData;
    [Obsolete] // 2차 구현에서 로그인 & 회원가입 기능 도입 시 사용 예정
    public List<UserData> userDatas = new List<UserData>();
    public List<FishingRob> fishingRobs = new List<FishingRob>();
    public List<Bait> baits = new List<Bait>();

    // SingleTon 패턴
    public static SaveCtrl Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            myData = new UserData();
            DontDestroyOnLoad(this.gameObject);

            var bro = Backend.Initialize(true);
            if (bro.IsSuccess())
            {
                //Backend.BMember.DeleteGuestInfo();
                Login();
            }
            else
            {
                Debug.Log("서버 초기화 실패 : " + bro.GetErrorCode());
            }

            // 낚시대 데이터 생성
            for (int i = 0; i < FishingRob.fishingRobNum; i++)
                fishingRobs.Add(new FishingRob(i));
            // 낚시대 데이터 생성
            for (int i = 0; i < Bait.BaitNum; i++)
                baits.Add(new Bait(i));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 로그인 시도 함수
    /// </summary>
    public void Login()
    {
        if(Backend.BMember.GetGuestID().Equals(""))
        {
            // ID 없음 -> 데이터 초기화
            Backend.BMember.GuestLogin();
            InsertData();
            Debug.Log("회원가입 및 데이터 초기화");
        }
        else
        {
            string id = Backend.BMember.GetGuestID();
            BackendReturnObject bro = Backend.BMember.GuestLogin();

            Debug.Log("로컬 기기에 저장된 아이디 :" + id);
            if (bro.IsSuccess())
            {
                LoadData();
            }
            else
            {
                Debug.Log("\n로그인 실패 : " + bro.GetMessage());
            }
        }
    }


    /// <summary>
    /// User Data를 서버에 저장합니다.
    /// </summary>
    public void SaveData()
    {
        if (!Backend.IsInitialized) return;
        string errorCode = null;

        Param param = new Param();
        SettingParam(param);

        errorCode = Backend.GameData.Update("userData", myData.data_inDate, param).GetErrorCode();
        Debug.Log("UserData 서버 저장 <error> = " + errorCode);
    }

    /// <summary>
    /// 서버로부터 User Data를 불러옵니다.
    /// </summary>
    public void LoadData()
    {
        if (!Backend.IsInitialized) return;
        BackendReturnObject BRO = Backend.GameData.GetMyData("userData", new Where());
        if (BRO.IsSuccess())
        {
            JsonData json2 = BRO.GetReturnValuetoJSON()["rows"][0];
            DataParsing(json2);
        }
        else
        {
            Debug.Log("서버 공통 에러 발생: " + BRO.GetMessage());
        }
    }

    /// <summary>
    /// 데이터 테이블 추가 함수
    /// </summary>
    private void InsertData()
    {
        Param param = new Param();
        SettingParam(param);

        BackendReturnObject BRO = Backend.GameData.Insert("userData", param);
        myData.data_inDate = BRO.GetInDate();
        myData.ID = Backend.BMember.GetGuestID();

        string error = BRO.GetErrorCode();
        Debug.Log("public param save <error> : " + error);

        SaveData();
    }

    /// <summary>
    /// param을 설정하는 함수
    /// </summary>
    /// <param name="param">데이터 전달 Parameter</param>
    private void SettingParam(Param param)
    {
        param.Add("data_inDate", myData.data_inDate);
        param.Add("ID", myData.ID);
        param.Add("gold", myData.gold);
        param.Add("fishNums", myData.fishNums);
        param.Add("fishBaits", myData.fishBaits);
        param.Add("hasFishingRod", myData.hasFishingRod);
        param.Add("equipFishingRod", myData.equipFishingRod);
    }

    /// <summary>
    /// 서버로부터 얻은 데이터를 클라이언트에 저장
    /// </summary>
    /// <param name="json">서버 데이터 JsonData</param>
    private void DataParsing(JsonData json)
    {
        myData.data_inDate = json["data_inDate"][0].ToString();
        myData.ID = json["ID"][0].ToString();
        myData.gold = long.Parse(json["gold"][0].ToString());
        for (int i = 0; i < json["fishNums"]["L"].Count; i++)
            myData.fishNums[i] = int.Parse(json["fishNums"]["L"][i][0].ToString());
        for (int i = 0; i < json["fishBaits"]["L"].Count; i++)
            myData.fishBaits[i] = int.Parse(json["fishBaits"]["L"][i][0].ToString());
        for (int i = 0; i < json["hasFishingRod"]["L"].Count; i++)
            myData.hasFishingRod[i] = bool.Parse(json["hasFishingRod"]["L"][i][0].ToString());
        myData.equipFishingRod = int.Parse(json["equipFishingRod"][0].ToString());
    }
}