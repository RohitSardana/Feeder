﻿using Org.Feeder.App.Services;
using System;

namespace Org.Feeder.App.Framework
{
    public interface INavigator
    {
        void GoToIntro();
        void GoToMain();
        void ShowError(string title, string message, Action recoveryAction, string actionTitle);
    }
}