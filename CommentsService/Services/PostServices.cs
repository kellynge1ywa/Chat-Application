using Newtonsoft.Json;
namespace CommentsService;

public class PostServices : IPosts
{
    private readonly IHttpClientFactory _IhttpClientFactory;

    public PostServices(IHttpClientFactory httpClientFactory)
    {
        _IhttpClientFactory=httpClientFactory;
    }
    public async Task<PostDto> GetPost(Guid Id)
    {
        var client= _IhttpClientFactory.CreateClient("Posts");
        var response= await client.GetAsync($"{Id}");
        var content= await response.Content.ReadAsStringAsync();//We get content inform of a string
        var responseDto=JsonConvert.DeserializeObject<ResponseDto>(content);
        if(response.IsSuccessStatusCode){
            return JsonConvert.DeserializeObject<PostDto>(Convert.ToString(responseDto.Result));
        }
        return new PostDto();

    }
}
