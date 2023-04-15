using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public SpriteRenderer grids;
    public GameObject RoomSettingPanel;
    public GameObject ReturnPanel;
    public Toggle mode;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!mode.isOn)
            return;
        mode.onValueChanged.AddListener(value => RoomSettingPanel.SetActive(value));
        mode.onValueChanged.AddListener(value => ReturnPanel.SetActive(value));
        mode.onValueChanged.AddListener(value => grids.enabled = value); //enabled 속성은 오브젝트의 활성화/비활성화 여부를 결정한다.
                                                                         //value는 onValueChanged 이벤트에서 전달되는 새로운 Toggle 값입니다.
    }
}
