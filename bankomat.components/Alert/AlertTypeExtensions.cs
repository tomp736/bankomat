namespace bankomat.components
{
    public static class AlertTypeExtensions
    {
        public static string AlertClass(this AlertType alertType)
        {
            switch (alertType)
            {
                case AlertType.Primary:
                    return "alert alert-primary";
                case AlertType.Secondary:
                    return "alert alert-secondary";
                case AlertType.Success:
                    return "alert alert-success";
                case AlertType.Danger:
                    return "alert alert-danger";
                case AlertType.Warning:
                    return "alert alert-warning";
                case AlertType.Info:
                    return "alert alert-info";
                case AlertType.Light:
                    return "alert alert-light";
                case AlertType.Dark:
                    return "alert alert-dark";
            }
            return "";
        }
    }
}