//  SuperTux Editor
//  Copyright (C) 2006 Matthias Braun <matze@braunis.de>
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;

namespace DataStructures
{
	/// <summary>This class represents a dynamic 2-dimensional array</summary>
	public class Field<T>
	{
		public    List<T> Elements = new List<T>();
		protected int width;
		protected int height;

		public Field()
		{
		}

		public Field(int Width, int Height, T FillValue)
		{
			this.width = Width;
			this.height = Height;
			for(int i = 0; i < Width * Height; ++i)
				Elements.Add(FillValue);
		}

		public Field(List<T> Values, int Width, int Height)
		{
			Assign(Values, Width, Height);
		}

		/// <summary>
		/// Clone Subset of other field
		/// </summary>
		public Field(Field<T> Other, int startX, int startY, int width, int height) {
			this.width = width;
			this.height = height;
			if (startX < 0) throw new ArgumentOutOfRangeException("startX");
			if (startY < 0) throw new ArgumentOutOfRangeException("startY");
			if (startX + width > Other.Width) throw new ArgumentOutOfRangeException("startX");
			if (startY + height > Other.Height) throw new ArgumentOutOfRangeException("startY");
			for (int y = 0; y < height; y++) {
				for (int x = 0; x < width; x++) {
					Elements.Add(Other[x + startX, y + startY]);
				}
			}
		}

		/// <summary>
		/// Clone Subset of other field, filling in missing values with FillValue
		/// </summary>
		public Field(Field<T> Other, int startX, int startY, int width, int height, T FillValue) {
			this.width = width;
			this.height = height;
			for (int y = startY; y < startY + height; y++) {
				for (int x = startX; x < startX + width; x++) {
					if(x < 0 || y < 0 || x >= Other.Width || y >= Other.Height)
						Elements.Add(FillValue);
					else
						Elements.Add(Other[x, y]);
				}
			}
		}

		/// <summary>
		/// Width of array
		/// </summary>
		public int Width
		{
			get
			{
				return width;
			}
		}

		/// <summary>
		/// Height of array
		/// </summary>
		public int Height
		{
			get
			{
				return height;
			}
		}

		public T this[int X, int Y]
		{
			get
			{
				return Elements[Y * width + X];
			}
			set
			{
				Elements[Y * width + X] = value;
			}
		}

		public T this[FieldPos Pos]
		{
			get
			{
				return this[Pos.X, Pos.Y];
			}
			set
			{
				this[Pos.X, Pos.Y] = value;
			}
		}

		public void Assign(List<T> Values, int Width, int Height)
		{
			if(Values.Count != Width * Height)
				throw new Exception("invalid size of value list for field");
			this.width = Width;
			this.height = Height;
			Elements.Clear();
			foreach(T val in Values) {
				Elements.Add(val);
			}
		}

		/// xOffset/yOffset are the coordinates of the old
		/// Field relative to the top/left of the new Field,
		/// same as Gimps "Image->Canvas Size"
		public void Resize(int xOffset, int yOffset, int newWidth, int newHeight, T fillValue)
		{
			if (xOffset == 0 && yOffset == 0)
			{
				Resize(newWidth, newHeight, fillValue);
			}
			else
			{
				List<T> newElements = new List<T>();

				for(int y = 0; y < newHeight; ++y)
				{
					for(int x = 0; x < newWidth; ++x)
					{
						int tX = x - xOffset;
						int tY = y - yOffset;

						if (0 <= tX && tX < width &&
						    0 <= tY && tY < height)
						{
							newElements.Add(this[tX, tY]);
						}
						else
						{
							newElements.Add(fillValue);
						}
					}
				}

				Elements = newElements;
				width  = newWidth;
				height = newHeight;
			}
		}

		public void Resize(int NewWidth, int NewHeight, T FillValue)
		{
			List<T> NewElements = new List<T>();
			for(int y = 0; y < NewHeight; ++y) {
				for(int x = 0; x < NewWidth; ++x) {
					if(x < Width && y < Height)
						NewElements.Add(this[x, y]);
					else
						NewElements.Add(FillValue);
				}
			}
			Elements = NewElements;
			width = NewWidth;
			height = NewHeight;
		}

		public List<T> GetContentsArray()
		{
			List<T> Result = new List<T>(Elements);
			return Result;
		}

		public bool InBounds(FieldPos pos) {
			if (pos.X < 0) return false;
			if (pos.Y < 0) return false;
			if (pos.X >= Width) return false;
			if (pos.Y >= Height) return false;
			return true;
		}

		public bool InBounds(int x, int y) {
			if (x < 0) return false;
			if (y < 0) return false;
			if (x >= Width) return false;
			if (y >= Height) return false;
			return true;
		}

		public bool EqualContents(object obj) {
			if (!(obj is Field<T>)) return false;
			Field<T> other = (Field<T>)obj;
			if (this.width != other.width) return false;
			if (this.height != other.height) return false;
			if (this.Elements.Count != other.Elements.Count) return false;
			for (int i = 0; i < this.Elements.Count; i++) {
				if (!this.Elements[i].Equals(other.Elements[i])) return false;
			}
			return true;
		}

	}
}

/* EOF */
