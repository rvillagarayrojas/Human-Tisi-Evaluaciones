using System;

namespace Siscom.Models.Base
{
    [Serializable]
    public class BaseModel
    {
        public Guid? Id { get; set; }
        public ActionType Action { get; set; }
        public int? TotalRegistros { get; set; }

        public enum ActionType
        {
            Create,
            Edit,
            Delete,
            See
        }
    }
}