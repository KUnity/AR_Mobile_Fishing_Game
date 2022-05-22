using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFishes : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject[] normalFishPrefabs;
    private GameObject[] sharkPrefabs;

    void Start()
    {
        // 모든 프리팹 가져오기 
        normalFishPrefabs = Resources.LoadAll<GameObject>("Prefabs/Fishes/NormalFish");
        sharkPrefabs = Resources.LoadAll<GameObject>("Prefabs/Fishes/Shark");
        // 사용자 보유 물고기 가져오기 
        int[] fishNum = SaveCtrl.instance.myData.fishNums;
        int len = fishNum.Length;

        // 물고기 동적 배치 
        for(int i=0;i<len;i++){
           // Debug.Log(fishNum[i]);
            // i값으로 물고기 타입, 코드 알아내기 
            int itemCode;
            int itemType;
            itemType = Fish.GetItemType(i,out itemCode);
            
            // prefab -> 오브젝트 생성 
            GameObject fishPrefab ;
            switch(itemType){
                case 0:
                    for(int j =0;j<fishNum[i];j++){
                        fishPrefab = Instantiate(normalFishPrefabs[itemCode]);
                        fishPrefab.name = "NormalFish " + i ; 
                    }
                    break;
                case 1:
                    // 상어 프리팹으로 할당 
                    for(int j =0;j<fishNum[i];j++){
                        fishPrefab = Instantiate(sharkPrefabs[itemCode]); // 이거 상어 itemcode 반환하는 거 있는지 
                        fishPrefab.name = "Shark " + i; 
                    }
                    break;
            }
           
        }
    }


}
