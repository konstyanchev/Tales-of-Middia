using System;
using UnityEngine;

namespace ModularInput
{
	public class StandaloneController : Controller
	{
		public override void GetAxis(ref float horizontal, ref float vertical)
		{
			vertical	= 0;
			horizontal	= 0;
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
				vertical = 1F;
			else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
				vertical = -1F;
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
				horizontal = -1F;
			else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
				horizontal = 1F;
		}

		public override float GetAxisValue(AxisDirection dir)
		{
			float axisValue = 0;
			if(dir == AxisDirection.Horizontal)
			{
				if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
					axisValue = -1F;
				else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
					axisValue = 1F;
			}
			else
			{
				if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
					axisValue = 1F;
				else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
					axisValue = -1F;
			}
			return axisValue;	
		}

		public override CrossDirection GetDirection()
		{
			if (Input.GetKey(KeyCode.W))
				return CrossDirection.Up;
			else if (Input.GetKey(KeyCode.S))
				return CrossDirection.Down;
			else if (Input.GetKey(KeyCode.A))
				return CrossDirection.Left;
			else if (Input.GetKey(KeyCode.D))
				return CrossDirection.Right;
			return CrossDirection.None;
		}

		public override CrossDirection GetMenuDirection()
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
				return CrossDirection.Up;
			else if (Input.GetKeyDown(KeyCode.DownArrow))
				return CrossDirection.Down;
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
				return CrossDirection.Left;
			else if (Input.GetKeyDown(KeyCode.RightArrow))
				return CrossDirection.Right;
			return CrossDirection.None;
		}

		public override bool Pause()
		{
			return Input.GetKeyDown(KeyCode.P);
		}

		public override bool Cancel()
		{
			return Input.GetKeyDown(KeyCode.Escape);
		}

		public override bool Validate()
		{
			return	Input.GetKeyDown(KeyCode.Return) ||
					Input.GetKeyDown(KeyCode.KeypadEnter);
		}

		public override bool Details()
		{
			return Input.GetKeyDown(KeyCode.Q);
		}

		public override bool LeftClick()
		{
			return Input.GetMouseButtonDown(0);
		}

		public override bool RightClick()
		{
			return Input.GetMouseButtonDown(1);
		}

		public override bool Space()
		{
			return Input.GetKeyDown(KeyCode.Space);
		}
	}
}