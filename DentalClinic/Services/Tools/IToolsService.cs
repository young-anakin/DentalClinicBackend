namespace DentalClinic.Services.Tools
{
    public interface IToolsService
    {
        int CalculateAge(DateTime birthDate);
        string[] ReturnArrayofCommaSeparatedStrings(string inputString);
    }
}