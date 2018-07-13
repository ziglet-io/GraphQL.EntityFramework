﻿namespace EfCoreGraphQL
{
    public class UlongGraph : ScalarGraph<ulong>
    {
        protected override ulong InnerParse(string value)
        {
            return ulong.Parse(value);
        }
    }
}