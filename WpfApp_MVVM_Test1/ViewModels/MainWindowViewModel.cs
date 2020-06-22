using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp_MVVM_Test1.Commands;
using WpfApp_MVVM_Test1.Models;

namespace WpfApp_MVVM_Test1.ViewModels
{
    class MainWindowViewModel:INotifyPropertyChanged
    {
        public Restaurant Restaurant { get; set; }
        public ObservableCollection<DishItemViewModel> DishCollection { get; set; }

        private int count;
        public int Count
        {
            get { return count; }
            set 
            { 
                count = value;
                OnPropertyChanged("Count");
            }
        }

        private double sum;
        public double Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                OnPropertyChanged("Sum");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public DelegateCommand SelectCommand { get; set; }
        public DelegateCommand PlaceOrderCommand { get; set; }

        public MainWindowViewModel()
        {
            this.loadRestaurant();
            this.loadDishCollection();
            this.SelectCommand = new DelegateCommand();
            SelectCommand.ExecuteAction = new Action<object>(this.SelectedExecute);
            this.PlaceOrderCommand = new DelegateCommand();
            PlaceOrderCommand.ExecuteAction = new Action<object>(this.PlaceOrderExecute);
        }

        private void loadDishCollection()
        {
            DishCollection = new ObservableCollection<DishItemViewModel>();
            //string[] lines=File.ReadAllLines(Properties.Resources.DishData);
            string[] lines = Properties.Resources.DishData.Split(new string[] {"\n"}, StringSplitOptions.RemoveEmptyEntries);
            foreach(string line in lines)
            {
                string[] fragments = line.Split(';');
                Dish dish = new Dish
                {
                    Name = fragments[0],
                    Description = fragments[1],
                    Price = Double.Parse(fragments[2])
                };
                DishItemViewModel dishItem = new DishItemViewModel
                {
                    Dish = dish,
                    IsSelected = false,
                };
                DishCollection.Add(dishItem);
            }
            //int i = DishCollection.Count();
        }

        private void loadRestaurant()
        {
            this.Restaurant = new Restaurant()
            {
                Name = "Test-Restaurant",
                WelcomeMessage = "Welcome to Test-Restaurant and enjoy the best food in Melbourne."
            };
        }

        private void SelectedExecute(object obj)
        {
            this.Count = this.DishCollection.Count(i => i.IsSelected == true);
            this.Sum = this.DishCollection.Where(i=>i.IsSelected==true).Sum(dishItem => dishItem.Dish.Price);

        }
        private void PlaceOrderExecute(object obj)
        {
            var selectedDish = this.DishCollection.Where(i => i.IsSelected == true).
                Select(i => i.Dish.Name+"--"+String.Format("{0:C}",i.Dish.Price)).ToList();
            MessageBox.Show(string.Join("\n", selectedDish)+"\n\nCount:"+Count+"\nSum:"+Sum, "Place oder sucessfully.");
        }

    }
}
