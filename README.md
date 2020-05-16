# Felli

**Project 2 - Linguagens de Programação I 2019/2020**

**Videojogos - Universidade Lusófona**

## Author

**Pedro Marques - 21900800**

**Github:** [Link](https://github.com/p-marques/FelliLP1)

## Solution Architecture

### Flow

The program starts by handling any given arguments.
They are all optional, but once passed in they must be valid and serve to
override default application settings.

**Available Commands**

- -h: displays arguments help messages and closes the program;
- -p1: the name of player 1. Must have length between 2 and 15;
- -p2: the name of player 2. Must have length between 2 and 15;

If any errors are found, the appropriate error messages are displayed and the
program closes.

Assuming that all passed in arguments are valid and -h wasn't invoked,
an instance of Game is created and the game loop begins.

First the players decide which one of them plays with the black pieces and
who plays first. Then the players take turn playing until a winner is found or
one of the players decides to quit the game.

### UML Diagram

![uml](images/uml.png "UML Diagram")

**Figura 1** - UML Diagram. Made with [Draw.io](https://www.draw.io/).

I believe the concept of a `Screen` that has multiple `UIElements` requires the
most detailed explanation here. Well a `Screen` is exactly what it sounds like,
it's a representation of everything on screen. It allows for a very easy
interface to add and remove `UIElements` from the `Screen` and have it always be
displayed correctly. If one adds the `UIElement` to the `Screen`, one is certain
that on `Screen.Refresh()`, one will see all of those `UIElements` on screen.
Now `Screen.Refresh()` should be used sparingly since it performs a
`Console.Clear()` and writes all of the `UIElements`. Based on the design of a
`UIElement`, which I will discuss in a moment, it's much more efficient to use
`Display()` on `UIElements` individually, since that will only output itself to
the console. However this is not always possible, specifically when we want do
remove something from the console. This is a potential area of improvement,
since implementing a 'erase' type method is doable.

A `UIElement` represents anything that is designed to have a visual
representation on the console, and potentially be interactive, meaning capable
of responding to player input. The way I looked at it, I see them as something
akin to WPF controls. In this project I created the children as they were
required. Here's a brief explanation:
- `UITitle`, is what it sounds like, a simple title. It can have a border on the
left, and horizontal and vertical padding;
- `UIDialog`, is my attempt at a console version of a popup dialog, the idea of
prompting the user to make a decision. The way that it's designed it could be
used for the usual "Cancel / Confirm", or for uses like the required by this
project, of having the user select one of the players. The UIDialog uses 2
`UIButtons`, which I don't think require much explanation;
- `UIFelliBoard`, `UIFelliRow` and `UIFelliSquare` are the visual
representation of `Board`, `BoardRow` and `BoardSquare` respectively. It's
noteworthy to mention that the position of each `UIFelliRow` is determined by
`UIFelliBoard` and the position of each `UIFelliSquare` is determine by
`UIFelliRow`;
- `UIInfoPanel` is basically a collection of `UIText` objects.

It's important to reinforce that each one of these `UIElements` are responsible
for their own output. The concept is: if the property `TopLeft`, which is
the `UIElement`'s anchor point is correctly set/calculated, the display is
always correct.

One really helpful advantage of using this architecture is the fact that it's
possible to use one `UIElement`'s anchor points, to add another `UIElement` to
the screen. This way it's never required to be concerned with cursor position or
anything of the sort. If I add a new `UIElement` using another `UIElements`'s
`BottomLeft` for instance, I know that the new `UIElement` will output in the
correct position.

It's worth it, perhaps, to mention that one could design an infinite amount of
children of `UIElements`, as a response to any sort of UX requirements.

### References

The only thing that deserves a mention is the fact that the design of `Options`
was, initially, heavily inspired by [this][1], which is the professor's own
proposed solution to Project 2 of last year.

[1]: https://github.com/VideojogosLusofona/lp1_2018_p2_solucao
