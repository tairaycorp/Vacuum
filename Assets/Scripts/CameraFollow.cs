using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public float bounds = 3f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if(target)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // target.position + GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 point = (GetComponent<Camera>().WorldToViewportPoint(target.position) + mousePosition) / 2;
            Vector3 delta = (target.position) - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = ((transform.position + transform.position + delta + transform.position + transform.position + transform.position + transform.position) + mousePosition) / 7;
            destination = new Vector3(Mathf.Clamp(destination.x, target.position.x - bounds, target.position.x + bounds), Mathf.Clamp(destination.y, target.position.y - bounds, target.position.y + bounds), -10f);
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
            // transform.position = new Vector3(Mathf.Clamp(transform.position.x, target.position.x - bounds, target.position.x + bounds), Mathf.Clamp(transform.position.y, target.position.y - bounds, target.position.y + bounds), -10f);
        }
    }
}

