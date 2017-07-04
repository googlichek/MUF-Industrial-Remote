using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ImageLoader : MonoBehaviour
{
	public Transform Root;
	public Image ImagePrefab;

	public List<Image> Images;

	private string _path;

	private void Awake()
	{
		_path = Application.streamingAssetsPath;
		InitiateImageCreation();
	}

	private void InitiateImageCreation()
	{
		string[] fileNames = Directory.GetFiles(_path, "*.png").ToArray();

		for (int i = 0; i < fileNames.Length; i++)
		{
			StartCoroutine(CreateImage(fileNames[i], i));
		}
	}

	private IEnumerator CreateImage(string url, int index)
	{
		WWW www = new WWW("file://" + url);
		yield return www;

		Image image = Instantiate(ImagePrefab, Root.transform);
		image.transform.SetParent(Root);

		Texture texture = www.texture;
		Sprite loadedSprite =
			Sprite.Create(
				(Texture2D)texture,
				new Rect(0, 0, texture.width, texture.height),
				Vector2.zero);

		image.sprite = loadedSprite;
		image.type = Image.Type.Filled;
		image.fillAmount = 0;
		image.fillMethod = Image.FillMethod.Horizontal;

		Images.Add(image);
	}
}
