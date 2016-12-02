namespace KyubiCode.FluentRest.Test.RestCollectionMutators
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Xunit;

    public class DeserializationTest
    {
        [Fact]
        public void DeserializeEmptyJson()
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>(string.Empty);
            Assert.Null(result);
        }

        [Fact]
        public void DeserializeFaultyJson()
        {
            Assert.Throws<JsonSerializationException>(
                () => JsonConvert.DeserializeObject<Dictionary<string, string>>("{"));
        }

        [Fact]
        public void DeserializeEmptyObjectJson()
        {
            var result = JsonConvert.DeserializeObject<Dictionary<string, string>>("{}");
            Assert.Equal(0, result.Count);
        }
    }
}
