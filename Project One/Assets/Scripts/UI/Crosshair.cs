using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Camera cam;
    Vector3 mousePos;
    Vector3 mouseScreenPos;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        mousePos = Input.mousePosition;
        mouseScreenPos = cam.ScreenToWorldPoint(mousePos);

        float tileSize = 1.0f; 
        float offsetX = tileSize / 2.0f;
        float offsetY = tileSize / 2.0f;

        transform.position = new Vector2(Mathf.Floor(mouseScreenPos.x / tileSize) * tileSize + offsetX,
                                         Mathf.Floor(mouseScreenPos.y / tileSize) * tileSize + offsetY);
    }

}
