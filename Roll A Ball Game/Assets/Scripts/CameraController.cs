using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;

    private void Start()
    {
        offset = transform.position - player.transform.position;
        DontDestroyOnLoad(this.gameObject);
    }

    private void FixedUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
