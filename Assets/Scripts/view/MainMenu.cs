using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button Play;

    private void Start()
    {
        Play.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("GameScene");
    }

    private void OnDestroy()
    {
        Play.onClick.RemoveListener(OnClick);
    }
}