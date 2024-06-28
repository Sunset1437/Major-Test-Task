using MajorTestTask.DataBase.Entities;
using MajorTestTask.Extensions;
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
        #region fields
        private ObservableCollection<ApplicationEntity> applications;
        public ObservableCollection<ApplicationEntity> Applications
        {
            get => applications;
            set
            {
                applications = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ApplicationEntity> allItems;
        private string searchTerm;
        public string SearchTerm
        {
            get => searchTerm;
            set
            {
                searchTerm = value;
                OnPropertyChanged();
                PerformSearch();
            }
        }
        #endregion
        public Command GoToNewApplicationCommand { get; }
        public Command EditApplicationCommand { get; }
        public ApplicationVM() 
        {
            allItems = new ObservableCollection<ApplicationEntity>();
            ExecuteLoadItemsAsync();
            Applications = new ObservableCollection<ApplicationEntity>(allItems);
            GoToNewApplicationCommand = new Command(OnNewApplicationClicked);
            EditApplicationCommand = new Command(OnEditApplicationClicked);
            
        }

        private async void OnEditApplicationClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewApplicationPage)}?{nameof(NewApplicationVM.IsOnEdit)}={true}");
        }

        public async void OnNewApplicationClicked(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewApplicationPage));
        }
        private void PerformSearch()
        {
            Applications = allItems.Search(SearchTerm);
        }
        public async Task ExecuteLoadItemsAsync()
        {
            allItems.Clear();
            allItems.Add(new ApplicationEntity() { SenderAddress= "г.Москва", ReceiverAddress = "г.Екатерининбург", TakingDate = new DateTime(2024,10,20), Weight = 400, Length=20, Height=20, Width=20});
            allItems.Add(new ApplicationEntity() { SenderAddress = "г.Москва", ReceiverAddress = "г.Брянск", TakingDate = new DateTime(2024, 10, 22), Weight = 500, Length = 22, Height = 24, Width = 28 });
        }
    }
}
