using UnityEngine;
using UnityEngine.InputSystem;

public class CarInputHandler : MonoBehaviour
{
    public CarController carController; // Öffentliche Referenz auf das CarController-Skript

    public CarControls carControls;

    public void Awake()
    {
        carControls = new CarControls();
    }

    public void OnEnable()
    {
        carControls.Enable();
        carControls.Car.MoveForward.performed += OnMoveForward;
        carControls.Car.MoveForward.canceled += OnMoveCanceled;
        carControls.Car.MoveBackward.performed += OnMoveBackward;
        carControls.Car.MoveBackward.canceled += OnMoveCanceled;
        carControls.Car.TurnLeft.performed += OnTurnLeft;
        carControls.Car.TurnLeft.canceled += OnTurnCanceled;
        carControls.Car.TurnRight.performed += OnTurnRight;
        carControls.Car.TurnRight.canceled += OnTurnCanceled;
    }

    public void OnDisable()
    {
        carControls.Disable();
        carControls.Car.MoveForward.performed -= OnMoveForward;
        carControls.Car.MoveForward.canceled -= OnMoveCanceled;
        carControls.Car.MoveBackward.performed -= OnMoveBackward;
        carControls.Car.MoveBackward.canceled -= OnMoveCanceled;
        carControls.Car.TurnLeft.performed -= OnTurnLeft;
        carControls.Car.TurnLeft.canceled -= OnTurnCanceled;
        carControls.Car.TurnRight.performed -= OnTurnRight;
        carControls.Car.TurnRight.canceled -= OnTurnCanceled;
    }

    public void OnMoveForward(InputAction.CallbackContext context)
    {
        if (carController != null)
        {
            carController.Move(1);
        }
    }

    public void OnMoveBackward(InputAction.CallbackContext context)
    {
        if (carController != null)
        {
            carController.Move(-1);
        }
    }

    public void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (carController != null)
        {
            carController.Move(0);
        }
    }

    public void OnTurnLeft(InputAction.CallbackContext context)
    {
        if (carController != null)
        {
            carController.Steer(-1);
        }
    }

    public void OnTurnRight(InputAction.CallbackContext context)
    {
        if (carController != null)
        {
            carController.Steer(1);
        }
    }

    public void OnTurnCanceled(InputAction.CallbackContext context)
    {
        if (carController != null)
        {
            carController.Steer(0);
        }
    }

    // Öffentliche Methoden für die UI-Buttons
    public void MoveForward()
    {
        if (carController != null)
        {
            carController.Move(1);
        }
    }

    public void MoveBackward()
    {
        if (carController != null)
        {
            carController.Move(-1);
        }
    }

    public void StopMove()
    {
        if (carController != null)
        {
            carController.Move(0);
        }
    }

    public void TurnLeft()
    {
        if (carController != null)
        {
            carController.Steer(-1);
        }
    }

    public void TurnRight()
    {
        if (carController != null)
        {
            carController.Steer(1);
        }
    }

    public void StopTurn()
    {
        if (carController != null)
        {
            carController.Steer(0);
        }
    }
}
