using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioCaller : MonoBehaviour
{
    public AudioManager audioManager;

    public AudioClip[] ClipList;

    public enum AUDSTAT{
        SAFEPLANE,
        BATSLOW,
        BATFAST,
        INTRO,
        BRKDWN,
        OUTRO,
        ENTSONG,
        INVALID
    }
    public AUDSTAT curAudStat;
    public AUDSTAT defAudStat;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "HomeMenu"){
            audioManager.changeBGM(ClipList[(int)AUDSTAT.ENTSONG]);
            defAudStat = AUDSTAT.ENTSONG;
        }
        else if (SceneManager.GetActiveScene().name == "Level 1"){
            audioManager.changeBGM(ClipList[(int)AUDSTAT.INTRO]);
            defAudStat = AUDSTAT.INTRO;
        }

        else if (SceneManager.GetActiveScene().name == "Level 2"){
            audioManager.changeBGM(ClipList[(int)AUDSTAT.BRKDWN]);
            defAudStat = AUDSTAT.BRKDWN;
        }
            
        else if (SceneManager.GetActiveScene().name == "Level 3"){
            audioManager.changeBGM(ClipList[(int)AUDSTAT.INTRO]);
            defAudStat = AUDSTAT.INTRO;
        }
        else {
            audioManager.changeBGM(ClipList[(int)AUDSTAT.ENTSONG]);
            defAudStat = AUDSTAT.INTRO;
        }
    }

    int next = 0;
    // Update is called once per frame
    void Update()
    {
        if(curAudStat == AUDSTAT.SAFEPLANE) return;
        
        if(curAudStat != defAudStat){
            audioManager.SwapTrack(ClipList[(int)defAudStat]);
            curAudStat = defAudStat;
        }

    }
}
