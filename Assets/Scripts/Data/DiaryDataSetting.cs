using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Random = UnityEngine.Random;
using System.Dynamic;
using Newtonsoft.Json;

public class DiaryDataSetting : MonoBehaviour
{

    private int diaryCnt = 0;
    private ResDiaryData res;
    private GameObject[] diaryBook;
    private int RandomNum;
    public List<GameObject> prefabInstanceList = new List<GameObject>();

    public GameObject panel1;
    public GameObject panel2;

    public class ResDiaryData
    {
        public List<DiaryData> diaryData = new List<DiaryData>();
    }
    public class DiaryData
    {
        public string boardID;
        public string title;
        public string writer;
        public string date;
        public string contents;
    }
    // Start is called before the first frame update
    void Start()
    {
        diaryBook = Resources.LoadAll<GameObject>("Prefabs_book");
        StartCoroutine(GetDiaryDataCount());
    }

    public void GenerateBook()
    {
        Debug.Log(diaryCnt);
        if(diaryCnt != 0)
        {
            for(int i = 0;i < diaryCnt; i++)
            {
                RandomNum = Random.Range(0, 4);
                if(panel1.transform.childCount != 12)
                {
                    GameObject prefabInstance = Instantiate(diaryBook[RandomNum]);
                    prefabInstance.transform.SetParent(panel1.transform);
                    prefabInstance.transform.localScale = new Vector3(10, 8, 0);
                    prefabInstanceList.Add(prefabInstance);
                }
                else
                {
                    GameObject prefabInstance = Instantiate(diaryBook[RandomNum]);
                    prefabInstance.transform.SetParent(panel2.transform);
                    prefabInstance.transform.localScale = new Vector3(10, 8, 0);
                    prefabInstanceList.Add(prefabInstance);
                }

            }
        }
    }

    IEnumerator GetDiaryDataCount()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://172.30.1.50/mosyroomDB/display.php", form))
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

                res = JsonConvert.DeserializeObject<ResDiaryData>(data);
                if (res == null) Debug.Log("res is null");

                diaryCnt = res.diaryData.Count;
            }
        }

        GenerateBook();
    }
}
