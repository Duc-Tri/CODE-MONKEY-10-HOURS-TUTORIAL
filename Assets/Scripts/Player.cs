using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private GameInput gameInput;

    void Start()
    {

    }

    public bool IsWalking => isWalking;

    private bool isWalking;

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        if (inputVector != Vector2.zero)
        {
            Debug.Log("inputVector " + inputVector);

            isWalking = true;

            Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
            transform.position += moveDir * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
        else
            isWalking = false;
    }

}
