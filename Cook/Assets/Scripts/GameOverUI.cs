using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textMeshProUGUI1;
    [SerializeField]private TextMeshProUGUI textMeshProUGUI2;
    [SerializeField]private TextMeshProUGUI textMeshProUGUI3;
    [SerializeField]private Button escButton;

    private void Start()
    {
        Hide();
        escButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.start);
        });
    }

    public void Hide()
    {
        textMeshProUGUI1.enabled = false;
        textMeshProUGUI2.enabled = false;
        textMeshProUGUI3.enabled = false;
        this.gameObject.SetActive(false);
    }
    public void Show()
    {
        textMeshProUGUI1.enabled=true;
        textMeshProUGUI2.enabled=true;
        textMeshProUGUI3.enabled=true;
        this.gameObject.SetActive(true);
    }
    public void SetNumber(string num)
    {
        textMeshProUGUI3.text = num;
    }
}


