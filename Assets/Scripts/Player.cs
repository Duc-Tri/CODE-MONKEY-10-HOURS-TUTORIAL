using System;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public static Player Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter eaSelectedCounter;
    }

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayermask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    private KitchenObject kitchenObject;

    private bool _isWalking;
    private Vector3 _lastInteractDir;
    private BaseCounter selectedCounter;

    public bool IsWalking { get => _isWalking; }
    public KitchenObject KitchenObject
    {
        get => kitchenObject;
        set => kitchenObject = value;
    }

    // before Start of subscribers
    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("More than one instance of Player !");

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter != null)
            selectedCounter.InteractAlternate(this);
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (selectedCounter != null)
            selectedCounter.Interact(this);
    }

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
            if (raycasthit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                // has counter
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);

                    //if (selectedCounter != null) Debug.Log(selectedCounter);
                }
            }
            else
                SetSelectedCounter(null);
        }
        else
            SetSelectedCounter(null);
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
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if (canMove)
                // can move only on the X
                moveDir = moveDirX;
            else
            {
                // attempt only Z movement
                Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

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
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            eaSelectedCounter = selectedCounter
        });
    }

    // ========== IKitchenObjectParent
    public Transform GetKitchenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject ko)
    {
        kitchenObject = ko;
        Debug.Log(this + " ► SetKitchenObject ► " + kitchenObject);
    }

    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }

    public bool HasKitchenObject() => (kitchenObject != null);

}
