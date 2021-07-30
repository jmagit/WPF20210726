using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Domains.Core
{
    public abstract class ObservableBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(PropertyChangedEventArgs ev) {
            if (ev != null && PropertyChanged != null) {
                PropertyChanged(this, ev);
            }
        }

        public void RaisePropertyChanged(string PropertyName) {
            OnPropertyChanged(new PropertyChangedEventArgs(PropertyName));
        }


    }
}
