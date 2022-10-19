using UnityEngine;
using UnityEngine.Events;

public class GameTurnHandler : MonoBehaviour
{
    [Header("Win conditions")]
    [SerializeField, Min(1)] private int _maxTurnCount = 1;
    [SerializeField, Min(1)] private int _minCompletedGroupCount = 1;
    [SerializeField] private MessageDialog _messageDialog;
    [Space, Header("Events")]
    [SerializeField] private UnityEvent _gameOver;

    private int _turnCount;
    private int _completedGroupCount;

    public void OnGameTurnDone(bool groupCompleted)
    {
        _turnCount++;

        if (groupCompleted)
            _completedGroupCount++;

        CheckGameEndCondition();
    }

    public void OnNewGameStarted()
    {
        _turnCount = 0;
        _completedGroupCount = 0;
    }

    private void CheckGameEndCondition()
    {
        if (_completedGroupCount >= _minCompletedGroupCount)
            _messageDialog.Show("онаедю!", () => _gameOver.Invoke());
        else if (_turnCount >= _maxTurnCount)
            _messageDialog.Show("ньхайю", () => _gameOver.Invoke());
    }
}
