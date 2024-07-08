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
        private int applicationsCount;
        public int ApplicationsCount { get => applicationsCount; set=>SetProperty(ref applicationsCount, value); }
        private int applicationsCanceledCount;
        public int ApplicationsCanceledCount { get => applicationsCanceledCount; set => SetProperty(ref applicationsCanceledCount, value); }
        private int applicationsDoneCount;
        public int ApplicationsDoneCount { get => applicationsDoneCount; set => SetProperty(ref applicationsDoneCount, value); }
        private int applicationsSendedCount;
        public int ApplicationsSendedCount { get => applicationsSendedCount; set => SetProperty(ref applicationsSendedCount, value); }
        private int applicationsNewCount;
        public int ApplicationsNewCount { get => applicationsNewCount; set => SetProperty(ref applicationsNewCount, value); }
        private ObservableCollection<ApplicationEntity> applications;
        public ObservableCollection<ApplicationEntity> Applications { get => applications; set => SetProperty(ref applications, value); }
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
        public async Task ExecuteLoadItemsAsync() //загрузка items в коллекции для поиска
        {
            Applications.Clear();
            allItems.Clear();
            var applications = await DataBaseHelper.GetItemsAsync<ApplicationEntity>();
            allItems=new ObservableCollection<ApplicationEntity>(applications);
            Applications=new ObservableCollection<ApplicationEntity>(applications);
            ApplicationsCount = Applications.Count;
            ApplicationsCanceledCount = Applications.Count(x => x.Status == "Отменено");
            ApplicationsDoneCount = Applications.Count(x => x.Status == "Выполнено");
            ApplicationsSendedCount = Applications.Count(x => x.Status == "Передано на выполнение");
            ApplicationsNewCount = Applications.Count(x => x.Status == "Новая");
        }
    }
}
