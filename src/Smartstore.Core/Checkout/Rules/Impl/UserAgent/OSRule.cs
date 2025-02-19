﻿using Smartstore.Core.Rules;
using Smartstore.Core.Web;

namespace Smartstore.Core.Checkout.Rules.Impl
{
    internal class OSRule : IRule
    {
        private readonly IUserAgent _userAgent;

        public OSRule(IUserAgent userAgent)
        {
            _userAgent = userAgent;
        }

        public static RuleValueSelectListOption[] GetDefaultOptions()
        {
            return UserAgentPatterns.Platforms
                .Select(x => x.Name)
                .OrderBy(x => x)
                .Concat(new[] { "Unknown" })
                .Select(x => new RuleValueSelectListOption { Value = x, Text = x })
                .ToArray();
        }

        public Task<bool> MatchAsync(CartRuleContext context, RuleExpression expression)
        {
            var match = expression.HasListMatch(_userAgent.Platform.Name.NullEmpty());
            return Task.FromResult(match);
        }
    }
}
