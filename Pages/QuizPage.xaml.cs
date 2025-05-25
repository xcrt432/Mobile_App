namespace Recraft.Pages
{
    public partial class QuizPage : ContentPage
    {
        private int currentQuestionIndex = 0;
        private readonly string[] questions =
        {
            "What is the best way to recycle plastic?",
            "How long does it take for paper to decompose?",
            "What is composting?",
            "Which material is not recyclable?",
            "What does 'eco-friendly' mean?"
        };

        public QuizPage()
        {
            InitializeComponent();
            ShowQuestion();
        }

        private void ShowQuestion()
        {
            if (currentQuestionIndex < questions.Length)
            {
                QuestionLabel.Text = questions[currentQuestionIndex];
            }
            else
            {
                QuestionLabel.Text = "Quiz complete!";
            }
        }

        private void OnNextClicked(object sender, EventArgs e)
        {
            currentQuestionIndex++;
            ShowQuestion();
        }
    }
}