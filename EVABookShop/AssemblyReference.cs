using System.Reflection;

namespace EVABookShop.Repositories;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}