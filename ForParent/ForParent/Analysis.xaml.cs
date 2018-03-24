using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ForParent
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Analysis : Page
    {
        public Analysis()
        {
            this.InitializeComponent();
            string[] colors = {
                "no",
                "agree",
                "towalk",
                "car",
                "grandmother",
                "dad",
                "bro",
                "dinner",
                "for",
                "anger",
                "tosleep",
                "pants",
                "grandfather",
                "sis",
                "mum",
                "watermallon",
                "pain",
                "want",
                "love",
                "shirt",
                "shoes",
                "im",
                "juice",
                "breakfast",
                "allsymbols"



            };

            // Specify the ComboBox item source
            ComBox.ItemsSource = colors;
        }

        private void Button_Click_back(object sender, RoutedEventArgs e)
        {
            Frame toHome = Window.Current.Content as Frame;
            toHome.Navigate(typeof(MainPage));
        }

        private void Button_Click_graph(object sender, RoutedEventArgs e)
        {
            string completeUri = "http://chart.googleapis.com/chart?cht=bvg&chs=400x300&chxs=0,6699ff,12,0,lt|1,6699ff,10,1,lt&chxt=x,y&chxl=0:|Jan|Feb|Mar|Apr|May|June|July|Aug|Sep|Oct|Nov|Dec&chd=t:10,70,50,40,80,5,25,95,3,5,7&chf=bg,s,ccffff&chco=3399ff";
            Uri requestUri = new Uri(completeUri);
            image.Source = new BitmapImage(requestUri);
        }

        public async Task<string> create_graphAsync(string combox)
        {
            symbol userSymbol = new symbol();
            userSymbol.email = Common.myChild;
            userSymbol.symbolName = combox;
            symbol[] userSymbolUsage = await Common.GetUserCounterAsync(combox.ToString());
            string graph_values = "";
            int[] month_values = new int[12];
            for (int i= 0;i< userSymbolUsage.Length; i++){
                month_values[userSymbolUsage[i].date.Month] += 1;
            }
            for (int i = 0; i < 11; i++) {
                graph_values += month_values[i].ToString() + ",";
            }
            graph_values += month_values[11].ToString();        
            return "http://chart.googleapis.com/chart?cht=bvg&chs=380x250&chxs=0,6699ff,12,0,lt|1,6699ff,10,1,lt&chxt=x,y&chxl=0:|Jan|Feb|Mar|Apr|May|June|July|Aug|Sep|Oct|Nov|Dec&chd=t:"+ graph_values +"&chf=bg,s,ccffff&chco=3399ff";
        }


        private async void ComBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string completeUri = "";
            if (ComBox.SelectedItem.ToString().Equals("allsymbols"))
            {
                completeUri = await create_graphAsync(ComBox.SelectedItem.ToString());
            }
            else {
                completeUri = await create_graphAsync("i" + ComBox.SelectedItem.ToString());
            }
            Uri requestUri = new Uri(completeUri);
            image.Source = new BitmapImage(requestUri);
         }
    }
}
