using System;
using System.Collections.Generic;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// A UI Element representative of a Dialog.
    /// This serves to ask the player to select one of 2 options.
    /// </summary>
    public class UIDialog : UIElement
    {
        /// <summary>
        /// The amount of top/bottom padding to use for the dialog buttons.
        /// </summary>
        private const int dialogButtonTopBottomPadding = 1;

        /// <summary>
        /// The title of the dialog.
        /// </summary>
        private readonly string title;

        /// <summary>
        /// The title's foreground color.
        /// </summary>
        private readonly ConsoleColor titleColorFg;

        /// <summary>
        /// The text to be displayed in the body of the dialog.
        /// </summary>
        private readonly string text;

        /// <summary>
        /// The padding to add to the entire dialog.
        /// </summary>
        private readonly int padding;

        /// <summary>
        /// The margin between the title and the text.
        /// </summary>
        private readonly int titleTextMargin;

        /// <summary>
        /// The margin between the text and the buttons.
        /// </summary>
        private readonly int textButtonsMargin;

        /// <summary>
        /// The margin between the buttons.
        /// </summary>
        private readonly int buttonsMarging;

        /// <summary>
        /// The dialog buttons.
        /// </summary>
        private readonly UIButton[] buttons;

        public override int Width 
        { 
            get { return this.text.Length + padding * 2; } 
        }

        public override int Height 
        { 
            get 
            { 
                return padding * 2 + titleTextMargin + 
                    textButtonsMargin + 3 + dialogButtonTopBottomPadding * 2; 
            }
        }

        /// <summary>
        /// Created a new instance of <see cref="UIDialog"/>.
        /// </summary>
        /// <param name="name">The name given to the dialog. 
        /// This is basically an ID.</param>
        /// <param name="position">The anchor position of this element.</param>
        /// <param name="title">The title of the dialog.</param>
        /// <param name="text">The text for the body of the dialog.</param>
        /// <param name="button1Text">The text for the first button.</param>
        /// <param name="button2Text">The text for the second button.</param>
        /// <param name="padding">The padding to add to the entire dialog.</param>
        /// <param name="titleTextMargin">The margin between the title and the 
        /// text.</param>
        /// <param name="textButtonsMargin">The margin between the text and the 
        /// buttons.</param>
        /// <param name="buttonsMargin">The margin between the buttons.</param>
        /// <param name="colorTitleFg">The title's foreground color.</param>
        /// <param name="colorBg">The background color for the dialog.</param>
        /// <param name="colorFg">The foreground color for the dialog.</param>
        public UIDialog(string name, UIPosition position, string title, 
            string text, string button1Text, string button2Text,
            int padding = 1, int titleTextMargin = 1, int textButtonsMargin = 2, 
            int buttonsMargin = 1, 
            ConsoleColor colorTitleFg = UISettings.ColorDialogTitleFg, 
            ConsoleColor colorBg = UISettings.ColorDialogBg, 
            ConsoleColor colorFg = UISettings.ColorDialogFg) 
            : base(name, position, colorBg, colorFg)
        {
            this.title = title;

            this.text = text;

            this.padding = padding;

            this.titleTextMargin = titleTextMargin;

            this.textButtonsMargin = textButtonsMargin;

            this.buttonsMarging = buttonsMargin;

            this.titleColorFg = colorTitleFg;

            this.buttons = new UIButton[2]
            {
                new UIButton("dialogBtn1", this.TopLeft, button1Text, 2, 
                dialogButtonTopBottomPadding, 
                UISettings.ColorDialogButtonBg,
                UISettings.ColorDialogButtonFg, 
                UISettings.ColorDialogButtonHoverBg, 
                UISettings.ColorDialogButtonHoverFg),

                new UIButton("dialogBtn2", this.TopRight, button2Text, 2, 
                dialogButtonTopBottomPadding, 
                UISettings.ColorDialogButtonBg,
                UISettings.ColorDialogButtonFg, 
                UISettings.ColorDialogButtonHoverBg, 
                UISettings.ColorDialogButtonHoverFg)
            };
        }

        /// <summary>
        /// Gets user selected button index.
        /// </summary>
        /// <returns>The index of the selected button.</returns>
        public int GetSelected()
        {
            ConsoleKey userInput;
            int selected = 0;

            SetHoveredButton(selected);

            while (true)
            {
                userInput = GetUserInputKey();

                if (userInput == ConsoleKey.Enter)
                {
                    break;
                }

                selected = GetSelectedButton(userInput);

                if (selected < 0)
                {
                    continue;
                }

                SetHoveredButton(selected);
            }

            return selected;
        }

        public override void Display()
        {
            ResetCursorPosition();

            DrawBackground();

            ResetCursorPosition();

            WriteTitle();

            ResetCursorPosition();

            WriteText();

            ShowButtons();

            base.Display();
        }

        /// <summary>
        /// Writes the dialog's title to the console.
        /// </summary>
        private void WriteTitle()
        {
            Console.ForegroundColor = titleColorFg;

            SetCursorDelta(padding, padding);

            Console.Write(title);
        }

        /// <summary>
        /// Writes the dialog's text to the console.
        /// </summary>
        private void WriteText()
        {
            Console.ForegroundColor = ForegroundColor;

            SetCursorDelta(padding, padding + 1 + titleTextMargin);

            Console.Write(text);
        }

        /// <summary>
        /// Shows the dialog buttons.
        /// </summary>
        private void ShowButtons()
        {
            int x, y;
            UIPosition position;

            x = this.BottomRight.X - 1 - padding - this.buttons[1].Width;
            y = this.BottomRight.Y - padding - this.buttons[1].Height;

            position = new UIPosition(x, y);

            this.buttons[1].SetPosition(position);

            x = this.buttons[1].TopLeft.X - buttonsMarging - this.buttons[1].Width;
            y = this.buttons[1].TopLeft.Y;

            position = new UIPosition(x, y);

            this.buttons[0].SetPosition(position);

            this.buttons[0].Display();

            this.buttons[1].Display();
        }

        /// <summary>
        /// Sets the hovered button by its index.
        /// </summary>
        /// <param name="index">The index of an element in 
        /// <see cref="buttons"/></param>
        private void SetHoveredButton(int index)
        {
            if (buttons[index].IsHovered)
            {
                return;
            }

            buttons[0].IsHovered = false;
            buttons[1].IsHovered = false;

            buttons[index].IsHovered = true;

            Display();
        }

        /// <summary>
        /// Gets the selected button.
        /// </summary>
        /// <param name="key">A user inputed <see cref="ConsoleKey"/>.</param>
        /// <returns>The index of the selected button.</returns>
        private int GetSelectedButton(ConsoleKey key)
        {
            int index;

            switch (key)
            {
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    index = 0;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    index = 1;
                    break;
                default:
                    index = -1;
                    break;
            }

            return index;
        }
    }
}
