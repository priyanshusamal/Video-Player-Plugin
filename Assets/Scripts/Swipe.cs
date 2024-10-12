using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class Swipe : MonoBehaviour
{
    [SerializeField]private VideoPlayer videoPlayer;
    [SerializeField]private Vector2 startTouchPosition;
    [SerializeField]private Vector2 endTouchPosition;
    [SerializeField]private TMP_Text t;
    private float screenwidth, screenheight;
    private Animation anim;


    [SerializeField]private float lastTapTime = 0;
    [SerializeField]private float doubleTapThreshold = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();
        screenwidth = Screen.width;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            float touchPos = Input.GetTouch(0).position.x;
            if(touch.phase == TouchPhase.Began)
            {
                if(touchPos > (screenwidth/2))
                {
                    if(Time.time - lastTapTime <= doubleTapThreshold)
                    {
                        lastTapTime = 0;
                        Debug.Log("Right Tap");
                        anim.Play("fastforward");
                        videoPlayer.time += 10f;
                    }
                    else
                    {
                        lastTapTime = Time.time;
                    }
                }
                if(touchPos < (screenwidth/2))
                {
                    if(Time.time - lastTapTime <= doubleTapThreshold)
                    {
                        lastTapTime = 0;
                        Debug.Log("Left Tap");
                        anim.Play("rewind");
                        videoPlayer.time -= 10f;
                    }
                    else
                    {
                        lastTapTime = Time.time;
                    }
                }
            }
        }
        // if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Began)
        // {
        //     startTouchPosition = Input.GetTouch(0).position;
        // }
        // if(Input.touchCount>0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        // {
        //     endTouchPosition = Input.GetTouch(0).position;
        //     if(endTouchPosition.x > (screenwidth/2))
        //     {
        //         if(endTouchPosition.y > startTouchPosition.y)
        //         {
        //             Debug.Log("+1");
        //             t.text = "Brightness +1";
        //         }
        //         if(endTouchPosition.y < startTouchPosition.y)
        //         {
        //             Debug.Log("-1");
        //             t.text = "Brightness -1";
        //         }
        //     }
            
        //     if(endTouchPosition.x > (screenwidth/2))
        //     {

        //         if(endTouchPosition.y > startTouchPosition.y)
        //         {
        //             Debug.Log("Volume +1");
        //             t.text = "Volume +1";
        //         }
        //         if(endTouchPosition.y < startTouchPosition.y)
        //         {
        //             Debug.Log("Volume -1");
        //             t.text = "Volume -1";
        //         }
        //     }
        // }
       
    }
}
