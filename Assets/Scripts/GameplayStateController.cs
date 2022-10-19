using UnityEngine;
using UnityEngine.Events;

public class GameplayStateController : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private Slot _leftSlot;
    [SerializeField] private Slot _rightSlot;
    [SerializeField] private ItemSpawner _itemSpawner;
    [Space, Header("Events")]
    [SerializeField] private UnityEvent _gameplayStarted;

    private void Start()
    {
        StartGameplay();
    }

    public void StartGameplay()
    {
        _itemSpawner.Spawn(_board.Slots[3]);
        _itemSpawner.Spawn(_board.Slots[5]);
        _itemSpawner.Spawn(_leftSlot);

        _gameplayStarted.Invoke();
    }

    public void OnGameOver()
    {
        _board.Clear(() =>
        {
            _leftSlot.Clear(() =>
            {
                _rightSlot.Clear(() =>
                {
                    StartGameplay();
                });
            });
        });
    }
}
