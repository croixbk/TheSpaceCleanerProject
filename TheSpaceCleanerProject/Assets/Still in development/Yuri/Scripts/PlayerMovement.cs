using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float velocity;
    public float maxVelocity;
    public float screenUsePercentage;
    Camera mc;
    Vector2 screenSize;
    Vector2 edges;
    Rigidbody rb;
    void Start () {
        rb = GetComponent<Rigidbody>();
        mc = Camera.main;
        screenSize = new Vector2(mc.pixelWidth,mc.pixelHeight);
        //valor do tamanho reduzido da tela
        edges = new Vector2(mc.pixelWidth * (screenUsePercentage/100),
                            mc.pixelHeight * (screenUsePercentage/100));
	}
	
	void FixedUpdate () {
        print(Input.mousePosition + " - " + (Input.mousePosition.x < (screenSize.x - edges.x) ||
                  Input.mousePosition.x > edges.x ||
                  Input.mousePosition.y < (screenSize.y - edges.y) ||
                  Input.mousePosition.y > edges.y));
        //Fixed por recomendacao da unity quando se mexe com fisica no rb
        Move();
	}

    void Move(){
        if (Input.GetMouseButton(0)) {
            //checa se ele esta dentro das bordas
            if (!(Input.mousePosition.x < (screenSize.x - edges.x) ||
                  Input.mousePosition.x > edges.x ||
                  Input.mousePosition.y < (screenSize.y - edges.y) ||
                  Input.mousePosition.y > edges.y))
            {         
                Vector3 dir =
                    new Vector3(
                        //tamanho dividido por 2 para permitir a divisao dos polos positivo e negativo
                        ((Input.mousePosition.x) - screenSize.x / 2),
                        0,
                        (Input.mousePosition.y) - screenSize.y / 2);
                //adiciona as forcas separadamente para o eixo horizontal e vertical
                rb.AddForce(Vector3.Slerp(transform.position, ((dir.x * 9.5f) * transform.right) * velocity * Time.fixedDeltaTime, 1.0f));
                rb.AddForce(Vector3.Slerp(transform.position, ((dir.z * 9.5f) * transform.forward) * velocity * Time.fixedDeltaTime, 1.0f));
               // print(screenSize.x + " - " + ((Input.mousePosition.x) - screenSize.x / 2));
                LimitVelocity();
            }
        }   
    }


    bool LimitVelocity(){
        
        Vector3 vel = rb.velocity;//velocidade atual
        if (vel.sqrMagnitude > (maxVelocity * maxVelocity))//sqr eh mais rapido
        {
            //pega a velocidade atual e multiplica pela velocidade do freio(velocidade atual - velocidade maxima) 
            rb.AddForce(-(vel.normalized *(vel.sqrMagnitude - (maxVelocity*maxVelocity))));
            return true;
        }
       //print(vel + " - " + vel.magnitude);
        //retorna falso se ele nao alterou a velocidade do objeto
        return false;
    }
}
