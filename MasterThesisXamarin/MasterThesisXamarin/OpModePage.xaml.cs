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
	public partial class OpModePage : ContentPage
	{

    public string Baseurl = "http://192.168.178.24";
    HttpClient client = new HttpClient();
    public OpModePage ()
		{
			InitializeComponent ();
      
      OpModeCall();

		}
    async void OpModeCall()
    { 
        var newOpModeData = new Collection<KeyValuePair<string, int>>();
        client.BaseAddress = new Uri(Baseurl);
        client.DefaultRequestHeaders.Clear();
        //Define request data format, here JSON
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        HttpResponseMessage Res = await client.GetAsync("api/OpMode/2018-08-22 17:00:00/2018-08-27 17:00:00/MES721");

        if (Res.IsSuccessStatusCode)
        {
          //Storing the response details recieved from web api
            var OpModeResponse = Res.Content.ReadAsStringAsync().Result;

          //Deserializing the response recieved from web api and storing into the Employee list
          newOpModeData = JsonConvert.DeserializeObject<Collection<KeyValuePair<string, int>>>(OpModeResponse);

        }
          List<Entry> entries = new List<Entry>
          {
            new Entry(newOpModeData[0].Value)
            {
              Color = SKColor.Parse("#444F50"),
              Label = newOpModeData[0].Key.ToString(),
              ValueLabel = newOpModeData[0].Value.ToString(),
            },
            new Entry(newOpModeData[1].Value)
            {
              Color = SKColor.Parse("#003D7C"),
              Label = newOpModeData[1].Key.ToString(),
              ValueLabel = newOpModeData[1].Value.ToString(),
            },
            new Entry(newOpModeData[2].Value)
            {
              Color = SKColor.Parse("#979797"),
              Label = newOpModeData[2].Key.ToString(),
              ValueLabel = newOpModeData[2].Value.ToString(),
            }
          };
        OpModeChart.Chart = new DonutChart()
        {
          Entries = entries,
          BackgroundColor = SKColors.Black,
          LabelTextSize = 18
          
        };
      var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
      leftSwipeGesture.Swiped += OnSwiped;
      var rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
      rightSwipeGesture.Swiped += OnSwiped;
      OpModeChart.GestureRecognizers.Add(leftSwipeGesture);
      OpModeChart.GestureRecognizers.Add(rightSwipeGesture);
     
    }
    void OnSwiped(object sender, SwipedEventArgs e)
    {
      switch (e.Direction)
      {
        case SwipeDirection.Left:
          Navigation.PushAsync(new OkNokPage());
          break;
          
        case SwipeDirection.Right:
          Navigation.PushAsync(new OutputPartsPage());
          break;
        default:
           break;
      }
      
   }
    
  }
}