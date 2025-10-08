namespace MultyThreadExample
{
    public class SendToApi
    {

        public async Task Test()
        {
            var httpClient = new HttpClient();

            // DO NOT wait 42 sec
            var answer = await httpClient.SendAsync("URL");

            // wait 42 sec
            var answer2 = httpClient.SendAsync("URL").Result;
            answer.Should().NotBeNull();
        }
    }
}
