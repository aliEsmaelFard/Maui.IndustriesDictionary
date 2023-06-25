namespace Maui.IndustriesDictionary;

public partial class MainPage : ContentPage
{

    string selctedLang = "en";
    string _imageSource ;
    string imageSource
    {
        get
        {
            return _imageSource;
        }
        set
        {
            _imageSource = value;
            SelectedItem.Source = value;

        }
    } 

	public MainPage()
	{
		InitializeComponent();
        

    }

    protected override void OnAppearing()
    {
        imageSource = "english.svg";
        base.OnAppearing();
    }
    private async void  ImageButton_Clicked(object sender, EventArgs e)
    {
        if (DropDown.IsVisible )
        {
            //await Arrow.RotateTo(Arrow.Rotation + 180, 100, Easing.Linear);
            await CloseDropDown();
        }
        else
        {
            DropDown.IsVisible =true;
       //     await Arrow.RotateTo(Arrow.Rotation + 180, 100, Easing.Linear);  
            await DropDown.TranslateTo(0, 10 , 200, Easing.Linear);


        }


    }

    private async Task CloseDropDown()
    {
        await DropDown.TranslateTo(0, -10, 100, Easing.Linear);
        DropDown.IsVisible = false;
    }

    private async void FirstItem_Tapped(object sender, TappedEventArgs e)
    {
        imageSource = "english.svg";
        selctedLang = "en";
        await CloseDropDown();
    }

    private async void SecondItem_Tapped(object sender, TappedEventArgs e)
    {
        imageSource = "germany.svg";
        selctedLang = "gr";
        await CloseDropDown();

    }

    private async void ThirdItem_Tapped(object sender, TappedEventArgs e)
    {
        imageSource = "iran2.png";
        selctedLang = "fa";
        await CloseDropDown();

    }
}

