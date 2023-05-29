using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bookPage : MonoBehaviour
{
    public Button backBtn;
    public Button writeBtn;
    public Button trashBtn;

    public GameObject page;
    public GameObject CreateBookPage;
    public GameObject bookSelves;

    // Start is called before the first frame update
    void Start()
    {
        backBtn.onClick.AddListener(() =>
        {
            page.SetActive(false);
            bookSelves.SetActive(true);
        });

        writeBtn.onClick.AddListener(() =>
        {
            bookSelves.SetActive(false);
            CreateBookPage.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
