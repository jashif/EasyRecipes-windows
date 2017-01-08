using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EasyRecipes.Views
{
    public sealed partial class RateControl : UserControl
    {
        
        public RateControl()
        {
            InitializeComponent();
        }
        /// <summary>
        /// BackgroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty BackgroundColorProperty =
            DependencyProperty.Register("BackgroundColor", typeof(SolidColorBrush), 
            typeof(RateControl),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    new PropertyChangedCallback(OnBackgroundColorChanged)));

        /// <summary>
        /// Gets or sets the BackgroundColor property.  
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            get { return (SolidColorBrush)GetValue(BackgroundColorProperty); }
            set { SetValue(BackgroundColorProperty, value); }
        }

        /// <summary>
        /// Handles changes to the BackgroundColor property.
        /// </summary>
        private static void OnBackgroundColorChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            RateControl control = (RateControl)d;
            foreach (Stars star in control.spStars.Children)
                star.BackgroundColor = (SolidColorBrush)e.NewValue;
        }
        /// <summary>
        /// StarForegroundColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarForegroundColorProperty =
            DependencyProperty.Register("StarForegroundColor", typeof(SolidColorBrush), 
            typeof(RateControl),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    new PropertyChangedCallback(OnStarForegroundColorChanged)));

        /// <summary>
        /// Gets or sets the StarForegroundColor property.  
        /// </summary>
        public SolidColorBrush StarForegroundColor
        {
            get { return (SolidColorBrush)GetValue(StarForegroundColorProperty); }
            set { SetValue(StarForegroundColorProperty, value); }
        }

        /// <summary>
        /// Handles changes to the StarForegroundColor property.
        /// </summary>
        private static void OnStarForegroundColorChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            RateControl control = (RateControl)d;
            foreach (Stars star in control.spStars.Children)
                star.StarForegroundColor = (SolidColorBrush)e.NewValue;

        }


        public SolidColorBrush StarFadeColor
        {
            get { return (SolidColorBrush)GetValue(StarFadeColorProperty); }
            set { SetValue(StarFadeColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StarFadeColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StarFadeColorProperty =
            DependencyProperty.Register("StarFadeColor", typeof(SolidColorBrush), typeof(RateControl), new PropertyMetadata(new SolidColorBrush(Colors.Gray),OnStarFadeColorCallBack));

        private static void OnStarFadeColorCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        
        /// <summary>
        /// StarOutlineColor Dependency Property
        /// </summary>
        public static readonly DependencyProperty StarOutlineColorProperty =
            DependencyProperty.Register("StarOutlineColor", typeof(SolidColorBrush), 
            typeof(RateControl),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    new PropertyChangedCallback(OnStarOutlineColorChanged)));

        /// <summary>
        /// Gets or sets the StarOutlineColor property.  
        /// </summary>
        public SolidColorBrush StarOutlineColor
        {
            get { return (SolidColorBrush)GetValue(StarOutlineColorProperty); }
            set { SetValue(StarOutlineColorProperty, value); }
        }

        /// <summary>
        /// Handles changes to the StarOutlineColor property.
        /// </summary>
        private static void OnStarOutlineColorChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            RateControl control = (RateControl)d;
            foreach (Stars star in control.spStars.Children)
                star.StarOutlineColor = (SolidColorBrush)e.NewValue;

        }
        /// <summary>
        /// Value Dependency Property
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), 
            typeof(RateControl),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent),
                    new PropertyChangedCallback(OnValueChanged)));

        /// <summary>
        /// Gets or sets the Value property.  
        /// </summary>
        public double Value
        {
            get 
            {
                try
                {
                    return (double)GetValue(ValueProperty);
                }
                catch
                {
                    return 0.0;
                }
            }
            set { SetValue(ValueProperty, value); }
        }

        /// <summary>
        /// Handles changes to the Value property.
        /// </summary>
        private static void OnValueChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            RateControl RateControl = (RateControl)d;
            SetupStars(RateControl);
        }
        /// <summary>
        /// NumberOfStars Dependency Property
        /// </summary>
        public static readonly DependencyProperty NumberOfStarsProperty =
            DependencyProperty.Register("NumberOfStars", typeof(Int32), typeof(RateControl),
                new PropertyMetadata((Int32)5,
                new PropertyChangedCallback(OnNumberOfStarsChanged)));

        /// <summary>
        /// Gets or sets the NumberOfStars property.  
        /// </summary>
        public Int32 NumberOfStars
        {
            get { return (Int32)GetValue(NumberOfStarsProperty); }
            set { SetValue(NumberOfStarsProperty, value); }
        }

        /// <summary>
        /// Handles changes to the NumberOfStars property.
        /// </summary>
        private static void OnNumberOfStarsChanged(DependencyObject d, 
            DependencyPropertyChangedEventArgs e)
        {
            RateControl RateControl = (RateControl)d;
            SetupStars(RateControl);
        }
        /// <summary>
        /// Sets up stars when Value or NumberOfStars properties change
        /// Will only show up to the number of stars requested (up to Maximum)
        /// so if Value > NumberOfStars * 1, then Value is clipped to maximum
        /// number of full stars
        /// </summary>
        /// <param name="RateControl"></param>
        private static void SetupStars(RateControl RateControl)
        {
            double localValue = RateControl.Value;

            RateControl.spStars.Children.Clear();
            for (int i = 0; i < RateControl.NumberOfStars; i++)
            {
                Stars star = new Stars();
                star.BackgroundColor = RateControl.BackgroundColor;
                if (i < localValue)
                {
                    star.StarForegroundColor = RateControl.StarForegroundColor;
                }
                else
                {
                    star.StarForegroundColor = RateControl.StarFadeColor;
                }
                star.StarOutlineColor = RateControl.StarOutlineColor;
                if (localValue > 1)
                    star.Value = 1.0;
                else if (localValue > 0)
                {
                    star.Value = localValue;
                }
                else
                {
                    star.Value = 0.0;
                }

                localValue -= 1.0;
                RateControl.spStars.Children.Insert(i,star);
            }
        }

    }
}
