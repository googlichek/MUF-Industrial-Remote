using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GraphGrower : MonoBehaviour
{
	public bool Vertical;
	public float MinLenght = 20;
	public bool IsFill = false;

	private Vector2 _sizeTo;
	private RectTransform _rectTransform;

	private void Start()
	{
		if (!IsFill)
		{
			_rectTransform = GetComponent<RectTransform>();
			_sizeTo = _rectTransform.sizeDelta;
		}
	}

	public void Reset()
	{
		if (!IsFill)
		{
			if (Vertical)
			{
				_rectTransform.sizeDelta = new Vector2(_sizeTo.x, MinLenght);
			}
			else
			{
				_rectTransform.sizeDelta = new Vector2(MinLenght, _sizeTo.y);
			}
		}
		else
			GetComponent<Image>().DOKill();
		GetComponent<Image>().fillAmount = 0;
	}

	public void Open(float delay = 0.5f)
	{
		if (!IsFill)
		{
			_rectTransform.DOKill();
			_rectTransform.DOSizeDelta(_sizeTo, 3f).SetDelay(delay);
		}
		else
		{
			GetComponent<Image>().DOKill();
			GetComponent<Image>().DOFillAmount(1, 3f).SetDelay(delay);
		}
	}
}
