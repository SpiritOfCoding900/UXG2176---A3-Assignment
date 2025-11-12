using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseButton : MonoBehaviour
{
    public Button btnOpen;
    // Start is called before the first frame update
    void Start()
    {
        btnOpen.onClick.AddListener(OnResumeClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnResumeClick()
    {
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        UIManager.Instance.OpenReplace(GameUIID.PauseMenu);
    }
}
