using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour {

    private AudioSource bgmSource;

    public AudioClip BGMDefault;
    public AudioClip BGMWaitingRoom;
    public AudioClip BGMQuiz;

    private int pageIndex;

    private void Start()
    {
        bgmSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Awake() {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if(objs.Length > 1) {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        pageIndex = SceneManager.GetActiveScene().buildIndex;

        //Splash~WaitingRoom
        if (pageIndex == 0)
        {               
                bgmSource.clip = BGMWaitingRoom;
        }

        //default
        else if (pageIndex == 3 || pageIndex == 7)
        {
                bgmSource.clip = BGMDefault;
        }

        //Quiz
        else if (pageIndex == 8 || pageIndex == 9 || pageIndex == 10 || pageIndex == 11)
        {
                bgmSource.clip = BGMQuiz;
        }

        //BGM Play
        if (!bgmSource.isPlaying) {
            bgmSource.Play();
        }
    }
}