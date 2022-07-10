using System.ComponentModel;

namespace DrawingLib;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    public GraphicsDrawable Drawable { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    public float Zoom
    {
        get => Drawable.Scale;
        set
        {
            if (Drawable.Scale != value)
        {
                Drawable.Scale = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Zoom"));
                GraphicsView.Invalidate();
            }
        }
    }

    public MainPage()
	{
		InitializeComponent();
        this.BindingContext = this;
        Loaded += MainPage_Loaded;
    }

    private void MainPage_Loaded(object? sender, EventArgs e)
    {
        GraphicsView.Drawable = Drawable;
        GraphicsView.Invalidate();
    }

    private void Reload(object sender, EventArgs e)
	{
        GraphicsView.Invalidate();
    }

    private async void Save(object sender, EventArgs e)
	{
        var screenShot = await GraphicsView.CaptureAsync();

		if(screenShot != null)
		{
            using var stream = File.OpenWrite("C:\\Users\\tobia\\Desktop\\test.png");
            await screenShot.CopyToAsync(stream, ScreenshotFormat.Png);
        }
    }
}

