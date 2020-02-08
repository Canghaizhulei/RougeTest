using GameFramework.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework.Localization;
using GameFramework;

public class LanguageChangeEventArgs : GameEventArgs
{
    public static readonly int EventId = typeof(LanguageChangeEventArgs).GetHashCode();

    public override int Id => EventId;

    public Language UseLanguage
    {
        private set;
        get;
    }

    public LanguageChangeEventArgs()
    {
        UseLanguage = Language.Unspecified;
    }

    public static LanguageChangeEventArgs Create(Language language)
    {
        LanguageChangeEventArgs newLanugage = ReferencePool.Acquire<LanguageChangeEventArgs>();
        newLanugage.UseLanguage = language;
        return newLanugage;
    }

    public override void Clear()
    {
        UseLanguage = Language.Unspecified;
    }
}
