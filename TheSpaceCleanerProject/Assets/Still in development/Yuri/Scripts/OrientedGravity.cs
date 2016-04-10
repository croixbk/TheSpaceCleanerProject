using UnityEngine;
using System.Collections;

public class OrientedGravity : MonoBehaviour {

    public GameObject refObject;
    public float gravityForce;
    Rigidbody rb;
    Vector3 dir;

	void Start () {
        rb = GetComponent<Rigidbody>();
    }

    void Update () {
        ApplyGravity();
	}

    void ApplyGravity() {
        //direcao do objeto para o objeto de referencia
        dir = refObject.transform.position - transform.position;
        //rotaciona o objeto para ficar em pé
        //se usado apenas o tranform.up ele se move irregularmente em alguns pontos
        transform.rotation = Quaternion.Slerp(transform.rotation
            ,Quaternion.FromToRotation(transform.up,-dir)*transform.rotation
            ,1.0f);
        rb.AddForce(dir.normalized*gravityForce, ForceMode.Force);
    }

}
