using System;

namespace BookStore.Application.Interface.Common;

public interface IUnitOfWork
{
    void SaveChanges();
}