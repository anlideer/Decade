using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float scaleSpeed = 10f;
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        // Zoom out
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (cam.orthographicSize > 5f)
                cam.orthographicSize -= scaleSpeed * Time.deltaTime;
        }
        // Zoom in
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (cam.orthographicSize < 20f)
                cam.orthographicSize += scaleSpeed * Time.deltaTime;
        }

	}
}
