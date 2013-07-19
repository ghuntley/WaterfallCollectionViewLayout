using System;
using System.Collections.Generic;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WaterfallCollectionViewLayout;

namespace WaterfallCollectionViewDemo
{
	public class ViewController : UIViewController
	{
		List<Tag> tags;
		List<float> cellHeights;

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
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
		
			var layout = new UICollectionViewWaterfallLayout ();
			layout.SectionInset = new UIEdgeInsets (9, 9, 9, 9);
			//layout.Delegate = new WaterfallDelegate (cellHeights);

			var view = new WaterfallViewController (tags, cellHeights, layout);

			View.AddSubview (view.View);
		}
	}
}

