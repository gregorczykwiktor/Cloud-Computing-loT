using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Dashboard : TabbedPage
    {
        public Dashboard()
        {
            InitializeComponent();

            //btnGoBack.Clicked += (s, e) => Navigation.PopAsync();

            Children.Add(new TabPage1());
            Children.Add(new TabPage2());
        }
    }
}