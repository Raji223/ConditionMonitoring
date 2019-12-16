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
	public partial class OutputPartsPage : ContentPage
	{
    
    public string Baseurl = "http://192.168.178.24";
    HttpClient client = new HttpClient();
    public OutputPartsPage ()
		{
			InitializeComponent ();
      OutputPartsCall();
      
		}
    async void OutputPartsCall()
    {
      var newOutputData = new Collection<KeyValuePair<int, int>>();

      HttpClient client = new HttpClient();
      client.BaseAddress = new Uri(Baseurl);
      client.DefaultRequestHeaders.Clear();
      //Define request data format, here JSON 
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      // the various overloading possibilities of the route calls depending on the required format. Comment out the required one.
      // Type 1: get output chart of a particular machine with internally configured time span
      //HttpResponseMessage Res = await client.GetAsync("api/OutputChart/MES721");

      // Type 2: get output chart of a particular machine by explicitly passing the exact from and to times
      ////Returns the data of the explicitly give time span
      HttpResponseMessage Res = await client.GetAsync("api/OutputChart/2018-08-27 17:00:00/24/MES721");

      if (Res.IsSuccessStatusCode)
      {
        //Storing the response details recieved from web api   
        var OutputDataResponse = Res.Content.ReadAsStringAsync().Result;

        //Deserializing the response recieved from web api and storing into the Employee list  
        newOutputData = JsonConvert.DeserializeObject<Collection<KeyValuePair<int, int>>>(OutputDataResponse);

      }
      List<Entry> entries = new List<Entry>();
      for (int i = 0; i < Convert.ToUInt32(newOutputData.Count); i++)
      {
        entries.Add(new Entry(newOutputData[i].Value)
        {
          Color = SKColor.Parse("#FF167A4D"),
          Label = newOutputData[i].Key.ToString(),
          ValueLabel = newOutputData[i].Value.ToString(),
        });
      };
      //{
      //  new Entry(newOutputData[0].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[0].Key.ToString(),
      //    ValueLabel = newOutputData[0].Value.ToString(),
      //  },
      //  new Entry(newOutputData[1].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[1].Key.ToString(),
      //    ValueLabel = newOutputData[1].Value.ToString(),
      //  },
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  },
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  },
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  },
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
      //  new Entry(newOutputData[2].Value)
      //  {
      //    Color = SKColor.Parse("#008BC5"),
      //    Label = newOutputData[2].Key.ToString(),
      //    ValueLabel = newOutputData[2].Value.ToString(),
      //  }
    //};
      OutputPartsCharts.Chart = new BarChart()
      {
        Entries = entries,
        BackgroundColor = SKColors.Black,
        MinValue = 0,
        MaxValue = 80,
        LabelTextSize = 9, 
      };
      var leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
      leftSwipeGesture.Swiped += OnSwiped;
      var rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
      rightSwipeGesture.Swiped += OnSwiped;
      OutputPartsCharts.GestureRecognizers.Add(leftSwipeGesture);
      OutputPartsCharts.GestureRecognizers.Add(rightSwipeGesture);
    }
    void OnSwiped(object sender, SwipedEventArgs e)
    {
      switch (e.Direction)
      {
        case SwipeDirection.Left:
          Navigation.PushAsync(new OpModePage());
          break;
        case SwipeDirection.Right:
          Navigation.PushAsync(new MotorTemperature());
          break;
        default:
          break;
      }
    }
    
  }
	}
