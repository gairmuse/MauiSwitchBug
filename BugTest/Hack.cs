namespace BugTest;

public static class Hack
{   
    public static void ApplyMacSwitchHack(Switch theSwitch)
    {
#if MACCATALYST
        var name = Application.Current.RequestedTheme == AppTheme.Dark ? "MacSwitchBackgroundDark" : "MacSwitchBackgroundLight";
        var color = Resource<Color>(name);
        if (color != null)
        {
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
                ApplyMacSwitchHack(s);
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