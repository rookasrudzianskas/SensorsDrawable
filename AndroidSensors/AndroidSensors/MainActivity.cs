using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Hardware;
using Android.Content;
using System;
using Android.Views;
using Android.Util;

namespace AndroidSensors
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        SensorManager sensMan;
        ImageView imgSensor;
        TableLayout tblLayout;
        TableRow row;
        TableRow.LayoutParams param;
        TextView lblName;
        LinearLayout lLayout;
        DisplayMetrics disp = new DisplayMetrics();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            tblLayout = FindViewById(Resource.Id.tblLayout) as TableLayout;

            sensMan = GetSystemService(Context.SensorService) as SensorManager;
            if (sensMan == null) return;
            var sensors = sensMan.GetSensorList(SensorType.All);
            row = new TableRow(this);

            param = new TableRow.LayoutParams(300,300);

            foreach (var item in sensors)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine(item.StringType);
                Console.WriteLine("=======================");
                imgSensor = new ImageView(this);
                lblName = new TextView(this);
                lLayout = new LinearLayout(this);
                

                imgSensor.LayoutParameters = new TableRow.LayoutParams(150,150);
                lblName.Text = item.Name;
                if (item.Type == SensorType.Accelerometer)
                    imgSensor.SetImageResource(Resource.Drawable.main_gravity);
                    
                else if (item.Type == SensorType.AmbientTemperature)
                    imgSensor.SetImageResource(Resource.Drawable.main_tempe);

                else if (item.Type == SensorType.Gyroscope)
                    imgSensor.SetImageResource(Resource.Drawable.main_gyro);

                else if (item.Type == SensorType.Light)
                    imgSensor.SetImageResource(Resource.Drawable.main_light);

                else if (item.Type == SensorType.MagneticField)
                    imgSensor.SetImageResource(Resource.Drawable.main_magn);

                else if (item.Type == SensorType.Orientation)
                    imgSensor.SetImageResource(Resource.Drawable.main_orien);

                else if (item.Type == SensorType.Pressure)
                    imgSensor.SetImageResource(Resource.Drawable.main_pressure);

                else if (item.Type == SensorType.Proximity)
                    imgSensor.SetImageResource(Resource.Drawable.main_dist);
                else
                {
                    imgSensor = null;
                    lblName = null;
                    lLayout = null;
                    continue;
                }
                lLayout.Orientation = Orientation.Vertical;
                lLayout.AddView(imgSensor);
                lLayout.AddView(lblName);
                //lLayout.LayoutParameters = new TableRow.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent);
                lLayout.LayoutParameters = param;
                lLayout.Click += delegate
                {
                    Intent sensor = new Intent(this, typeof(SensorActivity));
                    sensor.PutExtra("sensorType", item.Type.ToString());
                    StartActivity(sensor);
                };
                row.AddView(lLayout);

                if (row.ChildCount==3)
                {
                    tblLayout.AddView(row);
                    row = new TableRow(this);
                }
            }
            if(row.ChildCount>0)
            {
                tblLayout.AddView(row);
            }
        }
       
    }
}