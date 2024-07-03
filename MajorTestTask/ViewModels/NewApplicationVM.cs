using MajorTestTask.DataBase;
using MajorTestTask.DataBase.Entities;
using MajorTestTask.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    [QueryProperty(nameof(IsOnEdit), nameof(IsOnEdit))]
    public class NewApplicationVM : BaseViewModel
    {
        public string Title { get; set; }
        public Command SaveCommand { get; }
        public ObservableCollection<ApplicationEntity> Applications { get; set; }
        #region fields
        private string itemId;
        public string ItemId
        {
            get
            {
                return itemId;
            }
            set
            {
                itemId = value;
                LoadItemId(value).GetAwaiter().GetResult();
            }
        }
        private bool isOnEdit;
        public bool IsOnEdit { get => isOnEdit; set => SetProperty(ref isOnEdit, value); }
        public int StatusCode { get; set; }
        private string senderAddress;
        public string SenderAddress { get => senderAddress; set => SetProperty(ref senderAddress, value); }
        private string receiverAddress;
        public string ReceiverAddress { get => receiverAddress; set => SetProperty(ref receiverAddress, value); }
        private DateTime takingDate = DateTime.Today;
        public DateTime TakingDate { get => takingDate; set => SetProperty(ref takingDate, value); }
        private string weight;
        public string Weight { get => weight; set => SetProperty(ref weight, value); }
        private string length;
        public string Length { get => length; set => SetProperty(ref length, value); }
        private string width;
        public string Width { get => width; set => SetProperty(ref width, value); }
        private string height;
        public string Height { get => height; set => SetProperty(ref height, value); }
        private string? status;
        public string? Status { get => status; set => SetProperty(ref status, value); }
        private string? executor;
        public string? Executor { get => executor; set => SetProperty(ref executor, value); }
        private string? whyIsCanceled;
        public string? WhyIsCanceled { get => whyIsCanceled; set => SetProperty(ref whyIsCanceled, value); }

        private StatusHelper applicationStatus;
        public StatusHelper ApplicationStatus { get => applicationStatus; set => SetProperty(ref applicationStatus, value); }
        #endregion
        public NewApplicationVM() 
        {
            Title = "Добавление новой заявки";
            SaveCommand = new Command(OnSaveCommandClicked,CanExecute);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }
        public async Task LoadItemId(string itemId)
        {
            if (itemId != null)
            {
                IsOnEdit = true;
                Title = "Редактирование заявки";
                using (AppDbContext db = new AppDbContext())
                {
                    var application = await db.Applications.FindAsync(Guid.Parse(itemId)); //получаем заявку по ID
                    if (application != null)
                    {
                        ReceiverAddress = application.ReceiverAddress;
                        SenderAddress = application.SenderAddress;
                        TakingDate = application.TakingDate;
                        Weight = application.Weight.ToString();
                        Width = application.Width.ToString();
                        Height = application.Height.ToString();
                        Length = application.Length.ToString();
                        Executor = application.Executor;
                        Status = application.Status;
                        WhyIsCanceled = application.WhyIsCanceled;
                        ValidatePickerStatus(application); // передача идндекса для пикера на странице редактирования
                    }
                }
            }
            else
                IsOnEdit = false;
        }
        private void ValidatePickerStatus(ApplicationEntity application)
        {
            switch (application.Status)
            {
                case "Новая":
                    ApplicationStatus = StatusHelper.New;
                    break;
                case "Передано на выполнение":
                    ApplicationStatus = StatusHelper.Sended;
                    break;
                case "Выполнено":
                    ApplicationStatus = StatusHelper.Done;
                    break;
                case "Отменено":
                    ApplicationStatus = StatusHelper.Canceled;
                    break;
            }
        }


        private async void OnSaveCommandClicked(object arg)
        {
            // Вес от 0.1 кг до 10000 кг и не может быть отрицательным
            // Длина, ширина и высота не могут быть <0, 0 и больше 1000 см
            StatusCode = 0;
            using var db = new AppDbContext();
            try 
            {
                if(double.Parse(Weight) >= 10000 || double.Parse(Weight) <= 0.1)
                {
                    StatusCode = 1;
                    throw new NotImplementedException();
                }
                if ((double.Parse(Length) >= 1000 || double.Parse(Length) <= 0) || (double.Parse(Width) >= 1000 || double.Parse(Width) <= 0) || (double.Parse(Height) >= 1000 || double.Parse(Height) <= 0))
                {
                    StatusCode = 2;
                    throw new NotImplementedException();
                }
                var application = new ApplicationEntity()
                {
                    SenderAddress = SenderAddress,
                    ReceiverAddress = ReceiverAddress,
                    TakingDate = TakingDate,
                    Weight = double.Parse(Weight),
                    Length = double.Parse(Length),    
                    Width = double.Parse(Width),
                    Height = double.Parse(Height),
                    Status = Status,
                    Executor = Executor,
                    WhyIsCanceled = WhyIsCanceled,
                };
                if (application.Status != "Отменено") // проверяем если при редактировании меняем статус на другой, то убираем whyiscanceled
                {
                    application.WhyIsCanceled = null;
                }
                if(ItemId==null) //если ID пустой то создаём, иначе обновляем
                    await DataBaseHelper.AddItemAsync(application);
                else
                {
                    application.Id = Guid.Parse(itemId);
                    await DataBaseHelper.UpdateItemAsync(application);
                }
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                StatusCode = 3;
                Debug.WriteLine(ex);
            }
            if(StatusCode==0)
                await Shell.Current.GoToAsync("..");
        }
        private bool CanExecute(object arg)
        {
            bool baseCondition = !String.IsNullOrWhiteSpace(SenderAddress)
                    && !String.IsNullOrWhiteSpace(ReceiverAddress)
                    && !String.IsNullOrWhiteSpace(TakingDate.ToString())
                    && !String.IsNullOrWhiteSpace(Weight)
                    && !String.IsNullOrWhiteSpace(Length)
                    && !String.IsNullOrWhiteSpace(Height);
            if (!IsOnEdit)
            {
                return baseCondition;
            }
            else 
            {
                bool editCondition = baseCondition && !String.IsNullOrWhiteSpace(Status);
                switch (Status)
                {
                    case "Отменено":
                        return editCondition && !String.IsNullOrWhiteSpace(WhyIsCanceled);
                    case "Передано на выполнение":
                        return editCondition && !String.IsNullOrWhiteSpace(Executor) && !String.IsNullOrWhiteSpace(WhyIsCanceled);
                    case "Выполнено":
                        return editCondition && !String.IsNullOrWhiteSpace(Executor) && !String.IsNullOrWhiteSpace(WhyIsCanceled);
                    default:
                        return editCondition;
                }
            }

        }
    }
}
