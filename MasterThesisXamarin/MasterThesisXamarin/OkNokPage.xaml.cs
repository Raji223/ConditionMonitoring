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
using Xamarin.Forms.Xaml;

namespace MasterThesisXamarin
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class OkNokPage : ContentPage
  {

    public string Baseurl = "http://192.168.178.24";
    HttpClient client = new HttpClient();
    public enum OkNokEnum
    {
      UNDEFINED = -1,
      NOK = 0,
      OK = 1
    }
    public OkNokPage()
    {
      InitializeComponent();
      OkNokCall();


    }
    async void OkNokCall()
    {
      var newOkNokData = new Collection<KeyValuePair<OkNokEnum, int>>();
      client.BaseAddress = new Uri(Baseurl);
      client.DefaultRequestHeaders.Clear();
      //Define request data format, here JSON
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      HttpResponseMessage Res = await client.GetAsync("api/OkNokParts/2018-08-22 17:00:00/2018-08-27 17:00:00/MES721");

      if (Res.IsSuccessStatusCode)
      {
        //Storing the response details recieved from web api
        var OkNokResponse = Res.Content.ReadAsStringAsync().Result;

        //Deserializing the response recieved from web api and storing into the Employee list
        newOkNokData = JsonConvert.DeserializeObject<Collection<KeyValuePair<OkNokEnum, int>>>(OkNokResponse);

      }
      List<Entry> entries = new List<Entry>
          {

            new Entry(newOkNokData[0].Value)
            {
              Color = SKColor.Parse("#FF167A4D"),
              Label = newOkNokData[0].Key.ToString(),
              ValueLabel = newOkNokData[0].Value.ToString(),
            },
            new Entry(newOkNokData[1].Value)
            {
              Color = SKColor.Parse("#FFE04143"),
              Label = newOkNokData[1].Key.ToString(),
              ValueLabel = newOkNokData[1].Value.ToString(),
            }
          };
      OkNokPartsChart.Chart = new DonutChart()
      {
        Entries = entries,
        BackgroundColor = SKColors.Black,
        LabelTextSize = 35
        //Margin = 10,
      };
      var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
      leftSwipeGesture.Swiped += OnSwiped;
      var rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
      rightSwipeGesture.Swiped += OnSwiped;
      OkNokPartsChart.GestureRecognizers.Add(leftSwipeGesture);
      OkNokPartsChart.GestureRecognizers.Add(rightSwipeGesture);
    }
    void OnSwiped(object sender, SwipedEventArgs e)
    {
      switch (e.Direction)
      {
        case SwipeDirection.Left:
          Navigation.PushAsync(new MotorTemperature());
          break;
        case SwipeDirection.Right:
          Navigation.PushAsync(new OpModePage());
          break;
        default:
          break;
      }
    }
  }
}