using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RemoteHandler : MonoBehaviour
{
	public Button MapButton;
	public Button BackToMenuButton;
	public List<Button> TopRowMenuButtons;
	public List<Button> BottomRowMenuButtons;

	public List<Button> TopRowInvestButtons;
	public List<Button> BottomRowInvestButtons;

	public float MapButtonStartPositionX;
	public float MapButtonEndPositionX;

	public float ButtonsStartPositionY;
	public float ButtonsEndPositionY;

	public float BackToMenuButtonStartPositionY;
	public float BackToMenuButtonEndPositionY;

	public float Timeout = 1f;
	public float BaseDelay = 0.2f;

	void Start()
	{
		TweenMenuButtons(
			MapButtonEndPositionX,
			ButtonsEndPositionY,
			Timeout,
			0f,
			Ease.OutBack);
	}

	[RPC]
	public void OpenMap()
	{
		TweenMenuButtons(
			MapButtonStartPositionX,
			ButtonsStartPositionY,
			Timeout,
			0f,
			Ease.InBack);

		TweenInvestProjectButtons(
			BackToMenuButtonEndPositionY,
			ButtonsEndPositionY,
			Timeout,
			1.5f * Timeout,
			Ease.OutBack);
	}

	[RPC]
	public void BackToMenu()
	{
		TweenInvestProjectButtons(
			BackToMenuButtonStartPositionY,
			ButtonsStartPositionY,
			Timeout,
			0f,
			Ease.InBack);

		TweenMenuButtons(
			MapButtonEndPositionX,
			ButtonsEndPositionY,
			Timeout,
			1.5f * Timeout,
			Ease.OutBack);
	}

	private void TweenMenuButtons(
		float mapButtonPositionX,
		float buttonsPositionY,
		float time,
		float delay,
		Ease ease)
	{
		int i = 1;

		if (Mathf.FloorToInt(delay) == 0)
		{
			MapButton.DOKill();
			MapButton.transform.DOLocalMoveX(mapButtonPositionX, time).SetEase(ease).SetDelay(delay + BaseDelay * i);
			i++;

			i = TweenListOfButtons(TopRowMenuButtons, buttonsPositionY, time, delay, ease, i);
			i = TweenListOfButtons(BottomRowMenuButtons, -buttonsPositionY, time, delay, ease, i);
		}
		else
		{
			i = TweenListOfButtons(TopRowMenuButtons, buttonsPositionY, time, delay, ease, i);
			i = TweenListOfButtons(BottomRowMenuButtons, -buttonsPositionY, time, delay, ease, i);

			MapButton.DOKill();
			MapButton.transform.DOLocalMoveX(mapButtonPositionX, time).SetEase(ease).SetDelay(delay + BaseDelay * i);
		}
	}

	private void TweenInvestProjectButtons(
		float backButtonPositionY,
		float buttonsPositionY,
		float time,
		float delay,
		Ease ease)
	{
		int i = 1;

		if (Mathf.FloorToInt(delay) == 0)
		{
			BackToMenuButton.DOKill();
			BackToMenuButton.transform.DOLocalMoveY(
				backButtonPositionY, time).SetEase(ease).SetDelay(delay + BaseDelay * i);
			i++;

			i = TweenListOfButtons(TopRowInvestButtons, buttonsPositionY, time, delay, ease, i);
			i = TweenListOfButtons(BottomRowInvestButtons, -buttonsPositionY, time, delay, ease, i);
		}
		else
		{
			i = TweenListOfButtons(TopRowInvestButtons, buttonsPositionY, time, delay, ease, i);
			i = TweenListOfButtons(BottomRowInvestButtons, -buttonsPositionY, time, delay, ease, i);

			BackToMenuButton.DOKill();
			BackToMenuButton.transform.DOLocalMoveY(
				backButtonPositionY, time).SetEase(ease).SetDelay(delay + BaseDelay * i);
		}
	}

	private int TweenListOfButtons(
		List<Button> buttons,
		float buttonsPositionY,
		float time,
		float delay,
		Ease ease,
		int i)
	{
		foreach (Button button in buttons)
		{
			button.DOKill();
			button.transform.DOLocalMoveY(buttonsPositionY, time).SetEase(ease).SetDelay(delay + BaseDelay * i);
			i++;
		}

		return i;
	}
}
