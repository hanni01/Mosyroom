using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.Networking;
using static DiaryDataSetting;

public class DiraryData : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI writer;
    public TextMeshProUGUI date;
    public TextMeshProUGUI contents;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetDiaryData());
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

                ResDiaryData res = JsonFx.Json.JsonReader.Deserialize<ResDiaryData>(data);

                foreach(DiaryData diaryData in res.diaryData)
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
