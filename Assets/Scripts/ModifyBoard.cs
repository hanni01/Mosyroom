using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

    public TextMeshProUGUI boardID;
    public TextMeshProUGUI title;
    public TextMeshProUGUI contents;
    public TextMeshProUGUI writer;
    public TextMeshProUGUI date;

    public TMP_InputField title_input;
    public TMP_InputField contents_input;
    public TextMeshProUGUI createDate;
    public TextMeshProUGUI createWriter;

    int lastInsertID;
    private GameObject[] diaryBook;

    public GameObject currentclickedNew;

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

            createCanvas.SetActive(false);
            bookShelves.SetActive(true);
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

        using(UnityWebRequest www = UnityWebRequest.Post("http://172.30.1.50/mosyroomDB/insert.php", form))
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

            string response = www.downloadHandler.text;
            lastInsertID = Int32.Parse(response);
        }

        GameObject prefabInstance = Instantiate(diaryBook[Random.Range(0, 4)]);
        if(panel1.transform.childCount >= 12)
        {
            prefabInstance.transform.SetParent(panel2.transform);
            prefabInstance.transform.localScale = new Vector3(10, 8, 0);
        }
        else
        {
            prefabInstance.transform.SetParent(panel1.transform);
            prefabInstance.transform.localScale = new Vector3(10, 8, 0);
        }

        prefabInstance.transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            StartCoroutine(UpdateBoardData());
            currentclickedNew = EventSystem.current.currentSelectedGameObject;
            GameObject.Find("BookContentsCanvas").GetComponent<bookPage>().checkWhatBtnClicked = 2;
        });
    }

    IEnumerator UpdateBoardData()
    {
        DiaryData newDiary = new DiaryData();
        newDiary.boardID = lastInsertID.ToString();
        WWWForm form = new WWWForm();
        form.AddField("boardID", newDiary.boardID);

        using (UnityWebRequest www = UnityWebRequest.Post("http://172.30.1.50/mosyroomDB/newboard.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string data = www.downloadHandler.text;
                Debug.Log(data);

                newDiary = JsonConvert.DeserializeObject<DiaryData>(data);

                boardID.text = newDiary.boardID;
                title.text = newDiary.title;
                writer.text = newDiary.writer;
                date.text = newDiary.date;
                contents.text = newDiary.contents;
            }
        }
        contentsPage.SetActive(true);
        bookShelves.SetActive(false);
    }
}
