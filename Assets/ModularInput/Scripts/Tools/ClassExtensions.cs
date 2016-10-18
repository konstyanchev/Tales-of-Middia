using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace ModularInput
{
	public static class ClassExtensions
	{
		public static void ChangeColor(this Image image, CrossDirection direction)
		{
			Color current = image.color;
			switch (direction)
			{
				case CrossDirection.Up:
					image.color = new Color(Math.Min(1F, current.r + 0.02F), current.g, current.b);
					break;
				case CrossDirection.Down:
					image.color = new Color(Math.Max(0F, current.r - 0.02F), current.g, current.b);
					break;
				case CrossDirection.Right:
					image.color = new Color(current.r, Math.Min(1F, current.g + 0.02F), current.b);
					break;
				case CrossDirection.Left:
					image.color = new Color(current.r, Math.Max(0F, current.g - 0.02F), current.b);
					break;
			}
		}

		public static void ChangePosition(this RectTransform rect, CrossDirection direction)
		{
			Vector3 currentPos = rect.anchoredPosition;
			switch (direction)
			{
				case CrossDirection.Up:
					currentPos.y += 1F;
					rect.anchoredPosition = currentPos;
					break;
				case CrossDirection.Down:
					currentPos.y -= 1F;
					rect.anchoredPosition = currentPos;
					break;
				case CrossDirection.Left:
					currentPos.x -= 1F;
					rect.anchoredPosition = currentPos;
					break;
				case CrossDirection.Right:
					currentPos.x += 1F;
					rect.anchoredPosition = currentPos;
					break;
			}
		}
	}
}
