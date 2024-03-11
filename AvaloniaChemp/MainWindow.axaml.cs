using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Newtonsoft.Json;

namespace AvaloniaChemp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void LoginButton(object? sender, RoutedEventArgs e)
    {
        using (var Client = new HttpClient())
        {
            if (!string.IsNullOrEmpty(LoginText.Text))
            {
                HttpResponseMessage responseMessage = await Client.GetAsync($"http://localhost:5298/api/LoginDesctop/GetLoginDoctor?login={LoginText.Text}");
                string content = await responseMessage.Content.ReadAsStringAsync();
                List<string> result = JsonConvert.DeserializeObject<List<string>>(content)!.ToList();
                if (result.Count == 1)
                {
                    MenuWindow menuWindow = new MenuWindow();
                    menuWindow.Show();
                    Close();
                }
            }
        }
    }
}