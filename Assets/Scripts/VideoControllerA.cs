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
    public TextMeshProUGUI playbackSpeed;
    public Animation lockButtonAnimation;
    public TMP_Text currentMinutes, currentSeconds;
    public TMP_Text TotalMinutes, TotalSeconds;
    public TMP_Text Title;
    private Animation UIAnim;

    public VideoClip[] videoClips;
    [SerializeField] private int videoClipIndex=0;
    private VideoPlayer videoPlayer;
    [SerializeField] private VideoPlayer previewVideo;
    [SerializeField]private float elapseTime = 4f;

    [SerializeField]private bool videoplayerMode;
    [SerializeField]private bool videoplayerLock;
    private bool canFade = true;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        UIAnim = UIPanel.GetComponent<Animation>();
        Title.text = videoClips[videoClipIndex].name;
        videoplayerMode = true;
        videoplayerLock = false;
    }
    // float a,b;
    void Update()
    {
        
        // videoPlayer.time = progressBar.value;
        Debug.Log(videoPlayer.canSetTime);
        if(videoPlayer.isPlaying)
        {
            SetCurrentTime();
            SetTotalTime();
            SetProgressBar();

        }
        if(videoClipIndex <= 0)
        {
            prevButton.gameObject.SetActive(false);
        }else{prevButton.gameObject.SetActive(true);}

        if(videoClipIndex >= videoClips.Length-1)
        {
            nextButton.gameObject.SetActive(false);
        }else{nextButton.gameObject.SetActive(true);}




        if(UIPanel.activeSelf == true && canFade)
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
        if(Input.GetMouseButton(0) && videoplayerMode == true)
        {

            UIAnim.Play("fadehold");
            UIPanel.SetActive(true);
            elapseTime = 4f;
        }
        if(Input.GetMouseButton(0) && videoplayerLock == true)
        {
            lockButtonAnimation.Play("LockButton");
        }
    }
    // void SetVideoTime
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
        
        if(videoClipIndex >= videoClips.Length)
        {
            videoClipIndex = videoClips.Length;
        }
        else
        {
            videoClipIndex++;
        }
        videoPlayer.clip = videoClips[videoClipIndex];
        previewVideo.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
        Title.text = videoClips[videoClipIndex].name;
    }
    public void SetPreviousClip()
    {
        if(videoClipIndex < 0)
        {
            videoClipIndex = 0;
        }else
        {
            videoClipIndex--;
        }
        videoPlayer.clip = videoClips[videoClipIndex];
        previewVideo.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
        Title.text = videoClips[videoClipIndex].name;
        
    }

    public void SetProgressBar()
    {
        progressBar.maxValue = (float) videoPlayer.clip.length;
        progressBar.value = (float) videoPlayer.time;
    }
    public void SProgressBar()
    {
        videoPlayer.time = progressBar.value;
        videoPlayer.Play();
    }
    
    public void SetFramePreview()
    {
        int a = (int) progressBar.value;
        previewVideo.time = a;
    }

    public void SetPlaybackSpeed(int val)
    {
        if(val == 0)
        {
            videoPlayer.playbackSpeed = 0.25f;
        }
        else if(val == 1)
        {
            videoPlayer.playbackSpeed = 0.5f;
        }
        else if(val == 2)
        {
            videoPlayer.playbackSpeed = 1f;
        }
        else if(val == 3)
        {
            videoPlayer.playbackSpeed = 1.5f;
        }
        else if(val == 4)
        {
            videoPlayer.playbackSpeed = 2f;
        }
    }
    public void SetVideoPlayerMode(bool b)
    {
        videoplayerMode = b;
    }
    public void SetVideoPlayerLock(bool b)
    {
        videoplayerLock = b;
    }
    public void SetCanFade(bool b)
    {
        canFade = b;
    }
}
