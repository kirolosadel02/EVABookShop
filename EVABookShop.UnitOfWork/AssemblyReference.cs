using System.Reflection;

namespace EVABookShop.UnitOfWork;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}