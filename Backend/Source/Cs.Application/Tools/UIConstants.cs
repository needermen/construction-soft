namespace Cs.Application.Tools
{
    public static class UiConstants
    {
        public static string EntityWithSameNameAlreadyExists(string name) =>  $" \"{name}\" ასეთი სახელით უკვე არსებობს. დამატება არ შეიძლება";
        public static string ObjectNotFound => "object not found";
        public static string UnauthorizedAccess => "არაავტორიზირებული ოპერაცია";
    }
}