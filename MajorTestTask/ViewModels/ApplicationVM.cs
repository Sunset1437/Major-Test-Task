using MajorTestTask.DataBase.Entities;
using MajorTestTask.Extensions;
using MajorTestTask.Views;
using MajorTestTask.Helpers;
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
            Applications = new ObservableCollection<ApplicationEntity>(allItems);
            ExecuteLoadItemsAsync().GetAwaiter().GetResult();
            GoToNewApplicationCommand = new Command(OnNewApplicationClicked);
            EditApplicationCommand = new Command<ApplicationEntity>(OnEditApplicationClickedAsync);
            
        }

        private async void OnEditApplicationClickedAsync(ApplicationEntity item)
        {
            await Shell.Current.GoToAsync($"{nameof(NewApplicationPage)}?{nameof(NewApplicationVM.ItemId)}={item.Id}");
        }

        public async void OnNewApplicationClicked(object obj)
        {
            await Shell.Current.GoToAsync($"{nameof(NewApplicationPage)}");
        }
        private void PerformSearch()
        {
            Applications = allItems.Search(SearchTerm);
        }
        public async Task ExecuteLoadItemsAsync()
        {
            Applications.Clear();
            allItems.Clear();
            var applications = await DataBaseHelper.GetItemsAsync<ApplicationEntity>();
            foreach(var item in applications)
            {
                allItems.Add(new ApplicationEntity()
                {
                    Id = item.Id,
                    ReceiverAddress = item.ReceiverAddress,
                    SenderAddress = item.SenderAddress,
                    TakingDate = item.TakingDate,
                    Weight = item.Weight,
                    Width = item.Width,
                    Length = item.Length,
                    Height = item.Height,
                    Status = item.Status,
                    Executor = item.Executor,
                    WhyIsCanceled = item.WhyIsCanceled,
                });
                Applications.Add(new ApplicationEntity()
                {
                    Id = item.Id,
                    ReceiverAddress = item.ReceiverAddress,
                    SenderAddress = item.SenderAddress,
                    TakingDate = item.TakingDate,
                    Weight = item.Weight,
                    Width = item.Width,
                    Length = item.Length,
                    Height = item.Height,
                    Status = item.Status,
                    Executor = item.Executor,
                    WhyIsCanceled = item.WhyIsCanceled,
                });
            }
        }
    }
}
