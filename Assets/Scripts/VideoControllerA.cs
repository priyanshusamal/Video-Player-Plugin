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
    public string[] Url;
    public float videoTimeDutation = 0;
    [SerializeField] private int UrlClipIndex=0;
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
        // Title.text = videoClips[videoClipIndex].name;
        videoplayerMode = true;
        videoplayerLock = false;
        // if(videoPlayer)
        // {
        //     videoPlayer.url = Url[UrlClipIndex];
        //     videoPlayer.playOnAwake = false;
        //     videoPlayer.Prepare();

        //     videoPlayer.prepareCompleted += OnvideoPrepared;
        // }
    }

    // float a,b;
    void Update()
    {
        
        // videoPlayer.time = progressBar.value;
        if(videoPlayer.isPlaying)
        {
            SetCurrentTime();
            SetTotalTime();
            SetProgressBar();

        }
        if(UrlClipIndex <= 0)
        {
            prevButton.gameObject.SetActive(false);
        }else{prevButton.gameObject.SetActive(true);}

        if(UrlClipIndex >= Url.Length-1)
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
        
        string minutes = Mathf.Floor((int) (videoPlayer.time) / 60).ToString("00");
        string seconds = ((int) videoPlayer.time % 60).ToString("00");
        
        currentMinutes.text = minutes;
        currentSeconds.text = seconds;
    }
    void SetTotalTime()
    {

        string minutes = Mathf.Floor((int) (videoPlayer.frameCount / videoPlayer.frameRate) / 60).ToString("00");
        string seconds = ((int) (videoPlayer.frameCount / videoPlayer.frameRate) % 60).ToString("00");
        
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
        
        // if(videoClipIndex >= videoClips.Length)
        // {
        //     videoClipIndex = videoClips.Length;
        // }
        // else
        // {
        //     videoClipIndex++;
        // }
        if(UrlClipIndex >= Url.Length)
        {
            UrlClipIndex = Url.Length;
        }
        else
        {
            UrlClipIndex++;
        }
        videoPlayer.url = Url[UrlClipIndex];
        previewVideo.url = Url[UrlClipIndex];
        // videoPlayer.clip = videoClips[videoClipIndex];
        // previewVideo.clip = videoClips[videoClipIndex];
        videoPlayer.Play();
        // Title.text = videoClips[videoClipIndex].name;
        Title.text = videoPlayer.clip.name;
    }
    public void SetPreviousClip()
    {
        // if(videoClipIndex < 0)
        // {
        //     videoClipIndex = 0;
        // }else
        // {
        //     videoClipIndex--;
        // }
        if(UrlClipIndex < 0)
        {
            UrlClipIndex = 0;
        }else
        {
            UrlClipIndex--;
        }
        // videoPlayer.clip = videoClips[videoClipIndex];
        // previewVideo.clip = videoClips[videoClipIndex];
        videoPlayer.url = Url[UrlClipIndex];
        previewVideo.url = Url[UrlClipIndex];
        videoPlayer.Play();
        // Title.text = videoClips[videoClipIndex].name;
        Title.text = videoPlayer.clip.name;
        
    }

    public void SetProgressBar()
    {
        progressBar.maxValue = (float) (videoPlayer.frameCount / videoPlayer.frameRate);
        progressBar.value = (float) (videoPlayer.time);
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
