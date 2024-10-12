using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class VideoControllerA : MonoBehaviour
{
    public GameObject UIPanel;
    public Slider progressBar;
    public Button nextButton, prevButton;
    public TMP_Text currentMinutes, currentSeconds;
    public TMP_Text TotalMinutes, TotalSeconds;
    private Animation UIAnim;
    public VideoClip[] videoClips;
    [SerializeField] private int videoClipIndex=0;
    private VideoPlayer videoPlayer;
    [SerializeField]private float elapseTime = 4f;
    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        UIAnim = UIPanel.GetComponent<Animation>();
    }
    
    void Update()
    {
        if(videoPlayer.isPlaying)
        {
            SetCurrentTime();
            SetTotalTime();
            SetTotalTime();
        }
        if(videoClipIndex <= 0)
        {
            prevButton.gameObject.SetActive(false);
        }else{prevButton.gameObject.SetActive(true);}

        if(videoClipIndex >= videoClips.Length-1)
        {
            nextButton.gameObject.SetActive(false);
        }else{nextButton.gameObject.SetActive(true);}




        if(UIPanel.activeSelf == true)
        {
            if(elapseTime >= 0)
            {
                elapseTime -= Time.deltaTime;
            }
            if(elapseTime <= 1.5f) 
            {
                UIAnim.Play("fadeout");
            }
            if(elapseTime < 0)
            {
                UIPanel.SetActive(false);
                elapseTime = 4f;
            }
        }
        if(Input.GetMouseButton(0))
        {
            UIAnim.Play("fadein");
            UIPanel.SetActive(true);
            elapseTime = 4f;
        }
    }
    void SetCurrentTime()
    {

        string minutes = Mathf.Floor((int) videoPlayer.time / 60).ToString("00");
        string seconds = ((int) videoPlayer.time % 60).ToString("00");
        
        currentMinutes.text = minutes;
        currentSeconds.text = seconds;
    }
    void SetTotalTime()
    {

        string minutes = Mathf.Floor((int) videoPlayer.clip.length / 60).ToString("00");
        string seconds = ((int) videoPlayer.clip.length % 60).ToString("00");
        
        TotalMinutes.text = minutes;
        TotalSeconds.text = seconds;
    }
    public void PlayPause()
    {
        if(videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
        }
        else{
            videoPlayer.Play();
        }

    }
    public void SetNextClip()
    {
        Debug.Log("++1");
        
        if(videoClipIndex >= videoClips.Length)
        {
            videoClipIndex = videoClips.Length;
        }
        else
        {
            videoClipIndex++;
        }
        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
    }
    public void SetPreviousClip()
    {
        Debug.Log("--1");
        if(videoClipIndex < 0)
        {
            videoClipIndex = 0;
        }else
        {
            videoClipIndex--;
        }
        videoPlayer.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
        
    }

}
