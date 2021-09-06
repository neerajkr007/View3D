using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMovement : MonoBehaviour
{
    public float sensi = 100f;
    public GameObject tilesMenu;
    Transform player;


    float horizontalRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        if(!tilesMenu.activeInHierarchy)
        {
            float x = Input.GetAxis("Mouse X") * sensi * Time.deltaTime;
            float y = Input.GetAxis("Mouse Y") * sensi * Time.deltaTime;

            horizontalRotation -= y;
            horizontalRotation = Mathf.Clamp(horizontalRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(horizontalRotation, 0f, 0f);

            player.Rotate(Vector3.up * x);
        }
        
    }
}
