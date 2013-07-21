using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace WaterfallCollectionViewLayout
{
	public class PBCollectionViewWaterfallLayout : UICollectionViewLayout
	{
		// Property fields
		private int columnCount;
		private float itemWidth;
		private UIEdgeInsets sectionInset;

		// Class-related fields
		private PBCollectionViewDelegateWaterfallLayout pbDelegate;
		private int itemCount;
		private List<float> columnHeights;
		private float interItemSpacing;
		private List<UICollectionViewLayoutAttributes> itemAttributes;
		private static RectangleF rectForLayoutAttributes;

		public PBCollectionViewWaterfallLayout ()
		{
			// Default settings
			ColumnCount = 2;
			ItemWidth = 140;
			SectionInset = UIEdgeInsets.Zero;

			columnHeights = new List<float> ();
			itemAttributes = new List<UICollectionViewLayoutAttributes> ();
		}

		public PBCollectionViewWaterfallLayout (int numberOfColumns, float cellWidth, UIEdgeInsets insetOfSection)
		{
			ColumnCount = numberOfColumns;
			ItemWidth = cellWidth;
			SectionInset = insetOfSection;

			columnHeights = new List<float> ();
			itemAttributes = new List<UICollectionViewLayoutAttributes> ();
		}

		public int ColumnCount 
		{
			get 
			{
				return columnCount;
			}
			set 
			{
				if (value != columnCount) {
					columnCount = value;
					InvalidateLayout ();
				}
			}
		}

		public float ItemWidth
		{
			get 
			{
				return itemWidth;
			}
			set 
			{
				if (value != itemWidth) {
					itemWidth = value;
					InvalidateLayout ();
				}
			}
		}
	
		public UIEdgeInsets SectionInset
		{
			get 
			{
				return sectionInset;
			}
			set 
			{
				if (!UIEdgeInsets.Equals (sectionInset, value)) {
					sectionInset = value;
					InvalidateLayout ();
				}
			}
		}

		public PBCollectionViewDelegateWaterfallLayout WaterfallDelegate 
		{
			get 
			{
				return pbDelegate;
			}
			set 
			{
				pbDelegate = value;
			}
		}

		public override PointF TargetContentOffset (PointF proposedContentOffset, PointF scrollingVelocity)
		{
			return base.TargetContentOffset (proposedContentOffset, scrollingVelocity);
		}

		public override SizeF CollectionViewContentSize 
		{
			get
			{
				if (itemCount == 0) {
					return new SizeF (0, 0);
				}

				var contentSize = CollectionView.Frame.Size;
				var columnIndex = LongestColumnIndex ();
				var height = columnHeights[columnIndex];

				contentSize.Height = height - interItemSpacing + sectionInset.Bottom;

				return contentSize;
			}
		}

		public override void PrepareLayout ()
		{
			base.PrepareLayout ();

			itemCount = CollectionView.NumberOfItemsInSection (0);

			if (ColumnCount <= 1) {
				throw new ApplicationException ("You must have at least two columns to use UICollectionViewWaterfallLayout.");
			}
			var width = CollectionView.Frame.Size.Width - sectionInset.Left - sectionInset.Right;
			interItemSpacing = (float) Math.Floor ((width - columnCount * itemWidth) / (columnCount - 1));

			Console.WriteLine ("InterItemSpacing: {0}", interItemSpacing);
			Console.WriteLine ("Item Count {0}", itemCount);

			SetupSectionInsets ();
			PlaceItem ();
		}

		public override UICollectionViewLayoutAttributes LayoutAttributesForItem (NSIndexPath indexPath)
		{
			return itemAttributes[indexPath.Row];
		}


		public override UICollectionViewLayoutAttributes[] LayoutAttributesForElementsInRect (RectangleF rect)
		{
			rectForLayoutAttributes = rect;

			List<UICollectionViewLayoutAttributes> attributes = itemAttributes.FindAll (FindItemAttributes);

			foreach (var a in attributes)
			{
				Console.WriteLine (a.Frame);
			}
			
			return attributes.ToArray ();
		}

		public override bool ShouldInvalidateLayoutForBoundsChange (RectangleF newBounds)
		{
			return true;
		}

		private static bool FindItemAttributes (UICollectionViewLayoutAttributes attribute)
		{
			if (rectForLayoutAttributes.IntersectsWith (attribute.Frame)) {
				Console.WriteLine ("FindItemAttributes TRUE");
				return true;
			} else {
				Console.WriteLine ("FindItemAttributes FALSE");
				return false;
			}
		}

		private void SetupSectionInsets ()
		{
			for (int i = 0; i < columnCount; i++) {
				columnHeights.Add (sectionInset.Top);
				Console.WriteLine ("SetupSectionInsets: {0}", i);
			}
		}

		private void PlaceItem ()
		{
			for (int i = 0; i < itemCount; i++) {
				var indexPath = NSIndexPath.FromItemSection (i, 0);
				Console.WriteLine ("HeightForItem {0}", WaterfallDelegate.HeightForItem (CollectionView, this, indexPath));
				var itemHeight = WaterfallDelegate.HeightForItem (CollectionView, this, indexPath);
				var columnIndex = ShortestColumnIndex ();

				var xOffset = sectionInset.Left + (itemWidth + interItemSpacing) * columnIndex;
				var yOffset = columnHeights[columnIndex];

				var attributes = UICollectionViewLayoutAttributes.CreateForCell (indexPath);
				attributes.Frame = new RectangleF (xOffset, yOffset, itemWidth, itemHeight);
				Console.WriteLine ("Added attribute in PlaceItem: {0}", attributes.Frame);
				itemAttributes.Add (attributes);
				columnHeights[columnIndex] = yOffset + itemHeight + interItemSpacing;
			}
		}

		private int ShortestColumnIndex ()
		{
			var shortestIndex = 0;
			var shortestHeight = float.MaxValue;

			int index = 0;
			foreach (var height in columnHeights) {
				if (height < shortestHeight) {
					shortestHeight = height;
					shortestIndex = index;
				}

				index++;
			}

			Console.WriteLine ("Shortest Index: {0}", shortestIndex);

			return shortestIndex;
		}

		private int LongestColumnIndex ()
		{
			var largestIndex = 0;
			var largestHeight = float.MaxValue;

			int index = 0;
			foreach (var height in columnHeights) {
				if (height > largestHeight) {
					largestHeight = height;
					largestIndex = index;
				}

				index++;
			}

			Console.WriteLine ("Largest Index: {0}", largestIndex);

			return largestIndex;
		}
	}
}

