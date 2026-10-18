namespace SporTakip.Application.Validation;

/// <summary>Tek bir alan için doğrulama hatası. PropertyName domain özelliğinin adıdır (örn. nameof(Sporcu.Ad)).</summary>
public sealed record ValidationError(string PropertyName, string Message);

/// <summary>Doğrulama sonucu; hata yoksa geçerlidir.</summary>
public class ValidationResult
{
    protected ValidationResult(IReadOnlyList<ValidationError> errors) => Errors = errors;

    public IReadOnlyList<ValidationError> Errors { get; }

    public bool IsValid => Errors.Count == 0;

    public static ValidationResult Success { get; } = new([]);

    public static ValidationResult Failure(params ValidationError[] errors) => new(errors);

    public static ValidationResult Failure(IEnumerable<ValidationError> errors) => new(errors.ToArray());
}

/// <summary>Geçerliyse bir değer (örn. eklenen kaydın Id'si) taşıyan doğrulama sonucu.</summary>
public sealed class ValidationResult<T> : ValidationResult
{
    private ValidationResult(T? value, IReadOnlyList<ValidationError> errors) : base(errors) => Value = value;

    /// <summary>Yalnızca <see cref="ValidationResult.IsValid"/> true iken anlamlıdır.</summary>
    public T? Value { get; }

    public static ValidationResult<T> Ok(T value) => new(value, []);

    public static new ValidationResult<T> Failure(params ValidationError[] errors) => new(default, errors);

    public static new ValidationResult<T> Failure(IEnumerable<ValidationError> errors) => new(default, errors.ToArray());
}
