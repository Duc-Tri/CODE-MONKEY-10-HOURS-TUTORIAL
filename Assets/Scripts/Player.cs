using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("RightArrow");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("LeftArrow");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("DownArrow");
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("UpArrow");
        }
    }
}
