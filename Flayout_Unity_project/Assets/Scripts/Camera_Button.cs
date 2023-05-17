using UnityEngine;
using UnityEngine.Events;

public class Camera_Button : MonoBehaviour
{
    [SerializeField] private Camera_Trigger _trigger;
    [SerializeField] private Camera_Mover _mover;

    private event UnityAction<Transform> _click;

    private void OnEnable()
    {
        _click += _mover.OnClick;
    }

    private void OnDisable()
    {
        _click -= _mover.OnClick;
    }

    public void OnClick()
    {
        _click?.DynamicInvoke(_trigger.gameObject.transform);
    }
}
