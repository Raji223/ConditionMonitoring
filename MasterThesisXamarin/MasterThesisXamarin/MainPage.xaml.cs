using Microcharts;
using SkiaSharp;
using System;
using Microcharts.Forms;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;
using Entry = Microcharts.Entry;
using System.Net.Http;
using System.Net.Http.Headers;
using SkiaSharp.Views.Forms;

namespace MasterThesisXamarin
{
  
  public partial class MainPage : ContentPage
  {
   
    public MainPage()
    {
      NavigationPage.SetHasNavigationBar(this, false);
      
      InitializeComponent();
      EberhardIcon.Source = ImageSource.FromFile("EberhardIcon.png");      
    }

    private void OkNokButtonClicked(object sender, SKPaintSurfaceEventArgs e)
    {
      Navigation.PushAsync(new OkNokPage());
      

    }

    private void OpModeButtonClicked(object sender, SKPaintSurfaceEventArgs e)
    {

    Navigation.PushAsync(new OpModePage());
       
    }
    private void MotorTempButtonClicked(object sender, SKPaintSurfaceEventArgs e)
    {

      Navigation.PushAsync(new MotorTemperature());

    }
    private void OutputButtonClicked(object sender, SKPaintSurfaceEventArgs e)
    {
      Navigation.PushAsync(new OutputPartsPage());
    }
  }
  }

 

