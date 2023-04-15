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
        mode.onValueChanged.AddListener(value => grids.enabled = value); //enabled �Ӽ��� ������Ʈ�� Ȱ��ȭ/��Ȱ��ȭ ���θ� �����Ѵ�.
                                                                         //value�� onValueChanged �̺�Ʈ���� ���޵Ǵ� ���ο� Toggle ���Դϴ�.
    }
}
