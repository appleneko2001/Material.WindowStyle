﻿using Avalonia.Controls;

namespace Material.WindowStyle.Demo.Views.ViewModels.Entities
{
    public class PictureEntityViewModel : EntityViewModel
    {
        private Image _picture;

        public Image Picture
        {
            get => _picture;
            protected set
            {
                _picture = value;
                OnPropertyChanged();
            }
        }

        public PictureEntityViewModel()
        {
        
        }
    }
}