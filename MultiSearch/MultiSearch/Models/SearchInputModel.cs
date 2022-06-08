using System.ComponentModel.DataAnnotations;

namespace MultiSearch.Models;

public class SearchInputModel
{
    [Required]
    [StringLength(200, ErrorMessage = "Search terms are too long, max is 200 chars for all")]
    [MinLength(2, ErrorMessage = "Search terms are too small, minimal length is 2 chars")]
    [RegularExpression("^[a-zA-Z0-9 ]*$", ErrorMessage = "Only Alphabets and Numbers and Spaces allowed.")]
    public string SearchTerms { get; set; }
}