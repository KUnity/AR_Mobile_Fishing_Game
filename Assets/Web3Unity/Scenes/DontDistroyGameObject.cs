using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDistroyGameObject : MonoBehaviour
{
    public string account;
    public bool isLogined = false;
    
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
}
