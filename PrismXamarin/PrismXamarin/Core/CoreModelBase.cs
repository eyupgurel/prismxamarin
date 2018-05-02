using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Xamarin.Forms;

namespace PrismXamarin.Core
{
    public class CoreModelBase : BindableObject, INotifyPropertyChanged, INotifyCollectionChanged
    {
        // ReSharper disable once EmptyConstructor
        public CoreModelBase() //A parameterless constructor is required for serialization so might as well add it now, just in case.
        {

        }

        internal string GetStringFromResource(string word)
        {
            try
            {
                return (string)Application.Current.Resources[word];
            }
            catch (Exception)
            {
                //Word not in resources
            }
            return string.Empty;
        }

        #region Events
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public new event PropertyChangedEventHandler PropertyChanged;
        #endregion Events

        #region INotifyPropertyChanged Members

        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> e)
        {
            try
            {
                var handler = PropertyChanged;

                if (handler == null) return;

                var ts = e.ToString();
                var collection = ts.Split('.');

                if (!collection.Any()) return;

                var name = collection[collection.Length - 1];

                if (name == null) return;

                handler.Invoke(this, new PropertyChangedEventArgs(name));
            }
            catch (Exception)
            {
                //Do Nothing
            }
        }

        /// <summary>
        /// Filters properties that don't need to be persisted across launches
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //Do Nothing
        }

        #endregion INotifyPropertyChanged Members

        protected virtual void OnCollectionChanged<T>(Expression<Func<T>> e)
        {
            var handler = CollectionChanged;

            handler?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            //OnPropertyChanged(() => e);//If the contents of the property changed, then the property itself changed as far as binding is concerned
            //On the downside any ListView will see the entire collection as changed and repaint the entire control.
        }

        protected static object GetPropValue(object src, string propName)
        {
            try
            {
                if (src == null || string.IsNullOrWhiteSpace(propName)) return string.Empty;
                var t = src.GetType();
                //var n = t.GetProperty(propName, BindingFlags.IgnoreCase);
                var n = t.GetProperty(propName);
                if (n == null) return null;
                var x = n.GetValue(src, null);
                //object x = src.GetType().GetProperty(propName).GetValue(src, null);
                return x;
            }
            catch (Exception)
            {
                return null;
            }
        }
        protected static void SetPropertyValue(object target, string propertyName, object propertyvalue, string dataType)
        {
            try
            {
                if (target == null || propertyName == null || propertyvalue == null) return;
                var type = target.GetType();
                var prop = type.GetProperty(propertyName);
                //var prop = type.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.FlattenHierarchy);
                if (prop != null)
                {
                    object typedObject;
                    typedObject = Convert.ChangeType(propertyvalue, Type.GetType(dataType));
                    if (GetPropValue(target, propertyName) != typedObject)
                    {
                        prop.SetValue(target, typedObject, null);
                    }
                }
            }
            catch (Exception)
            {
                // Do nothing
            }
        }
    }
}
