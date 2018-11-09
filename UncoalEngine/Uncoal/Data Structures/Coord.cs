﻿using System;
using System.Globalization;

namespace Uncoal.Engine
{
	public struct Coord
	{
		public static readonly Coord empty = new Coord();

		public uint X;

		public uint Y;

		public Coord(uint x, uint y)
		{
			X = x;
			Y = y;
		}

		public Coord(int x, int y)
		{
			if (x < 0)
				throw new ArgumentOutOfRangeException($"x must be greater than 0. X was {x}");
			if (y < 0)
				throw new ArgumentOutOfRangeException($"y must be greater than 0. X was {y}");

			X = (uint)x;
			Y = (uint)y;
		}

		public static explicit operator CoordF(Coord coord)
		{
			return new CoordF(coord.X, coord.Y);
		}

		public void Clamp(Coord min, Coord max)
		{
			uint X = this.X;
			X = (X > max.X) ? max.X : X;
			X = (X < min.X) ? min.X : X;

			uint Y = this.Y;
			Y = (Y > max.Y) ? max.Y : Y;
			Y = (Y < min.Y) ? min.Y : Y;

			this.X = X;
			this.Y = Y;
		}

		public static bool operator ==(Coord left, Coord right)
		{
			return left.X == right.X && left.Y == right.Y;
		}

		public static bool operator !=(Coord left, Coord right)
		{
			return !(left == right);
		}

		public override bool Equals(object obj)
		{
			if (!(obj is Coord))
				return false;
			Coord coord = (Coord)obj;
			return coord.X == this.X && coord.Y == this.Y;
		}

		public override int GetHashCode()
		{
			return (int)(X ^ Y);
		}

		public override string ToString()
		{
			return $"X: {X.ToString(CultureInfo.CurrentCulture)} , Y: {Y.ToString(CultureInfo.CurrentCulture)}";
		}
	}
}
