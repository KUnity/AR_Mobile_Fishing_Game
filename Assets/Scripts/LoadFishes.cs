using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFishes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int[] fishNum = SaveCtrl.instance.myData.fishNums;
        int len = fishNum.Length;
        for(int i=0;i<len;i++){
            // i값으로 물고기 타입, 코드 알아내기 

            
        }
    }


}
