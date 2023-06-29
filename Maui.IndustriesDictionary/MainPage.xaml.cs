﻿using CommunityToolkit.Maui.Core.Extensions;
using Maui.Dictionary.Model;
using Maui.Dictionary.Repository;
using Maui.IndustriesDictionary.Util;
using Microsoft.Maui.Controls.Compatibility;
using System.Collections.ObjectModel;
using System.Reflection;

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
            //await Arrow.RotateTo(Arrow.Rotation + 180, 100, Easing.Linear);
            await CloseDropDown();
        }
        else
        {
            DropDown.IsVisible = true;
            //     await Arrow.RotateTo(Arrow.Rotation + 180, 100, Easing.Linear);  
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
    }

    private async void SecondItem_Tapped(object sender, TappedEventArgs e)
    {
        imageSource = "germany.svg";
        selctedLang = "Deutsch";
        await CloseDropDown();

    }

    private async void ThirdItem_Tapped(object sender, TappedEventArgs e)
    {
        imageSource = "iran2.png";
        selctedLang = "Farsi";
        await CloseDropDown();

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
        string search = SearchText.Text;

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
    }

    private  void LoadData(string search)
    {
        MyRepository repository = new MyRepository();

        words =  repository.GetList(search, selctedLang).ToObservableCollection();
        Utils.SetDataToCollectionView(CollectionMainWords, "", words,this);
    }

    private void Start_Tapped(object sender, TappedEventArgs e)
    {
        start_with_lay.BackgroundColor = Colors.Gray;
        start_label.TextColor = Colors.White;        

        contain_lay.BackgroundColor = Colors.White;
        contains_label.TextColor = Colors.Gray;

        advanceSearch = "Start";
    }    
    
    private void Contains_Tapped(object sender, TappedEventArgs e)
    {
        start_with_lay.BackgroundColor = Colors.White;
        start_label.TextColor = Colors.Gray;

        contain_lay.BackgroundColor = Colors.Gray;
        contains_label.TextColor = Colors.White;

        advanceSearch = "Contains";

    }
}

