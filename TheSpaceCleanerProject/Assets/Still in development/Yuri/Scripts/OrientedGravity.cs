using UnityEngine;
using System.Collections;

public class OrientedGravity : MonoBehaviour {

    public GameObject refObject;
    public float gravityForce;
    public Rigidbody rb;

	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
        ApplyGravity();
	}

    void ApplyGravity() {
        RaycastHit hit;
        Vector3 dir = refObject.transform.position - transform.position;
        if (Physics.Raycast(transform.position, dir,out hit, Mathf.Infinity)) {
            rb.AddForce(new Vector3(
                hit.point.x*-gravityForce,
                hit.point.y*-gravityForce,
                hit.point.z*-gravityForce), ForceMode.Force);
           //transform.up = hit.normal;
            print(hit.point);
        }
        else{
            print("Passou reto");
        }
        
    }
}
