using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WaterfallCollectionViewLayout;

namespace WaterfallCollectionViewLayoutDemo
{
	public class WaterfallDelegate : PBCollectionViewDelegateWaterfallLayout
	{
		List<float> cellHeights;

		public WaterfallDelegate (List<float> cellHeights) : base (cellHeights)
		{
			this.cellHeights = cellHeights;
		}

		public override float HeightForItem (UICollectionView collectionView, PBCollectionViewWaterfallLayout collectionViewLayout, NSIndexPath indexPath)
		{
			return cellHeights [indexPath.Row];
		}

		public override void Scrolled (UIScrollView scrollView)
		{
			Console.WriteLine ("Handle Scrolled event.");
		}
	}
}

