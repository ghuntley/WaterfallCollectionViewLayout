using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WaterfallCollectionViewLayout;

namespace WaterfallCollectionViewDemo
{
	public class WaterfallDelegate : UICollectionViewDelegateWaterfallLayout
	{
		List<float> cellHeights;

		public WaterfallDelegate (List<float> cellHeights)
		{
			this.cellHeights = cellHeights;
		}

		public override float CollectionView (UICollectionView collectionView, UICollectionViewWaterfallLayout collectionViewLayout, NSIndexPath indexPath)
		{
			return cellHeights[indexPath.Row];
		} 
	}
}

