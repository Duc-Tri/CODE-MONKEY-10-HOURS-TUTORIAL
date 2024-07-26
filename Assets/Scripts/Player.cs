using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayermask;

    private bool _isWalking;
    private Vector3 _lastInteractDir;

    public bool IsWalking { get => _isWalking; }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero)
        {
            _lastInteractDir = moveDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, _lastInteractDir, out RaycastHit raycasthit, interactDistance, counterLayermask))
        {
            Debug.Log("* " + raycasthit.transform);
            if (raycasthit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                // has clearcounter
                clearCounter.Interact();
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        if (inputVector == Vector2.zero)
        {
            _isWalking = false;
            return;
        }

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerRadius = .7f;
        float playerHeight = 2f;
        float moveDistance = Time.deltaTime * moveSpeed;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        // cannot move towards moveDir
        if (!canMove)
        {
            // attenpt only X movement
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
                // can move only on the X
                moveDir = moveDirX;
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                    // can move only on the Z
                    moveDir = moveDirZ;
                else
                {
                    // cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }

        _isWalking = moveDir != Vector3.zero;
        float rotateSpeed = 10;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);

        //Debug.Log(inputVector);

    }
}
