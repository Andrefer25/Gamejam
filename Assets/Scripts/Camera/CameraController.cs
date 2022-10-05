using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject Player;

    // Update is called once per frame
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Player.transform.position.x;
        position.y = Player.transform.position.y + 1;
        transform.position = position;
    }
}
