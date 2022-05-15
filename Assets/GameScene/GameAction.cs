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
    Animator reelAnimator;
    private bool topPos;
    private bool bottomPos;
    private byte handlePos;
    // Start is called before the first frame update
    void Start()
    {
        reelAnimator = reel.GetComponent<Animator>();
        reelAnimator.StartPlayback();
        topPos = true;
        bottomPos = false;
        handlePos = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tensionSlider.value -= 0.2f * Time.deltaTime;
        if (gameData.GetComponent<GameData>().fightStart) {

        }

        if (topPos && bottomPos) {
            if (handlePos == 1) {
                bottomPos = false;
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
        
        if (posY > 250) {
            topPos = true;
            handlePos = 1;
        }
        else if (posY < 60) {
            bottomPos = true;
            handlePos = 0;
        }
    }

    public void OnPointerUp(PointerEventData eventData) {
        reelAnimator.StartPlayback();
    }
}
