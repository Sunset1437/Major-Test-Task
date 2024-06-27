using MajorTestTask.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MajorTestTask.ViewModels
{
    public class MainPageVM: BaseViewModel
    {
        public Command GoToApplicationCommand => new(async () => await Shell.Current.GoToAsync(nameof(ApplicationPage)));
    }
}
