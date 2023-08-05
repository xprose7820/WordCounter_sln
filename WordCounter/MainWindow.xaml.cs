using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WordCounter
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>

	public partial class MainWindow : Window
	{
		private string fileContent = string.Empty;
		public MainWindow()
		{
			InitializeComponent();
		}

		private void BrowseButton_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
			if (openFileDialog.ShowDialog() == true)
			{
				fileContent = File.ReadAllText(openFileDialog.FileName);
				// Lowercase all words, remove punctuation and display
				fileContent = Regex.Replace(fileContent, @"\p{P}+", "").ToLower();
				FileContentTextBox.Text = fileContent;

				// Populate combo box with unique words
				var words = fileContent.Split(new char[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				WordComboBox.ItemsSource = words.Distinct();
			}
		}

		private void WordComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
		{
			// Count occurrences of selected word
			var selectedWord = WordComboBox.SelectedItem.ToString();
			var wordCount = Regex.Matches(fileContent, @"\b" + selectedWord + @"\b").Count;
			WordCountLabel.Content = $"The word '{selectedWord}' occurs {wordCount} times.";
		}

	}

}
