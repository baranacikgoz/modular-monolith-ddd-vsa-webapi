namespace Common.Application.Auth;

public static class CustomActions
{
    public const string Create = nameof(Create);
    public const string CreateMy = nameof(CreateMy);
    public const string Read = nameof(Read);
    public const string ReadMy = nameof(ReadMy);
    public const string Update = nameof(Update);
    public const string UpdateMy = nameof(UpdateMy);
    public const string Delete = nameof(Delete);
    public const string DeleteMy = nameof(DeleteMy);
    public const string Search = nameof(Search);
    public const string SearchMy = nameof(SearchMy);

    public const string Manage = nameof(Manage);

}

public static class CustomResources
{
    public const string Hangfire = nameof(Hangfire);

    public const string ApplicationUsers = nameof(ApplicationUsers);

    public const string Stores = nameof(Stores);
    public const string Products = nameof(Products);
    public const string StoreProducts = nameof(StoreProducts);
}
