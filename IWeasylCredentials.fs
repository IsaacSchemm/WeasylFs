namespace WeasylFs

open System

type IWeasylCredentials =
    abstract member ApiKey: string with get

type WeasylCredentials = {
    apiKey: string
} with
    interface IWeasylCredentials with
        member this.ApiKey = this.apiKey
    static member None =
        { apiKey = null }

type IAlternateWeasylUri =
    abstract member Uri: Uri with get