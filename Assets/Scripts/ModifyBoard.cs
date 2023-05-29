using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DiaryDataSetting;
using Random = UnityEngine.Random;

public class ModifyBoard : MonoBehaviour
{
    public Button completeBtn;
    public Button cancelModifyBtn;
    public GameObject createCanvas;
    public GameObject contentsPage;
    public GameObject panel1;
    public GameObject panel2;

    public GameObject bookShelves;

    public TextMeshProUGUI title;
    public TextMeshProUGUI contents;
    public TextMeshProUGUI writer;
    public TextMeshProUGUI date;

    public TMP_InputField title_input;
    public TMP_InputField contents_input;
    public TextMeshProUGUI createDate;
    public TextMeshProUGUI createWriter;

    private GameObject[] diaryBook;

    private void Awake()
    {
        createDate.text = DateTime.Now.ToString("yyyy-MM-dd");
        createWriter.text = "한니";
        diaryBook = Resources.LoadAll<GameObject>("Prefabs_book");
    }

    void Start()
    {
        completeBtn.onClick.AddListener(() =>
        {
            StartCoroutine(InsertBoardData(title_input.text, contents_input.text));
            completeBtn.interactable = false;
        });

        cancelModifyBtn.onClick.AddListener(() =>
        {
            createCanvas.SetActive(false);
            bookShelves.SetActive(true);
        });
    }

    IEnumerator InsertBoardData(string title, string contents)
    {
        WWWForm form = new WWWForm();
        form.AddField("title", title);
        form.AddField("contents", contents);
        form.AddField("date", createDate.text);
        form.AddField("writer", createWriter.text);

        using(UnityWebRequest www = UnityWebRequest.Post("http://localhost/mosyroomDB/insert.php", form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }

        GameObject prefabInstance = Instantiate(diaryBook[Random.Range(0, 4)]);
        if(panel1.transform.childCount == 12)
        {
            prefabInstance.transform.SetParent(panel2.transform);
            prefabInstance.transform.localScale = new Vector3(10, 8, 0);
        }
        prefabInstance.transform.SetParent(panel1.transform);
        prefabInstance.transform.localScale = new Vector3(10, 8, 0);

        prefabInstance.transform.GetComponent<Button>().onClick.AddListener(UpdateBoardData);
    }

    private void UpdateBoardData()
    {
        bookShelves.SetActive(false);
        contentsPage.SetActive(true);

        title.text = title_input.text;
        writer.text = createWriter.text;
        date.text = createDate.text;
        contents.text = contents_input.text;
    }
}
