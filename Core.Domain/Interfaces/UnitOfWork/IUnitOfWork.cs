using System;
using System.Data;

namespace Core.Domain.Interfaces.UnitOfWork
{
	public interface IUnitOfWork : IDisposable
	{
		IDbConnection Connection { get; }
		IDbTransaction Transaction { get; }
		void BeginTransaction();
		void RollBack();
		void Commit();
	}
}
