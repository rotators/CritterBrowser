using System;
using System.Collections.Generic;
using System.Text;

// enable extensions in .net 2.0
namespace System.Runtime.CompilerServices
{
    [AttributeUsage( AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method )]
    public sealed class ExtensionAttribute : Attribute { }
}

namespace CritterBrowser
{
    // probably should be removed when/if moving to .net 3.5 or higher
    public delegate void Action();
    public delegate void Action<T>( T t );
    public delegate void Action<T1, T2>( T1 t1, T2 t2 );
    public delegate void Action<T1, T2, T3>( T1 t1, T2 t2, T3 t3 );
    public delegate TResult Func<TResult>();
    public delegate TResult Func<T, TResult>( T t );
    public delegate TResult Func<T1, T2, TResult>( T1 t1, T2 t2 );
    public delegate TResult Func<T1, T2, T3, TResult>( T1 t1, T2 t2, T3 t3 );
    public delegate TResult Func<T1, T2, T3, T4, TResult>( T1 t1, T2 t2, T3 t3, T4 t4 );

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
}