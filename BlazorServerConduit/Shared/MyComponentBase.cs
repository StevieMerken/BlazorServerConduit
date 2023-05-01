using Microsoft.AspNetCore.Components;

namespace BlazorServerConduit.Shared
{
    /// <summary>
    /// See https://github.com/dotnet/aspnetcore/issues/45230
    /// </summary>
    public class MyComponentBase : ComponentBase
    {
        protected IReadOnlyDictionary<string, object>? _attributes;
        protected string _classes = "";

        [Parameter(CaptureUnmatchedValues = true)] 
        public IReadOnlyDictionary<string, object> UnmatchedParameters { get; set; } = new Dictionary<string, object>();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            // extract class attribute
            _classes = $"{UnmatchedParameters.GetValueOrDefault("class")}";

            // extract non-class attributes
            _attributes =
              UnmatchedParameters
              .Where(x => x.Key != "class")
              .ToDictionary(k => k.Key, v => v.Value);
        }
    }
}
