using Microsoft.EntityFrameworkCore;

namespace CTAM.Core.Enums
{
    public enum ChangeAction
    {
        // The entity is/will not be submitted.
        None = 0,

        // The entity is/will be deleted.
        Delete = 1,

        // The entity is/will be inserted.
        Insert = 2,

        // The entity is/will be updated.
        Update = 3,

        // The entity is/will be replaced.
        Replace = 4
    }

    public static class ExtensionsForChangeAction
    {
        public static ChangeAction GetChangeAction(this EntityState state)
        {
            return state switch
            {
                EntityState.Added => ChangeAction.Insert,
                EntityState.Modified => ChangeAction.Update,
                EntityState.Deleted => ChangeAction.Delete,
                _ => ChangeAction.None
            };
        }
    }
}
