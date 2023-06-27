using Maui.Dictionary.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maui.IndustriesDictionary.Util
{
    public static class Utils
    {
        public static bool FirstRun
        {
            get => Preferences.Get(nameof(FirstRun), true);
            set => Preferences.Set(nameof(FirstRun), value);
        }

        public static void SetDataToCollectionView(CollectionView collectionView, string error, ObservableCollection<WordsModel> observer, ContentPage page)
        {
                // if no data we have
                 if (observer.Count == 0)
                 {
                    page.BindingContext = observer;
                    collectionView.EmptyView = "مورد مشابه‌ای یافت تشد.";
                 }
                //otherwise we bind data to collectionView (everything is right)
                else
                {
                    page.BindingContext = observer;
                }
            }
        }
    }

