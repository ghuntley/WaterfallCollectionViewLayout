
using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WaterfallCollectionViewLayout;

namespace WaterfallCollectionViewLayoutDemo
{
	public class ViewController : UIViewController
	{
		List<Tag> tags;
		List<float> cellHeights;
		PBCollectionViewWaterfallLayout layout;
		WaterfallDelegate pbDelegate;
		WaterfallCollectionViewController vc;

		public ViewController ()
		{

			tags = new List<Tag> ();
			tags.Add (new Tag { Name = "Test", Image = UIImage.FromFile ("Stock1.png") });
			tags.Add (new Tag { Name = "Test", Image = UIImage.FromFile ("Stock2.png") });
			tags.Add (new Tag { Name = "Test", Image = UIImage.FromFile ("Stock3.png") });
			tags.Add (new Tag { Name = "Test", Image = UIImage.FromFile ("Stock1.png") });
			tags.Add (new Tag { Name = "Test", Image = UIImage.FromFile ("Stock2.png") });
			tags.Add (new Tag { Name = "Test", Image = UIImage.FromFile ("Stock3.png") });
			tags.Add (new Tag { Name = "Test", Image = UIImage.FromFile ("Stock1.png") });

			cellHeights = new List<float> ();
			foreach (var tag in tags) 
			{
				var height = tag.Image.Size.Height;
				cellHeights.Add (height);
			}

			pbDelegate = new WaterfallDelegate (cellHeights);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			layout = new PBCollectionViewWaterfallLayout () 
			{
				ColumnCount = 2,
				ItemWidth = 129,
				Delegate = pbDelegate,
				SectionInset = new UIEdgeInsets (9, 9, 9, 9)
			};

			vc = new WaterfallCollectionViewController (layout, tags);
			View.AddSubview (vc.View);
		}
	}
}

