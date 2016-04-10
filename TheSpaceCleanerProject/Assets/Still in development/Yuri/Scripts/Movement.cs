using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

    public float velocity;
    public float maxVelocity;
    public float teste;
    protected Rigidbody rb;

    protected void AddForceToDirection(float dirX, float dirY)
    {
        //--OLD--adiciona as forcas separadamente para os eixos horizontal e vertical
        // rb.AddForce(Vector3.Slerp(transform.position, ((dirY) * 1000f * transform.forward) * velocity * Time.fixedDeltaTime, 1.0f),ForceMode.Acceleration);
        //rb.AddForce(Vector3.Slerp(transform.position, ((dirx) * 1000f * transform.right) * velocity * Time.fixedDeltaTime, 1.0f), ForceMode.Acceleration);

        //Soma os dois vetores right e forward multiplicados pelas direçoes para conseguir a direçao
        rb.AddForce(Vector3.Slerp(transform.position,
            ((transform.forward * dirY + transform.right * dirX) * 1000f) * velocity * Time.fixedDeltaTime, 
            1.0f), ForceMode.Acceleration);

    }

    protected void MoveForwardToDirection(Vector3 dir)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.forward,
            dir) * transform.rotation, teste);
        // if(dir.magnitude > 0)
        //  rb.AddForce(Vector3.Slerp(transform.position, transform.forward *1000f * velocity * Time.fixedDeltaTime, 1.0f));
    }

    protected bool LimitVelocity()
    {
        Vector3 vel = rb.velocity;//velocidade atual
        if (vel.sqrMagnitude > (maxVelocity * maxVelocity))//sqr eh mais rapido
        {
            //pega a velocidade atual e multiplica pela velocidade do freio(velocidade atual - velocidade maxima) 
            rb.AddForce(-(vel.normalized * (vel.sqrMagnitude - (maxVelocity * maxVelocity))));
            return true;
        }
        //print(vel + " - " + vel.magnitude);
        //retorna falso se ele nao alterou a velocidade do objeto
        return false;
    }

    public bool GracefulSlowDown(float initialVel, float initialMaxVel)
    {
        return true;

    }
}
