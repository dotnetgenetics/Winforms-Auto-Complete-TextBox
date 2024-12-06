namespace AutoCompleteTextBox
{
    public partial class FCustomAutoComplete : Form
    {
        List<string> sourceText = new List<string>();

        public FCustomAutoComplete()
        {
            InitializeComponent();
        }

        private void FCustomAutoComplete_Load(object sender, EventArgs e)
        {
            SetSource();
        }

        private void SetSource()
        {
            sourceText.AddRange(new List<string>()
            {
                "January",
                "February",
                "March",
                "April",
                "May",
                "June",
                "July",
                "August",
                "September",
                "October",
                "November",
                "December"
            });

            customAutocompleteTextBox.MyAutoCompleteSource = sourceText;
        }
    }
}
