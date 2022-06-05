using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnOffEffect : MonoBehaviour
{
    [SerializeField] GameObject[] onoff;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i < 9; i++) {
            if(!SaveCtrl.instance.myData.fish_collections[i]) {
                onoff[i].SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
