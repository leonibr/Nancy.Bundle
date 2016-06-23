#Nancy.Bundle

###This project used the NancyFx wiki for bundle and minification to create a package. See websampleapp. 


 ###Of course using the Nancy way.
####Click for [SourceBrowser](http://sourcebrowser.io/Browse/leonibr/Nancy.Bundle.git/)



1. `Install-Package Nancy.Bundle`

1. Use a `DefaultConfigSettings` class or create your own
```c#
	using Nancy.Bundle.Settings;

	public class MyBundleConfig : DefaultConfigSettings
	{
		public override string CommonAssetsRoute
		{
			get
			{
				//The default route is '/assets' now changing to '/cli-bundles'
				return "/cli-bundles";
			}
		}
	}
```
1. Now create you bundles:
```c#
	public class MyJsBundle : JSFiles
	{


		public override List<IContentItem> Contents()
		{
			return new List<IContentItem>() {
				//order is important
				new ContentFile("~/content/lib/jquery-3.0.0.js", eMinify.DoNotMinifyIt),
				new ContentFolder("~/content/app",eRecursive.ThisFolderAndChildrenFolders, eMinify.MinifyIt)
			};
		}

		public override string ReleaseKey()
		{
			return "public-js";
		}

		public override string ReleaseUrl()
		{
			return "/js";
			//this will result `<your-host>/cli-bundles/js` route for js
		}
	}
```
```c#
	public class MyCssBundle : CSSFiles
	{
		public override List<IContentItem> Contents()
		{
			return new List<IContentItem>()
			{
				//order is important
				new ContentFile("~/css/style2.css", eMinify.MinifyIt),
				new ContentFile("~/css/style1.css", eMinify.MinifyIt)

			};
		}

		public override string ReleaseKey()
		{
			return "public-css";
		}

		public override string ReleaseUrl()
		{
			return "/public/css";
			//this will result `<your-host>/cli-bundles/public/css` route for css
		}
	}
```
1. Add bundles to `MyBundleConfig` class and attach to your applications bootstrapper class

```c#
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{

			var config = new MyBundleConfig();
			config.AddContentGroup(new MyCssBundle());
			config.AddContentGroup(new MyJsBundle());
			NancyBundle.Attach(container, config);
		}
```

1. Then in your views (this is a Razor View, but should work in other view engines as well)

```html
@using Nancy.Bundle
@using WebSampleApp.Bundles
<!DOCTYPE html>
<html>
<head>
	<title>Example view</title>
	<meta charset="utf-8" />
	<!--This Example uses key as string -->
	@Html.Raw(Bundles.GetCssKey("my-css"))
</head>
<body>

	<!--For strong type reasons this line uses an instance of the bundle to get the key-->
  @Html.Raw(Bundles.GetJsKey(new MyJsBundle().ReleaseKey()))
</body>
</html>
```

##Debug vs Release

In Debug mode will produce:
```html
<!-- In substitution of @Html.Raw(Bundles.GetCssKey("my-css")))-->
	<link rel="stylesheet" href="/css/style2.css" />
	<link rel="stylesheet" href="/css/style1.css" />

	
<!-- In substitution of @Html.Raw(Bundles.GetJsKey(new MyJsBundle().ReleaseKey())-->
	<script src="/content/lib/jquery-3.0.0.js"></script>
	<script src="/content/app/app.js"></script>
	<script src="/content/app/modules/javascript.js"></script>
	<script src="/content/app/modules/javascriptFile2.js"></script>
```

In Release mode will produce

```html
<!-- In substitution of @Html.Raw(Bundles.GetCssKey("my-css")))-->
	<link rel="stylesheet" href="/cli-bundles/public/css?r=23AE1B33C234C23E3B09673C" />

<!-- In substitution of @Html.Raw(Bundles.GetJsKey(new MyJsBundle().ReleaseKey())-->
	<script scr="/cli-bundles/js?r=43AC3C23E32E24F234E3443AD"></script>
```
	
##Road map: 

1.  Async 
1.  <s>Return Http header `304 Not Modified` for etag aleready sent to client. Now it always return `200 OK` and always leave to the browser control the use of the client cache</s>(Done!)

Before, always returning `200`:
<pre>
Concurrency Level:	    400
Time taken for tests:   37.458 seconds
Complete requests:		10000
</pre>

After, returning `304`:
<pre>
Concurrency Level:      400
Time taken for tests:   8.307 seconds
Complete requests:	    10000
</pre>
1.  Suggestions are welcome

##Dependencies

1. Nancy 
1. SquishIt

##Pull-Requests are welcome

##License
	MIT
