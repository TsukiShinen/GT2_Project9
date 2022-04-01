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

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

    }
    // Update is called once per frame
    void Update()
    {
        if(Input.mousePosition.y >= Screen.height * TopBarier || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * ScrollSpeed, Space.World);
        }
        if (Input.mousePosition.y <= Screen.height * BotBarier || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * ScrollSpeed, Space.World);
        }
        if (Input.mousePosition.x >= Screen.width * RightBarier || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * ScrollSpeed, Space.World);
        }
        if (Input.mousePosition.x <= Screen.width * LeftBarier || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * ScrollSpeed, Space.World);
        }
    }
}
