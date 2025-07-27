using System.Reflection;

namespace EVABookShop.DataAccess;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}