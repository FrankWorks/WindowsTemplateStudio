﻿using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Param_ItemNamespace.Models;
using Param_ItemNamespace.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace Param_ItemNamespace.Views
{
    public sealed partial class MasterDetailPage : Page, System.ComponentModel.INotifyPropertyChanged
    {
        private SampleOrder _selected;

        public SampleOrder Selected
        {
            get { return _selected; }
            set { Set(ref _selected, value); }
        }

        public ObservableCollection<SampleOrder> SampleItems { get; private set; } = new ObservableCollection<SampleOrder>();

        public MasterDetailPage()
        {
            InitializeComponent();
        }

        private async Task LoadDataAsync()
        {
            SampleItems.Clear();

            var data = await SampleDataService.GetSampleModelDataAsync();

            foreach (var item in data)
            {
                SampleItems.Add(item);
            }

            Selected = SampleItems.First();
        }

        private void MasterListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var item = e?.ClickedItem as SampleOrder;
            if (item != null)
            {
                if (WindowStates.CurrentState == NarrowState)
                {
                    NavigationService.Navigate<Views.MasterDetailDetailPage>(item);
                }
                else
                {
                    Selected = item;
                }
            }
        }
    }
}