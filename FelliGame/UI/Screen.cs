﻿using FelliGame.UI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FelliGame.UI
{
    /// <summary>
    /// A screen. A screen can have multiple <see cref="UIElement"/>
    /// and is responsible for their display.
    /// </summary>
    public class Screen
    {
        /// <summary>
        /// The background color for the screen.
        /// </summary>
        private readonly ConsoleColor backgroundColor;

        /// <summary>
        /// The margin that should be enforced on the left side of the screen.
        /// </summary>
        private readonly int marginLeft;

        /// <summary>
        /// The margin that should be enforced on the top of the screen.
        /// </summary>
        private readonly int marginTop;

        /// <summary>
        /// The screen's UI Elements.
        /// </summary>
        public IList<UIElement> Elements { get; }

        /// <summary>
        /// Creates a new instance of screen.
        /// </summary>
        /// <param name="colorBg">The background color for the screen.</param>
        /// <param name="marginLeft">The margin that should be enforced on the 
        /// left side of the screen.</param>
        /// <param name="marginTop">The margin that should be enforced on the 
        /// top of the screen.</param>
        public Screen(ConsoleColor colorBg = UISettings.ColorConsoleBg, 
            int marginLeft = UISettings.ScreenMarginLeft,
            int marginTop = UISettings.ScreenMarginTop)
        {
            this.backgroundColor = colorBg;
            this.marginLeft = marginLeft;
            this.marginTop = marginTop;
            this.Elements = new List<UIElement>();
        }

        /// <summary>
        /// Add a new <see cref="UIElement"/> to the screen.
        /// </summary>
        /// <param name="newElement">The new <see cref="UIElement"/>.</param>
        public void Add(UIElement newElement)
        {
            UIPosition delta;

            if (newElement.TopLeft.X < marginLeft || newElement.TopLeft.Y < marginTop)
            {
                delta = new UIPosition(marginLeft - newElement.TopLeft.X, 
                    marginTop - newElement.TopLeft.Y);

                newElement.Move(delta);
            }

            if (newElement.IsCentered)
            {
                newElement.Move(new UIPosition(-(int)(newElement.Width / 2), 0));
            }

            Elements.Add(newElement);
        }

        /// <summary>
        /// Add a new <see cref="UIElement"/> to the screen using another 
        /// element's anchor point.
        /// </summary>
        /// <param name="newElement">The new <see cref="UIElement"/>.</param>
        /// <param name="elementForAnchor">The name (Id) of the element to be 
        /// used as anchor.</param>
        /// <param name="anchor">The anchor point to be used.</param>
        public void Add(UIElement newElement, string elementForAnchor, 
            AnchorPoint anchor = AnchorPoint.BottomLeft)
        {
            UIPosition position, anchorPosition;
            UIElement anchorElement = GetElementByName(elementForAnchor);

            if (anchorElement == null)
            {
                return;
            }

            switch (anchor)
            {
                case AnchorPoint.BottomRight:
                    anchorPosition = anchorElement.BottomRight;
                    break;
                case AnchorPoint.TopRight:
                    anchorPosition = anchorElement.TopRight;
                    break;
                case AnchorPoint.BottomMiddle:
                    anchorPosition = anchorElement.BottomMiddle;
                    break;
                default:
                    anchorPosition = anchorElement.BottomLeft;
                    break;
            }

            position = newElement.TopLeft + anchorPosition;

            newElement.SetPosition(position);

            if (newElement.IsCentered)
            {
                newElement.Move(new UIPosition(-(int)(newElement.Width / 2), 0));
            }

            Elements.Add(newElement);
        }

        /// <summary>
        /// Clears the screen of all UI Elements.
        /// </summary>
        public void Clear()
        {
            Elements.Clear();
        }

        /// <summary>
        /// Removes the UI Elements at the given index.
        /// </summary>
        /// <param name="index">The index of the UI Element to be removed. 
        /// Default is -1 that results in the last element being removed.</param>
        public void Remove(int index = -1)
        {
            if (index == -1)
            {
                index = Elements.Count - 1;
            }

            if (index >= Elements.Count || index < 0)
            {
                return;
            }

            Elements.RemoveAt(index);
        }

        /// <summary>
        /// Performs a refresh of all the UI Elements on the screen.
        /// </summary>
        public void Refresh()
        {
            Console.BackgroundColor = backgroundColor;

            Console.Clear();

            for (int i = 0; i < Elements.Count; i++)
            {
                Elements[i].Display();
            }
        }

        /// <summary>
        /// Get UI Element by name (Id).
        /// </summary>
        /// <param name="name">The name (Id) of the UI Element.</param>
        /// <returns>The element with the given name or null if none found.</returns>
        public UIElement GetElementByName(string name)
        {
            UIElement element = null;

            for (int i = 0; i < Elements.Count; i++)
            {
                if (Elements[i].Name == name)
                {
                    element = Elements[i];
                }
            }

            return element;
        }
    }
}
