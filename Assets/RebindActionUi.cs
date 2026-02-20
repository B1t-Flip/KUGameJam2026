using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public InputActionReference actionReference;
    public int bindingIndex;
    public UnityEngine.UI.Text bindingText;

    private InputActionRebindingExtensions.RebindingOperation rebindingOperation;

    void Start()
    {
        UpdateBindingDisplay();
    }

    public void StartRebinding()
    {
        actionReference.action.Disable();

        rebindingOperation = actionReference.action
            .PerformInteractiveRebinding(bindingIndex)
            .OnComplete(operation =>
            {
                actionReference.action.Enable();
                operation.Dispose();
                UpdateBindingDisplay();
            });

        rebindingOperation.Start();
    }

    void UpdateBindingDisplay()
    {
        bindingText.text = InputControlPath.ToHumanReadableString(
            actionReference.action.bindings[bindingIndex].effectivePath,
            InputControlPath.HumanReadableStringOptions.OmitDevice);
    }
}
