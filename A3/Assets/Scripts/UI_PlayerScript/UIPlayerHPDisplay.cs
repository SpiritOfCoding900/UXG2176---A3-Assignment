using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlayerHPDisplay : MonoBehaviour
{
    // Image For HealthBar.
    [SerializeField] private Image hpImage;
    [SerializeField] private TMP_Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FSMProtoType.Instance.gameObject != null) // If hpImage is not null.
        {
            hpImage.fillAmount = FSMProtoType.Instance.Health / FSMProtoType.MaxHealth;
        }
        else
        {
            this.GetComponent<UIPlayerHPDisplay>().enabled = false;
        }

        if (FSMProtoType.Instance.gameObject != null) // If hpText is not null.
        {
            hpText.text = FSMProtoType.Instance.Health.ToString();
        }
        else
        {
            this.GetComponent<UIPlayerHPDisplay>().enabled = false;
        }
    }
}
