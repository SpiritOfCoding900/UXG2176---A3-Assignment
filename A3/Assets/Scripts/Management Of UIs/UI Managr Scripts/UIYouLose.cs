using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIYouLose : MonoBehaviour
{
    public Button btnTryAgain;
    // Start is called before the first frame update
    void Start()
    {
        btnTryAgain.onClick.AddListener(OnPlayClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnPlayClick()
    {
        UIManager.Instance.CloseAll();
        AudioManager.Instance.PlaySound(SoundID.Button_Sound);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        Destroy(gameObject);
    }
}
