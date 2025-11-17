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

    public bool isPause;

    // Start is called before the first frame update
    void Start()
    {
        isPause = true;

        btnOpen.onClick.AddListener(OnResumeClick);
        btnRestart.onClick.AddListener(OnRestartClick);
        btnSettings.onClick.AddListener(OnSettingsClick);
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPause)
        {
            Time.timeScale = 1f;
            AudioManager.Instance.PlaySound(SoundID.Button_Sound);
            UIManager.Instance.CloseAll();
        }
        else
        {
            Time.timeScale = 0f;
        }
    }

    private void OnResumeClick()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        UIManager.Instance.CloseAll();
    }

    private void OnRestartClick()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        SceneManager.LoadScene(0);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenReplace(GameUIID.MainMenu);
    }

    private void OnSettingsClick()
    {
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        UIManager.Instance.Open(GameUIID.Settings);
    }

}
