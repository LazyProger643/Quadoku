using System;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : MonoBehaviour
{
    [SerializeField] private Text _text;

    private Action _onCloseHandler;

    public void Show(string text, Action onClose)
    {
        _text.text = text;
        _onCloseHandler = onClose;

        gameObject.SetActive(true);
    }

    public void OnClick()
    {
        gameObject.SetActive(false);
        _onCloseHandler?.Invoke();
    }
}
