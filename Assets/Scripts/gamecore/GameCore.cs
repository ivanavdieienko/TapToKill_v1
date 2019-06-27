using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCore : MonoBehaviour
{
	private GameState gameState = GameState.Inactive;
	private GameState GameState
	{
		get { return gameState; }
		set
		{
			gameState = value;
			if (OnChangeState != null)
				OnChangeState(value);
		}
	}

	private short points;
	public  short Points
	{
		get { return points; }
		set
		{
			if (points != value)
			{
				points = value;

				if (value < 0)
				{
					points = 0;
					GameOver();
				}
				if (OnUpdatePoints != null)
					OnUpdatePoints(points);
			}
		}
	}

	private int playTimeValue;
	private int PlayTime
	{
		get { return playTimeValue; }
		set
		{
			if (playTimeValue != value)
			{
				playTimeValue = value;
				if (OnUpdatePlayTime != null)
					OnUpdatePlayTime(value);
			}
		}
	}

	public UIManager UIManager = new UIManager();

	public delegate void NumberUpdater(int value);
	public delegate void StateNotifier(GameState value);

	public event StateNotifier OnChangeState;
	public event NumberUpdater OnUpdatePoints;
	public event NumberUpdater OnUpdatePlayTime;

	private static GameCore instance;
	public  static GameCore Instance
	{
		get
		{
			if (instance == null)
			{
				instance =
					((GameObject) Instantiate(Resources.Load("GameCore"))).GetComponent<GameCore>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

	public void StartGame()
	{
		Points = 0;
		PlayTime = 60;
		if (GameState != GameState.Playing)
		{
			StartCoroutine("UpdateTime");
			GameState = GameState.Playing;
		}
	}

	public void PauseGame()
	{
		GameState = GameState == GameState.Paused ? GameState.Playing : GameState.Paused;

		if (GameState == GameState.Paused)
			StopCoroutine("UpdateTime");
		else
			StartCoroutine("UpdateTime");
	}

	private void GameOver()
	{
		PlayTime = 0;
		GameState = GameState.Over;
	}

	IEnumerator UpdateTime()
	{
		while (PlayTime > 0)
		{
			yield return new WaitForSeconds(1);
			PlayTime--;
		}
		GameOver();
	}

	public static Vector3 GetRandomPosition(RectTransform container, float offset = 0f)
	{
		return GetRandomPosition(container, offset, offset);
	}

	public static Vector3 GetRandomPosition(RectTransform container, float offsetX, float offsetY)
	{
		var bounds = container.rect;
		var x = Random.Range(bounds.xMin + offsetX, bounds.xMax - offsetX);
		var y = Random.Range(bounds.yMin + offsetY, bounds.yMax - offsetY);
		return new Vector3(x,y);
	}

	public static string GetRandomItem()
	{
		int colorId = Random.Range(0, 6);
		ItemTypes value = (ItemTypes) colorId;
		return value.ToString();
	}

	public void RestartGame()
	{
		GameState = GameState.Inactive;
		SceneManager.LoadScene("MainMenu");
	}
}
