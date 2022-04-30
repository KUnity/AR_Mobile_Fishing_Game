using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> User Data 저장 Class </summary>
public class UserData
{
    /// <summary> User Identifier </summary>
    public string owner_inDate;
    /// <summary> User Nickname </summary>
    public string nickName;
    /// <summary> User가 가진 재화 </summary>
    public int gold;
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
            DontDestroyOnLoad(this.gameObject);

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
    /// User Data를 서버에 저장합니다.
    /// </summary>
    [Obsolete] // 2차 구현에서 사용 예정
    public void SaveData()
    {
        
    }

    /// <summary>
    /// 서버로부터 User Data를 불러옵니다.
    /// </summary>
    [Obsolete] // 2차 구현에서 사용 예정
    public void LoadData()
    {

    }

    private void A()
    {
        Debug.Log("첫번째 낚시대 가격 : " + SaveCtrl.instance.fishingRobs[0].gold + " 입니다.");
        Debug.Log("두번째 미끼 가격 : " + SaveCtrl.instance.baits[1].gold + " 입니다.");
    }
}