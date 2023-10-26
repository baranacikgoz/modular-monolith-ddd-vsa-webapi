namespace Common.Core.Auth;

public static class RfActions
{
    public const string Create = nameof(Create);
    public const string CreateMy = nameof(CreateMy);
    public const string Read = nameof(Read);
    public const string ReadMy = nameof(ReadMy);
    public const string Update = nameof(Update);
    public const string UpdateMy = nameof(UpdateMy);
    public const string Delete = nameof(Delete);
}

public static class RfResources
{
    public const string Users = nameof(Users);

    public const string Venues = nameof(Venues);
    public const string Appointments = nameof(Appointments);
}
