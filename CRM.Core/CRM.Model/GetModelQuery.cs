namespace CRM.Model
{
    public class GetModelQuery : ParameterQuery
    {
        public string Name { get; set; }

        public string Mail { get; set; }

        public string Phone { get; set; }

        public string ActionName { get; set; }

        public string RequestHttpType { get; set; }

        public string RoleName { get; set; }

        public string RoleType { get; set; }

        public string GroupName { get; set; }

        public string GroupType { get; set; }

    }
}
