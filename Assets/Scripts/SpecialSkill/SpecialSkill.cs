using System.Collections;
using UnityEngine;

public class SpecialSkill : MonoBehaviour {

    public int SPMax = 20;
    private int SPcounter;

    // Use this for initialization
    void Start() {
        SPcounter = 0;
    }

    // Update is called once per frame
    void Update() {
        if(SPcounter < SPMax) return;

        //吸收情绪技能效果：使情绪条充满至100，进入子弹时间。前三发子弹不与外界交换情绪。
        if(Input.GetKeyDown(KeyCode.Q)){

        }
        //释放情绪技能效果：使情绪释放至-100，进入额外高速移动。在固定时间内将情绪固定至-100，不与外界交换情绪。
        else if(Input.GetKeyDown(KeyCode.E)){

        }
        SPcounter = 0;
    }

    public void SPadd(){
        SPcounter++;
    }
    //吸收情绪和释放情绪
    //技能条积攒方式：击中敌人获得SP，每击中一次SP值加1。两个技能共用一个条，通过按键选择是用哪个技能。

    




}