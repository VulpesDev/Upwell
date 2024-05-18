using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] Button _newGame;
    [SerializeField] Button _option;
    // [SerializeField] Button _exit;
    // Start is called before the first frame update
    void Start()
    {
        _newGame.onClick.AddListener(StartNewGame);
        _option.onClick.AddListener(Option);
        // _exit.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame() {
        Manager.Instance.LoadScene(Manager.Scene.MovementTest);
    }

    private void Option() {
        Manager.Instance.LoadScene(Manager.Scene.Option);
    }
}
