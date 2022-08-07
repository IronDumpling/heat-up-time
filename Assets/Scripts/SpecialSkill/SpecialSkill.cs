using System.Collections;
using UnityEngine;

public class SpecialSkill : MonoBehaviour {

    public int SPMax = 20;
    public int SPcounter;


    public float CoolTimeScale = 3f;
    public float CooltimePeriod = 3f;

    public bool countTime = false;
    public float curtime;
    
    TimeScaleEditor TSE;


    public int HeatBulletCount = 3;
    PlayerHeat plyHeat;
    ShootBullets shootBullets;

    // Use this for initialization
    void Start() {
        SPcounter = 0;
        TSE = FindObjectOfType<TimeScaleEditor>();
        plyHeat = GetComponent<PlayerHeat>();
        shootBullets = GetComponent<ShootBullets>();
    }

    // Update is called once per frame
    void Update() {
        if (countTime && timer()) {
            TSE.ReleaseUnsetTimeScale();
            plyHeat.disableBalance = false;
            countTime = false;
            curtime = 0;
        }
        if (SPcounter < SPMax) return;

        //吸收情绪技能效果：使情绪条充满至100，进入子弹时间。前三发子弹不与外界交换情绪。
        if (Input.GetKeyDown(KeyCode.Q)){
            plyHeat.forceMaxHeat();
            shootBullets.specialBulletCount = HeatBulletCount;

            SPcounter = 0;

        }
        //释放情绪技能效果：使情绪释放至-100，进入额外高速移动。在固定时间内将情绪固定至-100，不与外界交换情绪。
        else if(Input.GetKeyDown(KeyCode.E)){
            TSE.ForceSetTimeScale(CoolTimeScale);
            plyHeat.forceMinHeat();
            plyHeat.disableBalance = true;
            countTime = true;

            SPcounter = 0;

        }
    }

    public void SPadd(){
        SPcounter++;
    }
    
    private bool timer() {
        curtime += Time.deltaTime;
        if(curtime > CooltimePeriod) {
            return true;
        }
        return false;
    }

    




}