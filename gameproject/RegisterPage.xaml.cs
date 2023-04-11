using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Xamarin.Forms;
using SQLite;
using gameproject.Tables;
using Xamarin.Forms.Xaml;

namespace gameproject
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<RegUserTable>();

            var item = new RegUserTable()
            {
                UserName = EntryUserName.Text,
                Password = EntryUserPassword.Text,
                Email = EntryUserEmail.Text,
                PhoneNumber = EntryUserPhoneNumber.Text

            };

            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () => {

                var result = await this.DisplayAlert("Поздравляем", "Пользователь успешно зарегистрирован", "Хорошо", "Закрыть");

                if (result)
                    await Navigation.PushAsync(new LoginPage());
            });

            
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}