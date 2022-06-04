using System.Collections;
using System.Numerics;
using System.Collections.Generic;
using UnityEngine;

public class CheckFishNFT : MonoBehaviour
{
    [SerializeField]
    private GameObject onoff;
    [SerializeField]
    GameObject increment;
    async void Start()
    {
        string chain = "polygon";
        string network = "mainnet";
        string contract = "0x2953399124F0cBB46d2CbACD8A89cF0599974963";
        string account = GameObject.Find("DontDistroy_Wallet").GetComponent<DontDistroyGameObject>().account;
        string tokenId = "63509423994943966704742002637236716458408843518489414562095478430379177672714";

        if (account == "NOWALLET"){
            Debug.Log("연결된 지갑이 없음");
            onoff.SetActive(true);
        }
        else{
            BigInteger balanceOf = await ERC1155.BalanceOf(chain, network, contract, account, tokenId);
            print(balanceOf);

            if (balanceOf > 0) {
                Debug.Log("보유중인 NFT ID : 63509423994943966704742002637236716458408843518489414562095478430379177672714");
                // 활성화!
                increment.GetComponent<CollectIncText>().totalPowerUp += 200f;
                increment.GetComponent<CollectIncText>().totalProbUp += 2f;
                onoff.SetActive(false);
            }
            else{
                onoff.SetActive(true);
            }
        }

        
    }
}
