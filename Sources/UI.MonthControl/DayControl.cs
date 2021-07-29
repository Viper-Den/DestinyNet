﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace UIMonthControl
{
    [TemplatePart(Name = DayControl.TP_TITLE_PART, Type = typeof(FrameworkElement))]
    public class DayControl : Control
    {
        private Label _Title;
        private SolidColorBrush _BaseColor;
        private const string TP_TITLE_PART = "xTitle";
        static DayControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DayControl), new FrameworkPropertyMetadata(typeof(DayControl)));
        }
        ~ DayControl()
        {
            MouseEnter -= DoMouseEnter;
            MouseLeave -= DoMouseLeave;
        }

        #region Date
        public static readonly DependencyProperty DateProperty =
            DependencyProperty.Register("Date", typeof(DateTime), typeof(DayControl), new PropertyMetadata(DatePropertyChanged));

        public static void DatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DayControl)d).Date = (DateTime)e.NewValue;
        }
        public DateTime Date
        {
            get { return (DateTime)GetValue(DateProperty); }
            set
            {
                SetValue(DateProperty, value);
                UpdateElement();
            }
        }
        #endregion

        private void UpdateElement()
        {
            if (_Title == null) 
                return; 
            _Title.Content = Date.ToString("dd");
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _Title = (Label)GetTemplateChild(TP_TITLE_PART);
            _BaseColor = (SolidColorBrush)Background;
            MouseEnter += DoMouseEnter;
            MouseLeave += DoMouseLeave;
            UpdateElement();
        }
        private void DoMouseEnter(object sender, MouseEventArgs e)
        {
            //DateSelected?.Execute(Date);
            _BaseColor = (SolidColorBrush)Background;
        }
        private void DoMouseLeave(object sender, MouseEventArgs e)
        {
            Background = _BaseColor;
        }

    }
}
