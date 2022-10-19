using System;
using System.Collections;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(RectTransform))]
public sealed class ItemSizeChanger : MonoBehaviour
{
    private RectTransform _rectTransform;
    private Coroutine _coroutine;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void Change(float size, Action completed)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeSize(size, completed));
    }

    private IEnumerator ChangeSize(float size, Action completed)
    {
        Vector2 targetSize = new Vector2(size, size);
        float speed = (targetSize - _rectTransform.sizeDelta).magnitude / GameParameters.ItemSizeChangeDuration;
        float elapsedTime = 0;

        while (elapsedTime < GameParameters.ItemSizeChangeDuration)
        {
            _rectTransform.sizeDelta = Vector2.MoveTowards(_rectTransform.sizeDelta, targetSize, speed * Time.deltaTime);

            yield return null;

            elapsedTime += Time.deltaTime;
        }

        _rectTransform.sizeDelta = targetSize;
        _coroutine = null;

        completed?.Invoke();
    }
}
