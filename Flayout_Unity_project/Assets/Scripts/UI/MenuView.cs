using UnityEngine;

public class MenuView : UIView
{
    [SerializeField] private Color _enabledColor;
    [SerializeField] private Color _disabledColor;
    [SerializeField] private GameObject _menuPanel;

    private bool _isPause;

    private void Start()
    {
        DefaultColor = _disabledColor;
        _menuPanel.SetActive(false);
    }

    public void OnPause()
    {
        _isPause = true;
        _menuPanel.SetActive(true);
        ChangeColor(_enabledColor);
    }

    public void OnPlay()
    {
        _isPause = false;
        ChangeColor(_disabledColor);
    }

    public void OnEndChangeColor()
    {
        if (_isPause == false)
            _menuPanel.SetActive(false);
    }
}
