using UnityEngine;

public class NegativeBehavior : BaseBehavior
{
	[SerializeField]
	private short ScoreToRemove;

	protected override void OnMouseDown()
	{
		GameCore.Instance.Points -= ScoreToRemove;
		base.OnMouseDown();
	}
}
