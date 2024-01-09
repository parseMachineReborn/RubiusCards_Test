using Rubius.Enums;
using Rubius.Storage;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Rubius.Models
{
    public sealed class DropDownContentParser
    {
        private readonly Config _config;
        private readonly Regex _regex;

        public DropDownContentParser(Config config)
        {
            _config = config;
            _regex = new Regex(@"(?<=[A-Z])(?=[A-Z][a-z]) |(?<=[^A-Z])(?=[A-Z]) |(?<=[A-Za-z])(?=[^A-Za-z])", RegexOptions.IgnorePatternWhitespace);
        }

        public Dictionary<string, LoadType> GetContentFromConfig()
        {
            Dictionary<string, LoadType> content = new Dictionary<string, LoadType>();

            for (int i = 0; i < _config.LoadTypeConfigurations.Count; i++)
            {
                if (_config.LoadTypeConfigurations[i].IsSelected)
                {
                    content[ParseContent($"{_config.LoadTypeConfigurations[i].LoadType}")] = _config.LoadTypeConfigurations[i].LoadType;
                }
            }

            return content;
        }

        private string ParseContent(string content)
        {
            return _regex.Replace(content, " ");
        }
    }
}
