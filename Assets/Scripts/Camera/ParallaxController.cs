using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    private Transform cameraTransform;
    private Vector3 previousCameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        previousCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previousCameraPosition.x) * 0.7f;
        float deltaY = (cameraTransform.position.y - previousCameraPosition.y) * 0.9f;
        transform.Translate(new Vector3(deltaX, deltaY, 0));
        previousCameraPosition = cameraTransform.position;
    }
}
