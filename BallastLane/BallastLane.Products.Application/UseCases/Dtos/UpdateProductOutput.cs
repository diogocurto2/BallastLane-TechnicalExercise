namespace BallastLane.Products.Application.UseCases.Dtos
{
    public class UpdateProductOutput
    {
        public UpdateProductOutput(bool isUpdated)
        {
            IsUpdated = isUpdated;
        }

        public bool IsUpdated { get; private set; }
    }
}
