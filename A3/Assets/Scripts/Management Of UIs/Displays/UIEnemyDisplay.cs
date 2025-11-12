using TMPro;
using UnityEngine;

public class UIEnemyDisplay : MonoBehaviour
{
    TMP_Text _text;
    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelCondition.Instance != null)
        {
            //TODO will be replaced with global event system
            _text.text = "Enemies Left: " + LevelCondition.Instance.GetNumberOfEnemies();
        }

    }
}
