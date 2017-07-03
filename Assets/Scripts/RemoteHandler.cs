using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class RemoteHandler : MonoBehaviour
{
	public Button MapButton;
	public Button BackToMenuButton;
	public Button HiddenBackToMenuButton;
	public List<Button> TopRowMenuButtons;
	public List<Button> BottomRowMenuButtons;

	public List<Button> TopRowInvestButtons;
	public List<Button> BottomRowInvestButtons;
	public List<Button> HiddenButtons;

	public List<Image> MenuImages; 
	public List<Image> InvestImages;

	public List<Image> HighlightedNumbers;

	public float MapButtonStartPositionX;
	public float MapButtonEndPositionX;

	public float ButtonsStartPositionY;
	public float ButtonsEndPositionY;

	public float HiddenButtonsStartPositionY;
	public float HiddenButtonsEndPositionY;

	public float BackToMenuButtonStartPositionY;
	public float BackToMenuButtonEndPositionY;

	public float Timeout = 1f;
	public float BaseDelay = 0.2f;

	private bool _hiddenButtonShouldNotWork = false;

	void Start()
	{
		TweenMenuElements(
			MapButtonEndPositionX,
			ButtonsEndPositionY,
			Timeout,
			0f,
			Ease.OutBack);
	}

	[RPC]
	public void OpenMap()
	{
		_hiddenButtonShouldNotWork = true;

		TweenMenuElements(
			MapButtonStartPositionX,
			ButtonsStartPositionY,
			Timeout,
			0f,
			Ease.InBack);

		TweenInvestProjectElements(
			BackToMenuButtonEndPositionY,
			ButtonsEndPositionY,
			Timeout,
			1.5f * Timeout,
			Ease.OutBack);
	}

	[RPC]
	public void BackToMenu()
	{
		TweenInvestProjectElements(
			BackToMenuButtonStartPositionY,
			ButtonsStartPositionY,
			Timeout,
			0f,
			Ease.InBack);

		TweenMenuElements(
			MapButtonEndPositionX,
			ButtonsEndPositionY,
			Timeout,
			2f * Timeout,
			Ease.OutBack);
	}

	public void ShowHiddenButtons()
	{
		if (_hiddenButtonShouldNotWork)
		{
			return;
		}

		_hiddenButtonShouldNotWork = true;

		TweenMenuElements(
			MapButtonStartPositionX,
			ButtonsStartPositionY,
			Timeout,
			0f,
			Ease.InBack);

		int i = TweenListOfImages(MenuImages, 0, Timeout, 0, Ease.OutBack, 0);

		TweenListOfButtons(HiddenButtons, HiddenButtonsEndPositionY, Timeout, 2 * Timeout, Ease.OutBack, i);

		HiddenBackToMenuButton.DOKill();
		HiddenBackToMenuButton.transform.DOLocalMoveY(
			BackToMenuButtonEndPositionY, Timeout).SetEase(Ease.OutBack).SetDelay(2 * Timeout + BaseDelay * i);
	}

	public void HideHiddenButtons()
	{
		_hiddenButtonShouldNotWork = false;

		int i = 0;

		HiddenBackToMenuButton.DOKill();
		HiddenBackToMenuButton.transform.DOLocalMoveY(
			BackToMenuButtonStartPositionY, Timeout).SetEase(Ease.InBack).SetDelay(BaseDelay * i);

		i = TweenListOfButtons(HiddenButtons, HiddenButtonsStartPositionY, Timeout, Timeout, Ease.InBack, i);

		TweenMenuElements(
			MapButtonEndPositionX,
			ButtonsEndPositionY,
			Timeout,
			2 * Timeout,
			Ease.OutBack);

		TweenListOfImages(MenuImages, 1, Timeout, Timeout, Ease.InBack, i);
	}

	private void TweenMenuElements(
		float mapButtonPositionX,
		float buttonsPositionY,
		float time,
		float delay,
		Ease ease)
	{
		int i = 1;

		i = TweenListOfImages(InvestImages, 0, time, 0, ease == Ease.InBack ? Ease.OutBack : Ease.InBack, i);

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

		TweenListOfImages(MenuImages, 1, time, delay, ease, i);
	}

	private void TweenInvestProjectElements(
		float backButtonPositionY,
		float buttonsPositionY,
		float time,
		float delay,
		Ease ease)
	{
		int i = 1;

		i = TweenListOfImages(MenuImages, 0, time, 0, ease == Ease.InBack ? Ease.OutBack : Ease.InBack, i);

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

		TweenListOfImages(InvestImages, 1, time, delay, ease, i);
	}

	private int TweenListOfImages(List<Image> images, float value, float time, float delay, Ease ease, int i)
	{
		foreach (Image image in images)
		{
			image.DOKill();
			image.DOFade(value, time).SetEase(ease).SetDelay(delay + BaseDelay * i);
			i++;
		}

		return i;
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
