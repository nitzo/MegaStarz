namespace Facebook.Tests.ExceptionFactory.GetRestException
{
    using System.Collections.Generic;
    using Facebook;
    using Xunit;

    public class GivenANonEmptyDictionaryOfStringObjectThatDoesNotContainErrorCodeKeyThen
    {
        [Fact]
        public void ResultIsNull()
        {
            var dictWithoutErrorCodeKey = new Dictionary<string, object> { { "dummy_key", "dummy_value" } };

            var result = ExceptionFactory.GetRestException(dictWithoutErrorCodeKey);

            Assert.Null(result);
        }
    }
}