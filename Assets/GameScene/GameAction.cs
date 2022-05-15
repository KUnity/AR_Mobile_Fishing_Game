using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameAction : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] GameObject gameData;
    [SerializeField] GameObject reel;
    [SerializeField] Slider tensionSlider;
    [SerializeField] GameObject gameSceneManager;
    Animator reelAnimator;
    private bool topPos;
    private bool bottomPos;
    private byte handlePos;
    public Fish fish;


    // Start is called before the first frame update
    void Start()
    {
        reelAnimator = reel.GetComponent<Animator>();
        reelAnimator.StartPlayback();
        reel.GetComponent<Transform>().position = new Vector3(1f, -3f, -6f);
        topPos = true;
        bottomPos = false;
        handlePos = 0;
    }

    // Update is called once per frame
    void Update()
    {

        tensionSlider.value -= 0.1f * Time.deltaTime;
        if (gameData.GetComponent<GameData>().fightStart) {

        }

        if (topPos && bottomPos) {
            if (handlePos == 1) {
                bottomPos = false;
                Debug.Log("UP");
                tensionSlider.value += 0.05f;

                // Attak
            }
            else if (handlePos == 0) {
                topPos = false;
                Debug.Log("DOWN");
                tensionSlider.value += 0.05f;

                // Attak
            }

        }
    }

    public void OnPointerDown(PointerEventData eventData) {
        reelAnimator.StopPlayback();
    }

    public void OnDrag(PointerEventData eventData) {
        float posX = eventData.position.x;
        float posY = eventData.position.y;
        
        if (posY > 450) {
            topPos = true;
            handlePos = 1;
        }
        else if (posY < 250) {
            bottomPos = true;
            handlePos = 0;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        reelAnimator.StartPlayback();
    }
}
