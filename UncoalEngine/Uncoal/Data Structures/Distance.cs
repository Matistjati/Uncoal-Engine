﻿namespace Uncoal.Engine
{
	struct Distance
	{
		public int start;
		public int length;

		public Distance(int Start, int Length)
		{
			start = Start;
			length = Length;
		}

		public override string ToString()
		{
			return $"s: {start}  l: {length}";
		}
	}
}
