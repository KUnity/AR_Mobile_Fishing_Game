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
    public string public_inDate, private_inDate;
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
    /// <summary> User가 도감에 등록한 물고기 </summary>
    public bool[] fish_collections;
    /// <summary>  /// </summary>
    public int rank_score;
    /// <summary> User의 현재 등수</summary>
    public int rank;

    public UserData()
    {
        fishNums = new int[Fish.totalNum];
        fishBaits = new int[Bait.BaitNum];
        hasFishingRod = new bool[FishingRob.fishingRobNum];
        fish_collections = new bool[Fish.totalNum];
        hasFishingRod[0] = true;
        fishNums[0]=3;
        fishNums[1]=2;
        fishNums[7] = 1;
        fishNums[8] = 2;
        gold = 50;
    }

    /// <summary>
    /// 도감의 정보를 기반으로 Score를 계산하여 반환하는 함수
    /// </summary>
    public int GetRankScore()
    {
        int score = 0;
        Fish fish;

        for (int i = 0; i < fish_collections.Length; i++)
        {
            if (fish_collections[i]) 
            {
                fish = Fish.GetFish(i);
                score += SaveCtrl.instance.scoreAsQuality[fish.quality];
            }
        }

        return score;
    }
};

public class SaveCtrl : MonoBehaviour
{
    public static SaveCtrl instance = null;
    private readonly string rank_uuid = "3d41f510-d995-11ec-a5ca-df7f97b8d87a";
    public readonly int[] scoreAsQuality = { 1, 10, 100, 1000, 10000 };

    public UserData myData; // 플레이어 데이터
    public List<UserData> userDatas = new List<UserData>(); // 다른 유저 데이터 (Index = Rank - 1)
    public List<FishingRob> fishingRobs = new List<FishingRob>();
    public List<Bait> baits = new List<Bait>();
    public List<Shark> sharks = new List<Shark>();
    public List<NormalFish> normalFish = new List<NormalFish>();

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
            for (int i = 0; i < Shark.totalNum; i++)
                sharks.Add(new Shark(i));
            for (int i = 0; i < NormalFish.totalNum; i++)
                normalFish.Add(new NormalFish(i));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        SaveData();
        Application.Quit();
    }

    /// <summary>
    /// 로그인 함수
    /// </summary>
    public void Login()
    {
        if(Backend.BMember.GetGuestID().Equals(""))
        {
            // ID 없음 -> 자동 회원가입 및 로그인
            Debug.Log("No ID, Auto Auth & Login Start.");
            BackendReturnObject bro = Backend.BMember.GuestLogin();
            if (bro.IsSuccess())
            {
                InsertData();
            }
            else
            {
                Debug.Log("Login Failed : " + bro.GetMessage());
            }
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
                if (bro.GetErrorCode().Equals("BadUnauthorizedException"))
                {
                    Debug.Log("Error occured. Destroy ID.");
                    Backend.BMember.DeleteGuestInfo();
                    Login();
                }
            }
        }
    }


    /// <summary>
    /// User Data를 서버에 저장하는 함수
    /// </summary>
    public void SaveData()
    {
        if (!Backend.IsInitialized)
        {
            Debug.LogError("The client not login yet.");
            return;
        }

        myData.rank_score = myData.GetRankScore();
        Param param = new Param();
        SettingPublicParam(param);
        BackendReturnObject BRO = Backend.GameData.Update("userData_public", myData.public_inDate, param);
        if (!BRO.IsSuccess())
            Debug.LogError("Private data save failed = " + BRO.GetMessage());

        param = new Param();
        SettingPrivateParam(param);
        BRO = Backend.GameData.Update("userData_private", myData.private_inDate, param);
        if (!BRO.IsSuccess())
            Debug.LogError("Private data save failed = " + BRO.GetMessage());

        // 랭킹 등록
        param = new Param();
        param.Add("rank_score", myData.rank_score);
        BRO = Backend.URank.User.UpdateUserScore(rank_uuid, "userData_private", myData.private_inDate, param);
        if (!BRO.IsSuccess())
            Debug.LogError("Rank update failed. <error> : " + BRO.GetMessage());
    }

    /// <summary>
    /// 서버로부터 User Data를 로드하는 함수
    /// </summary>
    public void LoadData()
    {
        // 로그인 상태 체크
        if (!Backend.IsInitialized)
        {
            Debug.LogError("The client not login yet.");
            return;
        }

        // Public Data Load
        BackendReturnObject BRO = Backend.GameData.GetMyData("userData_public", new Where());
        if (BRO.IsSuccess())
        {
            JsonData json = BRO.GetReturnValuetoJSON()["rows"][0];
            PublicDataParsing(json);
        }
        else
        {
            Debug.LogError("Public data load failed = " + BRO.GetMessage());
        }

        // Private Data Load
        BRO = Backend.GameData.GetMyData("userData_private", new Where());
        if (BRO.IsSuccess())
        {
            JsonData json = BRO.GetReturnValuetoJSON()["rows"][0];
            PrivateDataParsing(json);
        }
        else
        {
            Debug.LogError("Private data load failed = " + BRO.GetMessage());
        }
    }

    /// <summary>
    /// 데이터 테이블을 초기화하는 함수
    /// </summary>
    private void InsertData()
    {
        // Public Data Setting
        Param param = new Param();
        SettingPublicParam(param);
        BackendReturnObject BRO = Backend.GameData.Insert("userData_public", param);
        myData.public_inDate = BRO.GetInDate();
        if(!BRO.IsSuccess())
            Debug.LogError("Public param save failed. <error> : " + BRO.GetMessage());

        // Private Data Setting
        param = new Param();
        SettingPrivateParam(param);
        BRO = Backend.GameData.Insert("userData_private", param);
        myData.private_inDate = BRO.GetInDate();
        if (!BRO.IsSuccess())
            Debug.LogError("Private param save failed. <error> : " + BRO.GetMessage());

        myData.ID = Backend.BMember.GetGuestID();

        // 랭킹 등록
        param = new Param();
        param.Add("rank_score", myData.rank_score);
        BRO = Backend.URank.User.UpdateUserScore(rank_uuid, "userData_private", myData.private_inDate, param);
        if (!BRO.IsSuccess())
            Debug.LogError("Rank update failed. <error> : " + BRO.GetMessage());

        SaveData();
    }

    /// <summary>
    /// Public param을 설정하는 함수
    /// </summary>
    /// <param name="param">서버에 전달될 Parameter</param>
    private void SettingPublicParam(Param param)
    {
        param.Add("public_inDate", myData.public_inDate);
        param.Add("private_inDate", myData.private_inDate);
        param.Add("ID", myData.ID);
        param.Add("gold", myData.gold);
        param.Add("fishNums", myData.fishNums);
        param.Add("fishBaits", myData.fishBaits);
        param.Add("hasFishingRod", myData.hasFishingRod);
        param.Add("equipFishingRod", myData.equipFishingRod);
        param.Add("equipBaits", myData.equipBaits);
        param.Add("fish_collections", myData.fish_collections);
    }

    /// <summary>
    /// Private param을 설정하는 함수
    /// </summary>
    /// <param name="param">서버에 전달될 Parameter</param>
    private void SettingPrivateParam(Param param)
    {
        param.Add("rank_score", myData.rank_score);
        param.Add("rank", myData.rank);
    }

    /// <summary>
    /// 서버로부터 불러온 Public Data를 UserData로 Parsing하는 함수
    /// </summary>
    /// <param name="json">서버에서 받아온 JsonData</param>
    private void PublicDataParsing(JsonData json)
    {
        myData.public_inDate = json["public_inDate"][0].ToString();
        myData.private_inDate = json["private_inDate"][0].ToString();
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
        for (int i = 0; i < json["fish_collections"]["L"].Count; i++)
            myData.fish_collections[i] = bool.Parse(json["fish_collections"]["L"][i][0].ToString());
    }

    /// <summary>
    /// 서버로부터 불러온 Private Data를 UserData로 Parsing하는 함수
    /// </summary>
    /// <param name="json">서버에서 받아온 JsonData</param>
    private void PrivateDataParsing(JsonData json)
    {
        myData.rank_score = int.Parse(json["rank_score"][0].ToString());
        myData.rank = int.Parse(json["rank"][0].ToString());
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
