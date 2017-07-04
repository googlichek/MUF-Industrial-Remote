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

	public List<Image> HighlightedNumbers;

	public Image SlideBackground;

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

	public List<Image> Slide04Images;
	public RawImage Slide04VideoImage;
	public VideoPlayer Slide04VideoPlayer;
	public List<NumberGrower> Slide04Numbers;

	public List<Image> Slide05Images;
	public RawImage Slide05VideoImage;
	public VideoPlayer Slide05VideoPlayer;
	public List<NumberGrower> Slide05Numbers;

	public List<Image> Slide06Images;
	public RawImage Slide06VideoImage;
	public VideoPlayer Slide06VideoPlayer;
	public List<NumberGrower> Slide06Numbers;

	public List<Image> MapSlide01Images;
	public List<NumberGrower> MapSlide01Numbers;

	public List<Image> MapSlide02Images;
	public List<NumberGrower> MapSlide02Numbers;

	public List<Image> MapSlide03Images;
	public List<NumberGrower> MapSlide03Numbers;

	public List<Image> MapSlide04Images;
	public List<NumberGrower> MapSlide04Numbers;

	public List<Image> MapSlide05Images;
	public List<NumberGrower> MapSlide05Numbers;

	public List<Image> MapSlide06Images;
	public List<NumberGrower> MapSlide06Numbers;

	public List<Image> MapSlide07Images;
	public List<NumberGrower> MapSlide07Numbers;

	public List<Image> MapSlide08Images;
	public List<NumberGrower> MapSlide08Numbers;

	public float Timeout = 1f;

	private int _currentSlideIndex = 0;
	private int _currentMapSlideIndex = 0;

	private bool _mapSlideIsOpened = false;

	void Start()
	{
		BackToMenu();
	}

	[RPC]
	public void OpenMap()
	{
		CloseSlide();
		SlideBackground.DOFade(0, Timeout * 2).SetEase(Ease.OutBack);
		TweenMenuImages(1, 1, Timeout, Timeout * 1.5f);
		MoveCamera(0, Timeout * 2);
	}

	[RPC]
	public void BackToMenu()
	{
		_mapSlideIsOpened = false;

		CloseMapSlide();
		MoveCamera(0, Timeout * 2);
	}

	[RPC]
	public void MoveToPointOnMap(int index)
	{
		if (_currentMapSlideIndex == index)
		{
			return;
		}

		CloseMapSlide();

		StartCoroutine(
			!_mapSlideIsOpened ? MoveCameraAndOpenMapSlide(index, -2 * Timeout) : MoveCameraAndOpenMapSlide(index));


		_mapSlideIsOpened = true;
	}

	[RPC]
	public void OpenHiddenSlide(int index)
	{
		List<Image> images = gameObject.GetComponent<ImageLoader>().Images;

		if (index == 0)
		{
			SlideBackground.DOFade(0, Timeout * 2).SetEase(Ease.OutBack);

			foreach (Image image in images)
			{
				image.DOKill();
				image.DOFade(0, Timeout * 2).SetEase(Ease.OutBack);
				image.DOFillAmount(0, Timeout * 2).SetEase(Ease.OutBack);
			}
		}
		else
		{
			SlideBackground.DOFade(1, Timeout * 2).SetEase(Ease.InBack);

			foreach (Image image in images)
			{
				image.DOKill();

				if (images.IndexOf(image) == index - 1)
				{
					image.DOFade(1, Timeout * 2).SetEase(Ease.InBack);
					image.DOFillAmount(1, Timeout * 2).SetEase(Ease.InBack);
				}
				else
				{
					image.DOFade(0, Timeout * 2).SetEase(Ease.OutBack);
					image.DOFillAmount(0, Timeout * 2).SetEase(Ease.OutBack);
				}
			}
			
		}
	}

	[RPC]
	public void OpenSlide(int index)
	{
		TweenMenuImages(0, 0.7f, 0.5f * Timeout, 0, 0.05f);

		if (_currentSlideIndex == index)
		{
			return;
		}

		SlideBackground.DOFade(1, Timeout * 2).SetEase(Ease.InBack);

		CloseSlide();

		switch (index)
		{
			case 1:
				_currentSlideIndex = 1;

				TweenSlide(
					Slide01Images,
					Slide01VideoImage,
					Slide01VideoPlayer,
					Slide01Numbers,
					1,
					Timeout * 2,
					Ease.InOutQuart);
				break;
			case 2:
				_currentSlideIndex = 2;

				TweenSlide(
					Slide02Images,
					Slide02VideoImage,
					Slide02VideoPlayer,
					Slide02Numbers,
					1,
					Timeout * 2,
					Ease.InOutQuart);
				break;
			case 3:
				_currentSlideIndex = 3;

				TweenSlide(
					Slide03Images,
					Slide03VideoImage,
					Slide03VideoPlayer,
					Slide03Numbers,
					1,
					Timeout * 2,
					Ease.InOutQuart);
				break;
			case 4:
				_currentSlideIndex = 4;

				TweenSlide(
					Slide04Images,
					Slide04VideoImage,
					Slide04VideoPlayer,
					Slide04Numbers,
					1,
					Timeout * 2,
					Ease.InOutQuart);
				break;
			case 5:
				_currentSlideIndex = 5;

				TweenSlide(
					Slide05Images,
					Slide05VideoImage,
					Slide05VideoPlayer,
					Slide05Numbers,
					1,
					Timeout * 2,
					Ease.InOutQuart);
				break;
			case 6:
				_currentSlideIndex = 6;

				TweenSlide(
					Slide06Images,
					Slide06VideoImage,
					Slide06VideoPlayer,
					Slide06Numbers,
					1,
					Timeout * 2,
					Ease.InOutQuart);
				break;
			default:
				_currentSlideIndex = 0;
				break;
		}
	}

	private void CloseSlide()
	{
		switch (_currentSlideIndex)
		{
			case 0:
				return;
			case 1:
				TweenSlide(
					Slide01Images,
					Slide01VideoImage,
					Slide01VideoPlayer,
					Slide01Numbers,
					0,
					Timeout * 2,
					Ease.OutQuart);
				break;
			case 2:
				TweenSlide(
					Slide02Images,
					Slide02VideoImage,
					Slide02VideoPlayer,
					Slide02Numbers,
					0,
					Timeout * 2,
					Ease.OutQuart);
				break;
			case 3:
				TweenSlide(
					Slide03Images,
					Slide03VideoImage,
					Slide03VideoPlayer,
					Slide03Numbers,
					0,
					Timeout * 2,
					Ease.OutQuart);
				break;
			case 4:
				TweenSlide(
					Slide04Images,
					Slide04VideoImage,
					Slide04VideoPlayer,
					Slide04Numbers,
					0,
					Timeout * 2,
					Ease.OutQuart);
				break;
			case 5:
				TweenSlide(
					Slide05Images,
					Slide05VideoImage,
					Slide05VideoPlayer,
					Slide05Numbers,
					0,
					Timeout * 2,
					Ease.OutQuart);
				break;
			case 6:
				TweenSlide(
					Slide06Images,
					Slide06VideoImage,
					Slide06VideoPlayer,
					Slide06Numbers,
					0,
					Timeout * 2,
					Ease.OutQuart);
				break;
		}

		_currentSlideIndex = 0;
	}

	private void OpenMapSlide(int index)
	{

		if (_currentMapSlideIndex == index)
		{
			return;
		}

		CloseMapSlide();

		switch (index)
		{
			case 1:
				_currentMapSlideIndex = 1;
				TweenMapSlide(MapSlide01Images, MapSlide01Numbers, 1, Timeout * 2, Ease.InOutQuart);
				break;
			case 2:
				_currentMapSlideIndex = 2;
				TweenMapSlide(MapSlide02Images, MapSlide02Numbers, 1, Timeout * 2, Ease.InOutQuart);
				break;
			case 3:
				_currentMapSlideIndex = 3;
				TweenMapSlide(MapSlide03Images, MapSlide03Numbers, 1, Timeout * 2, Ease.InOutQuart);
				break;
			case 4:
				_currentMapSlideIndex = 4;
				TweenMapSlide(MapSlide04Images, MapSlide04Numbers, 1, Timeout * 2, Ease.InOutQuart);
				break;
			case 5:
				_currentMapSlideIndex = 5;
				TweenMapSlide(MapSlide05Images, MapSlide05Numbers, 1, Timeout * 2, Ease.InOutQuart);
				break;
			case 6:
				_currentMapSlideIndex = 6;
				TweenMapSlide(MapSlide06Images, MapSlide06Numbers, 1, Timeout * 2, Ease.InOutQuart);
				break;
			case 7:
				_currentMapSlideIndex = 7;
				TweenMapSlide(MapSlide07Images, MapSlide07Numbers, 1, Timeout * 2, Ease.InOutQuart);
				break;
			case 8:
				_currentMapSlideIndex = 8;
				TweenMapSlide(MapSlide08Images, MapSlide08Numbers, 1, Timeout * 2, Ease.InOutQuart);
				break;
			default:
				_currentMapSlideIndex = 0;
				break;
		}
	}

	private void CloseMapSlide()
	{
		switch (_currentMapSlideIndex)
		{
			case 0:
				return;
			case 1:
				TweenMapSlide(MapSlide01Images, MapSlide01Numbers, 0, Timeout * 3, Ease.OutBack, 0);
				break;
			case 2:
				TweenMapSlide(MapSlide02Images, MapSlide02Numbers, 0, Timeout * 3, Ease.OutBack, 0);
				break;
			case 3:
				TweenMapSlide(MapSlide03Images, MapSlide03Numbers, 0, Timeout * 3, Ease.OutBack, 0);
				break;
			case 4:
				TweenMapSlide(MapSlide04Images, MapSlide04Numbers, 0, Timeout * 3, Ease.OutBack, 0);
				break;
			case 5:
				TweenMapSlide(MapSlide05Images, MapSlide05Numbers, 0, Timeout * 3, Ease.OutBack, 0);
				break;
			case 6:
				TweenMapSlide(MapSlide06Images, MapSlide06Numbers, 0, Timeout * 3, Ease.OutBack, 0);
				break;
			case 7:
				TweenMapSlide(MapSlide07Images, MapSlide07Numbers, 0, Timeout * 3, Ease.OutBack, 0);
				break;
			case 8:
				TweenMapSlide(MapSlide08Images, MapSlide08Numbers, 0, Timeout * 3, Ease.OutBack, 0);
				break;
		}

		_currentMapSlideIndex = 0;
	}

	private void TweenSlide(
		List<Image> slideImages,
		RawImage slideVideo,
		VideoPlayer videoPlayer,
		List<NumberGrower> numbers,
		float value,
		float time,
		Ease ease,
		float delay = 0.1f)
	{
		int i = 0;

		foreach (Image image in slideImages)
		{
			image.DOKill();
			image.DOFade(value, time).SetEase(ease).SetDelay(delay * i);
			i++;
		}

		slideVideo.DOKill();
		slideVideo.DOFade(value, time).SetEase(ease).SetDelay(delay * i);
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
				number.GetComponent<Text>().DOFade(value, time).SetEase(ease).SetDelay(delay * i);
				number.Animate();
			}
			else
			{
				number.GetComponent<Text>().DOFade(value, time).SetEase(ease).SetDelay(delay / 2 * i);
			}
			
			i++;
		}
	}

	private void TweenMapSlide(
		List<Image> mapSlideImages,
		List<NumberGrower> numbers,
		float value,
		float time,
		Ease ease,
		float delay = 0.1f)
	{
		int i = 0;

		foreach (Image image in mapSlideImages)
		{
			image.DOKill();
			image.DOFade(value, time).SetEase(ease).SetDelay(delay * i);
			i++;
		}

		foreach (NumberGrower number in numbers)
		{
			number.DOKill();

			if (value > 0)
			{
				number.Reset();
				number.GetComponent<Text>().DOFade(value, time).SetEase(ease).SetDelay(delay * i);
				number.Animate();
			}
			else
			{
				number.GetComponent<Text>().DOFade(value, time).SetEase(ease).SetDelay(delay  / 2 * i);
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

		MainCamera.transform.DOMove(CameraPositions[index].transform.position, time).SetEase(Ease.OutQuart).SetDelay(delay);

		if (index == 0)
		{
			MainCamera.transform.DORotate(CameraPositions[index].transform.eulerAngles, time).SetEase(Ease.OutQuart).SetDelay(delay);
		}
	}

	private void TweenMenuImages(float value, float notFullScale, float time, float delay = 0f, float subDelay = 0.1f)
	{
		int i = 0;
		foreach (Image image in MenuImages)
		{
			image.DOKill();

			if (image.CompareTag("MenuFillImage"))
			{
				image.DOFade(value, time).SetDelay(delay + subDelay * i);
				image.DOFillAmount(value, time).SetDelay(delay + subDelay * i);
			}
			else
			{
				image.DOFade(value, time).SetDelay(delay + subDelay * i);
				image.transform.DOScale(notFullScale, time).SetDelay(delay + subDelay * i);
			}

			i++;
		}
	}

	private IEnumerator MoveCameraAndOpenMapSlide(int index, float time = 0)
	{
		yield return new WaitForSeconds(time + Timeout * 2 + 0.1f);
		//TweenMenuImages(0, 0.7f, 0, Timeout * 2 + 0.1f, 0);
		MoveCamera(index, Timeout * 2 + 0.1f);
		MoveCamera(0, 1, Timeout * 2 + 0.1f);
		OpenMapSlide(index);
	}

	private IEnumerator DelayVideoRewind(VideoPlayer videoPlayer)
	{
		yield return new WaitForSeconds(Timeout * 1.5f);
		videoPlayer.Pause();
		videoPlayer.frame = 0;
	}
}
