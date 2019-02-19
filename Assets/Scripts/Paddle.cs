using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 0));

        transform.position = new Vector2(Mathf.Clamp(mousePosition.x, 1.0f, 15.0f), transform.position.y);
    }
}
