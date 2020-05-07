namespace Reactive
{
    using System;
    using System.Diagnostics;
    using System.Reactive.Linq;

    using Windows.Foundation;
    using Windows.UI.ViewManagement;

    using Reactive.Services;

    using Windows.UI.Xaml.Controls;
    using System.Linq;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        readonly SearchServiceClient _client = new SearchServiceClient();

        private IDisposable _subscription;

        public MainPage()
        {
            this.InitializeComponent();

            int minTextLength = 3;

            _subscription = Observable.FromEventPattern(Search, nameof(Search.TextChanged))
                .Select(_ => Search.Text)
                .Where(t => t.Length >= 3)
                .Throttle(TimeSpan.FromSeconds(0.5))
                .DistinctUntilChanged()
                .Select(t => this._client.SearchAsync(t))
                .Switch()
                .ObserveOnDispatcher()
                .Subscribe(
                    x => Search.ItemsSource = x,
                    ex => Debug.Write(ex));

            Observable.FromEventPattern(Search, "TextChanged")
                    .Select(_ => Search.Text)
                    .Where(txt => txt.Length < minTextLength)
                    .ObserveOnDispatcher()
                    .Subscribe(
                        results => Search.ItemsSource = Enumerable.Empty<string>(),
                        err => { Debug.WriteLine(err); },
                        () => { /* OnCompleted */ });
        }
    }
}
