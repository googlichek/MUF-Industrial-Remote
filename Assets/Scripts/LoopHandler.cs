using DG.Tweening;
using UnityEngine;

public class LoopHandler : MonoBehaviour
{
	public float ScaleModificator;

	private void Start()
	{
		gameObject.transform.DOScale(ScaleModificator, 1f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
	}
}
