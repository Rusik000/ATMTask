using ATMTask.Command;
using ATMTask.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATMTask.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainWindow MainView { get; set; }
        public RelayCommand LoadDataCommand { get; set; }

        public RelayCommand InsertCardCommand { get; set; }

        public RelayCommand TransferMoneyCommand { get; set; }


        private ObservableCollection<Person> _people;

        public ObservableCollection<Person> People
        {
            get { return _people; }
            set { _people = value; OnPropertyChanged(); }
        }

        public void Transfer()
        {
            lock (obj)
            {
                string p1 = MainView.BalanceTxtBlck.Text;
                string p2 = MainView.IncludingTextBx.Text;
                string p3 = MainView.AddingTxtBx.Text;

                if (MainView.IncludingTextBx.Text != null)
                {
                    if (double.Parse(MainView.BalanceTxtBlck.Text) >= 1)
                    {
                        MainView.BalanceTxtBlck.Text = (double.Parse(p1) - double.Parse(p2)).ToString();
                        MainView.AddingTxtBx.Text = (double.Parse(p3) + double.Parse(p2) / 10).ToString();
                    }
                }
            }
        }

        public static object obj = new object();


        public MainViewModel()
        {
            People = new ObservableCollection<Person>()
            {
                new Person()
                {
                    Id=1,
                    Name="Ruslan",
                    Surname="Mustafayev",
                    Balance=1500.40,
                    CardNumber="1234567891234567"
                },
                new Person()
                {
                    Id=2,
                    Name="Rafael",
                    Surname="Xelilzade",
                    Balance=2400.60,
                    CardNumber="1672819163027156"
                },

                new Person()
                {
                    Id=3,
                    Name="Kamran",
                    Surname="Aliyev",
                    Balance=3400.00,
                    CardNumber="5672124596302777"
                }
            };

            LoadDataCommand = new RelayCommand((sender) =>
            {
                if (People != null)
                {
                    foreach (var item in People)
                    {
                        if (MainView.InsertTxtBx.Text == item.CardNumber)
                        {
                            MainView.FullnameTxtBlck.Text = item.Name + " " + item.Surname;
                            MainView.BalanceTxtBlck.Text = item.Balance.ToString();
                        }
                    }
                }
            });


            InsertCardCommand = new RelayCommand((sender) =>
            {
                MainView.InsertTxtBx.Visibility = System.Windows.Visibility.Visible;
            });

            TransferMoneyCommand = new RelayCommand((sender) =>
            {
                MainView.Dispatcher.BeginInvoke( new ThreadStart(() => MainView.DataContext = MainView));

                for (int i = 0; i < 10; i++)
                {
                    ThreadStart start = new ThreadStart(Transfer);
                    new Thread(start).Start();
                }
            });











        }
    }
}
