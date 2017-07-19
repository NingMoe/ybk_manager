using System;
using Android.Graphics;
using Square.Picasso;

namespace YbkManage.App
{
	public class CircleImageTransformation : Java.Lang.Object, ITransformation
	{
        private static String KEY = "circleImageTransformation";

		private readonly Picasso picasso;

		public CircleImageTransformation(Picasso picasso)
		{
			this.picasso = picasso;
		}

		public Bitmap Transform(Bitmap source)
		{
            int minEdge = Math.Min(source.Width, source.Height);
			int dx = (source.Width - minEdge) / 2;
			int dy = (source.Height - minEdge) / 2;

			// Init shader
            Shader shader = new BitmapShader(source, Shader.TileMode.Clamp, Shader.TileMode.Clamp);
			Matrix matrix = new Matrix();
			matrix.SetTranslate(-dx, -dy);   // Move the target area to center of the source bitmap
			shader.SetLocalMatrix(matrix);

			// Init paint
            Paint paint = new Paint(PaintFlags.AntiAlias);
			paint.SetShader(shader);

			// Create and draw circle bitmap
            Bitmap output = Bitmap.CreateBitmap(minEdge, minEdge, source.GetConfig());
			Canvas canvas = new Canvas(output);
            canvas.DrawOval(new RectF(0, 0, minEdge, minEdge), paint);

			// Recycle the source bitmap, because we already generate a new one
			source.Recycle();

			return output;

			//Bitmap result = Bitmap.CreateBitmap(source.Width, source.Height, source.GetConfig());
			//Bitmap noise;
			//try
			//{
			//	noise = picasso.Load(Resource.Drawable.noise).Get();
			//}
			//catch (Exception)
			//{
			//	throw new Exception("Failed to apply transformation! Missing resource.");
			//}

			//BitmapShader shader = new BitmapShader(noise, Shader.TileMode.Repeat, Shader.TileMode.Repeat);

			//ColorMatrix colorMatrix = new ColorMatrix();
			//colorMatrix.SetSaturation(0);
			//ColorMatrixColorFilter filter = new ColorMatrixColorFilter(colorMatrix);

			//Paint paint = new Paint(PaintFlags.AntiAlias);
			//paint.SetColorFilter(filter);

			//Canvas canvas = new Canvas(result);
			//canvas.DrawBitmap(source, 0, 0, paint);

			//paint.SetColorFilter(null);
			//paint.SetShader(shader);
			//paint.SetXfermode(new PorterDuffXfermode(PorterDuff.Mode.Multiply));

			//canvas.DrawRect(0, 0, canvas.Width, canvas.Height, paint);

			//source.Recycle();
			//noise.Recycle();

			//return result;
		}

		public string Key
		{
			get { return KEY; }
		}
	}
}

