using System;
using System.Collections.Generic;
using System.Text;

// enable extensions in .net 2.0
namespace System.Runtime.CompilerServices
{
    [AttributeUsage( AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method )]
    public sealed class ExtensionAttribute : Attribute { }
}

static class Extensions
{
    public static bool IsAlpha( this string self )
    {
        if( self == null || self.Length == 0 )
            return (false);

        foreach( char ch in self.ToCharArray() )
        {
            if( !char.IsLetter( ch ) )
                return (false);
        }

        return (true);
    }
}
