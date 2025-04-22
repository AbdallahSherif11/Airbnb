using Airbnb.Core.DTOs.SmartSearchDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airbnb.Core.Services.Contract.SmartSearchService.Contract
{
    public interface IOllamaService
    {
        Task<FilterResult> ExtractFiltersFromPromptAsync(string prompt);
    }

}
