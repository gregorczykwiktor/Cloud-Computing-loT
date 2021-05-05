using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //var tabbedpage = new tabbedpage();

            //tabbedpage.children.add(new tabpage1());
            //tabbedpage.children.add(new tabpage2());

            //mainpage = tabbedpage;

            var mainPage = new MainPage();
            var navPage = new NavigationPage(mainPage);

            MainPage = navPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
