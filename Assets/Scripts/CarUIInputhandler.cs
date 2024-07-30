using UnityEngine;
using UnityEngine.UI;

public class CarUIInputHandler : MonoBehaviour
{
    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;

    private CarController carController;

    void Start()
    {
        // Verknüpfen der Events für die Buttons
        forwardButton.onClick.AddListener(() => OnButtonPress(ButtonType.Forward));
        backwardButton.onClick.AddListener(() => OnButtonPress(ButtonType.Backward));
        leftButton.onClick.AddListener(() => OnButtonPress(ButtonType.Left));
        rightButton.onClick.AddListener(() => OnButtonPress(ButtonType.Right));

        // Verknüpfen der PointerUp Events, um die Bewegung zu stoppen
        forwardButton.onClick.AddListener(() => OnButtonRelease(ButtonType.Forward));
        backwardButton.onClick.AddListener(() => OnButtonRelease(ButtonType.Backward));
        leftButton.onClick.AddListener(() => OnButtonRelease(ButtonType.Left));
        rightButton.onClick.AddListener(() => OnButtonRelease(ButtonType.Right));
    }

    void OnButtonPress(ButtonType buttonType)
    {
        if (carController == null) return;

        switch (buttonType)
        {
            case ButtonType.Forward:
                carController.Move(1);
                break;
            case ButtonType.Backward:
                carController.Move(-1);
                break;
            case ButtonType.Left:
                carController.Steer(-1);
                break;
            case ButtonType.Right:
                carController.Steer(1);
                break;
        }
    }

    void OnButtonRelease(ButtonType buttonType)
    {
        if (carController == null) return;

        switch (buttonType)
        {
            case ButtonType.Forward:
            case ButtonType.Backward:
                carController.Move(0);
                break;
            case ButtonType.Left:
            case ButtonType.Right:
                carController.Steer(0);
                break;
        }
    }

    public enum ButtonType { Forward, Backward, Left, Right }

    public void SetCarController(CarController controller)
    {
        carController = controller;
    }
}
