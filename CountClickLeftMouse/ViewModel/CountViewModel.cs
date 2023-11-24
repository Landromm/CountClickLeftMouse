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
		#region Поля.
		static readonly private int countClick = 0;
        static readonly private int finishPoint = 205;
        static readonly private int countDown = 29;
		#endregion

		#region Объекты классов.
		DispatcherTimer timer = null!;
		#endregion

		#region Определение свойств.

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

		#region CheckAvailability : bool - Флаг прохождения порога норматива.

		/// <summary>Флаг прохождения порога норматива. - поле.</summary>
		private bool _CheckAvailability;

		/// <summary>Флаг прохождения порога норматива. - свойство.</summary>
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

		#endregion

		#region Определение событий.
		public ICommand ClickMe { get; }
        public ICommand Reset { get; }
		#endregion

		// Конструктор.
		public CountViewModel()
        {
            CountClick = countClick;
			TimerText = finishPoint;

			ClickMe = new BaseCommand(ExecuteCliclMeCommand, CanExecuteCliclMeCommand);
            Reset = new BaseCommand(ExecuteResetCommand);

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
			};
			timer.Tick += timer_Tick;
        }

        // Метод подсчета количества кликов.
		private void ExecuteCliclMeCommand(object obj)
		{
			if (!timer.IsEnabled)
				timer.Start();

			CountClick++;

			if (CountClick >= countDown)
				CheckAvailability = true;
		}

        // Условие выполнения метода подсчета количества кликов.
		private bool CanExecuteCliclMeCommand(object obj)
		{
			return TimerText > 0 ? true : false;
		}

        // Метод сброса счетчика и времени в значения по умолчанию.
		private void ExecuteResetCommand(object obj)
		{
			if (timer.IsEnabled)
				timer.Stop();

			TimerText = countDown;
			CountClick = countClick;
			CheckAvailability = false;
		}

		/* Метод выолняющийся через равные промежутки,
		в соответствии с параметрами "DispatcherTimer". */
		private void timer_Tick(object sender, EventArgs e)
        {
            if (TimerText > 0)
				TimerText -= 1;
            else
                timer.Stop();
        }
    }
}
