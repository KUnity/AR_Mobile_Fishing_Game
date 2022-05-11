using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;
using LitJson;
using System.Text;

/// <summary> User Data ���� Class </summary>
public class UserData
{
    /// <summary> User Data InDate </summary>
    public string data_inDate;
    /// <summary> User ID </summary>
    public string ID;
    /// <summary> User�� ���� ��ȭ </summary>
    public long gold;
    /// <summary> User�� ���� �������� ���� </summary>
    public int[] fishNums;
    /// <summary> User�� ������ �̳��� ���� </summary>
    public int[] fishBaits;
    /// <summary> User�� ������ ���ô��� ���� </summary>
    public bool[] hasFishingRod;
    /// <summary> User�� ���� ���� ���ô� </summary>
    public int equipFishingRod;
    /// <summary> User�� ���� rank </summary>
    [Obsolete] // 2�� �������� ��ũ �ý��� ���� �� ��� ����
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
    [Obsolete] // 2�� �������� �α��� & ȸ������ ��� ���� �� ��� ����
    public List<UserData> userDatas = new List<UserData>();
    public List<FishingRob> fishingRobs = new List<FishingRob>();
    public List<Bait> baits = new List<Bait>();

    // SingleTon ����
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
                Debug.Log("���� �ʱ�ȭ ���� : " + bro.GetErrorCode());
            }

            // ���ô� ������ ����
            for (int i = 0; i < FishingRob.fishingRobNum; i++)
                fishingRobs.Add(new FishingRob(i));
            // ���ô� ������ ����
            for (int i = 0; i < Bait.BaitNum; i++)
                baits.Add(new Bait(i));
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// �α��� �õ� �Լ�
    /// </summary>
    public void Login()
    {
        if(Backend.BMember.GetGuestID().Equals(""))
        {
            // ID ���� -> ������ �ʱ�ȭ
            Backend.BMember.GuestLogin();
            InsertData();
            Debug.Log("ȸ������ �� ������ �ʱ�ȭ");
        }
        else
        {
            string id = Backend.BMember.GetGuestID();
            BackendReturnObject bro = Backend.BMember.GuestLogin();

            Debug.Log("���� ��⿡ ����� ���̵� :" + id);
            if (bro.IsSuccess())
            {
                LoadData();
            }
            else
            {
                Debug.Log("\n�α��� ���� : " + bro.GetMessage());
            }
        }
    }


    /// <summary>
    /// User Data�� ������ �����մϴ�.
    /// </summary>
    public void SaveData()
    {
        if (!Backend.IsInitialized) return;
        string errorCode = null;

        Param param = new Param();
        SettingParam(param);

        errorCode = Backend.GameData.Update("userData", myData.data_inDate, param).GetErrorCode();
        Debug.Log("UserData ���� ���� <error> = " + errorCode);
    }

    /// <summary>
    /// �����κ��� User Data�� �ҷ��ɴϴ�.
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
            Debug.Log("���� ���� ���� �߻�: " + BRO.GetMessage());
        }
    }

    /// <summary>
    /// ������ ���̺� �߰� �Լ�
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
    /// param�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="param">������ ���� Parameter</param>
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
    /// �����κ��� ���� �����͸� Ŭ���̾�Ʈ�� ����
    /// </summary>
    /// <param name="json">���� ������ JsonData</param>
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