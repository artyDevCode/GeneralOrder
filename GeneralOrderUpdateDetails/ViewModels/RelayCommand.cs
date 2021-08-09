using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace GeneralOrderUpdateDetails
{
    public class RelayCommand : ICommand
    {

        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private Action mAction;
        private Action<object> selectedChangedArgs;
        #endregion
        #region Constructor

        public RelayCommand(Action<object> args)
        {
            selectedChangedArgs = args;
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public RelayCommand(Action action)
        {
            mAction = action;
        }

        #endregion


        #region Public Events

        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (mAction != null)
                mAction();
            else
                selectedChangedArgs(parameter);
        }
    }
}
