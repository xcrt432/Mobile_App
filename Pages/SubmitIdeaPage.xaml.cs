using Microsoft.Maui.Storage;
using Recraft.Models;
using Recraft.Services;

namespace Recraft.Pages
{
    public partial class SubmitIdeaPage : ContentPage
    {
        private readonly DatabaseService _dbService;
        private string _imagePath;

        public SubmitIdeaPage()
        {
            InitializeComponent();
            _dbService = new DatabaseService();
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Pick an image"
            });

            if (result != null)
            {
                var newPath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
                using var stream = await result.OpenReadAsync();
                using var newStream = File.OpenWrite(newPath);
                await stream.CopyToAsync(newStream);
                _imagePath = newPath;
                SelectedImage.Source = ImageSource.FromFile(newPath);
            }
        }

        private async void OnSubmitClicked(object sender, EventArgs e)
        {
            var title = TitleEntry.Text;
            var description = DescriptionEditor.Text;
            var type = ItemTypePicker.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(type) || string.IsNullOrEmpty(_imagePath) || string.IsNullOrWhiteSpace(description))
            {
                await DisplayAlert("Error", "All fields and an image are required.", "OK");
                return;
            }

            var idea = new Idea { Title = title, Description = description, ItemType = type, ImagePath = _imagePath };
            await _dbService.SaveIdeaAsync(idea);
            await DisplayAlert("Success", "Idea submitted!", "OK");

            TitleEntry.Text = string.Empty;
            DescriptionEditor.Text = string.Empty;
            ItemTypePicker.SelectedIndex = -1;
            SelectedImage.Source = "image_placeholder.png";
            _imagePath = null;
        }
    }
}
