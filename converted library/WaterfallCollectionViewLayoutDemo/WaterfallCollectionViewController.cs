using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WaterfallCollectionViewLayout;

namespace WaterfallCollectionViewLayoutDemo
{
	public class WaterfallCollectionViewController : UICollectionViewController
	{
		List<Tag> data;
		
		static NSString CELL_IDENTIFIER = new NSString ("WATERFALL_CELL");

		public WaterfallCollectionViewController (UICollectionViewLayout layout, List<Tag> tags): base (layout)
		{
			data = tags;

			// CollectionView.AutoresizingMask = UIViewAutoresizing.FlexibleHeight | UIViewAutoresizing.FlexibleWidth;
			CollectionView.BackgroundColor = UIColor.FromPatternImage (UIImage.FromFile ("Background.png"));
			CollectionView.Frame = new RectangleF(0, 0, View.Bounds.Width, View.Bounds.Height);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			CollectionView.RegisterClassForCell (typeof (WaterfallCell), CELL_IDENTIFIER);

			View.AddSubview (CollectionView);
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			UpdateLayout ();
		}

		private void UpdateLayout ()
		{
			var layout = (PBCollectionViewWaterfallLayout)CollectionView.CollectionViewLayout;
			layout.ColumnCount = (int)(CollectionView.Bounds.Size.Width / Constants.CellWidth);
			layout.ItemWidth = Constants.CellWidth;
		}

		public override int NumberOfSections (UICollectionView collectionView)
		{
			return 1;
		}

		public override int GetItemsCount (UICollectionView collectionView, int section)
		{
			Console.WriteLine ("Count: {0}", data.Count);
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

