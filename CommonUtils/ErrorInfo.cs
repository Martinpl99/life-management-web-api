namespace WebAPITrainingProject.CommonUtils;

public enum ErrorCause
{
    eNone,
    eMappingErr,
}

public class ErrorInfo
{
    public ErrorCause ErrorCause { get; set; } = ErrorCause.eNone;
    public string ErrorMessage { get; set; } = string.Empty;
}