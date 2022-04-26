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
}