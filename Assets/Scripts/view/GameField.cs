using System.Collections;
using UnityEngine;

public class GameField : MonoBehaviour
{
    public int Time = 60;
    private bool runGame;
    private GameObject popup;

    private void Awake()
    {
        GameCore.Instance.OnChangeState += OnChangeState;
        GameCore.Instance.OnUpdatePlayTime += OnUpdatePlayTime;
    }

    private void OnDestroy()
    {
        GameCore.Instance.OnChangeState -= OnChangeState;
        GameCore.Instance.OnUpdatePlayTime -= OnUpdatePlayTime;
    }

    private void OnUpdatePlayTime(int value)
    {
        Time = value;
    }

    private void OnChangeState(GameState value)
    {
        switch (value)
        {
            case GameState.Playing:
                runGame = true;
                StartCoroutine("CreateItem");
                break;
            case GameState.Paused:
                runGame = false;
                StopCoroutine("CreateItem");
                break;
            case GameState.Over:
                RemoveItem();
                runGame = false;
                StopCoroutine("CreateItem");
                break;
        }
    }

    IEnumerator CreateItem()
    {
        while (runGame)
        {
            RemoveItem();

            string itemName = GameCore.GetRandomItem();
            Vector3 position = GameCore.GetRandomPosition((RectTransform) gameObject.transform, 50f);
            popup = (GameObject) Instantiate(Resources.Load("Prefabs/ItemTypes/" + itemName), position, Quaternion.identity, gameObject.transform);

            yield return new WaitForSeconds(0.5f + Time/120f);
        }
    }

    private void RemoveItem()
    {
        if (popup != null)
        {
            Destroy(popup);
        }
    }
}