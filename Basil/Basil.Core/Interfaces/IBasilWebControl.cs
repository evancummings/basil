namespace Basil.Interfaces
{
    public interface IBasilWebControl
    {
        string Label { get; set; }

        bool Required { get; set; }

        bool IsValid { get; set; }

        bool IsWarning { get; set; }

        bool IsInfo { get; set; }

        bool IsSuccess { get; set; }

        BasilValidator Validator { get; set; }
    }
}