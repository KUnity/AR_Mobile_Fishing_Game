using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAccount : MonoBehaviour
{
    public Text myAccount;

    // Start is called before the first frame update
    void Start()
    {
        myAccount.text = PlayerPrefs.GetString("Account");
    }
}