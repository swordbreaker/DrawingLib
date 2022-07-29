using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using System.ComponentModel;

namespace DrawingLib;

public partial class MainPage : ContentPage, INotifyPropertyChanged
{
    public GraphicsDrawable Drawable { get; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;

    private string _savePath;

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

    public string SavePath
    {
        get => _savePath;
        set
        {
            if (_savePath != value)
            {
                _savePath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SavePath"));
            }
        }
    }

    public MainPage()
	{
		InitializeComponent();
        this.BindingContext = this;
        Loaded += MainPage_Loaded;
        SavePath = "C:\\Users\\tobia\\Desktop\\test.png";
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
            using var stream = File.OpenWrite(SavePath);
            await screenShot.CopyToAsync(stream, ScreenshotFormat.Png);
        }
    }
}

