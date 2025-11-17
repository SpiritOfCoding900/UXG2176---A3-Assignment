using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPauseButton : MonoBehaviour
{
    public Button btnOpen;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        btnOpen.onClick.AddListener(OnResumeClick);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
            OnResumeClick();
        else
            ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        UIManager.Instance.CloseAll();
    }

    private void OnResumeClick()
    {
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        UIManager.Instance.OpenReplace(GameUIID.PauseMenu);
    }
}
