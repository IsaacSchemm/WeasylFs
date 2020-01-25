namespace WeasylFs

/// An interface for any object that contains a Weasyl API key.
type IWeasylCredentials =
    abstract member ApiKey: string with get

/// An object that contains only a Weasyl API key.
type WeasylCredentials = {
    api_key: string
} with
    interface IWeasylCredentials with
        member this.ApiKey = this.api_key
    static member None =
        { api_key = null }