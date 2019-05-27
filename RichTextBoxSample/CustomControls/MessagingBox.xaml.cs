using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RichTextBoxSample.CustomControls
{
    /// <summary>
    /// Interaction logic for MessagingBox.xaml
    /// </summary>
    public partial class MessagingBox : UserControl
    {
        // at the moment very generic
        object collection;

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ItemsSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource",
                typeof(IEnumerable),
                typeof(MessagingBox), new FrameworkPropertyMetadata(null
                    , FrameworkPropertyMetadataOptions.AffectsMeasure,
                    new PropertyChangedCallback(ItemsSourceChanged)));


        // watchout based on fact we operate on 'object types' we have to do a lost of casts
        // we should do sth generic and benefit from polimorphism, but at the moment I had 
        // no idea to think how do it gently :)
        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var msgBpx = d as MessagingBox;

            if (msgBpx == null)
                throw new Exception("SHould be message box...");

            msgBpx.collection = e.NewValue;

            if (e.OldValue != null)
            {
                try
                {
                    var oldObsCol = e.OldValue as INotifyCollectionChanged;
                    oldObsCol.CollectionChanged -= msgBpx.CollectionChanged;
                }
                catch
                {
                    throw new Exception("Ouppps what a stupid meaningless exception");
                }
                    
            }

            var obsCol = e.NewValue as INotifyCollectionChanged;

            if (obsCol != null)
                obsCol.CollectionChanged += msgBpx.CollectionChanged;
            // based on fact we have sth which is INot...Changed we could benefit from
            // adding components to this window and handle such event - watchout - this 
            // is not thread safe in case some threads are called explicitely
        }

        private void CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {                        
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    var items = e.NewItems;
                    foreach (var item in items)
                    {
                        var richmessage = item as IRichMessage;
                        if (richmessage != null)
                        {
                            ParseReachTestMessage(richmessage);
                        } else
                        {
                            // in fact it would mean we have received some object which is 
                            // not IRichMessage - so some other legal binding, in fact we
                            // do not know what it is so just use ToString()...
                            this.DefaultParagraph
                                .Inlines
                                .Add(item.ToString());
                        }
                    }
                    break;
            }
        }

        // this is very ugly code - just to give overview how could we treat this..
        private void ParseReachTestMessage(IRichMessage richmessage)
        {           
            // some linq - in fact we could try to use embedded parsers, but let's
            // do this brutal way...
            Run A = new Run(richmessage.Content);

            var foreground = from property in richmessage.GetProperties()
                             where property.Item1 == "Foreground"
                             select property.Item2;

            A.Foreground = new SolidColorBrush((Color)ColorConverter
                .ConvertFromString
                (foreground.First()));

            this.DefaultParagraph
                .Inlines
                .Add(A);
        }

        public MessagingBox()
        {
            InitializeComponent();            
        }
    }
}
