using System.Collections.Generic;
using UnityEngine;

public class FileIcon : MonoBehaviour {

	public string filePath;

	float numClicks = 0;
	float clicktime = 0;

	HingeJoint2D hinge;
	bool grabbed;

	void Start () 
	{
		hinge = GetComponent<HingeJoint2D>();
		hinge.enabled = false;

		Texture2D iconTex = Rainity.GetFileIcon(filePath);
		Sprite newIconSprite = Sprite.Create(iconTex, new Rect(0, 0, 256, 256), Vector2.one * 0.5f);

		GetComponent<SpriteRenderer>().sprite = newIconSprite;
	}

    private void Update()
    {
		if (RainityInput.GetMouseButton(0) && (numClicks == 1 || grabbed))
		{
            if (!grabbed)
            {
				grabbed = true;
			}

			hinge.enabled = true;
			hinge.connectedAnchor = WallpaperManager.instance.GetMousePosition();
		}
		if (RainityInput.GetMouseButtonUp(0))
		{
			grabbed = false;

			hinge.enabled = false;
		}
	}

    private void OnMouseOver() 
	{
		if (RainityInput.GetMouseButtonDown(0))
		{
			hinge.anchor = transform.InverseTransformPoint(WallpaperManager.instance.GetMousePosition());

			numClicks++;
			if (numClicks == 1)
            {
				clicktime = Time.time;
			}

			if (numClicks > 1 && Time.time - clicktime < .4f)
			{
				numClicks = 0;
				clicktime = 0;

				Rainity.OpenFile(filePath);
			}
			else if (numClicks > 2 || Time.time - clicktime > 1)
			{
				numClicks = 0;
			}
		}
	}

    private void OnMouseExit()
    {
		numClicks = 0;
    }
}
