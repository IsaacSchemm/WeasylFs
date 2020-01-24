namespace WeasylFs

open System

/// An interface for any object that contains an alternate URL to connect to (besides https://www.weasyl.com).
/// If you want to use this feature, IAlternateWeasylUri should be implemented by the same object that implements IWeasylCredentials.
type IAlternateWeasylUri =
    abstract member Uri: Uri with get