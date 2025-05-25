using Recraft.Models;
using Recraft.Services;

namespace Recraft.Pages
{
    public partial class HomePage : ContentPage
    {
        private readonly DatabaseService _dbService = new();
        private List<Idea> _ideas = new();
        private int _currentIndex = 0;

        public HomePage()
        {
            InitializeComponent();
            LoadIdeas();
        }

        private async void LoadIdeas()
        {
            _ideas = await _dbService.GetIdeasAsync();
            if (_ideas.Count > 0)
                ShowIdea(_ideas[_currentIndex]);
        }

        private void ShowIdea(Idea idea)
        {
            TitleLabel.Text = idea.Title;
            DescriptionLabel.Text = idea.Description;
            ItemTypeLabel.Text = $"Type: {idea.ItemType}";
            IdeaImage.Source = ImageSource.FromFile(idea.ImagePath);
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            if (_ideas.Count == 0) return;

            _currentIndex = (_currentIndex + 1) % _ideas.Count;
            ShowIdea(_ideas[_currentIndex]);
        }
    }
}
