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
  public partial class MotorTemperature : ContentPage
  {
    public string Baseurl = "http://192.168.178.24";
    HttpClient client = new HttpClient();
    
    public MotorTemperature()
    {
      InitializeComponent();
      MotorTempCall();
    }
    async void MotorTempCall()
    {
      var newTemperatureDataX = new Collection<KeyValuePair<DateTime, float>>();
      var newTemperatureDataY = new Collection<KeyValuePair<DateTime, float>>();
      var newTemperatureDataZ = new Collection<KeyValuePair<DateTime, float>>();
      HttpClient client = new HttpClient();
      client.BaseAddress = new Uri(Baseurl);
      client.DefaultRequestHeaders.Clear();
      //Define request data format, here JSON 
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      ////Returns the data of the explicitly give time span
      HttpResponseMessage ResX = await client.GetAsync("api/TemperatureChart/2018-08-26 17:00:00/2018-08-27 17:00:00/X/MES721");
      HttpResponseMessage ResY = await client.GetAsync("api/TemperatureChart/2018-08-26 17:00:00/2018-08-27 17:00:00/Y/MES721");
      HttpResponseMessage ResZ = await client.GetAsync("api/TemperatureChart/2018-08-26 17:00:00/2018-08-27 17:00:00/Z/MES721");
      if (ResX.IsSuccessStatusCode)
      {
        //Storing the response details recieved from web api   
        var TemperatureDataXResponse = ResX.Content.ReadAsStringAsync().Result;

        //Deserializing the response recieved from web api and storing into the newOkNokData  
        newTemperatureDataX = JsonConvert.DeserializeObject<Collection<KeyValuePair<DateTime, float>>>(TemperatureDataXResponse);

      }
      if (ResY.IsSuccessStatusCode)
      {
        //Storing the response details recieved from web api   
        var TemperatureDataYResponse = ResY.Content.ReadAsStringAsync().Result;

        //Deserializing the response recieved from web api and storing into the newOkNokData  
        newTemperatureDataY = JsonConvert.DeserializeObject<Collection<KeyValuePair<DateTime, float>>>(TemperatureDataYResponse);

      }
      if (ResZ.IsSuccessStatusCode)
      {
        //Storing the response details recieved from web api   
        var TemperatureDataZResponse = ResZ.Content.ReadAsStringAsync().Result;

        //Deserializing the response recieved from web api and storing into the newOkNokData  
        newTemperatureDataZ = JsonConvert.DeserializeObject<Collection<KeyValuePair<DateTime, float>>>(TemperatureDataZResponse);

      }

      List<Entry> entriesX = new List<Entry>();
      //for (int i = 0; i < 5; i++)
      for (int i = 0;i < Convert.ToUInt32(newTemperatureDataX.Count); i++)
      {
        if(newTemperatureDataX[i].Key.Minute % 30 == 0)
        { 
        entriesX.Add(new Entry(newTemperatureDataX[i].Value)
        {
          Color = SKColor.Parse("#FF167A4D"),
          Label = newTemperatureDataX[i].Key.ToString(),
          ValueLabel = newTemperatureDataX[i].Value.ToString(),
        });
        }
      };

      List<Entry> entriesY = new List<Entry>();

      for (int i = 0; i < Convert.ToUInt32(newTemperatureDataY.Count); i++)
      {
        if (newTemperatureDataY[i].Key.Minute % 15 == 0)
        {
          entriesY.Add(new Entry(newTemperatureDataY[i].Value)
          {
            Color = SKColor.Parse("#FF167A4D"),
            Label = newTemperatureDataY[i].Key.ToString(),
            ValueLabel = newTemperatureDataY[i].Value.ToString(),
          });
        }
      };

      List<Entry> entriesZ = new List<Entry>();

      for (int i = 0; i < Convert.ToUInt32(newTemperatureDataZ.Count); i++)
        {
        if (newTemperatureDataZ[i].Key.Minute % 15 == 0)
        {
          entriesZ.Add(new Entry(newTemperatureDataZ[i].Value)
          {
            Color = SKColor.Parse("#FF167A4D"),
            Label = newTemperatureDataZ[i].Key.ToString(),
            ValueLabel = newTemperatureDataZ[i].Value.ToString(),
          });
        }
      };
      //{

      //  new Entry(newTemperatureDataX[0].Value)
      //  {
      //    Color = SKColor.Parse("#FF167A4D"),
      //    Label = newTemperatureDataX[0].Key.ToString(),
      //    ValueLabel = newTemperatureDataX[0].Value.ToString(),
      //  },
      //  new Entry(newTemperatureDataX[1].Value)
      //  {
      //    Color = SKColor.Parse("#FF167A4D"),
      //    Label = newTemperatureDataX[1].Key.ToString(),
      //    ValueLabel = newTemperatureDataX[1].Value.ToString(),
      //  },
      //  new Entry(newTemperatureDataX[2].Value)
      //  {
      //    Color = SKColor.Parse("#FF167A4D"),
      //    Label = newTemperatureDataX[2].Key.ToString(),
      //    ValueLabel = newTemperatureDataX[2].Value.ToString(),
      //  },
      //  new Entry(newTemperatureDataY[0].Value)
      //  {
      //    Color = SKColor.Parse("#FFE04143"),
      //    Label = newTemperatureDataY[0].Key.ToString(),
      //    ValueLabel = newTemperatureDataY[0].Value.ToString(),
      //  },
      //  new Entry(newTemperatureDataY[1].Value)
      //  {
      //    Color = SKColor.Parse("#FFE04143"),
      //    Label = newTemperatureDataY[1].Key.ToString(),
      //    ValueLabel = newTemperatureDataY[1].Value.ToString(),
      //  },
      //   new Entry(newTemperatureDataY[2].Value)
      //  {
      //    Color = SKColor.Parse("#FFE04143"),
      //    Label = newTemperatureDataY[2].Key.ToString(),
      //    ValueLabel = newTemperatureDataY[2].Value.ToString(),
      //  },
      //   new Entry(newTemperatureDataZ[0].Value)
      //  {
      //    Color = SKColor.Parse("#FFE04143"),
      //    Label = newTemperatureDataZ[0].Key.ToString(),
      //    ValueLabel = newTemperatureDataZ[0].Value.ToString(),
      //  },
      //   new Entry(newTemperatureDataZ[1].Value)
      //  {
      //    Color = SKColor.Parse("#FFE04143"),
      //    Label = newTemperatureDataZ[1].Key.ToString(),
      //    ValueLabel = newTemperatureDataZ[1].Value.ToString(),
      //  },
      //  new Entry(newTemperatureDataZ[2].Value)
      //  {
      //    Color = SKColor.Parse("#FFE04143"),
      //    Label = newTemperatureDataZ[2].Key.ToString(),
      //    ValueLabel = newTemperatureDataZ[2].Value.ToString(),
      //  }
      //};
      //List<Entry> entries = new List<Entry>
      //{


      //};
      MotorTempChartX.Chart = new LineChart()
      {
        Entries = entriesX,
        BackgroundColor = SKColors.Black,

        //Margin = 10,
      };
      MotorTempChartY.Chart = new LineChart()
      {
        Entries = entriesY,
        BackgroundColor = SKColors.Black,

        //Margin = 10,
      };
      MotorTempChartZ.Chart = new LineChart()
      {
        Entries = entriesZ,
        BackgroundColor = SKColors.Black,

        //Margin = 10,
      };
      var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
      leftSwipeGesture.Swiped += OnSwiped;
      var rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
      rightSwipeGesture.Swiped += OnSwiped;
      MotorTempChartY.GestureRecognizers.Add(leftSwipeGesture);
      MotorTempChartY.GestureRecognizers.Add(rightSwipeGesture);
      //if(OnBackButtonPressed())
      //{
      //  await Navigation.PopToRootAsync();
      //}
    }
    void OnSwiped(object sender, SwipedEventArgs e)
    {
      Navigation.PopAsync();
      switch (e.Direction)
      {
        case SwipeDirection.Left:
          Navigation.PushAsync(new OutputPartsPage());
          break;
        case SwipeDirection.Right:
          Navigation.PushAsync(new OkNokPage());
          break;
        default:
          break;
      }
    }
  }
  }
