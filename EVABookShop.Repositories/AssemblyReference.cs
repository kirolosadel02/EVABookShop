using System.Reflection;

namespace OnCareEmergencyCard.Repository;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}