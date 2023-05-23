﻿using Features.UI.Windows.Base;
using Zenject;

namespace Features.Services.UI.Factory.BaseUI
{
  public interface IUIFactory : IService, IFactory<WindowId, BaseWindow>
  {

  }
}