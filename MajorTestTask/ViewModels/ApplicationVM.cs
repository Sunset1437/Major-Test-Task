using MajorTestTask.DataBase.Entities;
using MajorTestTask.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.ViewModels
{
    public class ApplicationVM : BaseViewModel
    {
        public Command GoToNewApplicationCommand { get; }
        public Command EditApplicationCommand { get; }
        public ObservableCollection<ApplicationEntity> Applications { get; set; }
        public ApplicationVM() 
        {
            Applications = new ObservableCollection<ApplicationEntity>();
            GoToNewApplicationCommand = new Command(OnNewApplicationClicked);
            EditApplicationCommand = new Command(OnEditApplicationClicked);
            ExecuteLoadItemsAsync();
        }

        private async void OnEditApplicationClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewApplicationPage)}?{nameof(NewApplicationVM.IsOnEdit)}={true}");
        }

        public async void OnNewApplicationClicked(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewApplicationPage));
        }

        public async Task ExecuteLoadItemsAsync()
        {
            Applications.Clear();
            Applications.Add(new ApplicationEntity() { SenderAddress= "г.Москва", ReceiverAddress = "г.Екатерининбург", TakingDate = new DateTime(2024,10,20), Weight = 400, Length=20, Height=20, Width=20});
            Applications.Add(new ApplicationEntity() { SenderAddress = "г.Москва", ReceiverAddress = "г.Брянск", TakingDate = new DateTime(2024, 10, 22), Weight = 500, Length = 22, Height = 24, Width = 28 });
        }
    }
}
