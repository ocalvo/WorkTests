using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SuperJupiter.Views
{
    public sealed partial class TextBoxView : Page
    {

        List<ITextRange> m_highlightedWords = null;

        public TextBoxView()
        {
            this.InitializeComponent();
            m_highlightedWords = new List<ITextRange>();
        }

        private void BoldButtonClick(object sender, RoutedEventArgs e)
        {
            ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Bold = FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
        }

        private void ItalicButtonClick(object sender, RoutedEventArgs e)
        {
            ITextSelection selectedText = editor.Document.Selection;
            if (selectedText != null)
            {
                ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                charFormatting.Italic = FormatEffect.Toggle;
                selectedText.CharacterFormat = charFormatting;
            }
        }

        private void FontColorButtonClick(object sender, RoutedEventArgs e)
        {
            fontColorPopup.IsOpen = true;
        }

        private void FindBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            string textToFind = findBox.Text;

            if (textToFind != null)
                FindAndHighlightText(textToFind);
        }

        private void FontColorButtonLostFocus(object sender, RoutedEventArgs e)
        {
            fontColorPopup.IsOpen = false;
        }

        private void FindBoxLostFocus(object sender, RoutedEventArgs e)
        {
            ClearAllHighlightedWords();
        }

        private void FindBoxGotFocus(object sender, RoutedEventArgs e)
        {
            string textToFind = findBox.Text;

            if (textToFind != null)
                FindAndHighlightText(textToFind);
        }

        private void ClearAllHighlightedWords()
        {
            ITextCharacterFormat charFormat;

            for (int i = 0; i < m_highlightedWords.Count; i++)
            {
                charFormat = m_highlightedWords[i].CharacterFormat;
                charFormat.BackgroundColor = Colors.Transparent;
                m_highlightedWords[i].CharacterFormat = charFormat;
            }
            m_highlightedWords.Clear();
        }

        private void FindAndHighlightText(string textToFind)
        {
            ClearAllHighlightedWords();

            ITextRange searchRange = editor.Document.GetRange(0, TextConstants.MaxUnitCount);
            searchRange.Move(0, 0);

            bool textFound = true;
            do
            {
                if (searchRange.FindText(textToFind, TextConstants.MaxUnitCount, FindOptions.None) < 1)
                    textFound = false;
                else
                {
                    ITextRange ss = searchRange.GetClone();
                    m_highlightedWords.Add(searchRange.GetClone());

                    ITextCharacterFormat charFormatting = searchRange.CharacterFormat;
                    charFormatting.BackgroundColor = Colors.Yellow;
                    searchRange.CharacterFormat = charFormatting;
                }
            } while (textFound);
        }

        private void ColorButtonClick(object sender, RoutedEventArgs e)
        {
            Button clickedColor = (Button)sender;

            ITextCharacterFormat charFormatting = editor.Document.Selection.CharacterFormat;
            switch (clickedColor.Name)
            {
                case "black":
                    {
                        charFormatting.ForegroundColor = Colors.Black;
                        break;
                    }

                case "gray":
                    {
                        charFormatting.ForegroundColor = Colors.Gray;
                        break;
                    }

                case "darkgreen":
                    {
                        charFormatting.ForegroundColor = Colors.DarkGreen;
                        break;
                    }

                case "green":
                    {
                        charFormatting.ForegroundColor = Colors.Green;
                        break;
                    }

                case "blue":
                    {
                        charFormatting.ForegroundColor = Colors.Blue;
                        break;
                    }

                default:
                    {
                        charFormatting.ForegroundColor = Colors.Black;
                        break;
                    }
            }
            editor.Document.Selection.CharacterFormat = charFormatting;

            editor.Focus(Windows.UI.Xaml.FocusState.Keyboard);
            fontColorPopup.IsOpen = false;
        }

        private void textEventsTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            AppendTextEventsLogging("TextChanged:" + textEventsTB.Text);
        }

        private void textEventsTB_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            AppendTextEventsLogging("TextChanging:" + textEventsTB.Text);
        }

        private void textEventsTB_TextCompositionStarted(TextBox sender, TextCompositionStartedEventArgs args)
        {
            AppendTextEventsLogging("CompositionStarted:" + textEventsTB.Text + "[" + args.StartIndex + "-" + args.Length + "]");
        }

        private void textEventsTB_TextCompositionChanged(TextBox sender, TextCompositionChangedEventArgs args)
        {
            AppendTextEventsLogging("CompositionChanged:" + textEventsTB.Text + "[" + args.StartIndex + "-" + args.Length + "]");
        }

        private void textEventsTB_TextCompositionEnded(TextBox sender, TextCompositionEndedEventArgs args)
        {
            AppendTextEventsLogging("CompositionEnded:" + textEventsTB.Text + "[" + args.StartIndex + "-" + args.Length + "]");
        }

        private void AppendTextEventsLogging(string log)
        {
            textEventsLoggingTB.Text += log + System.Environment.NewLine;
            textEventsLoggingScrollViewer.ChangeView(null, textEventsLoggingScrollViewer.ExtentHeight, null);
        }    
    }
}
