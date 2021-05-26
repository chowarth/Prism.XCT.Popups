# XCT.Popups.Prism

Unofficial package that allows the use of `Xamarin.Community.Toolkit` popups within a [Prism](https://prismlibrary.com/) based Xamarin.Forms project.

> **NOTE** This package will likely be superseded by the official Prism package, unfortunately there is currently no time frame on when that will become available - hence why I have created this.

# Installation

The library can be installed from [NuGet](https://www.nuget.org/packages/XCT.Popups.Prism/):

- This can be done through your IDE with the NuGet package manager
  - **Note:** You will only need to install the package in your Xamarin.Forms project.
- Ensure that you install the [Xamarin.Community.Toolkit](https://github.com/xamarin/XamarinCommunityToolkit) package in all the required platform projects.

# Getting Started

Once the package is installed you're good to go, so lets get started!

## Create a Popup

> A popup **must** inherit from `PrismPopup`, which itself inherits from the `Xamarin.CommunityToolkit.UI.Views.Popup`, in order to work properly.

```xml
<?xml version="1.0" encoding="utf-8" ?>
<popups:PrismPopup
    xmlns="http://xamarin.com/schemas/2014/forms"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:popups="clr-namespace:XCT.Popups.Prism;assembly=XCT.Popups.Prism"
    x:Class="XCT.Popups.Prism.Sample.Views.HelloWorldPopup"
    Size="300, 300">
    
    <StackLayout
        BackgroundColor="IndianRed">
        <Label
            Text="Welcome to XCT.Popups.Prism.Sample!"
            VerticalOptions="CenterAndExpand" 
            HorizontalOptions="CenterAndExpand" />
        <Button
            Text="Dismiss"
            Command="{Binding DismissCommand}"/>
    </StackLayout>

</popups:PrismPopup>
```

## Create a ViewModel

> Your ViewModel class **must** implement the `IPopupAware` interface otherwise it will not work with the PopupService.

### IPopupAware

Implementing `IPopupAware` on your ViewModel provides you with access to all the methods that you will need thoughout the lifetime of the popup.

```csharp
public class HelloWorldPopupViewModel : ViewModelBase, IPopupAware
{
    public DelegateCommand DismissCommand { get; }

    // Hooked into by the PopupService to trigger the dismiss of the Xamarin.Community.Toolky popup.
    // Invoke this when you want to dismiss the popup from code: see DimsissCommandExecuted below.
    public event Action<IPopupParameters> RequestDismiss;

    public HelloWorldPopupViewModel(INavigationService navigationService)
        : base(navigationService)
    {
        DismissCommand = new DelegateCommand(DimsissCommandExecuted);
    }

    // Invoked when the popup is opened
    public void OnPopupOpened(IPopupParameters parameters)
    {
    }

    // Invoked when the popup is dismissed via RequestDismiss
    public void OnPopupDismissed()
    {
    }

    private void DimsissCommandExecuted()
    {
        RequestDismiss?.Invoke(new PopupParameters()
        {
            { "dismissedParam", "This was returned from the popup viewmodel" }
        });
    }
}
```

### IPopupLightDismissAware

This is an optional interface that provides you with a way of passing parameters back when a popup was light dismissed. Simply implement the interface on your ViewModel and return the desired parameters inside `OnPopupLightDismissed`:

```csharp
public IPopupParameters OnPopupLightDismissed()
{
    return new PopupParameters()
    {
        { "lightDismissedParam", "This was returned from the popup viewmodel when it was light dismissed" }
    };
}
```

## Register the Popup

Regsitering a popup is done in pretty much the same way that pages and ViewModels are registered but using the popup specific extension method `RegisterPopup`:

```csharp
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    containerRegistry.RegisterPopup<HelloWorldPopup, HelloWorldPopupViewModel>();
}
```

## Using the Popup Service

Register the `PopupService` implementation with the container within `RegisterTypes` so that it can be injected as a dependency:

```csharp
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    containerRegistry.RegisterSingleton<IPopupService, PopupService>();
}
```

The simplest way to show a popup is to call `ShowPopup`:

```csharp
var parameters = new PopupParameters()
{
    { "message", "Hello world!" }
};

_popupService.ShowPopup("HelloWorldPopup", parameters, (result) => { }); // Callback can be null
```

Popups can be shown asynchronously by using the `ShowPopupAsync` extension method:

```csharp
IPopupResult result = await _popupService.ShowPopupAsync("HelloWorldPopup", new PopupParameters()
{
    { "message", "Hello world!" }
})

result.Exception // Any exception thrown when trying to show/dismiss a popup
result.LightDismissed // Whether the popup was dismissed by tapping outside the popup or not
result.Parameters // The parameters returned from the popup
```
