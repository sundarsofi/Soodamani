using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Windows;

namespace Mvvm.Core
{
    public abstract class BaseViewModel : NotifiableObject
        {
        private static bool? _isInDesignMode;

        public BaseViewModel(IEventAggregator eventAggregator)
            :base(eventAggregator)
        {
        }

        protected virtual void RaisePropertyChanged<T>(string propertyName, T oldValue = default(T), T newValue = default(T))
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("This method cannot be called with an empty string", "propertyName");
            }

            RaisePropertyChanged(propertyName);
        }

        protected virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression, T oldValue, T newValue)
        {
            var handler = PropertyChangedHandler;

            if (handler != null)
            {
                var propertyName = GetPropertyName(propertyExpression);

                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        protected bool Set<T>(Expression<Func<T>> propertyExpression, ref T field, T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            RaisePropertyChanging(propertyExpression);
            var oldValue = field;
            field = newValue;
            RaisePropertyChanged(propertyExpression, oldValue, field);
            return true;
        }

        protected bool Set<T>(string propertyName, ref T field, T newValue = default(T))
        {
            if (EqualityComparer<T>.Default.Equals(field, newValue))
            {
                return false;
            }

            RaisePropertyChanging(propertyName);
            var oldValue = field;
            field = newValue;
            RaisePropertyChanged(propertyName, oldValue, field);
            return true;
        }
    }
}
