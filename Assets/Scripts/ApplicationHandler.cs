using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ApplicationHandler : MonoBehaviour
{
	public float Timeout = 1f;

	[RPC]
	public void OpenMap()
	{
	}

	[RPC]
	public void BackToMenu()
	{
	}

	[RPC]
	public void MoveToPointOnMap(int index)
	{
	}

	[RPC]
	public void OpenSlide(int index)
	{
	}

	private void SwitchVideo(
		VideoPlayer video01,
		VideoPlayer video02,
		VideoPlayer video03,
		RawImage image01,
		RawImage image02,
		RawImage image03)
	{
		image01.DOKill();
		image02.DOKill();
		image03.DOKill();

		image03.DOFade(0, Timeout).SetEase(Ease.OutQuint);
		image02.DOFade(0, Timeout).SetEase(Ease.OutQuint);
		image01.DOFade(1, Timeout).SetEase(Ease.InQuint);

		video01.Play();

		StartCoroutine(DelayRewind(video02, video03));
	}

	private IEnumerator DelayRewind(VideoPlayer video01, VideoPlayer video02)
	{
		yield return new WaitForSeconds(Timeout);

		video01.Stop();
		video01.frame = 0;

		video02.Stop();
		video02.frame = 0;
	}
}
