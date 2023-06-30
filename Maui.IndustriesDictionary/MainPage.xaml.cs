using CommunityToolkit.Maui.Core.Extensions;
using Maui.Dictionary.Model;
using Maui.Dictionary.Repository;
using Maui.IndustriesDictionary.Util;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Maui.IndustriesDictionary;

public partial class MainPage : ContentPage
{

    string selctedLang = "English";
    string advanceSearch = "Start";
    string _imageSource;
    ObservableCollection<WordsModel> words = new ObservableCollection<WordsModel>();
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
        if (Utils.FirstRun)
        {
            // Perform an action.
            FillDataBase();
            Utils.FirstRun = false;
        }

        imageSource = "english.svg";
        base.OnAppearing();
    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        if (DropDown.IsVisible)
        {
            await Arrow.RotateTo(Arrow.Rotation + 180, 200, Easing.Linear);
            await CloseDropDown();
        }
        else
        {
            DropDown.IsVisible = true;
             await Arrow.RotateTo(Arrow.Rotation + 180, 200, Easing.Linear);  
            await DropDown.TranslateTo(0, 10, 200, Easing.Linear);
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
        selctedLang = "English";
        await CloseDropDown();
        //search
        CheckEntery();
    }

    private async void SecondItem_Tapped(object sender, TappedEventArgs e)
    {
        imageSource = "germany.svg";
        selctedLang = "Deutsch";
        await CloseDropDown();
        //search
        CheckEntery();

    }

    private async void ThirdItem_Tapped(object sender, TappedEventArgs e)
    {
        imageSource = "iran2.png";
        selctedLang = "Farsi";
        await CloseDropDown();
        //search
        CheckEntery();

    }

    private void FillDataBase()
    {
        try
        {
            //// TODO Only do this when app first runs
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("Maui.IndustriesDictionary.data.sqlite"))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);

                    File.WriteAllBytes(MyRepository.DbPath, memoryStream.ToArray());
                }
            }

        }
        catch (Exception ex) { string msg = ex.Message; }
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        CheckEntery();
    }

    private async void CheckEntery()
    {
        try
        {
            LoadingBar.IsVisible = true;
            CollectionMainWords.EmptyView = "";
            await Task.Delay(300);
            string search = SearchText.Text;
            search_img.IsVisible = (String.IsNullOrEmpty(search)) ? true : false;
            poster.IsVisible = false;
            if (!String.IsNullOrEmpty(search) && search.Length > 2)
            {
                //Connet to db
                LoadData(search);
            }
            else if (String.IsNullOrEmpty(search))
            {
                words = null;
                BindingContext = words;
            }
            LoadingBar.IsVisible = false;

        }
        catch (Exception)
        {

            throw;
        }
    }
    private  void LoadData(string search)
    {
        MyRepository repository = new MyRepository();

        words =  repository.GetList(search, selctedLang, advanceSearch).ToObservableCollection();
        Utils.SetDataToCollectionView(CollectionMainWords, "", words,this);
    }

    private void Start_Tapped(object sender, TappedEventArgs e)
    {
        start_with_lay.BackgroundColor = Colors.RoyalBlue;
        start_label.TextColor = Colors.White;        

        contain_lay.BackgroundColor = Colors.White;
        contains_label.TextColor = Colors.Gray;

        advanceSearch = "Start";
        //search
        CheckEntery();

    }

    private void Contains_Tapped(object sender, TappedEventArgs e)
    {
        start_with_lay.BackgroundColor = Colors.White;
        start_label.TextColor = Colors.Gray;

        contain_lay.BackgroundColor = Colors.RoyalBlue;
        contains_label.TextColor = Colors.White;

        advanceSearch = "Contains";

        //search
        CheckEntery();
    }

    private void toobar_Tapped(object sender, TappedEventArgs e)
    {
        toolbar.Stroke = Colors.DeepSkyBlue;

    }
}

