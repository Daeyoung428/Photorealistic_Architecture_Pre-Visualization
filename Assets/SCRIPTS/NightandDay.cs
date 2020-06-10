using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightandDay : MonoBehaviour {

    //speed at which the sun will rotate
    public float speed;

	// Update is called once per frame
	void Update () {

        //rotate this gameObject to the right (the x-axis) by speed and time
        transform.Rotate(Vector3.right * speed * Time.deltaTime);
    }
}
