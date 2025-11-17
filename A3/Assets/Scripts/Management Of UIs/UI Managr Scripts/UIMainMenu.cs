using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    public Button btnStart;
    public Button btnSettings;
    public Button btnQuit;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f; // 🔹 Pause everything at the start

        btnStart.onClick.AddListener(OnPlayClick);
        btnSettings.onClick.AddListener(OnSettingsClick);
        btnQuit.onClick.AddListener(OnQuitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnSettingsClick()
    {
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        UIManager.Instance.Open(GameUIID.Settings);
    }

    private void OnPlayClick()
    {
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        //UIManager.Instance.OpenReplace(GameUIID.Title);
        UIManager.Instance.CloseAll();
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        SceneManager.LoadScene(1);
        Time.timeScale = 1f; // 🔹 Resume game when Start is pressed
    }

    private void OnQuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
