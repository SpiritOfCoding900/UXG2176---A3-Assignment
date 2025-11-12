using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPauseMenu : MonoBehaviour
{
    public Button btnOpen;
    public Button btnRestart;
    public Button btnSettings;

    // Start is called before the first frame update
    void Start()
    {
        btnOpen.onClick.AddListener(OnResumeClick);
        btnRestart.onClick.AddListener(OnRestartClick);
        btnSettings.onClick.AddListener(OnSettingsClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnResumeClick()
    {
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        UIManager.Instance.CloseAll();
    }

    private void OnRestartClick()
    {
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        SceneManager.LoadScene(0);
    }

    private void OnSettingsClick()
    {
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        UIManager.Instance.Open(GameUIID.Settings);
    }

}
