using UnityEngine;

public class PositiveBehavior : BaseBehavior
{
	[SerializeField]
	private short ScoreToAdd;

	protected override void OnMouseDown()
	{
		GameCore.Instance.Points += ScoreToAdd;
		base.OnMouseDown();
	}
}
