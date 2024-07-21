using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameInput gameInput;

    private bool _isWalking;
    public bool IsWalking { get => _isWalking; }

    private void Update()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        if (inputVector != Vector2.zero)
        {
            _isWalking = true;
            Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);
            transform.position += moveDir * Time.deltaTime * moveSpeed;
            float rotateSpeed = 10;
            transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

            Debug.Log(inputVector);
        }
        else
            _isWalking = false;
    }

    private void Update1()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        if (inputVector != Vector2.zero)
        {
            Debug.Log("inputVector " + inputVector);

            _isWalking = true;

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
            _isWalking = false;
    }

}
