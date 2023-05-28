using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DiaryDataSetting;

public class DiraryData : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI writer;
    public TextMeshProUGUI date;
    public TextMeshProUGUI contents;
    public GameObject bookPage;
    public GameObject bookShelves;

    private ResDiaryData res;
    private List<GameObject> btnList= new List<GameObject>();

    private void Awake()
    {
        StartCoroutine(GetDiaryData());
        btnList = GameObject.Find("DiaryCanvas").GetComponent<DiaryDataSetting>().prefabInstanceList;
    }

    IEnumerator GetDiaryData()
    {
        WWWForm form = new WWWForm();
        
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/mosyroomDB/display.php", form))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                string data = www.downloadHandler.text;
                Debug.Log(data);

                
                res = JsonConvert.DeserializeObject<ResDiaryData>(data);

                foreach (DiaryData diaryData in res.diaryData)
                {
                    Debug.Log(diaryData.title);
                    Debug.Log(diaryData.writer);
                    Debug.Log(diaryData.date);
                    Debug.Log(diaryData.contents);
                }
            }
        }
    }
}
