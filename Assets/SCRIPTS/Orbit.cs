using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{

    public Transform objectToOrbit;
    public float orbitSpeed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vectorToObject = objectToOrbit.position - transform.position;

        Debug.DrawRay(transform.position, vectorToObject,Color.red,.1f);

        if (Input.GetKey(KeyCode.W))
        {
            transform.position = Vector3.MoveTowards(transform.position, objectToOrbit.position, Time.deltaTime * 15f);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.position = Vector3.MoveTowards(transform.position, objectToOrbit.position, -Time.deltaTime * 15f);
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            float translation = Input.GetAxis("Vertical");
            float rotation = Input.GetAxis("Horizontal");

            transform.LookAt(objectToOrbit);
            transform.Translate(Vector3.right * Time.deltaTime * rotation * orbitSpeed);
            transform.Translate(Vector3.up * Time.deltaTime * translation * orbitSpeed);
        }
    }
}
