using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using UnityEngine.Events;

[RequireComponent(typeof(Image))]
public class UIView : MonoBehaviour
{
    [SerializeField] protected Image ChangedImage;
    protected Color DefaultColor;

    [SerializeField] private float _changeColorDurationInSeconds;
    private Coroutine _changeColorCoroutine;
    private Color _targetColor;

    [SerializeField] private UnityEvent _endChangeColor;

    private void Awake()
    {
        if (ChangedImage == null)
            ChangedImage = GetComponent<Image>();
    }

    private void OnDisable()
    {
        ChangedImage.color = DefaultColor;
    }

    protected void ChangeColor(Color targetColor)
    {
        _targetColor = targetColor;

        if ((_changeColorCoroutine == null) == false)
        {
            StopCoroutine(_changeColorCoroutine);
        }

        _changeColorCoroutine = StartCoroutine(ChangeColor());
    }

   private IEnumerator ChangeColor()
   {
      Tween change = ChangedImage
                           .DOColor(_targetColor, _changeColorDurationInSeconds);

       yield return change.WaitForCompletion();

       _endChangeColor?.Invoke();
   }
}
