namespace Reactive
{
    public class Data
    {
        public const string _rxWikiPage = @"reactor In computing, reactive programming is a programming paradigm oriented around data flows and the propagation of change. This means that it should be possible to express static or dynamic data flows with ease in the programming languages used, and that the underlying execution model will automatically propagate changes through the data flow.
            For example, in an imperative programming setting, a := b + c would mean that a is being assigned the result of b + c in the instant the expression is evaluated.Later, the values of b and c can be changed with no effect on the value of a.
            In reactive programming, the value of a would be automatically updated based on the new values, the opposite of functional programming.
            A modern spreadsheet program is an example of reactive programming.Spreadsheet cells can contain literal values, or formulas such as =B1+C1 that are evaluated based on other cells.Whenever the value of the other cells change, the value of the formula is automatically updated.
            Another example is a hardware description language such as Verilog.In this case reactive programming allows changes to be modeled as they propagate through a circuit.
            Reactive programming has foremost been proposed as a way to simplify the creation of interactive user interfaces, animations in real time systems, but is essentially a general programming paradigm.
            For example, in a Model-view-controller architecture, reactive programming can allow changes in the underlying model to automatically be reflected in the view, and vice versa.[1]
            reactive extensions rx Rx Reactive Extensions The Reactive Manifesto Reactive Collections reactable";
    }
}
