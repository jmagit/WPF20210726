using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demos.Domains.Core
{
    public abstract class Entidad: ObservableBase, INotifyDataErrorInfo, IDataErrorInfo {
        #region errorsContainer
        private readonly Dictionary<string, List<string>> errorsContainer = new Dictionary<string, List<string>>();
        public void ClearErrors() {
            errorsContainer.Clear();
        }
        #endregion
        #region INotifyDataErrorInfo
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected virtual void OnErrorsChanged(string propertyName) {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        public IEnumerable GetErrors(string propertyName) {
            return errorsContainer.ContainsKey(propertyName) ? errorsContainer[propertyName] : null;
        }
        public bool HasErrors => errorsContainer.Any();
        #endregion
        #region IDataErrorInfo
        public string Error => errorsContainer.Any() ? String.Join(" ", errorsContainer.Select(item => $"[{item.Key}]: {this[item.Key]}")) : null;
        public string this[string propertyName] => errorsContainer.ContainsKey(propertyName) ? $"{String.Join(". ", errorsContainer[propertyName])}." : null;
        #endregion
        public bool IsValid => !HasErrors;
        public virtual void Validate() {
            foreach (var prop in this.GetType().GetProperties())
                RaiseValidationProperty(prop.Name);
        }
        public virtual void Validate(params string[] propertiesNames) {
            foreach (var prop in propertiesNames)
                ValidateProperty(prop);
        }

        protected abstract List<string> ValidateProperty(string propertyName);

        protected virtual void RaiseValidationProperty(string propertyName) {
            var rslt = ValidateProperty(propertyName);
            if (errorsContainer.ContainsKey(propertyName)) {
                if (rslt == null || rslt.Count == 0) {
                    errorsContainer.Remove(propertyName);
                    OnErrorsChanged(propertyName);
                } else {
                    errorsContainer[propertyName] = rslt;
                    OnErrorsChanged(propertyName);
                }
            } else if (rslt != null && rslt.Count > 0) {
                errorsContainer.Add(propertyName, rslt);
                OnErrorsChanged(propertyName);
            }
        }
        protected virtual void RaiseValidateAndChangedProperty(string propertyName) {
            RaiseValidationProperty(propertyName);
            RaisePropertyChanged(propertyName);
        }
    }
}
