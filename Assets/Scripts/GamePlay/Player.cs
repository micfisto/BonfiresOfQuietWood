using UnityEngine;
using Core;

public class Player : Character
{
    //movement settings
    [SerializeField] private float runningWithAcceleration = 10f;
    [SerializeField] private float holdToAccelerate = 1.8f;

    private float _holdTimer;
    private bool _isRunning;
    private float _currentSpeed;

    protected override void Awake()
    {
        base.Awake();
        stamina = staminaMax;
        _currentSpeed = speed;
    }

    private void FixedUpdate()
    {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();
        if (inputVector != Vector2.zero)
        {
            _holdTimer += Time.fixedDeltaTime;
            if (_holdTimer >= holdToAccelerate)
                _isRunning = true;
        }
        else
        {
            _holdTimer = 0f;
            _isRunning = false;
        }

        if (_isRunning)
        {
            stamina -= staminaDrainPerSecond * Time.fixedDeltaTime;
            if (stamina <= 0f)
            {
                stamina = 0f;
                _isRunning = false;
            }
        }
        else if (stamina < staminaMax)
        {
            stamina += staminaRestoredPerSecond * Time.fixedDeltaTime;
            if (stamina > staminaMax)
                stamina = staminaMax;
        }

        float targetSpeed = _isRunning ? runningWithAcceleration : speed;
        _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, Time.fixedDeltaTime * 5f);

        Move(inputVector.normalized, _currentSpeed);
    }

    protected override void Move(Vector2 inputVector, float moveSpeed)
    {
        if (inputVector == Vector2.zero)
            return;
        Rigidbody.MovePosition(Rigidbody.position + inputVector * (moveSpeed * Time.fixedDeltaTime));
    }
}