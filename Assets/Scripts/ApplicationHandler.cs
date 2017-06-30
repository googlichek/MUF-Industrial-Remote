using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class ApplicationHandler : MonoBehaviour
{
	public RawImage RawImage01;
	public RawImage RawImage02;
	public RawImage RawImage03;

	public VideoPlayer Video01;
	public VideoPlayer Video02;
	public VideoPlayer Video03;

	public float Timeout = 1f;

	[RPC]
	public void LaunchVideo01()
	{
		SwitchVideo(Video01, Video02, Video03, RawImage01, RawImage02, RawImage03);
	}

	[RPC]
	public void LaunchVideo02()
	{
		SwitchVideo(Video02, Video01, Video03, RawImage02, RawImage01, RawImage03);
	}

	[RPC]
	public void LaunchVideo03()
	{
		SwitchVideo(Video03, Video01, Video02, RawImage03, RawImage01, RawImage02);
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
