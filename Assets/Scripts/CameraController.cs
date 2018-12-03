using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float scaleSpeed = 10f;
    Camera cam;
    bool isMoving;
    Vector3 last = new Vector3();


	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        isMoving = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (cam.orthographicSize > 5f)
                cam.orthographicSize -= scaleSpeed * Time.deltaTime;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (cam.orthographicSize < 20f)
                cam.orthographicSize += scaleSpeed * Time.deltaTime;
        }
        if (Input.GetMouseButtonDown(2))
        {
            isMoving = true;
            last = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(2))
        {
            isMoving = false;
        }
        
        if (isMoving)
        {
            Vector3 tmp = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(last);
            last = Input.mousePosition;
            transform.position = new Vector3(transform.position.x - tmp.x, transform.position.y - tmp.y, transform.position.z);
            if (transform.position.x < -45f)
                transform.position = new Vector3(-45f, transform.position.y, transform.position.z);
            else if (transform.position.x > 32f)
                transform.position = new Vector3(32f, transform.position.y, transform.position.z);
            if (transform.position.y < -19f)
                transform.position = new Vector3(transform.position.x, -19f, transform.position.z);
            else if (transform.position.y > 22f)
                transform.position = new Vector3(transform.position.x, 22f, transform.position.z);
        }
    }
}
