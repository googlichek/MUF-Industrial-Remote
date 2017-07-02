using UnityEngine;
using UnityEngine.UI;

public class NumberGrower : MonoBehaviour
{
	public float To;

	public bool IsFloat = false;
	public bool IsFloatWithTwoZeros = false;
	public bool IsPersent = false;
	public bool StarSymbol = false;
	public bool DoubleStarSymbol = false;

	private Text _txt;

	private float _lerp = 0;

	private bool _animate;

	void Start()
	{
		_txt = GetComponent<Text>();
	}

	void Update()
	{
		if (_animate && _lerp < 1)
		{
			_lerp += (Time.deltaTime*0.33f);
		}


		if (IsFloatWithTwoZeros)
		{
			_txt.text = (Mathf.Lerp(0, To, _lerp)).ToString("0.00");
		}
		else if (IsFloat)
		{
			_txt.text = (Mathf.Lerp(0, To, _lerp)).ToString("0.0");
		}
		else
		{
			_txt.text = ((int) Mathf.Lerp(0, To, _lerp)).ToString();
		}

		if (IsPersent)
		{
			_txt.text += "%";
		}

		if (StarSymbol)
		{
			_txt.text += "*";
		}

		if (DoubleStarSymbol)
		{
			_txt.text += "**";
		}
	}

	public void Reset()
	{
		_txt.text = "0";
		_animate = false;
		_lerp = 0;
	}

	public void Animate()
	{
		_animate = true;
	}
}
