using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallTree
{
	public abstract class BallTree
	{

		// Leaves
		private class BallTreeLeaf : BallTree
		{
			private Point point;
			private int n;
		}

　
		private class BallTreeNode : BallTree
		{
			private BallTree left;
			private BallTree right;
			private Point center;
			private double radius;

			public BallTreeNode(BallTree left, BallTree right, Point center, double radius)
			{
				this.left = left;
				this.right = right;
				this.center = center;
				this.radius = radius;
			}
		}

		private BallTree createTree(Point[] points)
		{
			// Is the list empty?
			if (points.Length < 1) { throw new Exception("List of points cannot be empty."); }

			// Are all the points in the same location?
			bool allSame = false;
			foreach (Point p in points)
			{
				// Fun question mark if statements
				allSame = !p.Equals(points[0]) ? true : false;
			}

			// If points are all same, make BallTreeLeaf
			if (allSame)
			{
				return new BallTreeLeaf();
			}

			// Get the two most distant points (Grant method)
			Point farSplitA = points[0].farthestPoint(points);
			Point farSplitB = farSplitA.farthestPoint(points);

			List<Point> splitA = new List<Point> { };
			List<Point> splitB = new List<Point> { };

　
			foreach (Point p in points)
			{
				// Assign the points to their respective sides of the split
				if (p.distance(farSplitA) < p.distance(farSplitB))
				{
					splitB.Add(p);
				}
				else
				{
					splitA.Add(p);
				}
			}

			BallTree aTree = createTree(splitA.ToArray());
			BallTree bTree = createTree(splitB.ToArray());

			Point midPoint = farSplitA.midPoint(farSplitB);
			Point farthestpoint = midPoint.farthestPoint(points);
			double radius = midPoint.distance(farthestpoint);

			return new BallTreeNode(aTree, bTree, midPoint, radius);
		}

	}

	public class Point
	{
		// Position
		private double posX;
		private double posY;

		public double getX(Point a) { return a.posX; }
		public double getY(Point a) { return a.posY; }

		// Distance Method
		public double distance(Point other)
		{
			return Math.Sqrt(Math.Pow(posX - other.posX, 2) + Math.Pow(posY - other.posY, 2));
		}

		// Midpoint Method
		public Point midPoint(Point other)
		{
			return new Point((posX + other.posX) / 2, (posY + other.posY) / 2);
		}

　
		// Farthest Point Method
		public Point farthestPoint(Point[] set)
		{
			Point farthest = set[0];
			foreach (Point p in set)
			{
				// More fun code compression ?s
				farthest = set[0].distance(p) > set[0].distance(farthest) ? p : farthest;

				//if (set[0].distance(p) > set[0].distance(farthest))
				//{
				//	farthest = p;
				//}
			}
			return farthest;
		}

		// Constructor
		public Point(double posX, double posY) { }

	}
}