using ProjectKubRab.Worker.Application.Models;

namespace ProjectKubRab.Test.Application.Models
{
    public static class ProductMatchMock
    {
        public static ProductMatch Create(string name, string price)
        {
            return new ProductMatch(name, price);
        }
    }
}
