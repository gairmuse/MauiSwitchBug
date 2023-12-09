namespace BugTest;

public partial class MainPage : ContentPage
{
	int count = 0;

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
#endif
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{
		count++;

		if (count == 1)
			CounterBtn.Text = $"Clicked {count} time";
		else
			CounterBtn.Text = $"Clicked {count} times";

		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}


