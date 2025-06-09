namespace BugTest;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
#if MACCATALYST
        Microsoft.Maui.Handlers.SwitchHandler.Mapper.AppendToMapping("SlideSwitch", (handler, view) =>
        {
            if (handler != null && handler.PlatformView != null)
            {
                handler.PlatformView.PreferredStyle = UIKit.UISwitchStyle.Sliding;
            }
        });
        Application.Current.RequestedThemeChanged += Current_RequestedThemeChanged;
#endif
	}

    private void Current_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
    {
        Hack.ResetMacSwitchBackgroundColors(this.mainLayout);
    }

    private void WithoutHack_Toggled(object sender, ToggledEventArgs e)
    {
    }

    private void WithHack_Toggled(object sender, ToggledEventArgs e)
    {
        Hack.ApplyMacSwitchHack((Switch)sender);
    }
}


