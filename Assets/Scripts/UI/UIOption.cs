using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOption : MonoBehaviour
{
    [SerializeField] Button _back;

    void Start()
    {
        _back.onClick.AddListener(ToMenu);
    }

    private void ToMenu() {
        Manager.Instance.LoadScene(Manager.Scene.MainMenu);
    }

    
}
