namespace WeasylFs

/// An interface for any object that contains a Weasyl API key.
type IWeasylCredentials =
    abstract member ApiKey: string with get

/// An object that contains only a Weasyl API key.
type WeasylCredentials = {
    apiKey: string
} with
    interface IWeasylCredentials with
        member this.ApiKey = this.apiKey
    static member None =
        { apiKey = null }