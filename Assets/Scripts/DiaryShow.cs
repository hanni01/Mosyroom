using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryShow : MonoBehaviour
{
    public Button bookBtn;
    public Button cancelBtn;

    public GameObject DiaryCanvas;
    public Toggle mode;

    private void Start()
    {
        bookBtn.onClick.AddListener(() => {
            DiaryCanvas.SetActive(true);
            mode.interactable = false;
        });

        cancelBtn.onClick.AddListener(() =>
        {
            DiaryCanvas.SetActive(false);
            mode.interactable = true;
        });
    }
}
