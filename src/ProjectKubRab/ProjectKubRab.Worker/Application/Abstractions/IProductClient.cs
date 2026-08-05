using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectKubRab.Worker.Application.Abstractions
{
    public interface IProductClient
    {
        Task<string> GetHtmlContentFromProductsPageAsync(CancellationToken stoppingToken);
    }
}
