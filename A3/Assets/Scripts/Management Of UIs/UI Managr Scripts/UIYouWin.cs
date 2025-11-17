using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIYouWin : MonoBehaviour
{
    public Button btnTryAgain;
    public Button btnMainMenu;
    // Start is called before the first frame update
    void Start()
    {
        btnTryAgain.onClick.AddListener(OnTryAgain);
        btnMainMenu.onClick.AddListener(OnMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTryAgain()
    {
        GameManager.Instance.youWin = false;
        GameManager.Instance.youLose = false;

        UIManager.Instance.CloseAll();
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    private void OnMainMenu()
    {
        GameManager.Instance.youWin = false;
        GameManager.Instance.youLose = false;

        UIManager.Instance.CloseAll();
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        UIManager.Instance.OpenReplace(GameUIID.MainMenu);
    }
}
