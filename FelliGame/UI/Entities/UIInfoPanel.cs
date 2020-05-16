using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FelliGame.UI.Entities
{
    /// <summary>
    /// A UI Element representative of a Info Panel.
    /// </summary>
    public class UIInfoPanel : UIElement
    {
        /// <summary>
        /// The padding to be applied to the entire info panel.
        /// </summary>
        private readonly int padding;

        /// <summary>
        /// The content of the info panel.
        /// </summary>
        private readonly IList<UIText> content;

        public override int Width
        {
            get
            {
                int value = 0;

                for (int i = 0; i < content.Count; i++)
                {
                    if (content[i].Width > value)
                    {
                        value = content[i].Width;
                    }
                }

                return value + padding * 2;
            }
        }

        public override int Height
        {
            get
            {
                int value = 0;

                for (int i = 0; i < content.Count; i++)
                {
                    value += content[i].Height;
                }

                return value + padding * 2;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="UIInfoPanel"/>.
        /// </summary>
        /// <param name="name">The name given to the element. 
        /// This is basically an ID.</param>
        /// <param name="position">The anchor position of this element.</param>
        /// <param name="padding">The padding to be applied to the entire 
        /// info panel.</param>
        /// <param name="colorBg">The background color of the element.</param>
        /// <param name="colorFg">The foreground color of the element.</param>
        public UIInfoPanel(string name, UIPosition position, int padding,
            ConsoleColor colorBg = UISettings.ColorInfoPanelBg,
            ConsoleColor colorFg = UISettings.ColorInfoPanelFg) 
            : base(name, position, colorBg, colorFg)
        {
            this.padding = padding;

            this.content = new List<UIText>();
        }

        public override void Display()
        {
            ResetCursorPosition();

            DrawBackground();

            ResetCursorPosition();

            ShowContent();

            base.Display();
        }

        /// <summary>
        /// Outputs the content of the info panel to the console.
        /// </summary>
        private void ShowContent()
        {
            UIPosition position;
            UIText previous = null;

            for (int i = 0; i < content.Count; i++)
            {
                // First element uses the info panel's TopLeft as reference.
                if (previous == null)
                {
                    position = new UIPosition(this.TopLeft.X + padding, 
                                                this.TopLeft.Y + padding);

                    content[i].SetPosition(position);

                    previous = content[i];
                }
                // Other elements use the previous one has reference.
                else
                {
                    content[i].SetPosition(previous.BottomLeft);

                    previous = content[i];
                }

                content[i].Display();
            }
        }

        /// <summary>
        /// Add content to the info panel.
        /// </summary>
        /// <param name="content">The new content.</param>
        public void Add(UIText content)
        {
            this.content.Add(content);
        }

        /// <summary>
        /// Update an existing content item.
        /// </summary>
        /// <param name="name">The name of the content. ID.</param>
        /// <param name="value">The new value to give to content.</param>
        public void UpdateContentByName(string name, string value)
        {
            UIText item = GetContentByName(name);

            if (item != null)
            {
                item.Content = value;
            }
        }

        /// <summary>
        /// Get content by given name (Id).
        /// </summary>
        /// <param name="name">The name of the content.</param>
        /// <returns>The content with the given name if found.</returns>
        private UIText GetContentByName(string name)
        {
            return this.content.SingleOrDefault(x => x.Name == name);
        }
    }
}
