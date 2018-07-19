using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using System;
using Android.Views;
using Android.Support.Design.Widget;
using System.Threading.Tasks;
using ME.Itangqi.Waveloadingview;
using Java.Lang;
using System.Drawing;
using Android.Content.PM;
using Android.Gms.Ads;

namespace BodyMassIndex
{
    [Activity(Label = "Δείκτης Μάζας Σώματος", MainLauncher = true, Theme = "@style/TextLabel", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
        private EditText TextAge;
        private EditText TextWeight;
        private EditText TextHeight;
        private ImageButton ImgFemale;
        private ImageButton ImgMale;
        private ImageButton DeleteText;
        WaveLoadingView waveLoadingView;
        private TextInputLayout mtextLayoutAge;
        private TextInputLayout mtextLayoutWeight;
        private TextInputLayout mtextLayoutHeight;
        private TextView mtextEllipobaris;
        private TextView mtextNormalWeight;
        private TextView mtextOverWeight;
        private TextView mtextObese;
        private TextView mtextInfectiousObesity;
        private TextView TextCategory;
        private TextView TextOfKg;
        private Button mButton;
        double Result;
        string tmp;

        protected override void OnCreate(Bundle bundle )
        {
            RequestWindowFeature(WindowFeatures.NoTitle);

            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            TextAge = FindViewById<EditText>(Resource.Id.age);
            TextWeight = FindViewById<EditText>(Resource.Id.weight);
            TextHeight = FindViewById<EditText>(Resource.Id.Height);
            ImgFemale = FindViewById<ImageButton>(Resource.Id.female);
            ImgMale = FindViewById<ImageButton>(Resource.Id.male);
            waveLoadingView = FindViewById<WaveLoadingView>(Resource.Id.waveloadingView);
            mtextLayoutAge = FindViewById<TextInputLayout>(Resource.Id.textLayoutAge);
            mtextLayoutWeight = FindViewById<TextInputLayout>(Resource.Id.textLayoutWeight);
            mtextLayoutHeight = FindViewById<TextInputLayout>(Resource.Id.textLayoutHeight);
            mButton = FindViewById<Button>(Resource.Id.button);
            TextCategory = FindViewById<TextView>(Resource.Id.textCategory);
            TextOfKg = FindViewById<TextView>(Resource.Id.textKg);
            mtextEllipobaris = FindViewById<TextView>(Resource.Id.textEllipobaris);
            mtextNormalWeight = FindViewById<TextView>(Resource.Id.textNormalWeight);
            mtextOverWeight = FindViewById<TextView>(Resource.Id.textΟverWeight);
            mtextObese = FindViewById<TextView>(Resource.Id.textObese);
            mtextInfectiousObesity = FindViewById<TextView>(Resource.Id.textInfectiousObesity);
            AdView adView = FindViewById<AdView>(Resource.Id.adView);
            AdView adView2 = FindViewById<AdView>(Resource.Id.adView2);
            DeleteText = FindViewById<ImageButton>(Resource.Id.deleteText);

            AdRequest adRequest = new AdRequest.Builder().Build();
            adView.LoadAd(adRequest);
            AdRequest adRequest2 = new AdRequest.Builder().Build();
            adView2.LoadAd(adRequest2);

            mtextEllipobaris.Text += "                       < 20";
            mtextNormalWeight.Text += "          20 - 24.9";
            mtextOverWeight.Text += "                          25 - 29.9";
            mtextObese.Text += "                        30 - 34.9";
            mtextInfectiousObesity.Text += "      > 35";

            ImgMale.Click += ImgMale_Click;
            ImgFemale.Click += ImgFemale_Click;
            mButton.Click += MButton_Click;
            DeleteText.Click += DeleteText_Click;

        }

        private void DeleteText_Click(object sender, EventArgs e)
        {
            TextAge.Text = "";
            TextWeight.Text = "";
            TextHeight.Text = "";
            ImgFemale.SetImageResource(Resource.Drawable.female_grey);
            ImgMale.SetImageResource(Resource.Drawable.male_grey);
        }

        private void MButton_Click(object sender, EventArgs e)
        {
            if (TextAge.Text == string.Empty)
            {
                mtextLayoutAge.ErrorEnabled = true;
                mtextLayoutAge.Error = "Συμπληρώστε το πεδίο";
            }
            if (TextWeight.Text == string.Empty)
            {
                mtextLayoutWeight.ErrorEnabled = true;
                mtextLayoutWeight.Error = "Συμπληρώστε το πεδίο";
            }
            if (TextHeight.Text == string.Empty)
            {
                mtextLayoutHeight.ErrorEnabled = true;
                mtextLayoutHeight.Error = "Συμπληρώστε το πεδίο";
            }
            else
            {
                mtextLayoutAge.ErrorEnabled = false;
                mtextLayoutWeight.ErrorEnabled = false;
                mtextLayoutHeight.ErrorEnabled = false;
                waveLoadingView.ProgressValue = 0;

                Result = Convert.ToInt32(TextWeight.Text) / System.Math.Pow(Convert.ToInt32(TextHeight.Text), 2);
                tmp = Convert.ToString(Result);
                tmp =  tmp.Remove(0, 4);
                if(tmp.Length > 4)
                {
                    Result = Convert.ToDouble(tmp.Remove(4));
                    Result = Result / 100;
                }
                else
                {
                    Result = Convert.ToDouble(tmp);
                }


                if (Result < 20)
                {
                    waveLoadingView.CenterTitle = Result.ToString();
                    waveLoadingView.ProgressValue = Convert.ToInt32(Result);
                    waveLoadingView.CenterTitleColor = Android.Graphics.Color.White;
                    waveLoadingView.SetCenterTitleStrokeColor(Android.Graphics.Color.Black);
                    TextCategory.Text = "Ελλειποβαρής";
                    TextCategory.SetTextColor(Android.Graphics.Color.LightBlue);
                    waveLoadingView.BorderColor = Android.Graphics.Color.LightBlue;
                    waveLoadingView.WaveColor = Android.Graphics.Color.LightBlue;
                    TextOfKg.Text = "Πάρτε βάρος";
                    TextOfKg.SetTextColor(Android.Graphics.Color.LightBlue);

                }
                else if (Result >= 20 && Result < 24.99)
                {
                    waveLoadingView.CenterTitle = Result.ToString();
                    waveLoadingView.CenterTitleColor = Android.Graphics.Color.White;
                    waveLoadingView.SetCenterTitleStrokeColor(Android.Graphics.Color.Black);
                    waveLoadingView.ProgressValue = Convert.ToInt32(Result);
                    TextCategory.Text = "Φυσιολογικό βάρος";
                    TextCategory.SetTextColor(Android.Graphics.Color.Green);
                    waveLoadingView.BorderColor = Android.Graphics.Color.Green;
                    waveLoadingView.WaveColor = Android.Graphics.Color.Green;
                    TextOfKg.Text = "Μείνετε Σταθεροί";
                    TextOfKg.SetTextColor(Android.Graphics.Color.Green);
                }
                else if(Result >= 25 && Result < 29.99)
                {
                    waveLoadingView.CenterTitle = Result.ToString();
                    waveLoadingView.CenterTitleColor = Android.Graphics.Color.White;
                    waveLoadingView.SetCenterTitleStrokeColor(Android.Graphics.Color.Black);
                    waveLoadingView.ProgressValue = Convert.ToInt32(Result);
                    TextCategory.Text = "Υπέρβαρος";
                    TextCategory.SetTextColor(Android.Graphics.Color.Yellow);
                    waveLoadingView.BorderColor = Android.Graphics.Color.Yellow;
                    waveLoadingView.WaveColor = Android.Graphics.Color.Yellow;
                    TextOfKg.Text = "Απώλεια βάρους";
                    TextOfKg.SetTextColor(Android.Graphics.Color.Yellow);
                }
                else if(Result >= 30 && Result < 34.99 )
                {
                    waveLoadingView.CenterTitle = Result.ToString();
                    waveLoadingView.CenterTitleColor = Android.Graphics.Color.White;
                    waveLoadingView.SetCenterTitleStrokeColor(Android.Graphics.Color.Black);
                    waveLoadingView.ProgressValue = Convert.ToInt32(Result);
                    TextCategory.Text = "Παχύσαρκος";
                    TextCategory.SetTextColor(Android.Graphics.Color.Orange);
                    waveLoadingView.BorderColor = Android.Graphics.Color.Orange;
                    waveLoadingView.WaveColor = Android.Graphics.Color.Orange;
                    TextOfKg.Text = "Απώλεια βάρους";
                    TextOfKg.SetTextColor(Android.Graphics.Color.Orange);
                }
                else if(Result > 35)
                {
                    waveLoadingView.CenterTitle = Result.ToString();
                    waveLoadingView.CenterTitleColor = Android.Graphics.Color.Black;
                    waveLoadingView.SetCenterTitleStrokeColor(Android.Graphics.Color.Black);
                    waveLoadingView.ProgressValue = Convert.ToInt32(Result);
                    TextCategory.Text = "Νοσογόνος παχυσαρκία";
                    TextCategory.SetTextColor(Android.Graphics.Color.Red);
                    waveLoadingView.BorderColor = Android.Graphics.Color.Red;
                    waveLoadingView.WaveColor = Android.Graphics.Color.Red;
                    TextOfKg.Text = "Απώλεια βάρους";
                    TextOfKg.SetTextColor(Android.Graphics.Color.Red);
                }

            }
            
        }


        private void ImgFemale_Click(object sender, EventArgs e)
        {
            ImgFemale.SetImageResource(Resource.Drawable.female);
            ImgMale.SetImageResource(Resource.Drawable.male_grey);
        }

        private void ImgMale_Click(object sender, EventArgs e)
        {
            ImgMale.SetImageResource(Resource.Drawable.male);
            ImgFemale.SetImageResource(Resource.Drawable.female_grey);
        }
    }
}

