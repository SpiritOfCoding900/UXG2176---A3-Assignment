using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UISettings : MonoBehaviour
{
    public Button btnClose;

    // Start is called before the first frame update
    void Start()
    {
        btnClose.onClick.AddListener(OnCloseClick);
    }


    private void OnCloseClick()
    {
        //AudioManager.Instance.PlaySound(SoundID.ButtonClick);
        UIManager.Instance.Close(this.gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    
}
