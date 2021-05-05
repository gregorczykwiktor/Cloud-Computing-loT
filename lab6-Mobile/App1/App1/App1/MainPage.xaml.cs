using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            btnGoToDashboard.Clicked += (s, e) => Navigation.PushAsync(new Dashboard());
            btnGoToModal.Clicked += (s, e) => Navigation.PushModalAsync(new Modal());
        }
    }
}
