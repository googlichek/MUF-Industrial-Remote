using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CircleGrower : MonoBehaviour
{
	private Image _image;

	private void Start()
	{
		_image = GetComponent<Image>();
	}

	public void Reset()
	{
		_image.DOKill();
		_image.fillAmount = 0;
	}

	public void Open(float delay = 0.5f)
	{
		_image.DOFillAmount(1, 2f).SetDelay(delay);
	}
}
