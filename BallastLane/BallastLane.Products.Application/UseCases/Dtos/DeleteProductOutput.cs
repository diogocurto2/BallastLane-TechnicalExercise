namespace BallastLane.Products.Application.UseCases.Dtos
{
    public class DeleteProductOutput
    {
        public DeleteProductOutput(bool isDeleted)
        {
            IsDeleted = isDeleted;
        }

        public bool IsDeleted { get; private set; }
    }
}
