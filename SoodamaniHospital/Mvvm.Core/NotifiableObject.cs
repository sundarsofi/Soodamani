using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.PubSubEvents;

namespace Mvvm.Core
{
    public class NotifiableObject : INotifyPropertyChanged, INotifyPropertyChanging
    {
        

        public NotifiableObject()
        {

        }
        
      
        public event PropertyChangedEventHandler PropertyChanged;

        protected PropertyChangedEventHandler PropertyChangedHandler
        {
            get
            {
                return PropertyChanged;
            }
        }

        public event PropertyChangingEventHandler PropertyChanging;

        protected PropertyChangingEventHandler PropertyChangingHandler
        {
            get
            {
                return PropertyChanging;
            }
        }

        protected virtual void RaisePropertyChanging(string propertyName)
        {
            var handler = PropertyChanging;
            if (handler != null)
            {
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void RaisePropertyChanging<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = PropertyChanging;
            if (handler != null)
            {
                var propertyName = GetPropertyName(propertyExpression);
                handler(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                var propertyName = GetPropertyName(propertyExpression);
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected static string GetPropertyName<T>(Expression<Func<T>> propertyExpression)
        {
            if (propertyExpression == null)
            {
                throw new ArgumentNullException("propertyExpression");
            }

            var body = propertyExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Invalid argument", "propertyExpression");
            }

            var property = body.Member as PropertyInfo;

            if (property == null)
            {
                throw new ArgumentException("Argument is not a property", "propertyExpression");
            }

            return property.Name;
        }

        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            RaisePropertyChanging(propertyExpression);
            field = newValue;
            RaisePropertyChanged(propertyExpression);
            return true;
        }

        protected bool Set<T>(string propertyName, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            RaisePropertyChanging(propertyName);
            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }
        
        protected bool Set<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            return Set(propertyName, ref field, newValue);
        }
    }
}
