using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp_MVVM_Test1.Models;

namespace WpfApp_MVVM_Test1.ViewModels
{
    class DishItemViewModel:INotifyPropertyChanged
    {
        public Dish Dish{get;set;}
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set 
            { 
                isSelected = value;
                OnPropertyChanged("IsSelected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => 
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
