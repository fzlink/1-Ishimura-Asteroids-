
using UnityEngine;

public class GameOverUI : MonoBehaviour
{

    [SerializeField] private GameStateController GameStateController;
    [SerializeField] private Transform Root;
    
    // Start is called before the first frame update
    void Start()
    {
        Root.gameObject.SetActive(false);
        GameStateController.OnGameOver += OnGameOver;
    }

    private void OnGameOver()
    {
        Root.gameObject.SetActive(true);
    }

}
