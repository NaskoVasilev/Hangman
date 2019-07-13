using Hangman.Client.Infrastructure;
using Microsoft.AspNetCore.Components;

namespace Hangman.Client.Components
{
    public class BaseHangmanComponent : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public JsInterop JsInterop { get; set; }

        [Inject]
        public ApplicationState ApplicationState { get; set; }

        [Inject]
        public IUriHelper UriHelper { get; set; }
    }
}
