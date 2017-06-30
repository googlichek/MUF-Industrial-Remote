using UnityEngine;
using UnityEngine.UI;

public class NumberGrower : MonoBehaviour
{
	public float To;

	public bool IsFloat = false;
	public bool IsPersent = false;

	private Text _txt;

	private int _intern = 0;
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


		_txt.text = IsFloat ? (Mathf.Lerp(0, To, _lerp)).ToString("0.0") : ((int) Mathf.Lerp(0, To, _lerp)).ToString();

		if (IsPersent)
		{
			_txt.text += "%";
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
