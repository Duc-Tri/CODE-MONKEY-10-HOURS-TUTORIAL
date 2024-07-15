using JetBrains.Annotations;
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

            float playerRadius = .7f;
            float playerHeight = 2f;
            float moveDistance = moveSpeed * Time.deltaTime;
            bool canMove = !Physics.CapsuleCast(transform.position,
                transform.position + Vector3.up * playerHeight,
                playerRadius,
                moveDir,
                moveDistance);

            // canont move towards movedir
            if (!canMove)
            {
                // attempt only X movement
                Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
                canMove = !Physics.CapsuleCast(transform.position,
                   transform.position + Vector3.up * playerHeight,
                   playerRadius,
                   moveDirX,
                   moveDistance);

                if (canMove)
                    moveDir = moveDirX; // can move only on X
                else
                {
                    // attempt only Z movement
                    Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                    canMove = !Physics.CapsuleCast(transform.position,
                       transform.position + Vector3.up * playerHeight,
                       playerRadius,
                       moveDirZ,
                       moveDistance);

                    if (canMove)
                        moveDir = moveDirZ; // can move only on Z
                    else
                    {
                        // cannot move at all 
                    }
                }
            }

            if (canMove)
            {
                transform.position += moveDir * moveDistance;
            }

            float rotateSpeed = 10f;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        }
        else
            isWalking = false;
    }

}