using UnityEngine;

public class CarControlUI : MonoBehaviour
{
    public CarController carController; // Verknüpfe dies im Inspector mit deinem Auto-Prefab

    public void MoveForward()
    {
        carController.Move(1);
    }

    public void MoveBackward()
    {
        carController.Move(-1);
    }

    public void StopMove()
    {
        carController.Move(0);
    }

    public void TurnLeft()
    {
        carController.Steer(-1);
    }

    public void TurnRight()
    {
        carController.Steer(1);
    }

    public void StopTurn()
    {
        carController.Steer(0);
    }
}
