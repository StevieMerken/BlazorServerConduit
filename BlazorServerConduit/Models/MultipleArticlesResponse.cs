using BlazorServerConduit.Services;

namespace BlazorServerConduit.Models
{
    public record MultipleArticlesResponse(List<Article> Articles, int ArticlesCount);
}
