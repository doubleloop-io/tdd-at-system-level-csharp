public static class TestSupport
{
    public static void PrepareEmployeeFile(string employeeFile, string[] content)
    {
        File.WriteAllLines(employeeFile, [
            "last_name, first_name, date_of_birth, email",
            ..content
        ]);
    }
}