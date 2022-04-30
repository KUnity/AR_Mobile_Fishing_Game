using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> User Data ���� Class </summary>
public class UserData
{
    /// <summary> User Identifier </summary>
    public string owner_inDate;
    /// <summary> User Nickname </summary>
    public string nickName;
    /// <summary> User�� ���� ��ȭ </summary>
    public int gold;
    /// <summary> User�� ���� ������� ���� </summary>
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
            DontDestroyOnLoad(this.gameObject);

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
    /// User Data�� ������ �����մϴ�.
    /// </summary>
    [Obsolete] // 2�� �������� ��� ����
    public void SaveData()
    {
        
    }

    /// <summary>
    /// �����κ��� User Data�� �ҷ��ɴϴ�.
    /// </summary>
    [Obsolete] // 2�� �������� ��� ����
    public void LoadData()
    {

    }

    private void A()
    {
        Debug.Log("ù��° ���ô� ���� : " + SaveCtrl.instance.fishingRobs[0].gold + " �Դϴ�.");
        Debug.Log("�ι�° �̳� ���� : " + SaveCtrl.instance.baits[1].gold + " �Դϴ�.");
    }
}