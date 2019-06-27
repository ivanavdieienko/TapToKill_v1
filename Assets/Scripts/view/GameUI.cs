using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Text Time;
    [SerializeField]
    private Text Points;
    [SerializeField]
    private Button Pause;
    [SerializeField]
    private Button Restart;

    private void Awake()
    {
        GameCore.Instance.OnUpdatePoints += UpdatePoints;
        GameCore.Instance.OnUpdatePlayTime += UpdateTime;
        GameCore.Instance.OnChangeState += OnChangeState;

        Pause.onClick.AddListener(GameCore.Instance.PauseGame);
        Restart.onClick.AddListener(GameCore.Instance.RestartGame);

        GameCore.Instance.StartGame();
    }

    private void OnDestroy()
    {
        GameCore.Instance.OnUpdatePoints -= UpdatePoints;
        GameCore.Instance.OnUpdatePlayTime -= UpdateTime;
        GameCore.Instance.OnChangeState -= OnChangeState;

        Pause.onClick.RemoveListener(GameCore.Instance.PauseGame);
        Restart.onClick.RemoveListener(GameCore.Instance.RestartGame);
    }

    private void OnChangeState(GameState value)
    {
        if (value == GameState.Over)
        {
            GameCore.Instance.UIManager.AddPopup("GameOverPopup", transform);
            Pause.gameObject.SetActive(false);
        }
    }

    private void UpdatePoints(int score)
    {
        string value = "0000" + score;
        value = value.Substring(value.Length - 4, 4);
        Points.text = String.Format("Points: {0}", value);
    }

    private void UpdateTime(int seconds)
    {
        string value = (seconds > 9 ? "" : "0") + seconds;
        Time.text = String.Format("time: {0} seconds", value);
    }
}