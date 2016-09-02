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
        #region Properties

        /// <summary>
        /// Gets or sets the EventAggregator
        /// </summary>
        public IEventAggregator EventAggregator { get; set; }

        #endregion

        #region Ctor

        /// <summary>
        ///  Initialize a new object for BaseViewModel
        /// </summary>
        /// <param name="eventAggregator"></param>
        public BaseViewModel(IEventAggregator eventAggregator):this()
        {
            this.EventAggregator = eventAggregator;
        }

        /// <summary>
        /// Initialize a new object for BaseViewModel
        /// </summary>
        public BaseViewModel()
        {

        }

        #endregion

        /// <summary>
        /// Raised when ViewModel's property is changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        protected virtual void RaisePropertyChanged<T>(string propertyName, T oldValue = default(T), T newValue = default(T))
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                throw new ArgumentException("This method cannot be called with an empty string", "propertyName");
            }

            RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Raised when ViewModel's property is changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyExpression"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
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
