using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.Graphics;

namespace AndroidSensors
{
    public class CompassView : View
    {
        public CompassView(Context context):base(context)
        {
            Initialize();
        }
        Paint myPaint;
        bool firstDraw = true;
        private float val;

        private void Initialize()
        {
            myPaint = new Paint();
            myPaint.AntiAlias = true;
            myPaint.Color = Color.Black;
            myPaint.SetStyle(Paint.Style.Stroke);
        }

        protected override void OnDraw(Canvas canvas)
        {
            float xCenter = MeasuredWidth / 2;
            float yCenter = MeasuredHeight / 2;
            float radius;
            if (xCenter > yCenter)
                radius = yCenter * 0.95f;
            else
                radius = xCenter * 0.95f;

            canvas.DrawCircle(xCenter, yCenter, radius, myPaint);
            if(!firstDraw)
            {
                canvas.DrawLine
                    (
                        xCenter,
                        yCenter,
                        xCenter-radius*(float)(Math.Sin(-val*Math.PI/180+Math.PI)),
                        yCenter+radius*(float)(Math.Cos(-val*Math.PI/180+Math.PI)),
                        myPaint
                    );
            }
        }

        public void UpdateDirection(float val)
        {
            firstDraw = false;
            this.val = val;
            Invalidate();
        }
    }
}