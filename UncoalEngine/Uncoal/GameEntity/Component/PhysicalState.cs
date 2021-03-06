﻿using System;

namespace Uncoal.Engine
{
	public class PhysicalState : Component
	{
		public CoordF Position = CoordF.empty;

		private float scale = 1f;
		public float Scale
		{
			get => scale;
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException($"Scale must be greater than 0. Scale was {value}");
				}
				float oldValue = scale;
				scale = value;
				if (oldValue != value)
				{
					gameObject?.RecalculateSpriteSize();
				}
			}
		}
	}
}
