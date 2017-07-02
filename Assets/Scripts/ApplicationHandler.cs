using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ApplicationHandler : MonoBehaviour
{
	public Transform MainCamera;

	public List<Image> MenuImages;
	public List<Transform> CameraPositions;

	public List<Image> Slide01Images;
	public RawImage Slide01VideoImage;
	public VideoPlayer Slide01VideoPlayer;
	public List<NumberGrower> Slide01Numbers;

	public List<Image> Slide02Images;
	public RawImage Slide02VideoImage;
	public VideoPlayer Slide02VideoPlayer;
	public List<NumberGrower> Slide02Numbers;

	public List<Image> Slide03Images;
	public RawImage Slide03VideoImage;
	public VideoPlayer Slide03VideoPlayer;
	public List<NumberGrower> Slide03Numbers;

	public float Timeout = 1f;

	private int _currentSlideIndex = 0;

	void Start()
	{
		BackToMenu();
	}

	[RPC]
	public void OpenMap()
	{
		CloseSlide();
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
		if (_currentSlideIndex == index)
		{
			return;
		}

		CloseSlide();

		if (index == 1)
		{
			_currentSlideIndex = 1;

			TweenSlide(
				Slide01Images,
				Slide01VideoImage,
				Slide01VideoPlayer,
				Slide01Numbers,
				1,
				Timeout * 2,
				Ease.InOutQuart);
		}
		else if (index == 2)
		{
			_currentSlideIndex = 2;

			TweenSlide(
				Slide02Images,
				Slide02VideoImage,
				Slide02VideoPlayer,
				Slide02Numbers,
				1,
				Timeout * 2,
				Ease.InOutQuart);
		}
		else if (index == 3)
		{
			_currentSlideIndex = 3;

			TweenSlide(
				Slide03Images,
				Slide03VideoImage,
				Slide03VideoPlayer,
				Slide03Numbers,
				1,
				Timeout * 2,
				Ease.InOutQuart);
		}
		else
		{
			_currentSlideIndex = 0;
		}
	}

	private void CloseSlide()
	{
		if (_currentSlideIndex == 0)
		{
			return;
		}

		if (_currentSlideIndex == 1)
		{
			TweenSlide(
				Slide01Images,
				Slide01VideoImage,
				Slide01VideoPlayer,
				Slide01Numbers,
				0,
				Timeout * 2,
				Ease.OutQuart);
		}
		else if (_currentSlideIndex == 2)
		{
			TweenSlide(
				Slide02Images,
				Slide02VideoImage,
				Slide02VideoPlayer,
				Slide02Numbers,
				0,
				Timeout * 2,
				Ease.OutQuart);
		}
		else if (_currentSlideIndex == 3)
		{
			TweenSlide(
				Slide03Images,
				Slide03VideoImage,
				Slide03VideoPlayer,
				Slide03Numbers,
				0,
				Timeout * 2,
				Ease.OutQuart);
		}

		_currentSlideIndex = 0;
	}

	private void TweenSlide(
		List<Image> slideImages,
		RawImage slideVideo,
		VideoPlayer videoPlayer,
		List<NumberGrower> numbers,
		float value,
		float time,
		Ease ease)
	{
		int i = 0;

		foreach (Image image in slideImages)
		{
			image.DOKill();
			image.DOFade(value, time).SetEase(ease).SetDelay(0.1f * i);
			i++;
		}

		slideVideo.DOKill();
		slideVideo.DOFade(value, time).SetEase(ease).SetDelay(0.1f * i);
		i++;

		if (value > 0)
		{
			videoPlayer.frame = 0;
			videoPlayer.Play();
		}
		else
		{
			StartCoroutine(DelayVideoRewind(videoPlayer));
		}

		foreach (NumberGrower number in numbers)
		{
			number.DOKill();

			if (value > 0)
			{
				number.Reset();
				number.GetComponent<Text>().DOFade(value, time).SetEase(ease).SetDelay(0.1f * i);
				number.Animate();
			}
			else
			{
				number.GetComponent<Text>().DOFade(value, time).SetEase(ease).SetDelay(0.05f * i);
			}
			
			i++;
		}
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

			if (image.CompareTag("MenuImage"))
			{
				image.DOFade(value, time).SetDelay(0.1f * i);
				image.transform.DOScale(notFullScale, time).SetDelay(0.1f * i);
			}
			else if (image.CompareTag("MenuFillImage"))
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

	private IEnumerator DelayVideoRewind(VideoPlayer videoPlayer)
	{
		yield return new WaitForSeconds(Timeout * 1.5f);
		videoPlayer.frame = 0;
		videoPlayer.Stop();
	}
}
