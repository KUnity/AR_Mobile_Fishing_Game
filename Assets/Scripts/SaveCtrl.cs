using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using LitJson;
using System.Text;

/// <summary> User Data Class </summary>
public class UserData
{
    /// <summary> User Data InDate </summary>
    public string data_inDate;
    /// <summary> User ID </summary>
    public string ID;
    /// <summary> User 돈</summary>
    public long gold;
    /// <summary> User가 가진 물고기 개수 </summary>
    public int[] fishNums;
    /// <summary> User가 가진 미끼 개수 </summary>
    public int[] fishBaits;
    /// <summary> User가 현재 보유 중인 낚시대</summary>
    public bool[] hasFishingRod;
    /// <summary> User가 현재 장착 중인 미끼</summary>
    public int equipBaits;
    /// <summary> User가 현재 장착 중인 낚시대</summary>
    public int equipFishingRod;
    /// <summary> User의 현재 등수</summary>
    [Obsolete] // 2차 구현에서 사용될 변수
    public int rank;

    public UserData()
    {
        fishNums = new int[5];
        fishBaits = new int[7];
        hasFishingRod = new bool[5];
    }
};

public class SaveCtrl : MonoBehaviour
{
    public static SaveCtrl instance = null;

    public UserData myData;
    [Obsolete] // 2차 구현에서 사용될 변수
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
                Debug.Log("Server Initialization Failed. <error> : " + bro.GetErrorCode());
            }

            for (int i = 0; i < FishingRob.fishingRobNum; i++)
                fishingRobs.Add(new FishingRob(i));
            for (int i = 0; i < Bait.BaitNum; i++)
                baits.Add(new Bait(i));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// 로그인 함수
    /// </summary>
    public void Login()
    {
        if(Backend.BMember.GetGuestID().Equals(""))
        {
            // ID 없음 -> 자동 회원가입 및 로그인
            Backend.BMember.GuestLogin();
            InsertData();
        }
        else
        {
            string id = Backend.BMember.GetGuestID();
            BackendReturnObject bro = Backend.BMember.GuestLogin();

            Debug.Log("User ID :" + id);
            if (bro.IsSuccess())
            {
                LoadData();
            }
            else
            {
                Debug.Log("\nLogin Failed. <error> : " + bro.GetMessage());
            }
        }
    }


    /// <summary>
    /// User Data를 서버에 저장하는 함수
    /// </summary>
    public void SaveData()
    {
        if (!Backend.IsInitialized) return;
        string errorCode = null;

        Param param = new Param();
        SettingParam(param);

        errorCode = Backend.GameData.Update("userData", myData.data_inDate, param).GetErrorCode();
        Debug.Log("UserData Save = " + errorCode);
    }

    /// <summary>
    /// 서버로부터 User Data를 로드하는 함수
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
            Debug.Log("UserData Load = " + BRO.GetMessage());
        }
    }

    /// <summary>
    /// 데이터 테이블을 초기화하는 함수
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
    /// <param name="param">서버에 전달될 Parameter</param>
    private void SettingParam(Param param)
    {
        param.Add("data_inDate", myData.data_inDate);
        param.Add("ID", myData.ID);
        param.Add("gold", myData.gold);
        param.Add("fishNums", myData.fishNums);
        param.Add("fishBaits", myData.fishBaits);
        param.Add("hasFishingRod", myData.hasFishingRod);
        param.Add("equipFishingRod", myData.equipFishingRod);
        param.Add("equipBaits", myData.equipBaits);
    }

    /// <summary>
    /// 서버로부터 불러온 JsonData를 UserData로 Parsing하는 함수
    /// </summary>
    /// <param name="json">서버에서 받아온 JsonData</param>
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
        myData.equipBaits = int.Parse(json["equipBaits"][0].ToString());
    }

    /// <summary>
    /// 유저 데이터를 초기화하는 함수
    /// </summary>
    /// <param name="_gold">초기 Gold 값</param>
    public void ResetData(int _gold)
    {
        myData = new UserData();
        myData.gold = _gold;
        SaveData();
    }

    /// <summary>
    /// 유저 데이터를 초기화하는 함수
    /// </summary>
    public void ResetData()
    {
        ResetData(0);
    }
}