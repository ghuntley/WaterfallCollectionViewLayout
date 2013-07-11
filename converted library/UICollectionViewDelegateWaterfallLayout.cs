using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WaterfallCollectionViewLayout
{
	public abstract class UICollectionViewDelegateWaterfallLayout : UICollectionViewDelegate
	{
		public UICollectionViewDelegateWaterfallLayout () { }

		public UICollectionViewDelegateWaterfallLayout (List<float> cellHeights)
		{

		}

		public abstract float HeightForItem (UICollectionView collectionView, UICollectionViewWaterfallLayout collectionViewLayout, NSIndexPath indexPath);
	}
}

