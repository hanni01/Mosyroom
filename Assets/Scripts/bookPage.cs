using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class bookPage : MonoBehaviour
{
    public Button backBtn;
    public Button writeBtn;
    public Button trashBtn;

    public Button complete;

    public GameObject page;
    public GameObject CreateBookPage;
    public GameObject bookSelves;

    public TextMeshProUGUI boardID;
    public TMP_InputField title_input;
    public TMP_InputField contents_input;

    public int checkWhatBtnClicked = 0;

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
            title_input.text = "";
            contents_input.text = "";

            bookSelves.SetActive(false);
            CreateBookPage.SetActive(true);

            complete.interactable= true;
        });

        trashBtn.onClick.AddListener(() =>
        {
            string url = "http://172.30.1.50/mosyroomDB/delete.php";

            StartCoroutine(SendDeleteRequest(url));
            page.SetActive(false);
            if(checkWhatBtnClicked == 1)
            {
                Destroy(GameObject.Find("Canvas").GetComponent<DiaryShow>().currentClickedBook);
            }else if(checkWhatBtnClicked == 2) 
            {
                Destroy(GameObject.Find("BookCreateCanvas").GetComponent<ModifyBoard>().currentclickedNew);
            }
            bookSelves.SetActive(true);
        });
    }

    private IEnumerator SendDeleteRequest(string url)
    {
        WWWForm form = new WWWForm();

        form.AddField("boardID", boardID.text);

        using (UnityWebRequest request = UnityWebRequest.Post(url, form))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("데이터 삭제 성공");
            }
            else
            {
                Debug.LogError("데이터 삭제 실패: " + request.error);
            }
        }
    }
}
