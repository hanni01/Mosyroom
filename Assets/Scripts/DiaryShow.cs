using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;
using static DiaryDataSetting;

public class DiaryShow : MonoBehaviour
{
    public Button bookBtn;
    public Button cancelBtn;

    public GameObject DiaryCanvas;
    public Toggle mode;

    public TextMeshProUGUI title;
    public TextMeshProUGUI writer;
    public TextMeshProUGUI date;
    public TextMeshProUGUI contents;
    public GameObject bookPage;
    public GameObject bookShelves;

    private ResDiaryData res;

    private List<GameObject> btnList = new List<GameObject>();

    private void Awake()
    {
        StartCoroutine(GetDiaryData());
    }

    private void Start()
    {
        bookBtn.onClick.AddListener(() => {
            DiaryCanvas.SetActive(true);
            GameObject.Find("DiaryCanvas").GetComponent<DiaryDataSetting>().GenerateBook();
            mode.interactable = false;

            btnList = GameObject.Find("DiaryCanvas").GetComponent<DiaryDataSetting>().prefabInstanceList;

            for (int i = 0; i < btnList.Count; i++)
            {
                int index = i;
                btnList[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    bookShelves.SetActive(false);
                    bookPage.SetActive(true);

                    DiaryData diary = res.diaryData[index];
                    title.text = diary.title;
                    writer.text = diary.writer;
                    date.text = diary.date;
                    contents.text = diary.contents;
                });
                //for 루프에서 람다 표현식을 사용할 때, 클로저는 루프가 종료된 후에 실행되는 시점에서 변수를 참조
                //따라서 i의 최종 값인 4가 클로저에 포함되어 모든 버튼의 클릭 이벤트에 전달되는 것
                //이를 방지하기 위해 for문에 새로운 변수를 선언해 i값을 복사하여 넣어주면 된다!
            }
        });

        cancelBtn.onClick.AddListener(() =>
        {
            DiaryCanvas.SetActive(false);
            mode.interactable = true;
        });
    }

    IEnumerator GetDiaryData()
    {
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/mosyroomDB/display.php", form))
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
                Debug.Log(res);
            }
        }
    }
}
