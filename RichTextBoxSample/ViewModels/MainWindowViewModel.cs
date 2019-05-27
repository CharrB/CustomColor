using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RichTextBoxSample.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        // because IRichMessage does not implement INotifyPropertyChange...
        // any change done to this message will not lead to direct 
        // display update!
        private ObservableCollection<IRichMessage> someBindableDatas
            = new ObservableCollection<IRichMessage>();

        public ObservableCollection<IRichMessage> BindableCollection
        {
            get { return someBindableDatas; }
            set { SetProperty(ref someBindableDatas, value); }
        }
        
        

        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private ICommand add;
        public ICommand Add
        {
            get { return add; }
            set { SetProperty(ref add, value); }
        }

        public MainWindowViewModel()
        {
            add = new DelegateCommand<string>(AddSthToScreen);
        }

        // here we could use strategy pattern and polimorphism..
        // instead of this whole switch --> Messages.build[obj](someContent);
        private void AddSthToScreen(string obj)
        {
            switch(obj)
            {
                case "SrvCmd":
                    BindableCollection.Add(
                        new ServerMessage($"Server: {DateTime.Now.ToLongTimeString()}\n"));
                    break;
                case "LogAct":
                    BindableCollection.Add(
                        new ConnectionMessage($"Conn: {DateTime.Now.ToLongTimeString()}\n"));
                    break;
                case "Err":
                    BindableCollection.Add(
                        new ErrorMessage($"Error: {DateTime.Now.ToLongTimeString()}\n"));
                    break;

                default:
                    break;
            }            
        }
    }
}
