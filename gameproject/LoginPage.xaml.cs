using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SQLite;
using gameproject.Tables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace gameproject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

       
        

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PolzSogl());
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.UserName.Equals(EntryUserName.Text) && u.Password.Equals(EntryPassword.Text)).FirstOrDefault();
            if(myquery!=null)
            {
                App.Current.MainPage = new NavigationPage(new HomePage());
            }
            else
            {
                Device.BeginInvokeOnMainThread(async () => {

                    var result = await this.DisplayAlert("Ошибка", "Неправильное имя или пароль", "Хорошо", "Закрыть");

                    if (result)
                        await Navigation.PushAsync(new LoginPage());
                    else
                    {
                        await Navigation.PushAsync(new LoginPage());
                    }
                });
            }
        }
    }
}