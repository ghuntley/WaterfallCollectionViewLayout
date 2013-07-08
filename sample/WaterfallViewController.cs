using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WaterfallCollectionViewLayoutBinding;

namespace WaterfallCollectionViewDemo
{
	public class WaterfallViewController : UICollectionViewController
	{
		int width;
		int count;
		NSString CELL_IDENTIFIER;
		List<float> cellHeights;
		List<Tag> data;

		public WaterfallViewController (List<Tag> tags, List<float> cellHeights, UICollectionViewWaterfallLayout layout) : base (layout)
		{
			width = 129;
			CELL_IDENTIFIER = new NSString ("WATERFALL_CELL");
			this.cellHeights = cellHeights;
			data = tags;

			layout.Delegate = new WaterfallDelegate (cellHeights);
			CollectionView.RegisterClassForCell (typeof (WaterfallCell), CELL_IDENTIFIER);
			CollectionView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			CollectionView.Frame = new RectangleF(0, 0, View.Bounds.Width, View.Bounds.Height);
			CollectionView.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("Background.png"));
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.AddSubview (CollectionView);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			UpdateLayout ();
		}

		private void UpdateLayout ()
		{
			var layout = (UICollectionViewWaterfallLayout)CollectionView.CollectionViewLayout;
			layout.ColumnCount = (uint) (CollectionView.Bounds.Size.Width / width);
			layout.ItemWidth = width;
		}

		public override int NumberOfSections (UICollectionView collectionView)
		{
			return 1;
		}

		public override int GetItemsCount (UICollectionView collectionView, int section)
		{
			return data.Count;
		}

		public override UICollectionViewCell GetCell (UICollectionView collectionView, NSIndexPath indexPath)
		{
			var cell = (WaterfallCell) collectionView.DequeueReusableCell (CELL_IDENTIFIER, indexPath);
			var tag = data[indexPath.Row];

			cell.PopulateCell (tag.Name, tag.Image);

			return cell;
		}
	}
}