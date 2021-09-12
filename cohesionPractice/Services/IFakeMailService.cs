namespace cohesionPractice.Services
{
    public interface IFakeMailService
    {
        void Send(string subject, string message);
    }
}