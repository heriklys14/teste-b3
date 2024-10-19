namespace TesteB3.Domain.ResponseModels
{
    public class CdbResponseModel(double grossValue)
    {
        private readonly double _grossValue = grossValue;
        public double GrossValue { get => Math.Round(_grossValue, 2, MidpointRounding.ToZero); }

        public double NetValue { get; private set; }

        public void SetNetValue(int interval)
        {
            if (interval <= 6)
            {
                NetValue = Math.Round(GrossValue * 0.7752, 2, MidpointRounding.ToZero);
            }
            else if (interval <= 12)
            {
                NetValue = Math.Round(GrossValue * 0.8, 2, MidpointRounding.ToZero);
            }
            else if (interval <= 24)
            {
                NetValue = Math.Round(GrossValue * 0.825, 2, MidpointRounding.ToZero);
            }
            else
            {
                NetValue = Math.Round(GrossValue * 0.85, 2, MidpointRounding.ToZero);
            }
        }
    }
}
