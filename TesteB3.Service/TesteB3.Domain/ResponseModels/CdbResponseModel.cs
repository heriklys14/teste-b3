namespace TesteB3.Domain.ResponseModels
{
    public class CdbResponseModel(double grossValue, double netvalue)
    {
        private readonly double _grossValue = grossValue;
        public double GrossValue { get => Math.Round(_grossValue, 2, MidpointRounding.ToZero); }

        private readonly double _netValue = netvalue;
        public double NetValue { get => Math.Round(_netValue, 2, MidpointRounding.ToZero); }
    }
}
