using UnityEngine;
using UnityEngine.UI;

public class GameOverPopup : MonoBehaviour
{
    [SerializeField]
    private Text textField;

    private void Awake()
    {
        textField.text = string.Format(TextConstants.YouGotPoints, GameCore.Instance.Points);
    }
}