namespace BugTest;
using UIKit;

public static class Hack
{
    class MySwitch : Switch, ISwitch
    {
        private readonly Switch theSwitch;

        public MySwitch(Switch theSwitch)
        {
            this.theSwitch = theSwitch;
        }
        public Color TrackColor { get; set; }
        public bool IsOn { get; set; }
    }
    
    public static void ApplyMacSwitchHack(Switch theSwitch)
    {
#if MACCATALYST
        var name = Application.Current.RequestedTheme == AppTheme.Dark ? "MacSwitchBackgroundDark" : "MacSwitchBackgroundLight";
        var color = Resource<Color>(name);
        color = ((Layout) theSwitch.Parent).BackgroundColor;
        color = new Color(0, 0, 0, 255);

        ISwitch? switchView = new MySwitch(theSwitch) { TrackColor = color };

        if (color != null)
        {
            ((UISwitch) theSwitch.Handler.PlatformView).UpdateTrackColor(switchView);
           // Application.Current.MainPage.Dispatcher.Dispatch(async () =>  await Application.Current.MainPage.DisplayAlert("Switch Color", $"Applying Color: {color}", "OK"));
            theSwitch.BackgroundColor = theSwitch.IsToggled ? Colors.Transparent : color;
        }
#endif
    }

    public static void ResetMacSwitchBackgroundColors(Layout layout)
    {
#if MACCATALYST
        foreach (var child in layout.Children)
        {
            if (child is Microsoft.Maui.Controls.Switch s)
            {
                s.BackgroundColor = Colors.Transparent;
            }
            else if (child is Layout sibling)
            {
                ResetMacSwitchBackgroundColors(sibling);
            }
        }
#endif
    }

    public static T Resource<T>(string name)
    {
        if (typeof(T) == typeof(Color))
        {
            var colors = Application.Current.Resources.MergedDictionaries.First();
            return (T)colors[name];
        }
        else if (typeof(T) == typeof(Style))
        {
            var styles = Application.Current.Resources.MergedDictionaries.Last();
            return (T)styles[name];
        }

        return default;
    }
}