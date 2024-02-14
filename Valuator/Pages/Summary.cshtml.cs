using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Valuator.Storage;

namespace Valuator.Pages;
public class SummaryModel : PageModel
{
    private const string RankPrefix = "RANK-";
    private const string SimilarityPrefix = "SIMILARITY-";
    
    private readonly ILogger<SummaryModel> _logger;
    private readonly IStorage _storage;
    
    public SummaryModel(ILogger<SummaryModel> logger, IStorage storage)
    {
        _logger = logger;
        _storage = storage;
    }

    public double Rank { get; set; }
    public double Similarity { get; set; }

    public void OnGet(string id)
    {
        var rank = _storage.Get(RankPrefix + id);
        var similarity = _storage.Get(SimilarityPrefix + id);
        Rank = this.ParseDouble(rank);
        Similarity = this.ParseDouble(similarity);
    }

    private double ParseDouble(string? num)
    {
        if (num == null)
        {
            return 0;
        }
        
        return double.Parse(num, System.Globalization.CultureInfo.InvariantCulture);
    }
}
