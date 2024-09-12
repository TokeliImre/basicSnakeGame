using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
	internal class Cordinate
	{
		private int x;
		private int y;

        public int X { get { return x; }  }
		public int Y { get { return y; } }


        public Cordinate(int x ,int y)
        {
            this.x = x;
            this.y = y;
        }

		public override bool Equals(object? obj)
		{
            if (obj==null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            
            Cordinate other = (Cordinate)obj;
            return x==other.x && y==other.y;    
        }
	}
}
