using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFish : MonoBehaviour
{
    // float oldX;
    // float newX;
    float direction = 1;
    float rightLimit, leftLimit;
    float frontLimit, backLimit;
    float topLimit, bottomLimit;
    public float offset = 0.8f;
    public float zInterval=10; // 물고기가 나타날 수 있는 앞 뒤 간격 
    public float speed=1;
    private Vector3 moveVector;
    private GameObject waterWall;
    private GameObject target;
    private GameObject dlgFish; 
    private  ManageFishDlg fishDlgManager;
    Camera waterCam;
    // Start is called before the first frame update
    void Start()
    {
        // Random.InitState((int)(Time.time*100f));
        moveVector= Vector3.right * speed;

        dlgFish = GameObject.Find("Canvas").transform.Find("Dialog Fish").gameObject;
        Debug.Log(dlgFish);
        fishDlgManager =  dlgFish.GetComponent<ManageFishDlg>();
        InitLimitCoords(); // 물고기 움직이는 범위 제한
        InitObjectPosition(); // 물고기 초기 위치 설정
    }

    private void InitLimitCoords(){
        waterWall = GameObject.Find( "WaterWall" );
        waterCam = GameObject.Find("WaterCamera").GetComponent<Camera>();
        
        float height = Camera.main.orthographicSize;

        // x좌표 범위 설정
        rightLimit = waterCam.ScreenToWorldPoint(((float)Screen.width)*Vector3.right).x*offset;
        leftLimit = waterCam.ScreenToWorldPoint(Vector3.zero).x*offset;

        // y좌표 범위 설정
        topLimit = waterWall.transform.localScale.y/2 + waterWall.transform.position.y;
        bottomLimit = waterCam.ScreenToWorldPoint((height * -1 + waterCam.gameObject.transform.position.x)*Vector3.up).y*offset;

        // z좌표 범위 설정
        frontLimit = waterWall.transform.position.z/offset;
        backLimit = frontLimit - zInterval;
      
    }

    private void InitObjectPosition(){

        float x = Random.Range(leftLimit,rightLimit);
        float y = Random.Range(bottomLimit,topLimit);
        float z = Random.Range(frontLimit,backLimit);

        Vector3 pos = new Vector3(x,y,z);
        gameObject.transform.position = pos;

    }

    // Update is called once per frame
    void Update()
    {
         // 오른쪽 끝에 도달 
        if(gameObject.transform.position.x > rightLimit && direction > 0 ){
            direction=-1;
            transform.Rotate(Vector3.up, 180.0f, Space.World); // 좌우 반전
           
        } // 왼쪽 끝에 도달 
        else if(gameObject.transform.position.x < leftLimit && direction < 0 ){
            direction =1;
            transform.Rotate(Vector3.up, 180.0f, Space.World); // 좌우 반전
        }
        transform.position += moveVector*Time.deltaTime*direction;


        if(Input.GetMouseButtonDown(0)){
            target = GetClickedObject();

            if(target!=null && target.Equals(gameObject)){
                int itemType,itemCode;
                int i = GetItemIndexFromName();
                itemType = Fish.GetItemType(i,out itemCode);
                fishDlgManager.OpenDlg();
                fishDlgManager.SetItemInfo(itemType,itemCode);
                fishDlgManager.GetClickedFish(gameObject);
            }

        }
    }

    private GameObject GetClickedObject(){
        RaycastHit hit;
        GameObject target =  null;

        Ray ray = waterCam.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray.origin,ray.direction*10,out hit)){
            target = hit.collider.gameObject;
        }
        return target;
    }


    public void RemoveFish(){
        gameObject.SetActive(false);
    }

    public int GetItemIndexFromName(){
        string[] split_data = gameObject.name.Split(' ');
        int itemIndex =  int.Parse(split_data[1]);
        return itemIndex;
    }



    




}
