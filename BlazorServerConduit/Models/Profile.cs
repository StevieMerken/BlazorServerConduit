namespace BlazorServerConduit.Models
{
    public record Profile(string UserName,
                          string Bio,
                          string Image,
                          bool Following);
}
