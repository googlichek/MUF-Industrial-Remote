using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ElementGrower : MonoBehaviour
{
	private CanvasGroup _canvasGroup;
	private Image _image;
	private RectTransform _rectTransform;
	// Use this for initialization
	private void Start()
	{
		_canvasGroup = GetComponent<CanvasGroup>();
		_image = GetComponent<Image>();
		_rectTransform = GetComponent<RectTransform>();
	}

	public void Reset()
	{
		if (_canvasGroup)
		{
			_canvasGroup.DOKill();
			_canvasGroup.alpha = 0;
		}
		if (_image)
		{
			_image.DOKill();
			_image.color = new Color32(255, 255, 255, 0);
		}
		_rectTransform.DOKill();
		_rectTransform.localScale = Vector2.one * 0.7f;
	}

	public void Open()
	{
		Open(0.5f);
	}

	public void Open(float delay)
	{
		Reset();

		if (_canvasGroup)
		{
			_canvasGroup.DOKill();
			_canvasGroup.DOFade(1, 1f).SetDelay(delay);
		}

		if (_image)
		{
			_image.DOKill();
			_image.DOFade(1, 1f).SetDelay(delay);
		}

		_rectTransform.DOKill();
		_rectTransform.DOScale(1, 1.4f).SetEase(Ease.OutBack).SetDelay(delay);
	}
}
