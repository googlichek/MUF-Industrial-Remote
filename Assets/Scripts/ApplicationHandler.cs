using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ApplicationHandler : MonoBehaviour
{
	public Transform MainCamera;

	public List<Image> MenuImages;
	public List<Transform> CameraPositions;

	public float Timeout = 1f;

	void Start()
	{
		BackToMenu();
	}

	[RPC]
	public void OpenMap()
	{
		TweenMenuImages(1, 1, 1);
		MoveCamera(0, Timeout * 2);
	}

	[RPC]
	public void BackToMenu()
	{
		TweenMenuImages(0, 0.7f, 1);
		MoveCamera(0, Timeout * 2);
	}

	[RPC]
	public void MoveToPointOnMap(int index)
	{
		MoveCamera(index, Timeout * 2);
		MoveCamera(0, 1, 2.1f);
	}

	[RPC]
	public void OpenSlide(int index)
	{
	}

	private void MoveCamera(int index, float time, float delay = 0f)
	{
		if (delay <= 0)
		{
			MainCamera.DOKill();
		}

		MainCamera.transform.DOMove(CameraPositions[index].transform.position, time).SetEase(Ease.InOutQuad).SetDelay(delay);
	}

	private void TweenMenuImages(float value, float notFullScale, float time)
	{
		int i = 0;
		foreach (Image image in MenuImages)
		{
			image.DOKill();


			if (image.tag == "MenuImage")
			{
				image.DOFade(value, time).SetDelay(0.1f * i);
				image.transform.DOScale(notFullScale, time).SetDelay(0.1f * i);
			}
			else if (image.tag == "MenuFillImage")
			{
				image.DOFade(value, time).SetDelay(0.1f * i);
				image.DOFillAmount(value, time).SetDelay(0.1f * i);
			}
			else
			{
				image.DOFade(value, time).SetDelay(0.1f * i);
				image.transform.DOScale(notFullScale, time).SetDelay(0.1f * i);
			}

			i++;
		}
	}
}
