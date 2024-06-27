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
        public NewApplicationVM() 
        {
            if (!IsOnEdit)
                Title = "Добавление новой заявки";
            else
                Title = "Редактирование заявки";
        }
    }
}
