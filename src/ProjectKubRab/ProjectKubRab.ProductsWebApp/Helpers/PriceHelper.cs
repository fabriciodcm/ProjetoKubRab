namespace ProjectKubRab.ProductsWebApp.Helpers
{
    public static class PriceHelper
    {
        public static decimal ApplyRandomPrice(decimal basePrice, decimal minPercent, decimal maxPercent)
        {
            decimal variation = (decimal)Random.Shared.NextDouble() * (maxPercent - minPercent) + minPercent;  
            return basePrice * (1 + variation);
        }
    }
}
