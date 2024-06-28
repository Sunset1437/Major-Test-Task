using MajorTestTask.DataBase.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.Extensions
{
    public static class ObservableCollectionExtensions
    {
        public static ObservableCollection<ApplicationEntity> Search(this ObservableCollection<ApplicationEntity> collection, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new ObservableCollection<ApplicationEntity>(collection);

            searchTerm = searchTerm.ToLower();

            return new ObservableCollection<ApplicationEntity>(collection.Where(item =>
                item.SenderAddress.ToLower().Contains(searchTerm) ||
                item.ReceiverAddress.ToLower().Contains(searchTerm) ||
                item.TakingDate.ToString().ToLower().Contains(searchTerm) ||
                item.Weight.ToString().ToLower().Contains(searchTerm) ||
                item.Length.ToString().ToLower().Contains(searchTerm) ||
                item.Width.ToString().ToLower().Contains(searchTerm) ||
                item.Height.ToString().ToLower().Contains(searchTerm) ||
                (item.Status?.ToLower().Contains(searchTerm) ?? false) ||
                (item.Executor?.ToLower().Contains(searchTerm) ?? false) ||
                (item.WhyIsCanceled?.ToLower().Contains(searchTerm) ?? false)));
        }
    }
}
