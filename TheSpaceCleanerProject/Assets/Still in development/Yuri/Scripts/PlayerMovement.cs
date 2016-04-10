using UnityEngine;
using System.Collections;

public class PlayerMovement : Movement {
    public MyInput playerInput;
    public static string testDirections;
    public float screenUsePercentage;
    public bool mouse, teclado;
    Camera mc;

    void Start () {
        rb = GetComponent<Rigidbody>();
        mc = Camera.main;        
	}
	
	void FixedUpdate () {
        //Fixed por recomendacao da unity quando se mexe com fisica no rb
        if(teclado) KeyboardInput();
        if(mouse) MouseInput();
        /*----RETIRADO -- edges = new Vector2(mc.pixelWidth * (screenUsePercentage / 100),
                            mc.pixelHeight * (screenUsePercentage / 100));*/
    }

    public void KeyboardInput() {
        AddForceToDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        LimitVelocity();
        print(Input.GetAxis("Horizontal") + " - " + Input.GetAxis("Vertical"));
    }

    public void MouseInput() {
        if (Input.GetMouseButton(0))
        {
            //checa se ele esta dentro das bordas
            //(mc.pixelWidth * (screenUsePercentage / 100)) porcentagem da tela usada
            //mc.picelWidth - mc.pixelWidth * (screenUsePercentage / 100)
            if (!(Input.mousePosition.x < (mc.pixelWidth - mc.pixelWidth * (screenUsePercentage / 100)) ||
                  Input.mousePosition.x > mc.pixelWidth * (screenUsePercentage / 100) ||
                  Input.mousePosition.y < (mc.pixelHeight - mc.pixelHeight * (screenUsePercentage / 100)) ||
                  Input.mousePosition.y > mc.pixelHeight * (screenUsePercentage / 100)))
            {
                Vector3 dir =
                    new Vector2(
                        //tamanho dividido por 2 para permitir a divisao dos polos positivo e negativo e multiplicado por 2 para manter sua magnitude inalterada
                        (((Input.mousePosition.x) - (mc.pixelWidth / 2)) * 2),
                        ((Input.mousePosition.y) - (mc.pixelHeight / 2)) * 2);
                //direçao dividida pelo tamanho da tela para manter o valor de -1 a 1
                AddForceToDirection(dir.x/mc.pixelWidth, dir.y/mc.pixelHeight);
                LimitVelocity();
                testDirections = "x: " + dir.x + " - y: " + dir.z;
            }
        }
    }

}
