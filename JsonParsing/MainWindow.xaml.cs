using JsonParsing.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http.Headers;

namespace JsonParsing
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int ROLE_ID = 1;

        public MainWindow()
        {
            InitializeComponent();

            using (var webClient = new WebClient())
            {
                // Устанавливаем кодировку для символов кириллицы
                webClient.Encoding = Encoding.UTF8;
                // Модель - JsonModel
                //String url = "http://vongu3-001-site2.ctempurl.com/api/category";
                // Модель - AnotherJsonModel
                //String url = "http://vongu3-001-site2.ctempurl.com/api/Ads";
                // Модель - MoscowNews
                String url = "https://school.moscow/api/projects_posts/v0/posts";
                String rawJson = webClient.DownloadString(url);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                MoscowNews moscowNews = JsonConvert.DeserializeObject<MoscowNews>(rawJson);
                //List<AnotherJsonModel> jsonModel = JsonConvert.DeserializeObject<List<AnotherJsonModel>>(rawJson, settings);
                //List<JsonModel> jsonModel = JsonConvert.DeserializeObject<List<JsonModel>>(rawJson, settings);

                //Егор - твое дз спарсить вот это
                // http://api.themoviedb.org/3/movie/upcoming?api_key=fa844ca3beb3b19b4a871a3662894d27
                // http://vongu3-001-site2.ctempurl.com/api/userinfo

                // post запрос
                // Создаем модель UserInfo (там указываем поля, которые надо передать) и заполняем данными
                UserInfo user = new UserInfo();
                user.Email = "VS2@mail.ru";
                user.Password = "1234";
                user.PhoneNumber = "89267090729";
                user.RoleId = 1;
                user.CompanyName = "MyCompany2";
                // Превращаем обььект класса в json с помощью библиотеки NewtonSoft.Json
                // Передаем в метод SerializeObject наш обьект с данными
                string Serialized = JsonConvert.SerializeObject(user);
                // Создаем Http клиент для отправки данных на сервер
                using (var client = new HttpClient())
                {
                    // чистим там все вначале
                    client.DefaultRequestHeaders.Accept.Clear();
                    // Говорим что работаем с json
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // Преобразуем данные в специальный вид для отправки
                    HttpContent content = new StringContent(Serialized, Encoding.Unicode, "application/json");
                    // делаем Post запрос (передаем url и данные)
                    var response = client.PostAsync("http://vongu3-001-site2.ctempurl.com/api/Registration", content);
                    //var status = response.Status;
                    // Этот шаг обязательный!!! без него не работает
                    // возвращаем результат через геттер Result
                    var result = response.Result;
                    // И выводим на экран (там будет вся информация, статус запроса 200 - все ок)
                    MessageBox.Show(result.ToString());
                    // Вот и все!=)
                    // Твоя задача создать форму регистрации, регистрируешь пользователей на сервере
                    // потом авторизуешь, там есть GET запрос для этого, ну а GET мы уже работали!
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
