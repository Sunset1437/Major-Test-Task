using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.ViewModels
{
    //[QueryProperty(nameof(IsOnEdit), nameof(IsOnEdit))]
    public class NewApplicationVM : BaseViewModel
    {
        public string Title { get; set; }
        public bool IsOnEdit { get; set; }
        public Command SaveCommand { get; }
        public Command DeleteCommand { get; }
        #region fields
        private string senderAddress;
        public string SenderAddress { get => senderAddress; set => SetProperty(ref senderAddress, value); }
        private string receiverAddress;
        public string ReceiverAddress { get => receiverAddress; set => SetProperty(ref receiverAddress, value); }
        private DateTime takingDate;
        public DateTime TakingDate { get => takingDate; set => SetProperty(ref takingDate, value); }
        private double weight;
        public double Weight { get => weight; set => SetProperty(ref weight, value); }
        private double length;
        public double Length { get => length; set => SetProperty(ref length, value); }
        private double width;
        public double Width { get => width; set => SetProperty(ref width, value); }
        private double height;
        public double Height { get => height; set => SetProperty(ref height, value); }
        private string? status;
        public string? Status { get => status; set => SetProperty(ref status, value); }
        private string? executor;
        public string? Executor { get => executor; set => SetProperty(ref executor, value); }
        private string? whyIsCanceled;
        public string? WhyIsCanceled { get => whyIsCanceled; set => SetProperty(ref whyIsCanceled, value); }
        #endregion
        public NewApplicationVM() 
        {
            if (!IsOnEdit)
                Title = "Добавление новой заявки";
            else
                Title = "Редактирование заявки";
            SaveCommand = new Command(OnSaveCommandClicked,CanExecute);
            DeleteCommand = new Command(OnDeleteCommandClicked);
            PropertyChanged += (_, __) => SaveCommand.ChangeCanExecute();
        }
        public async Task ExecuteLoadItemsAsync()
        {

        }

        private void OnDeleteCommandClicked(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnSaveCommandClicked(object obj)
        {
            // Вес от 0.1 кг до 10000 кг и не может быть отрицательным
            // Длина, ширина и высота не могут быть <0, 0 и больше 1000 см
            throw new NotImplementedException();
        }
        private bool CanExecute(object arg)
        {
            bool baseCondition = !String.IsNullOrWhiteSpace(SenderAddress)
                    && !String.IsNullOrWhiteSpace(ReceiverAddress)
                    && !String.IsNullOrWhiteSpace(TakingDate.ToString())
                    && !String.IsNullOrWhiteSpace(Weight.ToString())
                    && !String.IsNullOrWhiteSpace(Length.ToString())
                    && !String.IsNullOrWhiteSpace(Height.ToString());
            if (!IsOnEdit)
            {
                return baseCondition;
            }
            else 
            {
                bool editCondition = baseCondition
                           && !String.IsNullOrWhiteSpace(Status)
                           && !String.IsNullOrWhiteSpace(Executor);
                if (Status == "Отменено")
                {
                    return editCondition && !String.IsNullOrWhiteSpace(WhyIsCanceled);
                }
                return editCondition;
            }

        }
    }
}
