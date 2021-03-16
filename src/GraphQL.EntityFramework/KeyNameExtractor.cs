using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

static class KeyNameExtractor
{
    public static IReadOnlyDictionary<Type, List<string>> GetKeyNames(this IModel model)
    {
        Dictionary<Type, List<string>> keyNames = new();
        foreach (var entityType in model.GetEntityTypes())
        {
            var primaryKey = entityType.FindPrimaryKey();
            //This can happen for views
            if (primaryKey == null)
            {
                continue;
            }

            if (entityType.IsOwned())
            {
                continue;
            }

#pragma warning disable EF1001
            var etype = entityType as EntityType;
            if (etype != null && etype.IsImplicitlyCreatedJoinEntityType)
            {
                continue;
            }
#pragma warning restore EF1001
            
            var names = primaryKey.Properties.Select(x => x.Name).ToList();
            keyNames.Add(entityType.ClrType, names);
        }

        return keyNames;
    }
}