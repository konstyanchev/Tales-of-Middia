using UnityEngine;
using UnityEngine.UI;

namespace HoMM
{
	public class PositionSquare : MonoBehaviour
	{
		public Color highlightedColor;

		private Color baseColor = Color.white;
		private Image image;
		private Text label;

		protected void Awake()
		{
			this.image = this.GetComponentInChildren<Image>();
			this.label = this.GetComponentInChildren<Text>();
		}

		public void Init(int number)
		{
			this.label.text = number.ToString("00");
		}

		public void Highlight()
		{
			this.image.color = this.highlightedColor;
		}

		public void Fade()
		{
			this.image.color = this.baseColor;
		}
	}
}