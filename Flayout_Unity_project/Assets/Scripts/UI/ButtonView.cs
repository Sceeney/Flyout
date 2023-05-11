using UnityEngine;

//[RequireComponent(typeof(Animator))]
public class ButtonView : UIView
{
    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _pressedColor;
    [SerializeField] private Color _hoverColor;

    //private Animator _selfAnimator;

    private void Start()
    {
        DefaultColor = _normalColor;
        //_selfAnimator = GetComponent<Animator>();
        ChangeColor(_normalColor);
    }

    public void OnMouseEnter()
    {
        ChangeColor(_hoverColor);
    }

    public void OnMouseDown()
    {
        ChangeColor(_pressedColor);
    }

    public void OnMouseUp()
    {
        ChangeColor(_hoverColor);
    }

    public void OnMouseExit()
    {
        ChangeColor(_normalColor);
    }

    public void OnPlay()
    {
        //_selfAnimator
            //.SetTrigger(ButtonAnimatorController.Parameters.OnDisable);
    }
}
