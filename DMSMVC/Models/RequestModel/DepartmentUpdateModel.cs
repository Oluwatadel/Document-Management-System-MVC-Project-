namespace DMSMVC.Models.RequestModel
{
    public class DepartmentUpdateModel
    {
        public string? DepartmentName { get; set; } = default!;
        public string? Acronym { get; set; } = default!;
        public string? StaffNumberOfPotentialHOD { get; set; }
    }
}
