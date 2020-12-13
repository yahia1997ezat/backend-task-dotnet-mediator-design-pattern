using System;

namespace TaskApi.Domain
{
    public  enum Status
    {
        Completed,
        Rejected,
        Draft,
        Assigned,
    }
    public class TaskItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? CompletedOn { get; set; }
        public DateTime? RejectedOn { get; set; }
        public string RejectedReason { get; set; }
        public string AssignedToUser { get; set; }
        public Status Status { get; set; }
    }
}