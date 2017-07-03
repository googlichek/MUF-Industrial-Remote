using DG.Tweening;
using UnityEngine;

public class LoopHandler : MonoBehaviour
{
	public float ScaleModificator;
	public float Delay = 0;

	private void Start()
	{
		gameObject.transform.DOScale(ScaleModificator, 1f).SetEase(Ease.InOutFlash).SetLoops(-1, LoopType.Yoyo).SetDelay(Delay);
	}
}
