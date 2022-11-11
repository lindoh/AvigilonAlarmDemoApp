using System;
using System.Windows;

namespace AvigilonAlarmDemoApp.UI.Services
{
    /// <summary>
    /// Service to Open Windows.
    /// </summary>
    public class DialogueService : IDisposable
    {
        static Window k_logInWindowView = null;
        static Window k_alarmListWindowView = null;
        static Window k_commentWindowView = null;
        static Window k_successWindowView = null;
        static Window k_failureWindowView = null;

        public DialogueService()
        {           

            if (k_alarmListWindowView == null)
            {
                k_alarmListWindowView = new View.AlarmListWindow();
                k_alarmListWindowView.Closing += AlarmListWindowView_Closing;
            }

            if (k_commentWindowView == null)
            {
                k_commentWindowView = new View.CommentWindow();
                k_commentWindowView.Closing += CommentWindowView_Closing;
            }
            if (k_successWindowView == null)
            {
                k_successWindowView = new View.SuccessWindow();
                k_successWindowView.Closing += SuccessWindowView_Closing;
            }
            if (k_failureWindowView == null)
            {
                k_failureWindowView = new View.FailureWindow();
                k_failureWindowView.Closing += FailureWindowView_Closing;
            }
        }

        public void ShowAlarmListWndowViewDialog()
        {
            if (k_alarmListWindowView != null)
                k_alarmListWindowView.ShowDialog();
        }

        public void CloseAlarmListWndowViewDialog()
        {
            if (k_alarmListWindowView != null)
                k_alarmListWindowView.Visibility = Visibility.Hidden;
        }

        public void ShowCommentWindowViewDialog()
        {
            if (k_commentWindowView != null)
                k_commentWindowView.ShowDialog();
        }

        public void CloseCommentWindowViewDialog()
        {
            if (k_commentWindowView != null)
                k_commentWindowView.Visibility = Visibility.Hidden;
        }
       
        void AlarmListWindowView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (k_alarmListWindowView != null)
            {
                e.Cancel = true;
                k_alarmListWindowView.Visibility = Visibility.Hidden;
            }
        }
        void CommentWindowView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (k_commentWindowView != null)
            {
                e.Cancel = true;
                k_commentWindowView.Visibility = Visibility.Hidden;
            }
        }
        public void ShowSuccessWndowViewDialog()
        {
            if (k_successWindowView != null)
                k_successWindowView.ShowDialog();
        }

        public void CloseSuccessWndowViewDialog()
        {
            if (k_successWindowView != null)
                k_successWindowView.Visibility = Visibility.Hidden;
        }

        void SuccessWindowView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (k_successWindowView != null)
            {
                e.Cancel = true;
                k_successWindowView.Visibility = Visibility.Hidden;
            }
        }
        public void ShowFailureWndowViewDialog()
        {
            if (k_failureWindowView != null)
                k_failureWindowView.ShowDialog();
        }

        public void CloseFailureWndowViewDialog()
        {
            if (k_failureWindowView != null)
                k_failureWindowView.Visibility = Visibility.Hidden;
        }

        void FailureWindowView_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (k_failureWindowView != null)
            {
                e.Cancel = true;
                k_failureWindowView.Visibility = Visibility.Hidden;
            }
        }

        public void Dispose()
        {
            // If this function is being called the user wants to release the
            // resources. lets call the Dispose which will do this for us.
            Dispose(true);

            // Now since we have done the cleanup already there is nothing left
            // for the Finalizer to do. So lets tell the GC not to call it later.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing == true)
            {
                if (k_alarmListWindowView != null)
                {
                    k_alarmListWindowView.Closing -= AlarmListWindowView_Closing;
                    k_alarmListWindowView = null;
                }

                if (k_commentWindowView != null)
                {
                    k_commentWindowView.Closing -= CommentWindowView_Closing;
                    k_commentWindowView = null;
                }
                if (k_successWindowView != null)
                {
                    k_successWindowView.Closing -= SuccessWindowView_Closing;
                    k_successWindowView = null;
                }
                if (k_failureWindowView != null)
                {
                    k_failureWindowView.Closing -= FailureWindowView_Closing;
                    k_failureWindowView = null;
                }
            }
        }

        ~DialogueService()
        {
            // The object went out of scope and finalized is called
            // Lets call dispose in to release unmanaged resources 
            // the managed resources will anyways be released when GC 
            // runs the next time.
            Dispose(false);
        }
    }
}