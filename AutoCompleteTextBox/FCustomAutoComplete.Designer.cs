namespace AutoCompleteTextBox
{
    partial class FCustomAutoComplete
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            customAutocompleteTextBox = new CustomAutocompleteTextBox4();
            SuspendLayout();
            // 
            // customAutocompleteTextBox
            // 
            customAutocompleteTextBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            customAutocompleteTextBox.IsBackspaceSelected = false;
            customAutocompleteTextBox.IsKeydownSelected = false;
            customAutocompleteTextBox.length = 0;
            customAutocompleteTextBox.Location = new Point(76, 71);
            customAutocompleteTextBox.MyAutoCompleteSource = null;
            customAutocompleteTextBox.Name = "customAutocompleteTextBox";
            customAutocompleteTextBox.selected = null;
            customAutocompleteTextBox.Size = new Size(278, 23);
            customAutocompleteTextBox.start = 0;
            customAutocompleteTextBox.StartString = null;
            customAutocompleteTextBox.TabIndex = 0;
            customAutocompleteTextBox.X_Coor = 0;
            customAutocompleteTextBox.Y_Coor = 0;
            // 
            // FCustomAutoComplete
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(440, 244);
            Controls.Add(customAutocompleteTextBox);
            Name = "FCustomAutoComplete";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "AutoComplete TextBox Demo";
            Load += FCustomAutoComplete_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CustomAutocompleteTextBox4 customAutocompleteTextBox;
    }
}
