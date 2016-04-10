using UnityEngine;
using System.Collections;

public class Skill_Boost : MonoBehaviour {

    public float speedBoost;
    public float velGrowFactor;
    public float maxBoostTime;
    float initialMaxVel;
    float initialVel;
    MyInput input = null;
    Movement body = null;

    bool onBoost;
    float boostTime;

    public Skill_Boost(float velocity, float maxVelocity) {
        initialVel = velocity;
        initialMaxVel = maxVelocity;
    }

    void Start() {
        if (transform.tag == "Player"){
            body = GetComponent<PlayerMovement>();
            input = GetComponent<MyInput>();
        }

        if (transform.tag == "Enemy")
            body = GetComponent<EnemyMovement>();

        initialVel = body.velocity;
        initialMaxVel = body.maxVelocity;
    }

    void Update() {
        ActiveUse();
    }

    public void ActiveUse() {
        /*
         Estou em um Boost?
            sim
             |   aumenta tempo atual no boost
             |   tempo de boost atual ultrapassou o limite?
             |       sim
             |        |-  inicia frenagem e aguarda até voltar a velocidade normal 
             |        |-  voltei a velocidade normal? se sim, não estou em um boost
             |       não 
             |        |-  aumento minha velocidade atual
             |
            não 
             |     ativei a skill?
             |        sim
             |         |-  aumenta a velocidade maxima
             |         |-  aumenta velocidade atual
             |         |-  agora estou em um Boost
             |        não
             |         |-  não faz nada 
             _______________________________________________________________________  
        */
        if (input != null) {
            if (onBoost) {
                boostTime += Time.deltaTime;
                if (boostTime >= maxBoostTime) {
                    if (body.GracefulSlowDown(initialVel, initialMaxVel))
                        onBoost = false;
                }else
                    body.velocity += velGrowFactor * 2 * Time.deltaTime;
            }
            else if (input.getDoubleTouch()){
                body.maxVelocity = initialMaxVel + speedBoost;
                body.velocity += velGrowFactor *2 * Time.deltaTime;
                onBoost = true;
            }          
        }
    }

    public void PassiveUse() {

    }

    public void AutomaticUse() {
        if (input != null)
        {
            if (input.getDoubleTouch()/*if it stays in one direction*/)
            {
                body.maxVelocity += speedBoost;
                body.velocity += velGrowFactor * Time.deltaTime;
            }
        }
    }
}
