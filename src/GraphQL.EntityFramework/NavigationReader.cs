using System;
using System.Collections.Generic;
using System.Linq;
using GraphQL.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Navigation = GraphQL.EntityFramework.Navigation;

static class NavigationReader
{
    public static IReadOnlyDictionary<Type, IReadOnlyList<Navigation>> GetNavigationProperties(IModel model)
    {
        var types = model.GetEntityTypes();

#pragma warning disable EF1001
        return model
            .GetEntityTypes()
            .Where(x => !x.IsOwned() && !(x as EntityType)!.IsImplicitlyCreatedJoinEntityType)
            .ToDictionary(x => x.ClrType, GetNavigations);
#pragma warning restore EF1001
    }

    static IReadOnlyList<Navigation> GetNavigations(IEntityType entity)
    {
        var navigations = entity.GetNavigations()
            .Cast<INavigationBase>().Concat(entity.GetSkipNavigations());
        return navigations
            .Select(x =>
            {
                var (itemType, isCollection) = GetNavigationType(x);
                return new Navigation(x.Name, itemType, x.PropertyInfo.IsNullable(), isCollection);
            })
            .ToList();
    }

    static (Type itemType, bool isCollection) GetNavigationType(INavigationBase navigation)
    {
        var navigationType = navigation.ClrType;
        Type? collectionType;

        if (navigationType.IsGenericType && navigationType.GetGenericTypeDefinition() == typeof(ICollection<>))
        {
            collectionType = navigationType;
        }
        else
        {
            collectionType = navigationType.GetInterfaces()
                .SingleOrDefault(x => x.IsGenericType &&
                                      x.GetGenericTypeDefinition() == typeof(ICollection<>));
        }

        if (collectionType == null)
        {
            return (navigationType, false);
        }

        return (collectionType.GetGenericArguments().Single(), true);
    }
}