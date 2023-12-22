﻿using Newtonsoft.Json;
namespace PostsService;

public class UserService : Iuser
{
    private readonly IHttpClientFactory _IhttpClientFactory;
    public UserService(IHttpClientFactory httpClientFactory)
    {
        _IhttpClientFactory=httpClientFactory;
    }
    public async Task<ChatUserDto> GetUserById(Guid Id)
    {
        var client= _IhttpClientFactory.CreateClient("Users");
        var response= await client.GetAsync($"{Id}");
        var content= await response.Content.ReadAsStringAsync();//We get content inform of a string
        var responseDto=JsonConvert.DeserializeObject<ResponseDto>(content);
        if(response.IsSuccessStatusCode){
            return JsonConvert.DeserializeObject<ChatUserDto>(Convert.ToString(responseDto.Result));
        }
        return new ChatUserDto();
    }
}
