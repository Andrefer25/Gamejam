using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    private GameObject Player;
    public Transform CameraLimitStart;
    public Transform CameraLimitEnd;
    private Vector3 TargetPos;
    [SerializeField] private float CameraOffset;
    [SerializeField] private float CameraSmoothing;

    // Update is called once per frame
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void FixedUpdate()
    {
        TargetPos = Player.transform.position;

        if (Player.transform.localScale.x == 1)
        {
            TargetPos = new Vector3(Mathf.Clamp(TargetPos.x + CameraOffset, CameraLimitStart.position.x, CameraLimitEnd.position.x), TargetPos.y + 1, transform.position.z);
        }
        if (Player.transform.localScale.x == -1)
        {
            TargetPos = new Vector3(Mathf.Clamp(TargetPos.x - CameraOffset, CameraLimitStart.position.x, CameraLimitEnd.position.x), TargetPos.y + 1, transform.position.z);
        }

        transform.position = Vector3.Lerp(transform.position, TargetPos, CameraSmoothing * Time.deltaTime);
    }
}
