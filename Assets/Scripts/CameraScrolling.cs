using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScrolling : MonoBehaviour
{
    public float ScrollSpeed;
    public float TopBarier;
    public float BotBarier;
    public float LeftBarier;
    public float RightBarier;

    // Update is called once per frame
    void Update()
    {
        if(Input.mousePosition.y >= Screen.height * TopBarier)
        {
            transform.Translate(Vector3.up * Time.deltaTime * ScrollSpeed, Space.World);
        }
        if (Input.mousePosition.y <= Screen.height * BotBarier)
        {
            transform.Translate(Vector3.down * Time.deltaTime * ScrollSpeed, Space.World);
        }
        if (Input.mousePosition.x >= Screen.width * RightBarier)
        {
            transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
        }
        if (Input.mousePosition.x <= Screen.width * LeftBarier)
        {
            transform.Translate(Vector3.left * Time.deltaTime * ScrollSpeed, Space.World);
        }
    }
}
