using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Valuator.Storage;

namespace Valuator.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IStorage _storage;

    public IndexModel(ILogger<IndexModel> logger, IStorage storage)
    {
        _logger = logger;
        _storage = storage;
    }

    public void OnGet()
    {

    }

    public IActionResult OnPost(string? text)
    {
        _logger.LogDebug(text);

        if (String.IsNullOrEmpty(text))
        {
            return Redirect($"summary");
        }
        
        string id = Guid.NewGuid().ToString();

        string similarityKey = "SIMILARITY-" + id;
        string similarity = HasDuplicates(text) ? "1" : "0";
        _storage.Save(similarityKey, similarity);
        
        string textKey = "TEXT-" + id;
        _storage.Save(textKey, text);

        string rankKey = "RANK-" + id;
        double rank = CalculateRank(text);
        _storage.Save(rankKey, rank.ToString());
        
        return Redirect($"summary?id={id}");
    }

    private static double CalculateRank(string text)
    {
        var regexp = new Regex(@"[A-Z,a-z,А-Я,а-я]");
        double notAlphabetCharsCount = text.Aggregate(
            0,
            (i, c) => !regexp.IsMatch(c.ToString()) ? i + 1 : i
        );

        return notAlphabetCharsCount == 0 
            ? 0 
            : Math.Round(notAlphabetCharsCount / text.Length, 2);
    }

    private bool HasDuplicates(string text)
    {
        return _storage
            .GetAllTexts()
            .Exists(value => value == text);
    }
}
