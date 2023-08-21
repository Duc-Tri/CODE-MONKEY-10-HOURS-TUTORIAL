using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        //Vector2.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("RightArrow");
            inputVector.x += 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("LeftArrow");
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("DownArrow");
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("UpArrow");
            inputVector.y += 1;
        }

        return inputVector.normalized;
    }

}
