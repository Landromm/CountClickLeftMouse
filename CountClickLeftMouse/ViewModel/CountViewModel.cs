using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using CountClickLeftMouse.Model;

namespace CountClickLeftMouse.ViewModel
{
    internal class CountViewModel : BaseViewModel
    {
        DispatcherTimer timer = null!;
        DispatcherTimer dispatcherTimer = null!;

        #region CountClick : int - Количество кликов.

        /// <summary>Количество кликов. - поле.</summary>
        private int _countClick;

        /// <summary>Количество кликов. - свойство.</summary>
        public int CountClick
        {
            get => _countClick;
            set
            {
                _countClick = value;
                OnPropertyChanged(nameof(CountClick));
            }
        }
        #endregion

        #region TimerText : int - Отображение таймера

        /// <summary>Отображение таймера - поле.</summary>
        private int _timerText;

        /// <summary>Отображение таймера - свойство.</summary>
        public int TimerText
        {
            get => _timerText;
            set
            {
                _timerText = value;
                OnPropertyChanged(nameof(TimerText));
            }
        }
        #endregion

        #region CheckAvailability : bool - Победитель

        /// <summary>Победитель - поле.</summary>
        private bool _CheckAvailability;

        /// <summary>Победитель - свойство.</summary>
        public bool CheckAvailability
        {
            get => _CheckAvailability;
            set
            {
                _CheckAvailability = value;
                OnPropertyChanged(nameof(CheckAvailability));
            }
        }
        #endregion

        public ICommand ClickMe { get; }
        public ICommand Reset { get; }

        public CountViewModel()
        {
            CountClick = 0;
			TimerText = 29;

			ClickMe = new BaseCommand(ExecuteCliclMeCommand, CanExecuteCliclMeCommand);
            Reset = new BaseCommand(ExecuteResetCommand);

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
			};
			timer.Tick += timer_Tick;
        }

		private void ExecuteCliclMeCommand(object obj)
		{
			if (!timer.IsEnabled)
				timer.Start();

			CountClick++;

			if (CountClick >= 205)
				CheckAvailability = true;
		}

		private bool CanExecuteCliclMeCommand(object obj)
		{
			return TimerText > 0 ? true : false;
		}

		private void ExecuteResetCommand(object obj)
		{
			if (timer.IsEnabled)
				timer.Stop();

			TimerText = 29;
			CountClick = 0;
			CheckAvailability = false;
		}

		private void timer_Tick(object sender, EventArgs e)
        {
            if (TimerText > 0)
				TimerText -= 1;
            else
                timer.Stop();
        }

    }
}
