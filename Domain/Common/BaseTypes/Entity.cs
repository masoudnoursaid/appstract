﻿namespace Domain.Common.BaseTypes;

public abstract class Entity<TId>
{
    public TId Id { get; set; }
}