using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelecter : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [SerializeField] private Load_Screen _load;

    [SerializeField] private List<Button> _levels;

    private void OnEnable()
    {
        if (_levels.Count > 0)
            Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    public void SetNewLevelButton(Button button)
    {
        _levels.Add(button);
        Subscribe();
    }

    private void Subscribe()
    {
        foreach (var level in _levels)
        {
            level.onClick.AddListener(_load.Load);
        }
    }

    private void Unsubscribe()
    {
        foreach (var level in _levels)
        {
            level.onClick.RemoveListener(_load.Load);
        }
    }
}
