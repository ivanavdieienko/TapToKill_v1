using UnityEngine;

public class BaseBehavior : MonoBehaviour
{
    protected virtual void OnMouseDown()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}